import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { AlertService } from '../services/alert.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { User } from '../models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm = this.fb.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private fb :FormBuilder,
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void { }

  onSubmit() { 
    let reqObj: User = {
          username: this.registerForm.value.email ?? '',
          password: this.registerForm.value.password ?? '',
          email: this.registerForm.value.email ?? '',
          role : 'User',
        };

    this.authService
          .register(reqObj)
          .subscribe({
            next: (res: boolean) => {
              this.alertService.success('Registered successful!');
              this.router.navigate(['/login']);
            },
            error: (err) => {
              this.alertService.error('Registration failed!');
            }
          });
  }
}
