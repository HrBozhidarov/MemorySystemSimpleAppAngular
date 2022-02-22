import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { IdentityService } from 'src/app/services/identity/identity.service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private identityService: IdentityService,
    private localStorageService: LocalStorageService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  get f() { return this.loginForm.controls; }

  public onLogin() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.identityService.login(this.loginForm.value).subscribe(
      result => {
        this.localStorageService.setItem("token", result.data.token);
        this.localStorageService.setItem('user-profile-picture', result.data.profileUrl);
        this.localStorageService.setItem('role', result.data.role);

        this.router.navigate(['/home']);
      },
      error => {
        this.toastrService.error(error?.error?.errorMessage);
      })
  }
}
