import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReusableProfileComponent } from './reusable.component';

describe('EditProfileComponent', () => {
	let component: ReusableProfileComponent;
	let fixture: ComponentFixture<ReusableProfileComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [ReusableProfileComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(ReusableProfileComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
