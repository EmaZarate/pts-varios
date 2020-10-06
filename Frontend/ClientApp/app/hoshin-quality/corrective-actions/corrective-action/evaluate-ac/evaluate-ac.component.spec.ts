import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluateAcComponent } from './evaluate-ac.component';

describe('EvaluateAcComponent', () => {
  let component: EvaluateAcComponent;
  let fixture: ComponentFixture<EvaluateAcComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EvaluateAcComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluateAcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
