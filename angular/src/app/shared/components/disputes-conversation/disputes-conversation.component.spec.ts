import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisputesConversationComponent } from './disputes-conversation.component';

describe('DisputesConversationComponent', () => {
	let component: DisputesConversationComponent;
	let fixture: ComponentFixture<DisputesConversationComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [DisputesConversationComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(DisputesConversationComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
