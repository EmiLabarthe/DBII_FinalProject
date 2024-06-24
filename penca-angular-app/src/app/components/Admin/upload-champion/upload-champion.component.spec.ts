import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadChampionComponent } from './upload-champion.component';

describe('UploadChampionComponent', () => {
  let component: UploadChampionComponent;
  let fixture: ComponentFixture<UploadChampionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UploadChampionComponent]
    });
    fixture = TestBed.createComponent(UploadChampionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
