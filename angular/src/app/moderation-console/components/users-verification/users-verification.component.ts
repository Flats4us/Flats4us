import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-properties-verification',
	templateUrl: './users-verification.component.html',
	styleUrls: ['./users-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UsersVerificationComponent extends BaseComponent {
	public users$: Observable<IUser[]> = this.service.getUsers();
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar
	) {
		super();
	}

	public acceptUser(userId: number) {
		this.service
			.acceptUser(userId)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
					duration: 2000,
				})
			);
	}

	public rejectUser(userId: number) {
		this.service
			.rejectUser(userId)
			.pipe(this.untilDestroyed())
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
					duration: 2000,
				})
			);
	}
}
