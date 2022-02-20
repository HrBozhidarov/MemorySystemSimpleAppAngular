import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { IdentityService } from 'src/app/services/identity/identity.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  public submitted: boolean = false;
  
  constructor(
    private fb: FormBuilder, 
    private identityService: IdentityService,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]],
      profileUrl: ['']
    });
  }

  get f() { return this.registerForm.controls; }

  public onRegister() {
    this.submitted = true;

    if (this.registerForm.invalid) {
        return;
    }

    this.identityService.register(this.registerForm.value).subscribe(() => {
      this.router.navigate(['/login']);
    })
  }
}
