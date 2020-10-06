import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailAuditComponent } from './detail-audit.component';

describe('DetailAuditComponent', () => {
  let component: DetailAuditComponent;
  let fixture: ComponentFixture<DetailAuditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailAuditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailAuditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
