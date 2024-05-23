import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnInit,
} from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	FormGroupDirective,
	Validators,
} from '@angular/forms';
import { IQuestionsData, TypeName } from '../../models/survey.models';
import { Observable, of, switchMap, tap } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SurveyService } from '../../services/survey.service';
import {
	IGroup,
	IRegionCity,
} from 'src/app/real-estate/models/real-estate.models';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { BaseComponent } from '../base/base.component';

@Component({
	selector: 'app-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent extends BaseComponent implements OnInit {
	@Input() public offerForm: FormGroup;
	@Input()
	public createProfileMode = false;

	public surveyForm = new FormGroup({});
	public formToAdd = new FormGroup({});
	public questions$: Observable<IQuestionsData[]>;
	public typeName: typeof TypeName = TypeName;
	public showMoreQuestions = false;
	public numberOfQuestions = 0;
	private regionCityArray: IRegionCity[] = [];
	public citiesGroupOptions$: Observable<IGroup[]>;
	public minAgeControl = new FormControl(null, [
		Validators.min(18),
		Validators.max(150),
	]);
	public maxAgeControl = new FormControl(null, [
		Validators.min(18),
		Validators.max(150),
	]);

	constructor(
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private service: SurveyService,
		private formDir: FormGroupDirective,
		private realEstateService: RealEstateService
	) {
		super();
		this.offerForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$ = this.getQuestions().pipe(
			tap(questions => {
				{
					this.getFormControls(questions);
					this.numberOfQuestions = questions.length;
				}
			})
		);
		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(this.untilDestroyed())
			.subscribe();
		this.citiesGroupOptions$ = of(this.realEstateService.citiesGroups);
	}

	public ngOnInit() {
		if (this.createProfileMode) {
			this.surveyForm = this.formDir.form;
			this.surveyForm.addControl('survey', this.formToAdd);
		}
	}

	public getQuestions(): Observable<IQuestionsData[]> {
		return this.route.paramMap.pipe(
			switchMap((params: ParamMap) => {
				const surveyType = params.get('survey-type');
				if (surveyType === 'student' || this.createProfileMode) {
					return this.service.getStudentQuestions();
				} else {
					return this.service.getOwnerQuestions();
				}
			})
		);
	}

	public getFormControls(questions: IQuestionsData[]) {
		questions.forEach(question => {
			switch (question.name) {
				case 'minRoommateAge':
					this.offerForm.addControl(question.name, this.minAgeControl);
					this.formToAdd.addControl(question.name, this.minAgeControl);
					break;
				case 'maxRoommateAge':
					this.offerForm.addControl(question.name, this.maxAgeControl);
					this.formToAdd.addControl(question.name, this.maxAgeControl);
					break;
				case 'smokingAllowed':
					this.offerForm.addControl(question.name, new FormControl(false));
					this.formToAdd.addControl(question.name, new FormControl(false));
					break;
				case 'partiesAllowed':
					this.offerForm.addControl(question.name, new FormControl(false));
					this.formToAdd.addControl(question.name, new FormControl(false));
					break;
				case 'animalsAllowed':
					this.offerForm.addControl(question.name, new FormControl(false));
					this.formToAdd.addControl(question.name, new FormControl(false));
					break;
				case 'gender':
					this.offerForm.addControl(question.name, new FormControl(2));
					this.formToAdd.addControl(question.name, new FormControl(2));
					break;
				default:
					this.offerForm.addControl(question.name, new FormControl(null));
					this.formToAdd.addControl(question.name, new FormControl(null));
					break;
			}
		});
	}
}
