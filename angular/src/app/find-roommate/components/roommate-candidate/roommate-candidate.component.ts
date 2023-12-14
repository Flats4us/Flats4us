import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IStudent } from '../../models/roommate-candidate.models';

@Component({
	selector: 'app-roommate-candidate',
	templateUrl: './roommate-candidate.component.html',
	styleUrls: ['./roommate-candidate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RoommateCandidateComponent {
	public starsScale: number[] = [1, 2, 3, 4, 5];

	public dataSource$: Observable<IStudent> = this.getData();

	constructor(private http: HttpClient) {}

	private getData(): Observable<IStudent> {
		return this.http.get<IStudent>('../../assets/student.json');
	}
}
