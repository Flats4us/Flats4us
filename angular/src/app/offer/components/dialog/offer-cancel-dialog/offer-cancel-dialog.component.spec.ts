import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferCancelDialogComponent } from './offer-cancel-dialog.component';

describe('OfferCancelDialogComponent', () => {
  let component: OfferCancelDialogComponent;
  let fixture: ComponentFixture<OfferCancelDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ OfferCancelDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferCancelDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
