import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-rents',
  templateUrl: './rents.component.html',
  styleUrls: ['./rents.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class RentsComponent {

  // public addRealEstateFormAddressData;

  // constructor(
	// 	private formBuilder: FormBuilder,
	// ) {
	// 	this.addRealEstateFormAddressData = formBuilder.group({
	// 		regionsGroup: new FormControl('', Validators.required),
	// 	});
//}
}
