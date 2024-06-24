import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeDisputeStatusDialogComponent } from './change-dispute-status-dialog.component';

describe('ChangeDisputeStatusDialogComponent', () => {
	let component: ChangeDisputeStatusDialogComponent;
	let fixture: ComponentFixture<ChangeDisputeStatusDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [ChangeDisputeStatusDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(ChangeDisputeStatusDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
