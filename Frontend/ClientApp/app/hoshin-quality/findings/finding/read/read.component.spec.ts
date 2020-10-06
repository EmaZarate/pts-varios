import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadComponent } from './read.component';

describe('Read2Component', () => {
  let component: ReadComponent;
  let fixture: ComponentFixture<ReadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
