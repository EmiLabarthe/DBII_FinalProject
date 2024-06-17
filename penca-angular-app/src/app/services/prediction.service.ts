import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { IPrediction } from '../interfaces/IPrediction';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { IPredictionItem } from '../interfaces/IPredictionItem';

@Injectable({
  providedIn: 'root'
})
export class PredictionService {
  
  private predictionsUrl= 'http://localhost:8080/Prediction';
  private studentId: string | null = null;
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private route: ActivatedRoute) { }
  
  /** GET student predictions from the server
  * 
  * @param studentId 
  * @returns student predictions array as IPrediction[]
  */
  getPredictions(studentId: string): Observable<IPrediction[]> {
    const url = `${this.predictionsUrl}/studentId/${studentId}`;
    return this.http.get<IPrediction[]>(url)
    .pipe(
      tap(_ => console.log(`fetched student '${studentId}' predictions`)),
      catchError(this.handleError<IPrediction[]>('getPredictions', []))
    );
  }
  
  /** GET  prediction by predictionId
  * 
  * @param studentId 
  * @returns student predictions array as IPrediction[]
  */
  getPrediction(predictionId: string): Observable<IPrediction> {
    const url = `${this.predictionsUrl}/prediction/${predictionId}`;
    return this.http.get<IPrediction>(url)
    .pipe(
      tap(_ => console.log(`fetched prediction id= '${predictionId}'.`)),
      catchError(this.handleError<IPrediction>('getPrediction'))
    );
  }
  
  
  /** GET - prediction items from the server
  * 
  * @param studentId 
  * @returns student prediction-items array as IPredictionItems[]
  */
  getPredictionItems(studentId: string): Observable<IPredictionItem[]> {
    const url = `${this.predictionsUrl}/items/${studentId}`;
    return this.http.get<IPredictionItem[]>(url)
    .pipe(
      tap(_ => console.log(`fetched student '${this.studentId}' prediction items`)),
      catchError(this.handleError<IPredictionItem[]>('getPredictions', []))
    );
  }
  
  /** POST - registers a new prediction for a match (for the first time)
  * 
  * @param studentId 
  * @param matchId 
  * @param LocalNationalTeamPredictedGoals 
  * @param VisitorNationalTeamPredictedGoals 
  * @returns 
  */
  add(studentId: string, matchId: string, LocalNationalTeamPredictedGoals: number, VisitorNationalTeamPredictedGoals: number): Observable<IPrediction> {    
    return this.http.post<IPrediction>
    (this.predictionsUrl, { StudentId: studentId, MatchId: matchId, LocalNationalTeamPredictedGoals: LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals: VisitorNationalTeamPredictedGoals }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IPrediction>('add'))
    );
  }
  
  /** PUT - update a student specific (and existing) prediction from the server
  * 
  * @param predictionId 
  * @param LocalNationalTeamPredictedGoals 
  * @param VisitorNationalTeamPredictedGoals 
  * @returns 
  */
  update(predictionId: bigint, LocalNationalTeamPredictedGoals: number, VisitorNationalTeamPredictedGoals: number): Observable<IPrediction> {
    const predictionUrl= `${this.predictionsUrl}/${predictionId}`;
    return this.http.put<IPrediction>(predictionUrl, 
      { LocalNationalTeamPredictedGoals: LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals: VisitorNationalTeamPredictedGoals }
    )
    .pipe(
      tap(_ => console.log(`fetched prediction '${predictionId}' predictions`)),
      catchError(this.handleError<IPrediction>('getPredictions'))
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
