import { Injectable } from '@angular/core';
import { IUser } from '../interfaces/IUser';
import { IStudent } from '../interfaces/IStudent';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private cachedUser: IUser | null = null;

  private usersUrl = 'http://localhost:8080/api/users';  // URL to web api - CHECK PORT!!

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  /** GET users from the server
   * 
   * @returns 
   */
  getUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.usersUrl)
      .pipe(
        tap(_ => console.log('fetched users')),
        catchError(this.handleError<IUser[]>('getusers', []))
      );
  }

  /** GET user by id. 
   * 
   * Checks if the id equals to the cachedUser (avoiding api request).
   * 
   * @param id 
   * @returns IUser found, or 404 if id not found
   */
  getUser(id: string): Observable<IUser> {
    if (this.cachedUser && this.cachedUser.Id === id) {
      return of(this.cachedUser); // Return the cached user if it equals the requested ID
    } else {
      const url = `${this.usersUrl}/${id}`;
      return this.http.get<IUser>(url).pipe(
        tap((user: IUser) => {
          this.cachedUser = user; // Cache the fetched user
          console.log(`fetched user id=${id}`);
        }),
        catchError(this.handleError<IUser>(`getUser id=${id}`))
      );
    }
  }


  /** POST - creates and register a new student
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  add(id: string, firstName: string, lastName: string, gender: string, mailAddress: string, password: string): Observable<IStudent> {
    const url= `${this.usersUrl}/register`;
    return this.http.post<IStudent>(url, { Id: id, Password: password, FirstName: firstName, LastName: lastName, Gender: gender, Email: mailAddress }, this.httpOptions).pipe(
        tap((newStudent: IStudent) => console.log(`added student w/ id=${newStudent.Id}`)),
        catchError(this.handleError<IStudent>('add'))
      );
  }

  /** POST: log of specified student
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  logStudentIn(id: string, password: string): Observable<IStudent> {
    const url = 'http://localhost:8080/api/students/login';
    return this.http.post<IStudent>(url, { Id: id, Password: password }, this.httpOptions).pipe(
        tap((student: IStudent) => console.log(`logged student w/ id=${student.Id}`)),
        catchError(this.handleError<IStudent>('login'))
      );
  }

  /** POST: log of specified admin
   * 
   * @param id 
   * @param password 
   * @returns 
   */
  logAdministratorIn(id: string, password: string): Observable<IUser> {
    const url = 'http://localhost:8080/api/administrators/login';
    return this.http.post<IUser>(url, { Id: id, Password: password }, this.httpOptions).pipe(
        tap((admin: IUser) => console.log(`logged administrator w/ id=${admin.Id}`)),
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