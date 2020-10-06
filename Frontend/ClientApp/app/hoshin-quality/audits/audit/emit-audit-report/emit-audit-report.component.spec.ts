import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmitAuditReportComponent } from './emit-audit-report.component';

describe('AddFindingComponent', () => {
  let component: EmitAuditReportComponent;
  let fixture: ComponentFixture<EmitAuditReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmitAuditReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmitAuditReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
