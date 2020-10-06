import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadStandardComponent } from './read-standard.component';

describe('ReadStandardComponent', () => {
  let component: ReadStandardComponent;
  let fixture: ComponentFixture<ReadStandardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadStandardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadStandardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
