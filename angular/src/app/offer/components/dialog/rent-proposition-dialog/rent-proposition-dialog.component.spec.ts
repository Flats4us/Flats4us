import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentPropositionDialogComponent } from './rent-proposition-dialog.component';

describe('RentPropositionDialogComponent', () => {
	let component: RentPropositionDialogComponent;
	let fixture: ComponentFixture<RentPropositionDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RentPropositionDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RentPropositionDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
