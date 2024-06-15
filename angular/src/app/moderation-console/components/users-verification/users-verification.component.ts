import {
	AfterViewInit,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { IUser } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { formatDate } from '@angular/common';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-properties-verification',
	templateUrl: './users-verification.component.html',
	styleUrls: ['./users-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UsersVerificationComponent
	extends BaseComponent
	implements AfterViewInit
{
	public pageSize = 4;
	public pageIndex = 0;
	public pageEvent = new PageEvent();
	public users$: Observable<IUser[]> = this.service.getUsers(
		this.pageSize,
		this.pageIndex
	);
	public usersCount$: Observable<number> = this.service.getUsersCount();
	protected baseUrl = environment.apiUrl.replace('/api', '');
	public displayedColumns: string[] = [
		'email',
		'university',
		'studentNumber',
		'documentNumber',
		'name',
		'surname',
		'document',
		'status',
		'verificationOrRejectionDate',
		'actions',
	];
	protected readonly formatDate = formatDate;

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar,
		private matPaginatorIntl: MatPaginatorIntl,
		private cdr: ChangeDetectorRef,
		private translate: TranslateService
	) {
		super();
	}
	public ngAfterViewInit(): void {
		this.matPaginatorIntl.itemsPerPageLabel = this.translate.instant(
			'Moderation-console.reports-website'
		);
	}

	public acceptUser(userId: number) {
		this.users$ = this.service.acceptUser(userId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open(
					this.translate.instant('Moderation-console.info2'),
					this.translate.instant('close'),
					{
						duration: 10000,
					}
				);
				return this.service.getUsers(this.pageSize, this.pageIndex);
			})
		);
	}

	public rejectUser(userId: number) {
		this.users$ = this.service.rejectUser(userId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open(
					this.translate.instant('Moderation-console.info3'),
					this.translate.instant('close'),
					{
						duration: 10000,
					}
				);
				return this.service.getUsers(this.pageSize, this.pageIndex);
			})
		);
	}

	public changePage(e: PageEvent) {
		this.pageEvent = e;
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.users$ = this.service.getUsers(this.pageSize, this.pageIndex);
		this.cdr.markForCheck();
	}
}
