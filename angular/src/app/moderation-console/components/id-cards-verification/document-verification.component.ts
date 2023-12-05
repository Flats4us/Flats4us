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
import { IUser } from './user.interface';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
	selector: 'app-id-cards-verification',
	templateUrl: './document-verification.component.html',
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
	styleUrls: ['./document-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DocumentVerificationComponent {
	// eslint-disable-next-line @typescript-eslint/ban-ts-comment
	// @ts-ignore
	@ViewChild(MatTable) public table: MatTable<IUser>;
	@ViewChild('enlargedImageTemplate')
	public enlargedImageTemplate!: TemplateRef<never>;
	public userColumnsToDisplay: Map<string, string> = new Map<string, string>([
		['email', 'Email'],
		['university', 'Uczelnia'],
		['studentNumber', 'Nr albumu'],
		['documentNumber', 'Nr dokumentu'],
		['name', 'Imię'],
		['surname', 'Nazwisko'],
		['documentExpireDate', 'Data ważności legitymacji'],
	]);
	private verificationType =
		this.route.snapshot.paramMap.get('verification-type');

	public columnsToDisplayWithExpand = [
		...this.userColumnsToDisplay.keys(),
		'expand',
	];
	public dataSource$: Observable<IUser[]>;
	public expandedElement: IUser | null | undefined;

	constructor(
		private snackBar: MatSnackBar,
		private service: ModerationConsoleService,
		private route: ActivatedRoute,
		private matDialog: MatDialog
	) {
		this.dataSource$ = this.loadData();
	}

	public openImageDialog(imageSrc: string): void {
		this.matDialog.open(this.enlargedImageTemplate, {
			data: { src: imageSrc },
			panelClass: 'custom-dialog-container',
		});
	}

	public loadData(): Observable<any> {
		if (this.verificationType === 'users') {
			return this.service.getUsers();
		} else {
			return this.service.getProperty();
		}
	}

	public reject(email: string) {
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
			duration: 2000,
		});
	}

	public accept(email: string) {
		const param = this.route.snapshot.paramMap.get('verification-type');
		if (param === 'users') {
			this.dataSource$.subscribe(users => {
				const user = users.find(user => user.email === email);
				this.service.acceptUser(user).subscribe(result => {
					// eslint-disable-next-line no-console
					console.log(result);
				});
			});

			this.dataSource$ = this.service.getUsers();
			this.table.renderRows();
			this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
				duration: 2000,
			});
		}
	}
}
