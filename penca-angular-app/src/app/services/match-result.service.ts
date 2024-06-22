import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IMatchResult } from '../interfaces/IMatchResult';
import { Observable, catchError, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchResultService {

  
  
  private matchesResultsUrl = 'http://localhost:8080/MatchesResults';  // URL to web api - CHECK PORT!!
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }
  

  /** POST: add new match to the server
   * 
   * @param localNationalTeamName 
   * @param visitorNationalTeamName 
   * @param date 
   * @param stadiumName
   * @returns 
   */
  addResult(matchId: string, localNationalTeamGoals: number, visitorNationalTeamGoals: number, winnerId: string): Observable<any> {
    return this.http.post<IMatchResult>(this.matchesResultsUrl, 
      { MatchId: matchId, LocalNationalTeamGoals: localNationalTeamGoals, VisitorNationalTeamGoals: visitorNationalTeamGoals, WinnerId: winnerId }, 
      this.httpOptions)
      .pipe(
        catchError(this.handleError<IMatchResult>('add'))
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
