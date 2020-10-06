import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtendDueDateTaskComponent } from './extend-due-date-task.component';

describe('ExtendDueDateTaskComponent', () => {
  let component: ExtendDueDateTaskComponent;
  let fixture: ComponentFixture<ExtendDueDateTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtendDueDateTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtendDueDateTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
