import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { DefaultInterceptor } from '../interceptor/default.interceptor';
import { authInterceptor } from '../interceptor/auth.interceptor';

@NgModule({
  declarations: [AppComponent, AuthComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [
    provideClientHydration(),
    provideHttpClient(
      withInterceptors([authInterceptor]),
      withInterceptorsFromDi() // Register interceptors from the DI container
    ),
    { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
