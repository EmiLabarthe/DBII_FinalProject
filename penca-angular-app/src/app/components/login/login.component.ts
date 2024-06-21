import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { AdministratorService } from 'src/app/services/administrator.service';
import { StudentService } from 'src/app/services/student.service';
import { IStudent } from '../../interfaces/IStudent';
import { IUser } from '../../interfaces/IUser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  
  model = { Id: '', Password: '', Type: '' };
  userTypes= ['Administrador', 'Estudiante'];
  
  falseType = false;
  falseId = false;
  falsePass = false;
  
  constructor(private studentService: StudentService, private adminService: AdministratorService, private router: Router) { }
  
  login(): void {
    if(this.model.Id && this.model.Password && this.model.Type){
      if(this.model.Type == this.userTypes[0]) { // Administrator login
        this.adminService.login(this.model.Id, this.model.Password)
        .pipe(
          catchError((error) => {
            console.error(error);
            alert('Error al iniciar sesión como administrador. Por favor, vuelva a intentar.');
            throw error;
          })
        ).subscribe({
          next: (response: IUser) => {
            this.router.navigate([`fixture`]);
            this.model = { Id: '', Password: '', Type: '' };
          }
        });
      } else { // Student login
        this.studentService.login(this.model.Id, this.model.Password)
        .pipe(
          catchError((error) => {
            console.error(error);
            alert('Error al iniciar sesión como estudiante. Por favor, vuelva a intentar.');
            throw error;
          })
        ).subscribe({
          next: (response: IStudent) => {
            this.router.navigate([`predictions/${this.model.Id}`]);
            this.model = { Id: '', Password: '', Type: '' };
          }
          
        });
      }
    }if(!this.model.Id){
      this.falseId = true;
    }if(!this.model.Type){
      this.falseType = true;
    }if(!this.model.Password){
      this.falsePass = true;
    }
  }
  
  submitted = false;
  onSubmit() { this.submitted = true; }
  
}
