import { TestBed } from '@angular/core/testing';

import { StudentTournamentPredictionService } from './student-tournament-prediction.service';

describe('StudentTournamentPredictionService', () => {
  let service: StudentTournamentPredictionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentTournamentPredictionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
