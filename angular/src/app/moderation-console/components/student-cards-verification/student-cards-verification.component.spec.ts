import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentCardsVerificationComponent } from './student-cards-verification.component';

describe('StudentCardsVerificationComponent', () => {
	let component: StudentCardsVerificationComponent;
	let fixture: ComponentFixture<StudentCardsVerificationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [StudentCardsVerificationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(StudentCardsVerificationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
