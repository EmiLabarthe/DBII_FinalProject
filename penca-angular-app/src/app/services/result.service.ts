import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { IResult } from '../interfaces/IResult';
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
  * @returns student results array as IResult[]
  */
  getResults(): Observable<IResult[]> {
    return this.http.get<IResult[]>(this.resultsUrl)
    .pipe(
      tap(_ => console.log('fetched results')),
      catchError(this.handleError<IResult[]>('getResults', []))
    );
  }
  
  /** GET result by resultId
  * 
  * @param studentId 
  * @returns student results array as IResult[]
  */
  getResult(resultId: string): Observable<IResult> {
    const url = `${this.resultsUrl}/${resultId}`;
    return this.http.get<IResult>(url)
    .pipe(
      tap(_ => console.log(`fetched result id= '${resultId}'.`)),
      catchError(this.handleError<IResult>('getResult'))
    );
  }
  
  /** POST - registers a new result for a match (for the first time)
  * 
  * @param matchId 
  * @param LocalNationalTeamGoals 
  * @param VisitorNationalTeamGoals 
  * @param WinnerId 
  * @returns 
  */
  add(matchId: string, LocalNationalTeamGoals: number, VisitorNationalTeamGoals: number, WinnerId: string): Observable<IResult> {    
    return this.http.post<IResult>
    (this.resultsUrl, { MatchId: matchId, LocalNationalTeamGoals: LocalNationalTeamGoals, VisitorNationalTeamGoals: VisitorNationalTeamGoals, WinnerId: WinnerId }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IResult>('add'))
    );
  }
  
  /** GET - result items from the server
  * 
  * @param studentId 
  * @returns student result-items array as IPredictionResultItems[]
  */
  getPredictionResultItems(studentId: string): Observable<IPredictionResultItem[]> {
    const url = `${this.resultsUrl}/prediction/${studentId}`;
    return this.http.get<IPredictionResultItem[]>(url)
    .pipe(
      tap(_ => console.log(`fetched student '${this.studentId}' result items`)),
      catchError(this.handleError<IPredictionResultItem[]>('getPredictionResults', []))
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
