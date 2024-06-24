import { TestBed } from '@angular/core/testing';

import { TournamentResultService } from './tournament-result.service';

describe('TournamentResultService', () => {
  let service: TournamentResultService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TournamentResultService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
