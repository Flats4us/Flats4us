import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent {
	public changeAdress = false;

	public fileName = '';
	public url = '../../../assets/candidate.webp';

	public hobbies: string[] = [
		'Muzyka',
		'Sport',
		'Filmy',
		'Podróże',
		'Sztuka',
		'Nauka',
		'Książki',
		'Kulinaria',
		'Moda',
		'Gry',
		'Piwo',
		'Kulturystka',
	];

	public firstFormGroup = this.formBuilder.group({
		firstCtrl: [''],
	});
	public secondFormGroup = this.formBuilder.group({
		secondCtrl: [''],
	});

	public isLinear = false;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private changeDetectorRef: ChangeDetectorRef
	) {}

	public changeAddressData() {
		this.changeAdress ? (this.changeAdress = false) : (this.changeAdress = true);
	}
	public changeEmail() {
		this.router.navigate(['settings/email-change']);
	}
	public changePassword() {
		this.router.navigate(['settings/password-change']);
	}

	public changeHobbies($event: any) {
		return;
	}

	public submitForm() {
		return;
	}

	public onFileSelected(event: Event) {
		const formData = new FormData();
		const fileEvent = (event.target as HTMLInputElement).files;

		if (!fileEvent) {
			return;
		}
		const file: File = fileEvent[0];
		const fileType = file.type;
		if (fileType.match(/image\/*/) == null) {
			return;
		}
		this.fileName = file.name;
		formData.append(this.fileName, file);
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			this.url = <string>reader.result;
			this.changeDetectorRef.detectChanges();
		};
	}
}
