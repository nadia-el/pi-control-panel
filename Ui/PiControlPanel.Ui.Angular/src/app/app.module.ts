import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ApolloModule, Apollo, APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';

import { environment } from '../environments/environment';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardResolve } from './dashboard/dashboard.resolve';
import { RealTimeModalComponent } from './dashboard/modal/real-time-modal.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, resolve: { raspberryPi: DashboardResolve } },
  { path: '**', redirectTo: '/home' },
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    RealTimeModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    NgxChartsModule,
    //StoreModule.forRoot(appReducers),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production
    }),
    ApolloModule,
    HttpLinkModule
  ],
  entryComponents: [
    RealTimeModalComponent
  ],
  providers: [
    DashboardResolve,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    {
      provide: APOLLO_OPTIONS, deps: [HttpLink], useFactory: (httpLink: HttpLink) => {
        return {
          cache: new InMemoryCache(),
          link: httpLink.create({ uri: 'http://192.168.2.251:8080/graphql' })
        }
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
