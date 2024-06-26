import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferMapComponent } from './offer-map.component';

describe('OfferMapComponent', () => {
	let component: OfferMapComponent;
	let fixture: ComponentFixture<OfferMapComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [OfferMapComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(OfferMapComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
