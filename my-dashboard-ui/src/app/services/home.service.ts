import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiEndpoints } from '../constants/api-endpoints';
import { Observable } from 'rxjs';
import { Content } from '../models/content';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) {}

  checkAuth(): Observable<Content> {
    return this.http.get<Content>(ApiEndpoints.HOME_CHECK_AUTH);
  }

  get() : Observable<Content> {
    return this.http.get<Content>(ApiEndpoints.HOME_GET);
  }
}
