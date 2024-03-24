import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IStudent } from '../../models/roommate-candidate.models';
import { FindRoommateService } from '../../services/find-roommate.service';
import { environment } from '../../../../environments/environment.prod';

@Component({
	selector: 'app-conversations',
	templateUrl: './conversations.component.html',
	styleUrls: ['./conversations.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConversationsComponent {
	public matches$: Observable<IStudent[]> = this.service.getMatches();
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(private service: FindRoommateService) {}
}
