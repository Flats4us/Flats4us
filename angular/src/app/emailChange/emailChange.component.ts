import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
	selector: 'app-email-change',
	templateUrl: './emailChange.component.html',
	styleUrls: ['./emailChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent {
  hide = true;
}
