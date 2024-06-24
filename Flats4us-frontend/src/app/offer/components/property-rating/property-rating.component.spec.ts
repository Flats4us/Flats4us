import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyRatingComponent } from './property-rating.component';

describe('PropertyRatingComponent', () => {
	let component: PropertyRatingComponent;
	let fixture: ComponentFixture<PropertyRatingComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [PropertyRatingComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(PropertyRatingComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
