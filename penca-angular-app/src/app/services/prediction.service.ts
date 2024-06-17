import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { IPrediction } from '../interfaces/IPrediction';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PredictionService {
  
  private predictionsUrl= 'http://localhost:8080/api/predictions';
  private studentId: string | null = null;
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private route: ActivatedRoute) { }
  
  /** GET student predictions from the server
  * 
  * @returns student predictions array as IPrediction[]
  */
  getPredictions(): Observable<IPrediction[]> {
    return this.http.get<IPrediction[]>(this.getUrl())
    .pipe(
      tap(_ => console.log(`fetched student '${this.studentId}' predictions`)),
      catchError(this.handleError<IPrediction[]>('getPredictions', []))
    );
  }

  getPredictionItems(): Observable<IPrediction[]> {
    return this.http.get<IPrediction[]>(this.getUrl())
    .pipe(
      tap(_ => console.log(`fetched student '${this.studentId}' predictions`)),
      catchError(this.handleError<IPrediction[]>('getPredictions', []))
    );
  }
  
  
  add(matchId: string, localNationalTeamGoals: number, visitorNationalTeamGoals: number): Observable<IPrediction> {
    return this.http.post<IPrediction>
    (this.getUrl(), { MatchId: matchId, LocalNationalTeamPredictedGoals: localNationalTeamGoals, VisitorNationalTeamPredictedGoals: visitorNationalTeamGoals }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IPrediction>('add'))
    );
  }
  
  /** PUT update a student specific (and existing) prediction from the server
  * 
  * @returns new prediction as IPrediction
  */
  update(predictionId: bigint, localNationalTeamGoals: number, visitorNationalTeamGoals: number): Observable<IPrediction> {
    const predictionUrl= `${this.predictionsUrl}/${predictionId}`;
    return this.http.put<IPrediction>(predictionUrl, 
      { LocalNationalTeamPredictedGoals: localNationalTeamGoals, VisitorNationalTeamPredictedGoals: visitorNationalTeamGoals }
    )
    .pipe(
      tap(_ => console.log(`fetched prediction '${predictionId}' predictions`)),
      catchError(this.handleError<IPrediction>('getPredictions'))
    );
  }
  
  private getUrl(): string {
    this.route.params.subscribe(params => {
      this.studentId = params['studentId'] // Retrieves the 'studentId' parameter from the URL
    });
    const url= `${this.predictionsUrl}/${this.studentId}`
    return url;
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
