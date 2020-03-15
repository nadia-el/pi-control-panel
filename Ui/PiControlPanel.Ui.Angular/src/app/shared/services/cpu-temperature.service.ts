import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { unionBy } from 'lodash';
import { Apollo, QueryRef } from 'apollo-angular';
import gql from 'graphql-tag';
import { ICpuTemperature } from '../interfaces/raspberry-pi';
import { Connection } from '../interfaces/connection';
import { DEFAULT_PAGE_SIZE } from '../constants/consts';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root',
})
export class CpuTemperatureService {

  protected cpuTemperatures$: BehaviorSubject<Connection<ICpuTemperature>> = new BehaviorSubject({
    items: [],
    totalCount: 0,
    pageInfo: {
      endCursor: null,
      hasNextPage: false,
      startCursor: null,
      hasPreviousPage: false
    }
  });
  searchQuery: QueryRef<any>;
  afterCursor: string = null;
  beforeCursor: string = null;
  searchQueryResult: Observable<Connection<ICpuTemperature>>;
  
  constructor(private apollo: Apollo,
    private errorHandlingService: ErrorHandlingService) {
  }

  getFirstCpuTemperatures(pageSize: number = DEFAULT_PAGE_SIZE): Observable<Connection<ICpuTemperature>> {
    const variables = {
      firstTemperatures: pageSize,
      afterTemperatures: null
    };
    if (!this.searchQuery) {
      this.searchQuery = this.apollo.watchQuery<{ cpuTemperatures: Connection<ICpuTemperature> }>({
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
                    startCursor
                    hasPreviousPage
                    endCursor
                    hasNextPage
                  }
                  totalCount
                }
              }
            }
          }`,
        variables,
        fetchPolicy: 'network-only'
      });
      this.searchQueryResult = this.searchQuery.valueChanges
        .pipe(
          map(result => {
            const connection = result.data.raspberryPi.cpu.temperatures as Connection<ICpuTemperature>;
            this.beforeCursor = connection.pageInfo.startCursor;
            this.afterCursor = connection.pageInfo.endCursor;
            return connection;
          }),
          tap(c => this.cpuTemperatures$.next(c)),
          catchError((err) => this.errorHandlingService.handleError(err))
        );
    } else {
      this.searchQuery.setVariables(variables, true, true);
    }
    return this.searchQueryResult;
  }

  getNextPage(): any {
    if (this.searchQuery) {
      this.searchQuery.fetchMore({
        variables: {
          afterTemperatures: this.afterCursor
        },
        updateQuery: ((prev, { fetchMoreResult }) => {
          if (!fetchMoreResult.raspberryPi.cpu.temperatures.items) {
            return prev;
          }
          return Object.assign({}, prev, {
            raspberryPi: {
              cpu: {
                temperatures: {
                  items: unionBy(prev.raspberryPi.cpu.temperatures.items, fetchMoreResult.raspberryPi.cpu.temperatures.items, 'dateTime'),
                  pageInfo: fetchMoreResult.raspberryPi.cpu.temperatures.pageInfo,
                  totalCount: fetchMoreResult.raspberryPi.cpu.temperatures.totalCount,
                  __typename: 'CpuTemperatureConnection'
                },
                __typename: 'CpuType'
              },
              __typename: 'RaspberryPiType'
            }
          });
        })
      });
    }
  }

  getLastCpuTemperatures(pageSize: number = DEFAULT_PAGE_SIZE): Observable<Connection<ICpuTemperature>> {
    const variables = {
      lastTemperatures: pageSize,
      beforeTemperatures: null
    };
    if (!this.searchQuery) {
      this.searchQuery = this.apollo.watchQuery<{ cpuTemperatures: Connection<ICpuTemperature> }>({
        query: gql`
          query CpuTemperatures($lastTemperatures: Int, $beforeTemperatures: String) {
            raspberryPi {
              cpu {
                temperatures(last: $lastTemperatures, before: $beforeTemperatures) {
                items {
                    value
                    dateTime
                  }
                  pageInfo {
                    startCursor
                    hasPreviousPage
                    endCursor
                    hasNextPage
                  }
                  totalCount
                }
              }
            }
          }`,
        variables,
        fetchPolicy: 'network-only'
      });
      this.searchQueryResult = this.searchQuery.valueChanges
        .pipe(
          map(result => {
            const connection = result.data.raspberryPi.cpu.temperatures as Connection<ICpuTemperature>;
            this.beforeCursor = connection.pageInfo.startCursor;
            this.afterCursor = connection.pageInfo.endCursor;
            return connection;
          }),
          tap(c => this.cpuTemperatures$.next(c)),
          catchError((err) => this.errorHandlingService.handleError(err))
        );
    } else {
      this.searchQuery.setVariables(variables, true, true);
    }
    return this.searchQueryResult;
  }

  getPreviousPage(): any {
    if (this.searchQuery) {
      this.searchQuery.fetchMore({
        variables: {
          beforeTemperatures: this.beforeCursor
        },
        updateQuery: ((prev, { fetchMoreResult }) => {
          if (!fetchMoreResult.raspberryPi.cpu.temperatures.items) {
            return prev;
          }
          return Object.assign({}, prev, {
            raspberryPi: {
              cpu: {
                temperatures: {
                  items: unionBy(prev.raspberryPi.cpu.temperatures.items, fetchMoreResult.raspberryPi.cpu.temperatures.items, 'dateTime'),
                  pageInfo: fetchMoreResult.raspberryPi.cpu.temperatures.pageInfo,
                  totalCount: fetchMoreResult.raspberryPi.cpu.temperatures.totalCount,
                  __typename: 'CpuTemperatureConnection'
                },
                __typename: 'CpuType'
              },
              __typename: 'RaspberryPiType'
            }
          });
        })
      });
    }
  }

}
