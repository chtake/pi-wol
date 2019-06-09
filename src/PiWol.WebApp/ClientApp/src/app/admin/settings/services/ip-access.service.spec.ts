import { TestBed, inject } from '@angular/core/testing';

import { IpAccessService } from './ip-access.service';

describe('IpAccessService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IpAccessService]
    });
  });

  it('should be created', inject([IpAccessService], (service: IpAccessService) => {
    expect(service).toBeTruthy();
  }));
});
