import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModerationConsoleComponent } from './moderation-console.component';

describe('ModerationConsoleComponent', () => {
	let component: ModerationConsoleComponent;
	let fixture: ComponentFixture<ModerationConsoleComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [ModerationConsoleComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(ModerationConsoleComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
