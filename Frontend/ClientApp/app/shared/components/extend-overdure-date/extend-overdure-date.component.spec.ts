import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtendOverdureDateComponent } from './extend-overdure-date.component';

describe('ExtendOverdureDateComponent', () => {
  let component: ExtendOverdureDateComponent;
  let fixture: ComponentFixture<ExtendOverdureDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtendOverdureDateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtendOverdureDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
