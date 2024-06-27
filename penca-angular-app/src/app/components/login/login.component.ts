import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
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
  userTypes = ['Administrador', 'Estudiante'];
  
  falseType = false;
  falseId = false;
  falsePass = false;
  
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
              alert('Error al iniciar sesión como admin. Por favor, vuelva a intentar.');
            }
          },
          error: (error) => {
            console.error('Error logging admin in: ', error);
            alert('Error al iniciar sesión como admin. Por favor, vuelva a intentar.');
          }
        });
        this.router.navigate([`/create-match`]);
      } else { // Student login
        this.studentService.login(this.model.id, this.model.password).subscribe({
          next: (response: IUser) => {
            console.log(response);
            if (response && response.id) {
              this.router.navigate([`predictions/${response.id}`]);
              this.model = { id: '', password: '', type: '' };
            } else {
              console.error('Invalid response structure:', response);
              alert('Error al iniciar sesión como estudiante. Por favor, vuelva a intentar.');
            }
          },
          error: (error) => {
            console.error('Error logging student in: ', error);
            alert('Error al iniciar sesión como estudiante. Por favor, vuelva a intentar.');
          }
        });
      }
    } else {
      if (!this.model.id) {
        this.falseId = true;
      }
      if (!this.model.type) {
        this.falseType = true;
      }
      if (!this.model.password) {
        this.falsePass = true;
      }
    }
  }

  submitted = false;
  onSubmit() { this.submitted = true; }
}
