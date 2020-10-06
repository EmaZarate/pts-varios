import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverduedEvaluateDateACComponent } from './overdued-evaluate-date-ac.component';

describe('OverduedEvaluateDateACComponent', () => {
  let component: OverduedEvaluateDateACComponent;
  let fixture: ComponentFixture<OverduedEvaluateDateACComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverduedEvaluateDateACComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverduedEvaluateDateACComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
