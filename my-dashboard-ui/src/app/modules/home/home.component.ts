import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HomeService } from '../../services/home.service';
import { Content } from '../../models/content';
import { AlertService } from '../../services/alert.service';
import { ErrorHandlerService } from '../../services/error.service';
import { Claims } from '../../models/claims';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  user: Claims = {email: '', role: '', name: ''};
  description: string = '';

  constructor(
    private authService: AuthService,
    private homeService: HomeService,
    private alertService: AlertService,
    private error: ErrorHandlerService,
    private router: Router,
  ) {
     this.user = this.authService.getTokenPayload();
  }

  ngOnInit(): void {
    this.homeService.checkAuth().subscribe({
      next: (res: Content) => {
        if (res) {
          this.description = res.message;
          console.log(res);
        }
      },
      error: (err) => {
        this.authService.logout();
        this.router.navigate(['/login']);
      }
    });
  }

  onThrowError() {
    this.homeService.throwError().subscribe({
      next: (res: Content) => {
        if (res) {
          this.description = res.message;
          console.log(res);
        }
      },
      error: (err) => {
        this.alertService.error(this.error.getHttpErrorMessage(err));
      }
    });
  }

  onThrowErrorWithServer() {
    throw new Error("This will trigger the global error handler");
  }

}
