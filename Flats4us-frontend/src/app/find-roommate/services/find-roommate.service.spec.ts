import { TestBed } from '@angular/core/testing';

import { FindRoommateService } from './find-roommate.service';

describe('RoommateCandidateService', () => {
	let service: FindRoommateService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(FindRoommateService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});
