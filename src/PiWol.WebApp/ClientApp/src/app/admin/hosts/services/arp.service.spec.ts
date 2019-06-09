import { TestBed, inject } from '@angular/core/testing';

import { ArpService } from './arp.service';

describe('ArpService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ArpService]
    });
  });

  it('should be created', inject([ArpService], (service: ArpService) => {
    expect(service).toBeTruthy();
  }));
});
