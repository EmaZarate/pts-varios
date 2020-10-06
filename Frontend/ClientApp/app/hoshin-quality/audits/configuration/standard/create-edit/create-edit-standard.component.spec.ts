import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditStandardComponent } from './create-edit-standard.component';

describe('CreateEditStandardComponent', () => {
  let component: CreateEditStandardComponent;
  let fixture: ComponentFixture<CreateEditStandardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditStandardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditStandardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
