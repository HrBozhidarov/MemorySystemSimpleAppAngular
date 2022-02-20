import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyMemoriesComponent } from './my-memories.component';

describe('MyMemoriesComponent', () => {
  let component: MyMemoriesComponent;
  let fixture: ComponentFixture<MyMemoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyMemoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyMemoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
