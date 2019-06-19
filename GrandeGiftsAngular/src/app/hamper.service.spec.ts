import { TestBed } from '@angular/core/testing';

import { HamperService } from './hamper.service';

describe('HamperService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HamperService = TestBed.get(HamperService);
    expect(service).toBeTruthy();
  });
});
