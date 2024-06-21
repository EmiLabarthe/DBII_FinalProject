import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { IMatchResult } from '../interfaces/IMatchResult';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { IPredictionResultItem } from '../interfaces/IPredictionResultItem';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  
  private resultsUrl= 'http://localhost:8080/Result';
  private studentId: string | null = null;
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private route: ActivatedRoute) { }
  
  /** GET student results from the server
  * 
  * @param studentId 
  * @returns student results array as IMatchResult[]
  */
  getResults(): Observable<IMatchResult[]> {
    return this.http.get<IMatchResult[]>(this.resultsUrl)
    .pipe(
      tap(_ => console.log('fetched results')),
      catchError(this.handleError<IMatchResult[]>('getResults', []))
    );
  }
  
  /** GET result by id
  * 
  * @param id 
  * @returns student results array as IMatchResult[]
  */
  getResult(id: bigint): Observable<IMatchResult> {
    const url = `${this.resultsUrl}/${id}`;
    return this.http.get<IMatchResult>(url)
    .pipe(
      tap(_ => console.log(`fetched result id= '${id}'.`)),
      catchError(this.handleError<IMatchResult>('getResult'))
    );
  }
  
  /** POST - registers a new result for a match (for the first time)
  * 
  * @param MatchId 
  * @param LocalNationalTeamGoals 
  * @param VisitorNationalTeamGoals 
  * @param WinnerId 
  * @returns 
  */
  add(MatchId: bigint, LocalNationalTeamGoals: number, VisitorNationalTeamGoals: number, WinnerId: string): Observable<IMatchResult> {    
    return this.http.post<IMatchResult>
    (this.resultsUrl, { MatchId: MatchId, LocalNationalTeamGoals: LocalNationalTeamGoals, VisitorNationalTeamGoals: VisitorNationalTeamGoals, WinnerId: WinnerId }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IMatchResult>('add'))
    );
  }
  
  update(LocalNationalTeamGoals: number, VisitorNationalTeamGoals: number, WinnerId: string): Observable<IMatchResult> {    
    return this.http.put<IMatchResult>
    (this.resultsUrl, { LocalNationalTeamGoals: LocalNationalTeamGoals, VisitorNationalTeamGoals: VisitorNationalTeamGoals, WinnerId: WinnerId }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IMatchResult>('update'))
    );
  }
  
  /** GET - result items from the server
  * 
  * @param studentId 
  * @returns student result-items array as IPredictionResultItems[]
  */
  getPredictionResultItems(studentId: string): Observable<IPredictionResultItem[]> {
    const url = `${this.resultsUrl}/${studentId}-prediction`;
    return this.http.get<IPredictionResultItem[]>(url)
    .pipe(
      tap(_ => console.log(`fetched student '${studentId}' result items`)),
      catchError(this.handleError<IPredictionResultItem[]>('getPredictionResults', []))
    );
  }
  
  /**
   * 
   * @param localGoals 
   * @param localPredictedGoals 
   * @param visitorGoals 
   * @param visitorPredictedGoals 
   * @returns points obtained from a prediction according to the actual result
   */
  getPoints(localGoals: number, localPredictedGoals: number, visitorGoals: number, visitorPredictedGoals: number): number {
    // predicted the exact result correctly
    if (localGoals == localPredictedGoals && visitorGoals == visitorPredictedGoals) {
      return 4;
    } // predicted the winner correctly
    else if ((localGoals > visitorGoals && localPredictedGoals > visitorPredictedGoals)
      || (localGoals < visitorGoals && localPredictedGoals < visitorPredictedGoals)
  ) {
    return 2;
  }
  return 0;
}

/** GET - result items from the server
* 
* @param studentId 
* @returns student result-items array as IPredictionResultItems[]

getPredictionResultItem(matchId: string): Observable<IPredictionResultItem[]> {
const url = `${this.resultsUrl}/${matchId}/item`;
return this.http.get<IPredictionResultItem[]>(url)
.pipe(
tap(_ => console.log(`fetched student '${this.studentId}' result items`)),
catchError(this.handleError<IPredictionResultItem[]>('getPredictionResults', []))
);
} */

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
