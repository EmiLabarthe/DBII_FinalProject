import { Injectable } from '@angular/core';
import { IUser } from '../interfaces/IUser';
import { IStudent } from '../interfaces/IStudent';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, map, of, tap } from 'rxjs';
import { AdministratorService } from './administrator.service';
import { StudentService } from './student.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private cachedUser: IUser | null = null;
  
  private usersUrl = 'http://localhost:8080/User';  // URL to web api
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient, private administratorService: AdministratorService, private studentService: StudentService) { }
  
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
    if (this.cachedUser && this.cachedUser.id === id) {
      return of(this.cachedUser); // Return the cached user if it equals the requested ID
    } else {
      const url = `${this.usersUrl}/${id}`;
      return this.http.get<IUser>(url).pipe(
        tap((user: IUser) => {
          this.cachedUser = user; // Cache the fetched user
          console.log(`fetched user id= '${id}'.`);
        }),
        catchError(this.handleError<IUser>(`getUser id= '${id}'.`))
      );
    }
  }
  
  
  /** POST - creates and register a new student
  * 
  * @param id 
  * @param firstName 
  * @param lastName 
  * @param gender 
  * @param mailAddress 
  * @param password 
  * @returns 
  */
  add(id: string, firstName: string, lastName: string, gender: string, mailAddress: string, career: string, password: string): Observable<IStudent> {
    const url = `${this.usersUrl}`;
    return this.http.post<IStudent>
    (url, { Id: id, FirstName: firstName, LastName: lastName, Email: mailAddress, Gender: gender,Career: career, Password: password }, this.httpOptions)
    .pipe(
      tap((response: any) => 
        console.log(response.message)),
      catchError(this.handleError<IStudent>('add'))
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
