import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MemoryCategoriesComponent } from './memory-categories.component';

describe('MemoryCategoriesComponent', () => {
  let component: MemoryCategoriesComponent;
  let fixture: ComponentFixture<MemoryCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemoryCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemoryCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
