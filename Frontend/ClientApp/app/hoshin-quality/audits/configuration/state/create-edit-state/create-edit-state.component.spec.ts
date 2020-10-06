import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditStateComponent } from './create-edit-state.component';

describe('CreateEditStatusComponent', () => {
  let component: CreateEditStateComponent;
  let fixture: ComponentFixture<CreateEditStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
