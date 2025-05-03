import { Component } from '@angular/core';
import { AlertMessage } from '../models/alert-message';
import { AlertService } from '../services/alert.service';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrl: './alert.component.css'
})
export class AlertComponent {
  alert: AlertMessage | null = null;

  constructor(private alertService: AlertService) {}

  ngOnInit(): void {
    this.alertService.alert$.subscribe((alert) => {
      this.alert = alert || null;

      if (alert) {
        setTimeout(() => this.alert = null, 3000);
      }
    });
  }
}
