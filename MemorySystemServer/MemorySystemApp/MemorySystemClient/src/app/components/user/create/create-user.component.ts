import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/users/user.service';

@Component({
  selector: 'create-profile',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  public from: FormGroup;
  public submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit() {
    this.from = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]],
      profileUrl: ['']
    });
  }

  get f() { return this.from.controls; }

  public onRegister() {
    this.submitted = true;

    if (this.from.invalid) {
      return;
    }

    this.userService.create(this.from.value).subscribe(
      () => this.router.navigate(['/login-user']),
      error => this.toastrService.error(error?.error?.errorMessage))
  }
}
