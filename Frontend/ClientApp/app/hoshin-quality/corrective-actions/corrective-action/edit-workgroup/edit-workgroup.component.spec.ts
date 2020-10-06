import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWorkgroupComponent } from './edit-workgroup.component';

describe('EditWorkgroupComponent', () => {
  let component: EditWorkgroupComponent;
  let fixture: ComponentFixture<EditWorkgroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditWorkgroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWorkgroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
