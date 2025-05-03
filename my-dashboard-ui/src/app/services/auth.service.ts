import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { ApiEndpoints } from '../constants/api-endpoints';
import { catchError, Observable } from 'rxjs';
import { Login } from '../models/login';
import { Tokens } from '../models/token';
import { Common } from '../constants/common';
import { User } from '../models/user';
import { ErrorHandlerService } from './error.service';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,
    private errorHandler: ErrorHandlerService,
    private cookieService: CookieService) { }

  login(credentials: Login): Observable<Tokens> {
    return this.http.post<Tokens>(ApiEndpoints.LOGIN, credentials).pipe<Tokens>(catchError((error) => {
      return this.errorHandler.handleError(error, () => this.login(credentials)
      );
    }));
  }

  refreshToken(): Observable<Tokens> {
    let token: Tokens = {
      accessToken: this.getToken() ?? '',
      refreshToken: this.getRefreshToken() ?? ''
    }
    return this.http.post<Tokens>(ApiEndpoints.REFRESH, token).pipe<Tokens>(catchError((error) => {
      return this.errorHandler.handleError(error, () => this.refreshToken()
      );
    }));
  }

  register(user: User): Observable<boolean> {
    return this.http.post<boolean>(ApiEndpoints.REGISTER, user).pipe<boolean>(catchError((error) => {
      return this.errorHandler.handleError(error, () => this.register(user)
      );
    }));
  }

  saveToken(token: string): void {
    this.cookieService.set(Common.ACCESS_TOKEN, token);
  }

  getToken(): string | null {
    const token = this.cookieService.get(Common.ACCESS_TOKEN);
    return token ? token : null;
  }

  saveRefreshToken(token: string): void {
    this.cookieService.set(Common.REFRESH_TOKEN, token);
  }

  getRefreshToken(): string | null {
    const token = this.cookieService.get(Common.REFRESH_TOKEN);
    return token ? token : null;
  }

  logout(): void {
    this.cookieService.delete(Common.ACCESS_TOKEN);
    this.cookieService.delete(Common.REFRESH_TOKEN);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
