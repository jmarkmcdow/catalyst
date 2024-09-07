import { TestBed } from '@angular/core/testing';

import { ContactVettingService } from './conttactvetting.service';

describe('VettingService', () => {
  let service: ContactVettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContactVettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
