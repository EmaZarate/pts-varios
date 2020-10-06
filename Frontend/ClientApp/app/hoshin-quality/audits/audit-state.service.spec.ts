import { TestBed } from '@angular/core/testing';

import { AuditStateService } from './audit-state.service';

describe('AuditStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuditStateService = TestBed.get(AuditStateService);
    expect(service).toBeTruthy();
  });
});
