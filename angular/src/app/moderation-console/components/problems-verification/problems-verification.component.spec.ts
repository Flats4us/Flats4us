import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProblemsVerificationComponent } from './problems-verification.component';

describe('ProblemsVerificationComponent', () => {
	let component: ProblemsVerificationComponent;
	let fixture: ComponentFixture<ProblemsVerificationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [ProblemsVerificationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(ProblemsVerificationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
