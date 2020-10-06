import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadStateComponent } from './read-state.component';

describe('ReadStatusComponent', () => {
  let component: ReadStateComponent;
  let fixture: ComponentFixture<ReadStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
