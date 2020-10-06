import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanAuditComponent } from './plan-audit.component';

describe('PlanAuditComponent', () => {
  let component: PlanAuditComponent;
  let fixture: ComponentFixture<PlanAuditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlanAuditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanAuditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
