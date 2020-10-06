import { TestBed } from '@angular/core/testing';

import { AuditStandardAspectService } from './audit-standard-aspect.service';

describe('AuditStandardAspectService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuditStandardAspectService = TestBed.get(AuditStandardAspectService);
    expect(service).toBeTruthy();
  });
});
