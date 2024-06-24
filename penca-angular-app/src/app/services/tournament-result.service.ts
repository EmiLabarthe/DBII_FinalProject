import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITournamentResult } from '../interfaces/ITournamentResult';
import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TournamentResultService {

  private tournamentUrl = 'http://localhost:8080/TournamentResult';  // URL to web api
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }
  
  addResult(championId: string, viceChampionId: string): Observable<any> {
    return this.http.put<ITournamentResult>(this.tournamentUrl, 
      { ChampionId: championId, ViceChampionId: viceChampionId}, 
      this.httpOptions)
      .pipe(
        catchError(this.handleError<ITournamentResult>('add'))
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
