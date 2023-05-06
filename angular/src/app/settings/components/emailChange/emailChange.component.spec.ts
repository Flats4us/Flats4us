import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailChangeComponent } from './emailChange.component';

describe('emailChangeComponent', () => {
	let component: EmailChangeComponent;
	let fixture: ComponentFixture<EmailChangeComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [EmailChangeComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(EmailChangeComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
