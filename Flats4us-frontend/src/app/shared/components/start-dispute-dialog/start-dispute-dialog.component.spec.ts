import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartDisputeDialogComponent } from './start-dispute-dialog.component';

describe('StartDisputeDialogComponent', () => {
	let component: StartDisputeDialogComponent;
	let fixture: ComponentFixture<StartDisputeDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [StartDisputeDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(StartDisputeDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
