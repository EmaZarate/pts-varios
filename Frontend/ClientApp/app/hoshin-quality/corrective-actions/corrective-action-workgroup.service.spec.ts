import { TestBed } from '@angular/core/testing';

import { CorrectiveActionWorkgroupService } from './corrective-action-workgroup.service';

describe('CorrectiveActionWorkgroupService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CorrectiveActionWorkgroupService = TestBed.get(CorrectiveActionWorkgroupService);
    expect(service).toBeTruthy();
  });
});
