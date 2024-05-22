import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { AdministratorService } from 'src/app/services/administrator.service';
import { StudentService } from 'src/app/services/student.service';
import { IStudent } from '../../interfaces/IStudent';
import { IUser } from '../../interfaces/IUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  model = { Id: 'Cédula de Identidad', Password: 'Contraseña', Type: 'Tipo de usuario' };
  userTypes= ['Administrador', 'Estudiante'];

  constructor(private studentService: StudentService, private adminService: AdministratorService) { }

  login(): void {
    if(this.model.Type.localeCompare(this.userTypes[0])) { // Administrator login
      this.adminService.login(this.model.Id, this.model.Password)
      .pipe(
        catchError((error) => {
          console.error(error);
          alert('Error al iniciar sesión como administrador. Por favor, vuelva a intentar.');
          throw error;
        })
      ).subscribe({
        next: (response: IUser) => {
          alert('Inicio de sesión aprobado. ¡Bienvenido!');
          this.model = { Id: 'Cédula de Identidad', Password: 'Contraseña', Type: 'Tipo de usuario' };
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
          alert('Inicio de sesión aprobado. ¡Bienvenido!');
          this.model = { Id: 'Cédula de Identidad', Password: 'Contraseña', Type: 'Tipo de usuario' };
        }
      });
    }
  }

}
