import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverduedPlanningDateComponent } from './overdued-planning-date.component';

describe('OverduedPlanningDateComponent', () => {
  let component: OverduedPlanningDateComponent;
  let fixture: ComponentFixture<OverduedPlanningDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverduedPlanningDateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverduedPlanningDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
