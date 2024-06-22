import { Injectable } from '@angular/core';
import { IMatch } from '../interfaces/IMatch';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  
  private cachedMatch: IMatch | null = null;
  
  private matchesUrl = 'http://localhost:8080/Matches';  // URL to web api - CHECK PORT!!
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }
  
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
  getMatch(id: bigint): Observable<IMatch> {
    if (this.cachedMatch && this.cachedMatch.Id === id) {
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
   * @param localNationalTeamName 
   * @param visitorNationalTeamName 
   * @param date 
   * @param stadiumName
   * @returns 
   */
  add(localNationalTeamName: string, visitorNationalTeamName: string, date: string, stadiumId: number, stageId: string): Observable<IMatch> {
    return this.http.post<IMatch>('http://localhost:8080/Fixture/match', 
      { LocalNationalTeam: localNationalTeamName, VisitorNationalTeam: visitorNationalTeamName, Date: date, StadiumId: stadiumId, StageId: stageId }, 
      this.httpOptions)
      .pipe(
        tap((newMatch: IMatch) => console.log(`added match w/ id=${newMatch.Id}`)),
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
  