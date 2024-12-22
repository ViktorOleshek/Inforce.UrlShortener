import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthentificationService } from '../../shared/services/authentification.service';
import {FormsModule} from '@angular/forms';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthentificationService, private router: Router) {}


  login(): void {
    this.authService.login(this.username, this.password).subscribe(
      () => {
          this.router.navigate(['/urls']);
      },
      (error) => {
        this.errorMessage = 'Login failed. Please try again.';
      }
    );
  }
}
