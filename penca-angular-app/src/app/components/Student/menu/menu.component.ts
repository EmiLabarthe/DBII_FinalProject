import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent {

  constructor(private route: ActivatedRoute) { }

  studentId: string | null = null;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.studentId = params.get('studentId');
    });
  }
}
