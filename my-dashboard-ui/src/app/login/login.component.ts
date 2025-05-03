import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AlertService } from '../services/alert.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Login } from '../models/login';
import { Tokens } from '../models/token';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {


  loginForm = this.db.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private db: FormBuilder,
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void { }

  onSubmit() {
    let reqObj: Login = {
      username: this.loginForm.value.email ?? '',
      password: this.loginForm.value.password ?? '',
    };

    this.authService
      .login(reqObj)
      .subscribe({
        next: (res: Tokens) => {
          this.alertService.success('Login successful!');

          this.authService.saveToken(res.accessToken);
          this.authService.saveRefreshToken(res.refreshToken);
          this.router.navigate(['/home']);
        },
        error: (err) => {
          this.alertService.error('Login failed!');
        }
      });
  }

}
