import { Injectable } from '@angular/core';
import { IMatch } from '../interfaces/IMatch';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  private cachedMatch: IMatch | null = null;

  private matchesUrl = 'http://localhost:3000/api/matches';  // URL to web api - CHANGE PORT!!

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor() { }

  /** GET matches from the server
   * 
   * @returns 
   */
  getMatches(): Observable<IMatch[]> {
    return this.http.get<IMatch[]>(this.matchesUrl)
      .pipe(
        tap(_ => console.log('fetched matches')),
        catchError(this.handleError<IMatch[]>('getMatches', []))
      );
  }

  /** GET match by id. 
   * 
   * Checks if the id equals to the cachedMatch (avoiding api request).
   * 
   * @param id 
   * @returns IMatch found, or 404 if id not found
   */
  getMatch(id: string): Observable<IMatch> {
    if (this.cachedMatch && this.cachedMatch._id === id) {
      return of(this.cachedMatch); // Return the cached match if it equals the requested ID
    } else {
      const url = `${this.matchesUrl}/${id}`;
      return this.http.get<IMatch>(url).pipe(
        tap((match: IMatch) => {
          this.cachedMatch = match; // Cache the fetched Match
          console.log(`fetched match id=${id}`);
        }),
        catchError(this.handleError<IMatch>(`getMatch id=${id}`))
      );
    }
  }

  /** POST: add new match to the server
   * 
   * @param name 
   * @param category 
   * @param description 
   * @param image 
   * @returns 
   */
  add(localNationalTeamName: string, visitorNationalTeamName: string, date: Date, stadiumId: string): Observable<IMatch> {
    return this.http.post<IMatch>(this.matchesUrl, { localNationalTeamName, visitorNationalTeamName, date, stadiumId }, this.httpOptions).pipe(
        tap((newMatch: IMatch) => console.log(`added match w/ id=${newMatch._id}`)),
        catchError(this.handleError<IMatch>('add'))
      );
  }

  /** DELETE: remove specified Match from the server
   * 
   * @param id 
   */
  delete(id: number): Observable<boolean> {
    const url = `${this.matchesUrl}/${id}`;
    return this.http.delete(url).pipe(
      tap(_ => console.log(`deleted match id=${id}`)),
      map(() => true), // If the operation is successful, response is mapped into a 'true' boolean
      catchError(this.handleError<boolean>(`delete id=${id}`))
    );
  }

}
