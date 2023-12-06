import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisputeConversationComponent } from './dispute-conversation.component';

describe('DisputeConversationComponent', () => {
	let component: DisputeConversationComponent;
	let fixture: ComponentFixture<DisputeConversationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [DisputeConversationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(DisputeConversationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
