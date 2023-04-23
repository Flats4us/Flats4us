import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-owner-form',
	templateUrl: './owner-form.component.html',
	styleUrls: ['./owner-form.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OwnerFormComponent {
	smokingYes!: boolean;
	smokingNo!: boolean;
	smokingNotImportant!: boolean;
	partingYes!: boolean;
	partingNo!: boolean;
	partingNotImportant!: boolean;
	animalsYes!: boolean;
	animalsNo!: boolean;
	animalsNotImportant!: boolean;
	genderM!: boolean;
	genderW!: boolean;
	genderNotImportant!: boolean;
}
