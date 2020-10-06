import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtendDueDateComponent } from './extend-due-date.component';

describe('ExtendDueDateComponent', () => {
  let component: ExtendDueDateComponent;
  let fixture: ComponentFixture<ExtendDueDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtendDueDateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtendDueDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
