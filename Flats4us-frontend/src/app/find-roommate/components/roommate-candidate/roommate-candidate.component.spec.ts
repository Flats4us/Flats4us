import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoommateCandidateComponent } from './roommate-candidate.component';

describe('RoommateCandidateComponent', () => {
	let component: RoommateCandidateComponent;
	let fixture: ComponentFixture<RoommateCandidateComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [RoommateCandidateComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RoommateCandidateComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
