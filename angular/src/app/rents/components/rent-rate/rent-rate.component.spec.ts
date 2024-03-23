import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentRateComponent } from './rent-rate.component';

describe('RentRateComponent', () => {
  let component: RentRateComponent;
  let fixture: ComponentFixture<RentRateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ RentRateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
