import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerSurveyComponent } from './owner-survey.component';

describe('StudentSurveyComponent', () => {
	let component: OwnerSurveyComponent;
	let fixture: ComponentFixture<OwnerSurveyComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [OwnerSurveyComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(OwnerSurveyComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
