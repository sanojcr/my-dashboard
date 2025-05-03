import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiEndpoints } from '../constants/api-endpoints';
import { catchError, Observable, throwError } from 'rxjs';
import { Content } from '../models/content';
import { ErrorHandlerService } from './error.service';

@Injectable({
    providedIn: 'root'
})
export class HomeService {

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService
    ) { }

    checkAuth(): Observable<Content> {
        return this.http.get<Content>(ApiEndpoints.HOME_CHECK_AUTH)
            .pipe<Content>(catchError((error) => {
                return this.errorHandler.handleError(error, () => this.checkAuth()
                );
            }));
    }

    get(): Observable<Content> {
        return this.http.get<Content>(ApiEndpoints.HOME_GET)
            .pipe<Content>(catchError((error) => {
                return this.errorHandler.handleError(error, () => this.get()
                );
            }));
    }

    throwError(): Observable<Content> {
        return this.http.get<Content>(ApiEndpoints.HOME_THROW)
            .pipe<Content>(catchError((error) => {
                return this.errorHandler.handleError(error, () => this.get()
                );
            }));
    }

}
