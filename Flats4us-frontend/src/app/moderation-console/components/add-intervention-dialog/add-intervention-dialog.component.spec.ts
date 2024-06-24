import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInterventionDialogComponent } from './add-intervention-dialog.component';

describe('AddInterventionDialogComponent', () => {
	let component: AddInterventionDialogComponent;
	let fixture: ComponentFixture<AddInterventionDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [AddInterventionDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(AddInterventionDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
