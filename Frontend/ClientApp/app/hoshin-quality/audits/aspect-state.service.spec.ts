import { TestBed } from '@angular/core/testing';

import { AspectStateService } from './aspect-state.service';

describe('AspectStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AspectStateService = TestBed.get(AspectStateService);
    expect(service).toBeTruthy();
  });
});
