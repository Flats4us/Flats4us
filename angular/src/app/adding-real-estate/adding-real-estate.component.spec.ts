import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingRealEstateComponent } from './adding-real-estate.component';

describe('AddingRealEstateComponent', () => {
	let component: AddingRealEstateComponent;
	let fixture: ComponentFixture<AddingRealEstateComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [AddingRealEstateComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(AddingRealEstateComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
