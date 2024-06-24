import { TestBed } from '@angular/core/testing';

import { ModerationConsoleService } from './moderation-console.service';

describe('ModerationConsoleService', () => {
	let service: ModerationConsoleService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(ModerationConsoleService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});
