import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReassingCorrectiveActionsComponent } from './reassing-corrective-actions.component';

describe('ReassingCorrectiveActionsComponent', () => {
  let component: ReassingCorrectiveActionsComponent;
  let fixture: ComponentFixture<ReassingCorrectiveActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReassingCorrectiveActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReassingCorrectiveActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
