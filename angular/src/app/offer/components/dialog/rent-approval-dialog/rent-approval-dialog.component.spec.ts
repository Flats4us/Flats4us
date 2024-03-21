import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentApprovalDialogComponent } from './rent-approval-dialog.component';

describe('RentApprovalDialogComponent', () => {
	let component: RentApprovalDialogComponent;
	let fixture: ComponentFixture<RentApprovalDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RentApprovalDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RentApprovalDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
