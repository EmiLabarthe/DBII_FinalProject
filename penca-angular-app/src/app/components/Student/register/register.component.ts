import { Component } from '@angular/core';
import {IStudent} from "../../../interfaces/IStudent";
import {UserService} from "../../../services/user.service";
import {CAREERS} from "../../../constants/careers";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private userService: UserService, private router: Router) { }
  
  
  careers: string[] = CAREERS.map(career => career.name);
  
  model = { id:'', firstName: '', lastName: '', gender: '', email: '', career: '', password: '' } as IStudent;
  
  genders= ['Masculino', 'Femenino', 'Otros'];
  
  async register() {
    this.userService.add(this.model.id, this.model.firstName, this.model.lastName, this.model.gender, this.model.email, this.model.career, this.model.password)
    .subscribe({
      next: (response: IStudent) => {
        console.log(response);
        this.reset();
        this.router.navigate(['/select-champion/', this.model.id]);
        alert('Usuario registrado con éxito. ¡Bienvenido a la UcuPenca2024!');
      }
    });
  }
  
  private reset(): void {
    this.model.id= '';
    this.model.firstName= '';
    this.model.lastName= '';
    this.model.gender= '';
    this.model.email= '';
    this.model.career= '';
    this.model.password= '';
  }
}