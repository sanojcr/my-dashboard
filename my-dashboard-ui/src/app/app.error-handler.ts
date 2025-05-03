import { ErrorHandler, Injectable } from "@angular/core";
import { AlertService } from "./services/alert.service";
import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandlerService } from "./services/error.service";

@Injectable()
export class AppErrorHandler implements ErrorHandler {
    constructor(private alert: AlertService, private error: ErrorHandlerService) { }

    handleError(error: any): void {
        let message = 'An unexpected error occurred.';

        if (error instanceof HttpErrorResponse) {
            if (!navigator.onLine) {
                message = 'No Internet Connection';
            } else {
                message = this.error.getHttpErrorMessage(error);
            }
        }
        else if (error instanceof Error) {
            message = error.message;
        }

        this.alert.error(message);
        this.error.logToServer(message)
        .subscribe({
            next: (res) => {
                console.log('Error logged to server:', res);
            },
            error: (err) => {
                console.error('Failed to log error to server:', err);
            }
        });
        console.error('Error occurred:', error); 
    }

}