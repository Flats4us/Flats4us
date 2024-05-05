import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IProperty } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-properties-verification',
	templateUrl: './properties-verification.component.html',
	styleUrls: ['./properties-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PropertiesVerificationComponent extends BaseComponent {
	public properties$: Observable<IProperty[]> = this.service.getProperties();
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar
	) {
		super();
	}

	public acceptProperty(propertyId: number) {
		this.service
			.acceptProperty(propertyId)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open(
					'Nieruchomość została pomyślnie zaakceptowana!',
					'Zamknij',
					{
						duration: 2000,
					}
				)
			);
	}

	public rejectProperty(propertyId: number) {
		this.service
			.rejectProperty(propertyId)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Nieruchomość została pomyślnie odrzucona!', 'Zamknij', {
					duration: 2000,
				})
			);
	}
}
