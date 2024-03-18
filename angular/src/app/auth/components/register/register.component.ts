import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnInit,
} from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent implements OnInit {
	@Input()
	public createProfileMode = false;

	public hidePassword = true;
	public hideConfirmPasword = true;

	public registerForm: FormGroup = this.formDir.form;

	constructor(private formDir: FormGroupDirective) {}

	public ngOnInit() {
		this.registerForm = this.formDir.form;
	}
}
