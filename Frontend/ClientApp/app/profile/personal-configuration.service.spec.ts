import { TestBed } from '@angular/core/testing';

import { PersonalConfigurationService } from './personal-configuration.service';

describe('PersonalConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PersonalConfigurationService = TestBed.get(PersonalConfigurationService);
    expect(service).toBeTruthy();
  });
});
