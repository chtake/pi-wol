import { TestBed, inject } from '@angular/core/testing';

import { WolService } from './wol.service';

describe('WolService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WolService]
    });
  });

  it('should be created', inject([WolService], (service: WolService) => {
    expect(service).toBeTruthy();
  }));
});
