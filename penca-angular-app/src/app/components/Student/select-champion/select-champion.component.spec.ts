import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectChampionComponent } from './select-champion.component';

describe('SelectChampionComponent', () => {
  let component: SelectChampionComponent;
  let fixture: ComponentFixture<SelectChampionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectChampionComponent]
    });
    fixture = TestBed.createComponent(SelectChampionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
