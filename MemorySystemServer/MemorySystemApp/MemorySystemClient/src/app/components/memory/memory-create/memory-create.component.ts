import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MemoryService } from 'src/app/services/memory/memory.service';
import { CategoryType } from 'src/app/enums/CategoryType';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-memory-create',
  templateUrl: './memory-create.component.html',
  styleUrls: ['./memory-create.component.css']
})
export class MemoryCreateComponent implements OnInit {
  public memoryForm: FormGroup;
  public submitted: boolean = false;
  public CategoryType = CategoryType;
  public categoryKeys = [];

  constructor(
    private fb: FormBuilder, 
    private memoryService: MemoryService,
    private router: Router,
    private toastrService: ToastrService) { }

  ngOnInit() {    
    this.categoryKeys = Object.keys(CategoryType).reduce((arr, key) => {
      if (!arr.includes(key)) {
        arr.push(CategoryType[key]);
      }
      return arr;
    }, []);
    
    this.memoryForm = this.fb.group({
      url: ['', Validators.required],
      description: [''],
      type: [CategoryType.Love]
    });
  }
  
  get f() { return this.memoryForm.controls; }

  public onMemory() {
    this.submitted = true;

    if (this.memoryForm.invalid) {
        return;
    }

    this.memoryService.create(this.memoryForm.value).subscribe(data => {
      this.toastrService.success('Successfully created picture.');

      this.router.navigate(['/home']);
    });
  }
}
