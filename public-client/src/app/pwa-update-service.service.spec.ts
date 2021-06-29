import { TestBed } from '@angular/core/testing';

import { PwaUpdateServiceService } from './pwa-update-service.service';

describe('PwaUpdateServiceService', () => {
  let service: PwaUpdateServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PwaUpdateServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
