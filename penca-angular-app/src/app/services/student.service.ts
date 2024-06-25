import { Injectable } from '@angular/core';
import { IStudent } from '../interfaces/IStudent';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { IPrediction } from '../interfaces/IPrediction';
import { ActivatedRoute } from '@angular/router';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  
  private cachedStudent: IStudent | null = null;
  
  private studentsUrl = 'http://localhost:8080/Student';  // URL to web api
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private route: ActivatedRoute) { }
  
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
  getStudent(id: string): Observable<IStudent> {
    if (this.cachedStudent && this.cachedStudent.id === id) {
      return of(this.cachedStudent); // Return the cached student if it equals the requested ID
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
  
  /** POST: log of specified student
  * 
  * @param id 
  * @param password 
  * @returns 
  */
  login(id: string, password: string): Observable<IUser> {
    const url = `${this.studentsUrl}/login`;
    return this.http.post<IUser>(url, { Id: id, Password: password }, this.httpOptions).pipe(
      tap((user: IUser) => console.log(`logged student w/ id=${user.id}`)),
      catchError(this.handleError<IUser>('login'))
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
