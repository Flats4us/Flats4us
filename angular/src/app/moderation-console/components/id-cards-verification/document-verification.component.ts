import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { IUser } from './document.interface';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

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

	public columnsToDisplay: Map<string, string> = new Map<string, string>([
		['email', 'Email'],
		['studentNumber', 'Nr albumu'],
		['firstName', 'Imię'],
		['lastName', 'Nazwisko'],
		['cardExpirationDate', 'Data ważności legitymacji'],
	]);

	public columnsToDisplayWithExpand = [
		...this.columnsToDisplay.keys(),
		'expand',
	];
	public dataSource$: Observable<IUser[]>;
	public expandedElement: IUser | null | undefined;

	constructor(
		private snackBar: MatSnackBar,
		private service: ModerationConsoleService,
		private route: ActivatedRoute
	) {
		this.dataSource$ = /*this.loadData();*/ this.service.getStudentCards();
	}

	/*public loadData(): Observable<IStudentCard[]> {
		const param = this.route.snapshot.paramMap.get('');
		if (param === 'verification') {
			return this.service.getStudentCards();
		}
		return this.service.getStudentCards();
	}*/

	public reject(email: string) {
		/*ELEMENT_DATA.splice(
			ELEMENT_DATA.findIndex(a => a.email === email),
			1
		);*/
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
			duration: 2000,
		});
	}

	public accept(email: string) {
		/*ELEMENT_DATA.splice(
			ELEMENT_DATA.findIndex(a => a.email === email),
			1
		);*/
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
			duration: 2000,
		});
	}
}
