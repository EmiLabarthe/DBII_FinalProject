import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IFixtureItem } from '../../../interfaces/IFixtureItem';
import { FixtureService } from 'src/app/services/fixture.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-fixture',
  templateUrl: './fixture.component.html',
  styleUrls: ['./fixture.component.css']
})
export class FixtureComponent implements OnInit {

  fixture: IFixtureItem[] | undefined;
  studentId: string | null = null;

  constructor(private route: ActivatedRoute, private fixtureService: FixtureService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.studentId = params.get('studentId');
    });
    
    this.fixtureService.getFixture().subscribe(
      (data: IFixtureItem[]) => {
        this.fixture = data;
      },
      (error) => {
        console.error('Error fetching fixture', error)
      }
    )
  }

}
