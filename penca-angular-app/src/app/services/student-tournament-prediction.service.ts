import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IStudentTournamentPrediction } from '../interfaces/IStudentTournamentPrediction';
import { Observable, catchError, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentTournamentPredictionService {

  private tournamentUrl = 'http://localhost:8080/Prediction/tournament';  // URL to web api
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }
  

  /** POST - posts a new tournament prediction
  * 
  * @param id 
  * @param firstName 
  * @param lastName 
  * @param gender 
  * @param mailAddress 
  * @param password 
  * @returns 
  */
  add(StudentId: string, ChampionId: string, ViceChampionId: string): Observable<IStudentTournamentPrediction> {
    const url = `${this.tournamentUrl}`;
    return this.http.post<IStudentTournamentPrediction>
    (url, { StudentId : StudentId, ChampionId : ChampionId, ViceChampionId : ViceChampionId }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IStudentTournamentPrediction>('add'))
    );
  }

  /**
  * Handles the Http-operation that failed; letting the app continue its course.
  * 
  * @param operation - name of the operation that failed
  * @param result - optional value to return as the observable result
  */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
