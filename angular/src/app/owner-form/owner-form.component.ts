import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-owner-form',
	templateUrl: './owner-form.component.html',
	styleUrls: ['./owner-form.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OwnerFormComponent {
	public smokingYes!: boolean;
	public smokingNo!: boolean;
	public smokingNotImportant!: boolean;
  public partingYes!: boolean;
  public partingNo!: boolean;
  public partingNotImportant!: boolean;
  public animalsYes!: boolean;
  public animalsNo!: boolean;
  public animalsNotImportant!: boolean;
  public genderM!: boolean;
  public genderW!: boolean;
  public genderNotImportant!: boolean;
}
