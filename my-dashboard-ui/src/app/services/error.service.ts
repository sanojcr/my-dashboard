import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { ApiEndpoints } from "../constants/api-endpoints";
import { LogEntry } from "../models/log-entry";

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService {

    constructor(private http: HttpClient
    ) { }

    logToServer(message: string): Observable<boolean> {
        let error: LogEntry = {
            date: new Date(),
            message: message,
            exception: message,
            logLevel: 'Error',
            source: 'Client',
        }

        return this.http.post<boolean>(ApiEndpoints.LOG_ERROR, error)
            .pipe<boolean>(catchError((error) => {
                return this.handleError(error, () => this.logToServer(message)
                );
            }));
    }


    handleError<T>(error: any, retryCallback: () => Observable<T>): Observable<T> {
        const shouldRetry = window.confirm('An error occurred. Retry?');
        return shouldRetry ? retryCallback() : throwError(() => error);
    }


    getHttpErrorMessage(error: HttpErrorResponse): string {
        if (error.error instanceof ErrorEvent) {
            return `Client-side error: ${error.error.message}`;
        }

        if (error.status === 0) {
            return 'Cannot connect to server.';
        }

        if (error.status === 404) {
            return 'Resource not found.';
        }

        if (error.status === 500) {
            return 'Internal server error. Please try again later.';
        }

        if (typeof error.error === 'string') {
            return error.error;
        }

        if (error.error && error.error.message) {
            return error.error.message;
        }

        return `Server returned code: ${error.status}`;
    }
}