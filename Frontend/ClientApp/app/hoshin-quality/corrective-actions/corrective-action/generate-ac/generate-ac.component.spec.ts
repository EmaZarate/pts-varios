import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateAcComponent } from './generate-ac.component';

describe('GenerateAcComponent', () => {
  let component: GenerateAcComponent;
  let fixture: ComponentFixture<GenerateAcComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenerateAcComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateAcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
