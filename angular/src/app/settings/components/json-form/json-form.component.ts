import {
	ChangeDetectionStrategy,
	OnChanges,
	Component,
	Input,
	SimpleChanges,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

export interface IRoot {
	questions: IQuestion[];
}

export interface IQuestion {
	id: number;
	title: string;
	content: string;
	type: IType;
}

export interface IJsonFormData {
	controls: IQuestion[];
}

export interface IType {
	type_name: string;
	answers: any;
}

@Component({
	selector: 'app-json-form',
	templateUrl: './json-form.component.html',
	styleUrls: ['./json-form.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class JsonFormComponent implements OnChanges {
	@Input() public jsonFormData!: IJsonFormData;

	public myForm: FormGroup = this.fb.group({});

	constructor(private fb: FormBuilder) {}

	public createForm(questions: IQuestion[]) {
		for (const question of questions) {
			this.myForm.addControl(question.title, this.fb.control(question.content));
		}
	}

	public onSubmit() {
		// eslint-disable-next-line no-console
		console.log('Form valid: ', this.myForm.valid);
		// eslint-disable-next-line no-console
		console.log('Form values: ', this.myForm.value);
	}

	public ngOnChanges(changes: SimpleChanges): void {
		if (!changes['jsonFormData'].firstChange) {
			this.createForm(this.jsonFormData.controls);
		}
	}
}
