import {
	ChangeDetectionStrategy,
	Component,
	ViewChild,
	TemplateRef,
} from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { IProperty, IUser } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, switchMap } from 'rxjs';

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

	public usersDataSource$: Observable<IUser[]>;
	public propertiesDataSource$: Observable<IProperty[]>;
	public dataSource$: Observable<unknown[]>;

	public expandedElement: IUser | null | undefined;

	constructor(
		private snackBar: MatSnackBar,
		private service: ModerationConsoleService,
		private route: ActivatedRoute,
		private matDialog: MatDialog,
		private router: Router
	) {
		this.usersDataSource$ = this.service.getUsers();
		this.propertiesDataSource$ = this.service.getProperty();

		this.router.routeReuseStrategy.shouldReuseRoute = () => false;
		this.dataSource$ =
			this.verificationType === 'users'
				? this.usersDataSource$
				: this.propertiesDataSource$;
	}

	public openImageDialog(imageSrc: string): void {
		this.matDialog.open(this.enlargedImageTemplate, {
			data: { src: imageSrc },
			panelClass: 'custom-dialog-container',
		});
	}

	public reject(email: string) {
		if (this.verificationType === 'users') {
			this.usersDataSource$
				.pipe(
					switchMap(users => users?.filter(user => user.email === email)),
					switchMap(user => this.service.rejectUser(user))
				)
				.subscribe(() =>
					this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
						duration: 2000,
					})
				);
		}
	}

	public accept(email: string) {
		if (this.verificationType === 'users') {
			this.usersDataSource$
				.pipe(
					switchMap(users => users?.filter(user => user.email === email)),
					switchMap(user => this.service.acceptUser(user))
				)
				.subscribe(() =>
					this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
						duration: 2000,
					})
				);
		}
	}
}
