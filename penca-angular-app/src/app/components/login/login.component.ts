import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdministratorService } from 'src/app/services/administrator.service';
import { StudentService } from 'src/app/services/student.service';
import { IUser } from '../../interfaces/IUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
  model = { id: '', password: '', type: '' };
  userTypes = ['Administrator', 'Student'];
  
  constructor(private studentService: StudentService, private adminService: AdministratorService, private router: Router) { }
  
  login(): void {
    if (this.model.id && this.model.password && this.model.type) {
      if (this.model.type === this.userTypes[0]) { // Administrator login
        this.adminService.login(this.model.id, this.model.password).subscribe({
          next: (response: IUser) => {
            console.log(response);
            if (response && response.id) {
              this.router.navigate([`upload-result`]);
              this.model = { id: '', password: '', type: '' };
            } else {
              console.error('Invalid response structure:', response);
              alert('Error during login as admin. Please, try again.');
            }
          },
          error: (error) => {
            console.error('Error logging admin in: ', error);
            alert('Error during login as admin. Please, try again.');
          }
        });
        this.router.navigate([`/create-match`]);
      } else { // Student login
        this.studentService.login(this.model.id, this.model.password).subscribe({
          next: (response: IUser) => {
            console.log(response);
            if (response && response.id) {
              this.router.navigate([`predictions/${response.id}`]);
              this.reset();
            } else {
              console.error('Invalid response structure:', response);
              alert('Error during login as student. Please, try again.');
            }
          },
          error: (error) => {
            console.error('Error logging student in: ', error);
            alert('Error during login as student. Please, try again.');
          }
        });
      }
    }
  }
  
  private reset(): void {
    this.model.type= '';
    this.model.id= '';
    this.model.password= '';
  }
}
