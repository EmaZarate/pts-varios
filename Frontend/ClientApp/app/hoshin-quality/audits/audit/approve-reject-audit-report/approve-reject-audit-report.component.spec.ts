import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveRejectAuditReportComponent } from './approve-reject-audit-report.component';

describe('ApproveRejectAuditReportComponent', () => {
  let component: ApproveRejectAuditReportComponent;
  let fixture: ComponentFixture<ApproveRejectAuditReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveRejectAuditReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveRejectAuditReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
