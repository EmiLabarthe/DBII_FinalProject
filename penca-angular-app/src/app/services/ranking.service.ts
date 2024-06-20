import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRankingItem } from '../interfaces/IRankingItem';
import { Observable, catchError, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RankingService {
  
  private rankingURL = 'http://localhost:8080/Ranking';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  cachedRanking: IRankingItem | null = null;

  constructor(private http: HttpClient) { }

  /** GET fixture from the server
  * 
  * @returns 
  */
  getRanking(): Observable<IRankingItem[]> {
    return this.http.get<IRankingItem[]>(this.rankingURL)
    .pipe(
      tap(_ => console.log('fetched ranking')),
      catchError(this.handleError<IRankingItem[]>('getRanking', []))
    );
  }
  
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
