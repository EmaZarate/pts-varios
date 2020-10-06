import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverdueTaskComponent } from './overdue-task.component';

describe('OverdueTaskComponent', () => {
  let component: OverdueTaskComponent;
  let fixture: ComponentFixture<OverdueTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverdueTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverdueTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
