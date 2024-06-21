import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '@shared/services/user.service';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-notifications',
	templateUrl: './notifications.component.html',
	styleUrls: ['./notifications.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NotificationsComponent extends BaseComponent implements OnInit {
	public notificationForm: FormGroup = new UntypedFormGroup({
		pushChatConsent: new FormControl<boolean>(false),
		emailChatConsent: new FormControl<boolean>(false),
		pushOtherConsent: new FormControl<boolean>(false),
		emailOtherConsent: new FormControl<boolean>(false),
	});

	constructor(private snackBar: MatSnackBar, private service: UserService) {
		super();
	}

	public save() {
		this.service
			.changeConsents(this.notificationForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(() => {
				this.snackBar.open(
					'Pomyślnie zmieniono ustawienia powiadomień!',
					'Zamknij',
					{
						duration: 10000,
					}
				);
			});
	}

	public ngOnInit(): void {
		this.service
			.getConsents()
			.pipe(this.untilDestroyed())
			.subscribe(consents => this.notificationForm.patchValue(consents));
	}
}
