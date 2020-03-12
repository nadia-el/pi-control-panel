import { Component, OnInit } from '@angular/core';
import { orderBy, map } from 'lodash';
import { RaspberryPiService } from '../../shared/services/raspberry-pi.service';
import {
  ICpuTemperature,
  ICpuAverageLoad,
  ICpuRealTimeLoad,
  IMemoryStatus } from '../../shared/interfaces/raspberry-pi';
import { take } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  templateUrl: './real-time-modal.component.html',
  styleUrls: ['./real-time-modal.component.css']
})
export class RealTimeModalComponent implements OnInit {
  errorMessage: string;

  chartData = [];
  temperatureDataReady = false;
  averageLoadDataReady = false;
  realTimeLoadDataReady = false;
  memoryStatusDataReady = false;

  constructor(public bsModalRef: BsModalRef,
    private raspberryPiService: RaspberryPiService) { }

  ngOnInit() {
    this.raspberryPiService.getCpuTemperatures()
      .pipe(take(1))
      .subscribe(
      result => {
        var temperatureData = map(result.items, (temperature: ICpuTemperature) => {
          return {
            value: temperature.value,
            name: new Date(temperature.dateTime)
          };
        });
        this.chartData.push({ name: "CPU Temperature", series: orderBy(temperatureData, 'name') });
        this.temperatureDataReady = true;
      },
      error => this.errorMessage = <any>error
    );

    this.raspberryPiService.getCpuAverageLoads()
      .pipe(take(1))
      .subscribe(
        result => {
          var averageLoadData = map(result.items, (averageLoad: ICpuAverageLoad) => {
            return {
              value: averageLoad.lastMinute,
              name: new Date(averageLoad.dateTime)
            };
          });
          this.chartData.push({ name: "CPU Average Load (x100)", series: orderBy(averageLoadData, 'name') });
          this.averageLoadDataReady = true;
        },
        error => this.errorMessage = <any>error
      );

    this.raspberryPiService.getCpuRealTimeLoads()
      .pipe(take(1))
      .subscribe(
        result => {
          var realTimeLoadData = map(result.items, (realTimeLoad: ICpuRealTimeLoad) => {
            return {
              value: realTimeLoad.total,
              name: new Date(realTimeLoad.dateTime)
            };
          });
          this.chartData.push({ name: "CPU Real-Time Load (%)", series: orderBy(realTimeLoadData, 'name') });
          this.realTimeLoadDataReady = true;
        },
        error => this.errorMessage = <any>error
      );

    this.raspberryPiService.getMemoryStatuses()
      .pipe(take(1))
      .subscribe(
        result => {
          var memoryStatusData = map(result.items, (memoryStatus: IMemoryStatus) => {
            return {
              value: 100 * memoryStatus.used / (memoryStatus.available + memoryStatus.used),
              name: new Date(memoryStatus.dateTime)
            };
          });
          this.chartData.push({ name: "Memory Usage (%)", series: orderBy(memoryStatusData, 'name') });
          this.memoryStatusDataReady = true;
        },
        error => this.errorMessage = <any>error
      );
  }

  isChartDataReady() {
    return this.temperatureDataReady && this.averageLoadDataReady &&
      this.realTimeLoadDataReady && this.memoryStatusDataReady;
  }

  closeModal() {
    this.bsModalRef.hide();
  }
  
}
