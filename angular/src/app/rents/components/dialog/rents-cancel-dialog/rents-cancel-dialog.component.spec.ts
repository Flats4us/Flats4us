import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentsCancelDialogComponent } from './rents-cancel-dialog.component';

describe('RentsCancelDialogComponent', () => {
	let component: RentsCancelDialogComponent;
	let fixture: ComponentFixture<RentsCancelDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RentsCancelDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RentsCancelDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
