import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IMessage } from '@shared/models/conversation.models';
import { map, Observable, switchMap } from 'rxjs';
import { IMyProfile } from '@shared/models/user.models';
import { environment } from '../../../../environments/environment.prod';
import { ConversationService } from '../../../conversations/services/conversation.service';
import { UserService } from '@shared/services/user.service';
import { AuthService } from '@shared/services/auth.service';
import { DisputesService } from '../../../disputes/services/disputes.service';
import { IParticipant } from '../../../disputes/models/disputes.models';
import { formatDate, Location } from '@angular/common';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-disputes-conversation',
	templateUrl: './disputes-conversation.component.html',
	styleUrls: ['./disputes-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputesConversationComponent implements OnInit, OnDestroy {
	public messageControl = new FormControl();
	public messages: { groupId: number; user: number; message: string }[] = [];
	public groupChatId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('conversationId') ?? '')
	);
	protected messages$: Observable<IMessage[]> = this.groupChatId$.pipe(
		switchMap(value => this.disputesService.getDisputeMessages(value))
	);
	public currentUser$: Observable<IMyProfile> = this.userService.getMyProfile();
	public participants$: Observable<IParticipant[]> = this.groupChatId$.pipe(
		switchMap(value => this.disputesService.getGroupChats(Number(value))),
		map(groupChat => groupChat.users)
	);
	public baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		public route: ActivatedRoute,
		public conversationService: ConversationService,
		public userService: UserService,
		protected authService: AuthService,
		private disputesService: DisputesService,
		private location: Location
	) {}

	public ngOnInit(): void {
		this.conversationService.startConnection(this.authService.getAuthToken());

		this.conversationService.addReceiveGroupMessageHandler(
			(groupId, user, message) => {
				this.messages.push({ groupId, user, message });
			}
		);
	}

	public ngOnDestroy(): void {
		this.conversationService.stopConnection();
	}

	public onSend(groupId: string, senderId: number): void {
		if (!this.messageControl.value) {
			return;
		}
		this.conversationService.sendGroupMessage(
			Number(groupId),
			this.messageControl.value
		);
		this.messages.push({
			groupId: Number(groupId),
			user: senderId,
			message: this.messageControl.value,
		});

		this.messageControl.reset();
	}

	public getParticipantById(participants: IParticipant[], userId: number) {
		return participants.find(participant => participant.userId === userId);
	}

	public goBack() {
		this.location.back();
	}

	protected readonly formatDate = formatDate;
	protected readonly authModels = AuthModels;
}
