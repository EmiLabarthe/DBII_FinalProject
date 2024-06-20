import { Injectable } from '@angular/core';
import { IFixtureItem } from '../interfaces/IFixtureItem';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, of, tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class FixtureService {

  private fixtureURL = 'http://localhost:8080/Fixture';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  cachedFixture: IFixtureItem | null = null;

  constructor(private http: HttpClient) { }

  /** GET fixture from the server
  * 
  * @returns 
  */
  getFixture(): Observable<IFixtureItem[]> {
    return this.http.get<IFixtureItem[]>(this.fixtureURL)
    .pipe(
      tap(_ => console.log('fetched matches')),
      catchError(this.handleError<IFixtureItem[]>('getFixture', []))
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

