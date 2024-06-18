import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { IProperty } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { environment } from '../../../../environments/environment';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PageEvent } from '@angular/material/paginator';
import { formatDate } from '@angular/common';

@Component({
	selector: 'app-properties-verification',
	templateUrl: './properties-verification.component.html',
	styleUrls: ['./properties-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PropertiesVerificationComponent extends BaseComponent {
	public pageEvent = new PageEvent();
	public pageSize = 4;
	public pageIndex = 0;
	public properties$: Observable<IProperty[]> = this.service.getProperties(
		this.pageSize,
		this.pageIndex
	);
	public propertiesCount$: Observable<number> =
		this.service.getPropertiesCount();

	protected baseUrl = environment.apiUrl.replace('/api', '');

	public displayedColumns: string[] = [
		'ownerName',
		'ownerEmail',
		'address',
		'document',
		'status',
		'verificationOrRejectionDate',
		'actions',
	];

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar,
		private cdr: ChangeDetectorRef
	) {
		super();
	}

	public acceptProperty(propertyId: number) {
		this.properties$ = this.service.acceptProperty(propertyId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open(
					'Nieruchomość została pomyślnie zaakceptowana!',
					'Zamknij',
					{
						duration: 10000,
					}
				);
				return this.service.getProperties(this.pageSize, this.pageIndex);
			})
		);
	}

	public rejectProperty(propertyId: number) {
		this.properties$ = this.service.rejectProperty(propertyId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open('Nieruchomość została pomyślnie odrzucona!', 'Zamknij', {
					duration: 10000,
				});
				return this.service.getProperties(this.pageSize, this.pageIndex);
			})
		);
	}

	public changePage(e: PageEvent) {
		this.pageEvent = e;
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.properties$ = this.service.getProperties(this.pageSize, this.pageIndex);
		this.cdr.markForCheck();
	}

	protected readonly formatDate = formatDate;
}
