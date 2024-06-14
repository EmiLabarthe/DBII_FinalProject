import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { IPrediction } from '../interfaces/IPrediction';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PredictionService {

  private cachedPrediction: IPrediction | null = null;
  
  private predictionsUrl = 'http://localhost:8080/api/predictions';  // URL to web api
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }
  
  /** GET Predictions from the server
  * 
  * @returns 
  */
  getPredictions(): Observable<IPrediction[]> {
    return this.http.get<IPrediction[]>(this.predictionsUrl)
    .pipe(
      tap(_ => console.log('fetched Predictions')),
      catchError(this.handleError<IPrediction[]>('getPredictions', []))
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
