import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule, routes } from './app.routes';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainNavComponent } from './main-nav/main-nav.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { AuthService } from './services/auth.service';
import { AlertComponent } from './alert/alert.component';
import { AlertService } from './services/alert.service';
import { HomeService } from './services/home.service';
import { AppErrorHandler } from './app.error-handler';
import { ErrorHandlerService } from './services/error.service';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainNavComponent,
    NotFoundComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    AuthService,
    AlertService,
    HomeService,
    ErrorHandlerService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    { provide: ErrorHandler, useClass: AppErrorHandler },
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }