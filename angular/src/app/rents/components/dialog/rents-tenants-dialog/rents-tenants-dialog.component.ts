import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RentsService } from '../../../services/rents.service';
import { IRent } from '../../../models/rents.models';
import { statusName } from '../../../statusName';
import {
	MatChipEditedEvent,
	MatChipInputEvent,
	MatChipsModule,
} from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'app-rents-tenants-dialog',
	templateUrl: './rents-tenants-dialog.component.html',
	styleUrls: ['./rents-tenants-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
	standalone: true,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		ReactiveFormsModule,
		MatChipsModule,
		MatIconModule,
		CommonModule,
	],
	providers: [RentsService],
})
export class RentsTenantsDialogComponent {
	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public addOnBlur = true;
	public tenantsCtrl = new FormControl('');
	public tenants: string[] = [];
	public statusName: typeof statusName = statusName;
	constructor(
		public rentsService: RentsService,
		@Inject(MAT_DIALOG_DATA) public data: IRent
	) {}

	public onYesClick() {
		return;
	}
	public add(
		event: MatChipInputEvent,
		items: string[],
		formControl: FormControl
	): void {
		const value = (event.value || '').trim();
		if (value && !items.includes(value.trim())) {
			items.push(value);
		}
		event.chipInput.clear();

		formControl.setValue(null);
	}

	public remove(item: string, items: string[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);
		}
	}

	public edit(tenant: string, event: MatChipEditedEvent) {
		const value = event.value.trim();
		if (!value) {
			this.remove(tenant, this.tenants);
			return;
		}
		const index = this.tenants.indexOf(tenant);
		if (index >= 0) {
			this.tenants[index] = value;
		}
	}
}
