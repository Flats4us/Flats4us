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
import { IStudentCard } from './IStudentCard';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { Observable } from 'rxjs';

@Component({
	selector: 'app-student-cards-verification',
	templateUrl: './student-cards-verification.component.html',
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
	styleUrls: ['./student-cards-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentCardsVerificationComponent {
	// eslint-disable-next-line @typescript-eslint/ban-ts-comment
	// @ts-ignore
	@ViewChild(MatTable) public table: MatTable<IStudentCard>;

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
	public dataSource$: Observable<IStudentCard[]>;
	public expandedElement: IStudentCard | null | undefined;

	constructor(
		private snackBar: MatSnackBar,
		private service: ModerationConsoleService
	) {
		this.dataSource$ = this.service.getStudentCards();
	}

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
