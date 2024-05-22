import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class AdministratorService {

  private cachedAdministrator: IUser | null = null;

  private administratorsUrl = 'http://localhost:8080/api/administrators';  // URL to web api - CHECK PORT!!

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  /** GET administrators from the server
   * 
   * @returns 
   */
  getAdministrators(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.administratorsUrl)
      .pipe(
        tap(_ => console.log('fetched administrators')),
        catchError(this.handleError<IUser[]>('getAdministrators', []))
      );
  }

  /** GET administrator by id. 
   * 
   * Checks if the id equals to the cachedAdministrator (avoiding api request).
   * 
   * @param id 
   * @returns IUser found, or 404 if id not found
   */
  getAdministrator(id: string): Observable<IUser> {
    if (this.cachedAdministrator && this.cachedAdministrator.Id === id) {
      return of(this.cachedAdministrator); // Return the cached match if it equals the requested ID
    } else {
      const url = `${this.administratorsUrl}/${id}`;
      return this.http.get<IUser>(url).pipe(
        tap((administrator: IUser) => {
          this.cachedAdministrator = administrator; // Cache the fetched administrator
          console.log(`fetched administrator id=${id}`);
        }),
        catchError(this.handleError<IUser>(`getAdministrator id=${id}`))
      );
    }
  }


  /** POST - registers an administator
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  add(id: string, password: string): Observable<IUser> {
    return this.http.post<IUser>(this.administratorsUrl, { Id: id, Password: password }, this.httpOptions).pipe(
        tap((newAdministrator: IUser) => console.log(`added administrator w/ id=${newAdministrator.Id}`)),
        catchError(this.handleError<IUser>('add'))
      );
  }

  /** POST: log of specified administrator
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  login(id: string, password: string): Observable<IUser> {
    const url = `${this.administratorsUrl}/login`;
    return this.http.post<IUser>(url, { Id: id, Password: password }, this.httpOptions).pipe(
        tap((administrator: IUser) => console.log(`logged administrator w/ id=${administrator.Id}`)),
        catchError(this.handleError<IUser>('login'))
      );
  }

  /** DELETE: remove specified administrator from the server
   * 
   * @param id 
   */
  delete(id: number): Observable<boolean> {
    const url = `${this.administratorsUrl}/${id}`;
    return this.http.delete(url).pipe(
      tap(_ => console.log(`deleted administrator id=${id}`)),
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
