import { Injectable } from '@angular/core';
import { IStudent } from '../interfaces/IStudent';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private cachedStudent: IStudent | null = null;

  private studentsUrl = 'http://localhost:8080/api/students';  // URL to web api - CHECK PORT!!

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  /** GET students from the server
   * 
   * @returns 
   */
  getStudents(): Observable<IStudent[]> {
    return this.http.get<IStudent[]>(this.studentsUrl)
      .pipe(
        tap(_ => console.log('fetched students')),
        catchError(this.handleError<IStudent[]>('getStudents', []))
      );
  }

  /** GET student by id. 
   * 
   * Checks if the id equals to the cachedStudent (avoiding api request).
   * 
   * @param id 
   * @returns IStudent found, or 404 if id not found
   */
  getMatch(id: string): Observable<IStudent> {
    if (this.cachedStudent && this.cachedStudent.Id === id) {
      return of(this.cachedStudent); // Return the cached match if it equals the requested ID
    } else {
      const url = `${this.studentsUrl}/${id}`;
      return this.http.get<IStudent>(url).pipe(
        tap((student: IStudent) => {
          this.cachedStudent = student; // Cache the fetched student
          console.log(`fetched student id=${id}`);
        }),
        catchError(this.handleError<IStudent>(`getStudent id=${id}`))
      );
    }
  }


  /**
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  add(id: string, password: string): Observable<IStudent> {
    return this.http.post<IStudent>(this.studentsUrl, { StudentId: id, Password: password }, this.httpOptions).pipe(
        tap((newStudent: IStudent) => console.log(`added match w/ id=${newStudent.Id}`)),
        catchError(this.handleError<IStudent>('add'))
      );
  }

  /** POST: log of specified student
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  login(id: string, password: string): Observable<IStudent> {
    const url = `${this.studentsUrl}/login`;
    return this.http.post<IStudent>(url, { StudentId: id, Password: password }, this.httpOptions).pipe(
        tap((newStudent: IStudent) => console.log(`logged student w/ id=${newStudent.Id}`)),
        catchError(this.handleError<IStudent>('login'))
      );
  }

  /** DELETE: remove specified Student from the server
   * 
   * @param id 
   */
  delete(id: number): Observable<boolean> {
    const url = `${this.studentsUrl}/${id}`;
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