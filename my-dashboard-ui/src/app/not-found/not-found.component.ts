import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.css'
})
export class NotFoundComponent implements OnInit {

  constructor(private route: Router
  ) {
    
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  redirectToHome(){
    this.route.navigate(['/']);
  }

}
