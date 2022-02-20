import { Component, Output, EventEmitter, ViewChildren, QueryList, ElementRef, AfterViewInit } from '@angular/core';

import { LocalStorageService } from 'src/app/share/services/local-storage.service';

@Component({
  selector: 'app-memory-categories',
  templateUrl: './memory-categories.component.html',
  styleUrls: ['./memory-categories.component.css']
})
export class MemoryCategoriesComponent implements AfterViewInit {
  @Output() categoryChange: EventEmitter<string> = new EventEmitter<string>();

  @ViewChildren('all,love,education,travel,sport,nature,animal')
  categories: QueryList<ElementRef>

  constructor(private localStorageService: LocalStorageService) { }

  ngAfterViewInit() {
    let currentCategory = this.localStorageService.getItem('my-memory-category-key');
    if (!currentCategory) {
      currentCategory = 'All'; 

      this.localStorageService.setItem('my-memory-category-key', currentCategory);
    }

    for (const category of this.categories.toArray()) {
      if (currentCategory === category.nativeElement.innerText) {
        category.nativeElement.classList.add('click-box');

        return;
      }
    }
  }

  public setCategory(category: string, el: any) {
    if (!el) {
      return;
    }

    if (!this.categories || !this.categories.length) {
      return;
    }

    for (const category of this.categories.toArray()) {
      category.nativeElement.classList.remove('click-box');
    }

    el.classList.add('click-box');

    this.localStorageService.setItem('my-memory-category-key', category);
    this.categoryChange.emit(category);
  }
}
