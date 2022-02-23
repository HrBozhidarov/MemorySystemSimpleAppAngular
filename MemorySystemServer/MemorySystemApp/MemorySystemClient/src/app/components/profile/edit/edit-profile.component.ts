import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/users/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class EditProfileComponent implements OnInit {
  public editForm: FormGroup;
  public submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit() {


    this.editForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]],
      profileUrl: ['']
    });
  }

  get f() { return this.editForm.controls; }

  public onRegister() {
    this.submitted = true;

    if (this.editForm.invalid) {
      return;
    }

    this.userService.register(this.editForm.value).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      error => {
        this.toastrService.error(error?.error?.errorMessage);
      })
  }
}
