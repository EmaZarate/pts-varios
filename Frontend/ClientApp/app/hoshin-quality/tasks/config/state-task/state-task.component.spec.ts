import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StateTaskComponent } from './state-task.component';

describe('StateTaskComponent', () => {
  let component: StateTaskComponent;
  let fixture: ComponentFixture<StateTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StateTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StateTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
