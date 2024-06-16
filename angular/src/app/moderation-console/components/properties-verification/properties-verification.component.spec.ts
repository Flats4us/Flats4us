import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesVerificationComponent } from './properties-verification.component';

describe('PropertyVerificationComponent', () => {
	let component: PropertiesVerificationComponent;
	let fixture: ComponentFixture<PropertiesVerificationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [PropertiesVerificationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(PropertiesVerificationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
