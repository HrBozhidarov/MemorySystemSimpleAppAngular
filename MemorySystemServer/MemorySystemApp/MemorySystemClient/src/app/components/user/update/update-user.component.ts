import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/users/user.service';

@Component({
  selector: 'edit-profile',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {
  public form: FormGroup;
  public submitted: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit() {
    this.userService.details().subscribe(user => {
      this.form = this.fb.group({
        username: [user.username, Validators.required],
        email: [user.email, Validators.required],
        password: [user.password, [Validators.required, Validators.minLength(3)]],
        profileUrl: [user.profileUrl]
      })
    });
  }

  get f() { return this.form.controls; }

  public onRegister() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.userService.update(this.form.value).subscribe(
      () => {
        this.toastrService.success('You successfully update your profile!');
        this.router.navigate(['/home']);
      },
      error => this.toastrService.error(error?.error?.errorMessage))
  }
}
