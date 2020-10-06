import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveRejectAuditComponent } from './approve-reject-audit.component';

describe('ApproveRejectAuditComponent', () => {
  let component: ApproveRejectAuditComponent;
  let fixture: ComponentFixture<ApproveRejectAuditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveRejectAuditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveRejectAuditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
