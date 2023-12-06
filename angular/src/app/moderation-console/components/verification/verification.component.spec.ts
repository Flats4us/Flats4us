import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerificationComponent } from './verification.component';

describe('StudentCardsVerificationComponent', () => {
	let component: VerificationComponent;
	let fixture: ComponentFixture<VerificationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [VerificationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(VerificationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
