import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { get, isNil } from 'lodash';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';
import { HttpErrorResponse } from '@angular/common/http';
import { IRaspberryPi, ICpuTemperature, ICpuAverageLoad, ICpuRealTimeLoad, IDiskStatus, IMemoryStatus } from '../interfaces/raspberry-pi';
import { Connection } from '../interfaces/connection';

@Injectable({
  providedIn: 'root',
})
export class RaspberryPiService {

  constructor(private apollo: Apollo) {
  }

  getRaspberryPi(): Observable<IRaspberryPi> {
    return this.apollo.query<{ raspberryPi: IRaspberryPi }>({
      query: gql`
        query RaspberryPi {
          raspberryPi {
            chipset {
              model
              revision
              serial
              version
            }
            cpu {
              cores
              model
              temperature {
                value
                dateTime
              }
              averageLoad {
                lastMinute
                last5Minutes
                last15Minutes
                dateTime
              }
              realTimeLoad {
                kernel
                user
                total
                dateTime
              }
            }
            disk {
              fileSystem
              type
              total
              status {
                used
                available
                dateTime
              }
            }
            memory {
              total
              status {
                used
              	available
                dateTime
              }
            }
            gpu {
              memory
            }
            os {
              name
              kernel
              hostname
            }
          }
        }`,
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi')),
      catchError(this.handleError)
    );
  }

  getCpuTemperatures(): Observable<Connection<ICpuTemperature>> {
    return this.apollo.query<{ cpuTemperatures: any }>({
      query: gql`
        query CpuTemperatures($firstTemperatures: Int, $afterTemperatures: String) {
          raspberryPi {
            cpu {
              temperatures(first: $firstTemperatures, after: $afterTemperatures) {
	            items {
                  value
                  dateTime
                }
                pageInfo {
                  endCursor
                  hasNextPage
                  startCursor
                  hasPreviousPage
                }
                totalCount
              }
            }
          }
        }`,
      variables: {
        firstTemperatures: 1000,
        afterTemperatures: null
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi.cpu.temperatures')),
      catchError(this.handleError)
    );
  }

  getCpuAverageLoads(): Observable<Connection<ICpuAverageLoad>> {
    return this.apollo.query<{ cpuAverageLoads: any }>({
      query: gql`
        query CpuAverageLoads($firstAverageLoads: Int, $afterAverageLoads: String) {
          raspberryPi {
            cpu {
              averageLoads(first: $firstAverageLoads, after: $afterAverageLoads) {
                items {
                  lastMinute
                  last5Minutes
                  last15Minutes
                  dateTime
                }
                pageInfo {
                  endCursor
                  hasNextPage
                  startCursor
                  hasPreviousPage
                }
                totalCount
              }
            }
          }
        }`,
      variables: {
        firstAverageLoads: 1000,
        afterAverageLoads: null
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi.cpu.averageLoads')),
      catchError(this.handleError)
    );
  }

  getCpuRealTimeLoads(): Observable<Connection<ICpuRealTimeLoad>> {
    return this.apollo.query<{ cpuRealTimeLoads: any }>({
      query: gql`
        query CpuRealTimeLoads($firstRealTimeLoads: Int, $afterRealTimeLoads: String) {
          raspberryPi {
            cpu {
              realTimeLoads(first: $firstRealTimeLoads, after: $afterRealTimeLoads) {
                items {
                  kernel
                  user
                  total
                  dateTime
                }
                pageInfo {
                  endCursor
                  hasNextPage
                  startCursor
                  hasPreviousPage
                }
                totalCount
              }
            }
          }
        }`,
      variables: {
        firstRealTimeLoads: 1000,
        afterRealTimeLoads: null
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi.cpu.realTimeLoads')),
      catchError(this.handleError)
    );
  }

  getDiskStatuses(): Observable<Connection<IDiskStatus>> {
    return this.apollo.query<{ diskStatuses: any }>({
      query: gql`
        query DiskStatuses($firstDiskStatuses: Int, $afterDiskStatuses: String) {
          raspberryPi {
            disk {
              statuses(first: $firstDiskStatuses, after: $afterDiskStatuses) {
        	    items {
                  used
                  available
                  dateTime
                }
                pageInfo {
                  endCursor
                  hasNextPage
                  startCursor
                  hasPreviousPage
                }
                totalCount
              }
            }
          }
        }`,
      variables: {
        firstDiskStatuses: 1000,
        afterDiskStatuses: null
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi.disk.statuses')),
      catchError(this.handleError)
    );
  }

  getMemoryStatuses(): Observable<Connection<IMemoryStatus>> {
    return this.apollo.query<{ memoryStatuses: any }>({
      query: gql`
        query MemoryStatuses($firstMemoryStatuses: Int, $afterMemoryStatuses: String) {
          raspberryPi {
            memory {
              statuses(first: $firstMemoryStatuses, after: $afterMemoryStatuses) {
        	    items {
                  used
                  available
                  dateTime
                }
                pageInfo {
                  endCursor
                  hasNextPage
                  startCursor
                  hasPreviousPage
                }
                totalCount
              }
            }
          }
        }`,
      variables: {
        firstMemoryStatuses: 1000,
        afterMemoryStatuses: null
      },
      fetchPolicy: 'network-only'
    }).pipe(
      map(result => get(result.data, 'raspberryPi.memory.statuses')),
      catchError(this.handleError)
    );
  }

  shutdownRaspberryPi(): Observable<boolean> {
    return this.apollo.mutate({
      mutation: gql`
        mutation shutdown {
          shutdown
        }`
    }).pipe(
      map(result => get(result.data, 'shutdown')),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `message was: ${error.message}`);
      if (error.status === 401) { //unauthorized
        return throwError('Unauthorized; please login again.');
      }
      if (!isNil(error.error.responseType)) {
        switch (error.error.responseType) {
          case 2: // Warning
          case 3: // Error
            alert(error.error.message);
            break;
        }
      }
    }
    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  };

}
