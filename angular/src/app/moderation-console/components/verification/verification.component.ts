import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import { IProperty, IUser } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subject, takeUntil } from 'rxjs';
import { environment } from '../../../../environments/environment.prod';
import {
	ChangeDetectionStrategy,
	Component,
	ViewChild,
	TemplateRef,
} from '@angular/core';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';

@Component({
	selector: 'app-verification',
	templateUrl: './verification.component.html',
	animations: [
		trigger('detailExpand', [
			state('collapsed', style({ height: '0px', minHeight: '0' })),
			state('expanded', style({ height: '*' })),
			transition(
				'expanded <=> collapsed',
				animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
			),
		]),
	],
	styleUrls: ['./verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VerificationComponent {
	@ViewChild(MatTable) public table!: MatTable<IUser>;
	@ViewChild('enlargedImageTemplate')
	public enlargedImageTemplate!: TemplateRef<never>;

	private readonly unsubscribe$: Subject<void> = new Subject();
	protected baseUrl = environment.apiUrl.replace('/api', '');
	public verificationType =
		this.route.snapshot.paramMap.get('verification-type');

	public userColumnsToDisplay: Map<string, string> = new Map<string, string>([
		['email', 'Email'],
		['university', 'Uczelnia'],
		['studentNumber', 'Nr albumu'],
		['documentNumber', 'Nr dokumentu'],
		['name', 'Imię'],
		['surname', 'Nazwisko'],
		['documentExpireDate', 'Data ważności legitymacji'],
	]);

	public propertyColumnsToDisplay: Map<string, string> = new Map<string, string>(
		[
			['ownerName', 'Właściciel'],
			['ownerEmail', 'Adres email właściciela'],
			['address', 'Addres nieruchomości'],
		]
	);

	public columnsToDisplay: Map<string, string> =
		this.verificationType == 'users'
			? this.userColumnsToDisplay
			: this.propertyColumnsToDisplay;

	public columnsToDisplayWithExpand = [
		...this.columnsToDisplay.keys(),
		'expand',
	];

	public usersDataSource$: Observable<IUser[]> = this.service.getUsers();
	public propertiesDataSource$: Observable<IProperty[]> =
		this.service.getProperties();
	public dataSource$: Observable<unknown[]> =
		this.verificationType === 'users'
			? this.usersDataSource$
			: this.propertiesDataSource$;

	public expandedElement: IUser | null | undefined;

	constructor(
		private snackBar: MatSnackBar,
		private service: ModerationConsoleService,
		private route: ActivatedRoute,
		private matDialog: MatDialog,
		private router: Router
	) {
		this.router.routeReuseStrategy.shouldReuseRoute = () => false;
	}

	public openImageDialog(imageSrc: string): void {
		this.matDialog.open(this.enlargedImageTemplate, {
			data: { src: imageSrc },
			panelClass: 'custom-dialog-container',
		});
	}

	public rejectUser(userId: number) {
		this.service
			.rejectUser(userId)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() =>
				this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
					duration: 2000,
				})
			);
		this.dataSource$ = this.service.getUsers();
		this.table.renderRows();
	}

	public acceptUser(userId: number) {
		this.service.acceptUser(userId).subscribe(() =>
			this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
				duration: 2000,
			})
		);
		this.dataSource$ = this.service.getUsers();
		this.table.renderRows();
	}

	public rejectProperty(propertyId: number) {
		this.service.rejectProperty(propertyId).subscribe(() =>
			this.snackBar.open('Nieruchomość została pomyślnie odrzucona!', 'Zamknij', {
				duration: 2000,
			})
		);
		this.dataSource$ = this.service.getProperties();
		this.table.renderRows();
	}

	public acceptProperty(propertyId: number) {
		this.service.acceptProperty(propertyId).subscribe(() =>
			this.snackBar.open(
				'Nieruchomość została pomyślnie zaakceptowana!',
				'Zamknij',
				{
					duration: 2000,
				}
			)
		);
		this.dataSource$ = this.service.getProperties();
		this.table.renderRows();
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
