import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadLargeFilesComponent } from './upload-large-files.component';

describe('UploadLargeFilesComponent', () => {
  let component: UploadLargeFilesComponent;
  let fixture: ComponentFixture<UploadLargeFilesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadLargeFilesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadLargeFilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
