import { TestBed } from '@angular/core/testing';

import { PwaUpdateService } from './pwa-update-service.service';

describe('PwaUpdateServiceService', () => {
  let service: PwaUpdateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PwaUpdateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
