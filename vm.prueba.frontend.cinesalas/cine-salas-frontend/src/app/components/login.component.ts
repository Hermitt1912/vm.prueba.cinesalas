import { Component, importProvidersFrom } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

interface LoginDTO { username: string; password: string; }

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports: [
    ReactiveFormsModule
  ],
})
export class LoginComponent {
  form = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    if (this.form.invalid) return;
    const dto: LoginDTO = {
      username: this.form.value.username!,
      password: this.form.value.password!
    };
    this.auth.login(dto).subscribe(() => this.router.navigate(['/rooms']));
  }
}
