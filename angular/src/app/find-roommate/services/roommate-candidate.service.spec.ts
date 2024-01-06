import { TestBed } from '@angular/core/testing';

import { RoommateCandidateService } from './roommate-candidate.service';

describe('RoommateCandidateService', () => {
	let service: RoommateCandidateService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(RoommateCandidateService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});
