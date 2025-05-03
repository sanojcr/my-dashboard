import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { AlertMessage } from '../models/alert-message';

@Injectable({ providedIn: 'root' })
export class AlertService {
  private alertSubject = new Subject<AlertMessage>();
  alert$: Observable<AlertMessage> = this.alertSubject.asObservable();

  success(message: string) {
    this.alertSubject.next({ type: 'success', text: message });
  }

  error(message: string) {
    this.alertSubject.next({ type: 'danger', text: message });
  }

  warning(message: string) {
    this.alertSubject.next({ type: 'warning', text: message });
  }

  info(message: string) {
    this.alertSubject.next({ type: 'info', text: message });
  }
}
