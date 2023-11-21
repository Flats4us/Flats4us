import { ChangeDetectionStrategy, Component } from '@angular/core';

export interface IPeriodicElement {
	email: string;
	studentNumber: string;
	firstName: string;
	lastName: string;
	cardExpirationDate: string;
}

const ELEMENT_DATA: IPeriodicElement[] = [
	{
		email: `jan.kowalski@example.com`,
		studentNumber: 's23993',
		firstName: 'Adam',
		lastName: 'Nowak',
		cardExpirationDate: '31-06-24',
	},
	{
		email: `anna.kwiatkowska@example.com`,
		studentNumber: 's24994',
		firstName: 'Anna',
		lastName: 'Kwiatkowska',
		cardExpirationDate: '31-09-24',
	},
	{
		email: `bartosz.piotrowski@example.com`,
		studentNumber: 's25995',
		firstName: 'Bartek',
		lastName: 'Piotrowski',
		cardExpirationDate: '31-08-24',
	},
	{
		email: `beata.zielonka@example.com`,
		studentNumber: 's26996',
		firstName: 'Beata',
		lastName: 'Zielonka',
		cardExpirationDate: '31-07-24',
	},
	{
		email: `czeslaw.wisniewski@example.com`,
		studentNumber: 's27997',
		firstName: 'Czesław',
		lastName: 'Wiśniewski',
		cardExpirationDate: '31-12-24',
	},
	{
		email: `danuta.kaczmarek@example.com`,
		studentNumber: 's28998',
		firstName: 'Danuta',
		lastName: 'Kaczmarek',
		cardExpirationDate: '31-11-24',
	},
	{
		email: `ewa.lewandowska@example.com`,
		studentNumber: 's29999',
		firstName: 'Ewa',
		lastName: 'Lewandowska',
		cardExpirationDate: '31-05-24',
	},
];

@Component({
	selector: 'app-moderation-console',
	templateUrl: './moderation-console.component.html',
	styleUrls: ['./moderation-console.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ModerationConsoleComponent {
	public displayedColumns: string[] = [
		'email',
		'studentNumber',
		'firstName',
		'lastName',
		'cardExpirationDate',
		'b1',
	];
	public dataSource = ELEMENT_DATA;
}
