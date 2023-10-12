import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { IGroup, IRegionCity } from '../../models/real-estate.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent implements OnInit {
	public addRealEstateForm1: FormGroup = new FormGroup({});
	public addRealEstateForm2: FormGroup = new FormGroup({});
	public addRealEstateForm3: FormGroup = new FormGroup({});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;

	public regionCityArray: IRegionCity[] = [];

	public numberOfPhotos = 0;

	public completed = false;
	public fileName = '';
	public url: any;
	public urls: any[] = [];

	public citiesGroups = this.realEstateService.citiesGroups;
	public districtGroups = this.realEstateService.districtGroups;
	public regions = this.realEstateService.regions;
	public numberOfFloors = this.realEstateService.numberOfFloors;
	public yearOfBuilds = this.realEstateService.yearOfBuilds;
	public properties = this.realEstateService.properties;
	public equipment = this.realEstateService.equipment;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		private realEstateService: RealEstateService
	) {
		this.addRealEstateForm1 = formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			districtsGroup: new FormControl(''),
			street: new FormControl('', Validators.required),
			streetNumber: new FormControl('', Validators.required),
			property: new FormControl('', Validators.required),
		});

		this.addRealEstateForm2 = formBuilder.group({
			area: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			maxResidents: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			equipment: new FormControl(''),
			rooms: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl('', Validators.required),
		});

		this.addRealEstateForm3 = formBuilder.group({
			photos: new FormControl(null, Validators.required),
		});
		this.realEstateService.readCitiesForRegions(
			this.http,
			this.regionCityArray,
			this.citiesGroups
		);
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter((item) => item.toLowerCase().includes(filterValue));
	};

	public onFileSelected(event: any) {
		if (event.target.files) {
			for (let i = 0; i < File.length; i++) {
				const reader = new FileReader();

				reader.readAsDataURL(event.target.files[i]);

				reader.onload = (event: any) => {
					if (this.urls.length <= 10) {
						this.urls.push(event.target.result);
					}
				};
			}
		}
	}

	public showMap() {
		this.router.navigate(['start/map']);
	}

	public saveRealEstate() {
		if (
			this.addRealEstateForm1.valid &&
			this.addRealEstateForm2.valid &&
			this.addRealEstateForm3.valid
		) {
			this.completed = true;
		}
	}

	public addOffer() {
		this.router.navigate(['/']);
	}

	public ngOnInit() {
		this.citiesGroupOptions$ = this.addRealEstateForm1
			.get('citiesGroup')
			?.valueChanges.pipe(
				map((value) => value ?? ''),
				map((value) => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.addRealEstateForm1
			.get('districtsGroup')
			?.valueChanges.pipe(
				map((value) => value ?? ''),
				map((value) => this.filterDistrictsGroup(value))
			);

		this.addRealEstateForm1
			.get('citiesGroup')
			?.valueChanges.subscribe((value) => {
				if (this.districtGroups.find((distr) => distr.whole === value)) {
					this.addRealEstateForm1.get('districtsGroup')?.enable();
				} else {
					this.addRealEstateForm1.get('districtsGroup')?.reset();
				}
			});
		this.addRealEstateForm1.get('regionsGroup')?.valueChanges.subscribe(() => {
			this.addRealEstateForm1.get('citiesGroup')?.reset();
		});
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.citiesGroups
			.map((group) => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.addRealEstateForm1.get('regionsGroup')?.value
			);
	}
	private filterDistrictsGroup(value: string): IGroup[] {
		return this.districtGroups
			.map((group) => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.addRealEstateForm1.get('citiesGroup')?.value
			);
	}

	public formReset() {
		this.addRealEstateForm1.reset();
		this.addRealEstateForm2.reset();
		this.addRealEstateForm3.reset();
		this.urls = [];
	}
}
