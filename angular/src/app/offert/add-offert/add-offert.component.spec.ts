import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOffertComponent } from './add-offert.component';

describe('AddOffertComponent', () => {
  let component: AddOffertComponent;
  let fixture: ComponentFixture<AddOffertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddOffertComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddOffertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
