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
import { IProperty, IUser } from '../../Models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CdkTableDataSourceInput } from '@angular/cdk/table';

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

	public columnsToDisplay: Map<string, string> =
		this.verificationType == 'users'
			? new Map<string, string>([
					['email', 'Email'],
					['university', 'Uczelnia'],
					['studentNumber', 'Nr albumu'],
					['documentNumber', 'Nr dokumentu'],
					['name', 'Imię'],
					['surname', 'Nazwisko'],
					['documentExpireDate', 'Data ważności legitymacji'],
			  ])
			: new Map<string, string>([
					['propertyType', 'Typ nieruchomości'],
					['ownerName', 'Właściciel'],
					['ownerEmail', 'Adres email właściciela'],
					['address', 'Addres nieruchomości'],
					['propertyType', 'Typ nieruchomości'],
					['creationDate', 'Data utworzenia'],
			  ]);

	public columnsToDisplayWithExpand = [
		...this.columnsToDisplay.keys(),
		'expand',
	];

	public usersDataSource$: CdkTableDataSourceInput<IUser>;
	public propertiesDataSource$: CdkTableDataSourceInput<IProperty>;
	public dataSource$: CdkTableDataSourceInput<any>;

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
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
			duration: 2000,
		});
	}

	public accept(email: string) {
		if (this.verificationType === 'users') {
			/*this.dataSource$.subscribe(users => {
				const user = users.find(user => user.email === email);
				this.service.acceptUser(user);
			});*/

			this.dataSource$ = this.service.getUsers();
			this.table.renderRows();
			this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
				duration: 2000,
			});
		}
	}
}
