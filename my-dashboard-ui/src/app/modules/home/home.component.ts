import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HomeService } from '../../services/home.service';
import { Content } from '../../models/content';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  description: string = '';

  constructor(
    private authService: AuthService,
    private homeService: HomeService,
    private router: Router,
  ) {
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

}
