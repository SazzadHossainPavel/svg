import { TestBed } from '@angular/core/testing';

import { RectangleApiService } from './rectangle-api.service';

describe('RectangleApiService', () => {
  let service: RectangleApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RectangleApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
