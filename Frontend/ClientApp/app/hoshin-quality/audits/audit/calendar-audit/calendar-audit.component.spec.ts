import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarAuditComponent } from './calendar-audit.component';

describe('CalendarAuditComponent', () => {
  let component: CalendarAuditComponent;
  let fixture: ComponentFixture<CalendarAuditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarAuditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarAuditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
