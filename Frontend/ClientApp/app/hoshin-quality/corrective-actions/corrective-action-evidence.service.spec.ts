import { TestBed } from '@angular/core/testing';

import { CorrectiveActionEvidenceService } from './corrective-action-evidence.service';

describe('CorrectiveActionEvidenceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CorrectiveActionEvidenceService = TestBed.get(CorrectiveActionEvidenceService);
    expect(service).toBeTruthy();
  });
});
