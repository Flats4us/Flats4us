import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferPromotionDialogComponent } from './offer-promotion-dialog.component';

describe('OfferPromotionDialogComponent', () => {
	let component: OfferPromotionDialogComponent;
	let fixture: ComponentFixture<OfferPromotionDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [OfferPromotionDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(OfferPromotionDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
