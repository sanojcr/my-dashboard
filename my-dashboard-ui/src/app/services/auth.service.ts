import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { ApiEndpoints } from '../constants/api-endpoints';
import { Observable } from 'rxjs';
import { Login } from '../models/login';
import { Tokens } from '../models/token';
import { Common } from '../constants/common';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {}

  login(credentials: Login): Observable<Tokens> {
    return this.http.post<Tokens>(ApiEndpoints.LOGIN, credentials);
  }

  refreshToken() : Observable<Tokens> {
    let token : Tokens = {
      accessToken: this.getToken() ?? '',
      refreshToken: this.getRefreshToken() ?? ''
    }
    return this.http.post<Tokens>(ApiEndpoints.REFRESH, token);
  }

  register(user: User ) : Observable<boolean> {
    return this.http.post<boolean>(ApiEndpoints.REGISTER, user);
  }

  saveToken(token: string): void {
    localStorage.setItem(Common.ACCESS_TOKEN, token);
  }

  getToken(): string | null {
    return localStorage.getItem(Common.ACCESS_TOKEN);
  }

  saveRefreshToken(token: string): void {
    localStorage.setItem(Common.REFRESH_TOKEN, token);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(Common.REFRESH_TOKEN);
  }

  logout(): void {
    localStorage.removeItem(Common.ACCESS_TOKEN);
    localStorage.removeItem(Common.REFRESH_TOKEN);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
