import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';
import { UserService } from '@shared/services/user.service';
import { map, switchMap } from 'rxjs';

import { environment } from '../../../environments/environment.prod';
import { ProfileService } from '../services/profile.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-add-opinion',
	templateUrl: './add-opinion.component.html',
	styleUrls: ['./add-opinion.component.scss'],
	standalone: true,
	imports: [
		CommonModule,
		MatCardModule,
		MatIconModule,
		FormsModule,
		MatButtonModule,
		MatCheckboxModule,
		MatInputModule,
		MatListModule,
		ReactiveFormsModule,
		MatSnackBarModule,
		TranslateModule,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOpinionComponent extends BaseComponent {
	public starsScale: number[] = [1, 2, 3, 4, 5];
	public opinionForm: FormGroup;
	public profileId$ = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	public user$ = this.profileId$.pipe(
		switchMap(id => this.userService.getUserById(id))
	);
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private profileService: ProfileService,
		private userService: UserService,
		public route: ActivatedRoute,
		private translate: TranslateService
	) {
		super();
		this.opinionForm = this.fb.group({
			helpful: false,
			cooperative: false,
			tidy: false,
			friendly: false,
			respectingPrivacy: false,
			communicative: false,
			unfair: false,
			lackOfHygiene: false,
			untidy: false,
			conflicting: false,
			noisy: false,
			notFollowingTheArrangements: false,
			rating: [0, Validators.min(1)],
			description: [''],
		});
	}

	public countStar(star: number) {
		this.opinionForm.controls['rating'].setValue(star);
	}

	public onSubmit(profileId: number) {
		this.profileService
			.addOpinion(profileId, this.opinionForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open(
					this.translate.instant('Profile-add-opinion.info1'),
					this.translate.instant('Profile-add-opinion.close'),
					{
						duration: 10000,
					}
				)
			);
	}
}
