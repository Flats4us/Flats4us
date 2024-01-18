import { ChangeDetectionStrategy, Component } from '@angular/core';
import { map, Observable } from 'rxjs';
import { IStudent } from '../../models/roommate-candidate.models';
import { FindRoommateService } from '../../services/find-roommate.service';
import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-roommate-candidate',
	templateUrl: './roommate-candidate.component.html',
	styleUrls: ['./roommate-candidate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RoommateCandidateComponent extends BaseComponent {
	protected iterator = 0;

	public students$: Observable<IStudent[]> = this.service.getStudents();

	public student$: Observable<IStudent> = this.students$.pipe(
		map(students => students[this.iterator])
	);

	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(private service: FindRoommateService) {
		super();
	}

	private updateStudent() {
		this.iterator += 1;
		if (this.iterator >= 5) {
			this.students$ = this.service.getStudents();
			this.iterator = 0;
		}
		this.student$ = this.students$.pipe(map(students => students[this.iterator]));
	}

	public accept(id: number) {
		this.updateStudent();
		this.service
			.accept(id, { decision: true })
			.pipe(this.untilDestroyed())
			.subscribe();
	}

	public reject(id: number) {
		this.updateStudent();
		this.service
			.accept(id, { decision: false })
			.pipe(this.untilDestroyed())
			.subscribe();
	}
}
