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

export interface IPeriodicElement {
	email: string;
	studentNumber: string;
	firstName: string;
	lastName: string;
	cardExpirationDate: string;
	description: string;
}

const ELEMENT_DATA: IPeriodicElement[] = [
	{
		email: `jan.kowalski@example.com`,
		studentNumber: 's23993',
		firstName: 'Adam',
		lastName: 'Nowak',
		cardExpirationDate: '31-06-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `anna.kwiatkowska@example.com`,
		studentNumber: 's24994',
		firstName: 'Anna',
		lastName: 'Kwiatkowska',
		cardExpirationDate: '31-09-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `bartosz.piotrowski@example.com`,
		studentNumber: 's25995',
		firstName: 'Bartek',
		lastName: 'Piotrowski',
		cardExpirationDate: '31-08-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `beata.zielonka@example.com`,
		studentNumber: 's26996',
		firstName: 'Beata',
		lastName: 'Zielonka',
		cardExpirationDate: '31-07-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `czeslaw.wisniewski@example.com`,
		studentNumber: 's27997',
		firstName: 'Czesław',
		lastName: 'Wiśniewski',
		cardExpirationDate: '31-12-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `danuta.kaczmarek@example.com`,
		studentNumber: 's28998',
		firstName: 'Danuta',
		lastName: 'Kaczmarek',
		cardExpirationDate: '31-11-24',
		description: 'Hydrogen is a chemical element',
	},
	{
		email: `ewa.lewandowska@example.com`,
		studentNumber: 's29999',
		firstName: 'Ewa',
		lastName: 'Lewandowska',
		cardExpirationDate: '31-05-24',
		description: 'Hydrogen is a chemical element',
	},
];

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
	constructor(private snackBar: MatSnackBar) {}

	// eslint-disable-next-line @typescript-eslint/ban-ts-comment
	// @ts-ignore
	@ViewChild(MatTable) public table: MatTable<IPeriodicElement>;

	public columnsToDisplay: string[] = [
		'email',
		'studentNumber',
		'firstName',
		'lastName',
		'cardExpirationDate',
	];
	public columnsNames: string[] = [
		'Email',
		'Nr albumu',
		'Imię',
		'Nazwisko',
		'Data ważności legitymacji',
	];
	public columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
	public dataSource = ELEMENT_DATA;
	public expandedElement: IPeriodicElement | null | undefined;

	public reject(email: string) {
		ELEMENT_DATA.splice(
			ELEMENT_DATA.findIndex(a => a.email === email),
			1
		);
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie odrzucony!', 'Zamknij', {
			duration: 2000,
		});
	}

	public accept(email: string) {
		ELEMENT_DATA.splice(
			ELEMENT_DATA.findIndex(a => a.email === email),
			1
		);
		this.table.renderRows();
		this.snackBar.open('Profil został pomyślnie zaakceptowany!', 'Zamknij', {
			duration: 2000,
		});
	}
}
