import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IMessage } from '@shared/models/conversation.models';
import { map, Observable, of, switchMap } from 'rxjs';
import { ConversationService } from '@shared/services/conversation.service';
import { UserService } from '@shared/services/user.service';
import { IMyProfile } from '@shared/models/user.models';
import { AuthService } from '@shared/services/auth.service';
import { environment } from '../../../../environments/environment.prod';

@Component({
	selector: 'app-messages-conversation',
	templateUrl: './messages-conversation.component.html',
	styleUrls: ['./messages-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessagesConversationComponent implements OnInit, OnDestroy {
	public messageControl = new FormControl();

	public messages: { user: number; message: string }[] = [];
	public conversationId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('conversationId') ?? '')
	);
	protected messages$: Observable<IMessage[]> = this.conversationId$.pipe(
		switchMap(value => {
			return value
				? this.conversationService.getMessages(parseInt(value))
				: of([]);
		})
	);
	public currentUser$: Observable<IMyProfile> = this.userService.getMyProfile();
	public participantId$: Observable<string> = this.conversationId$.pipe(
		switchMap(id => this.conversationService.getParticipantId(parseInt(id)))
	);
	public otherUser$ = this.route.paramMap.pipe(
		switchMap(params => {
			const receiverId = params.get('receiverId');
			return receiverId ? of(receiverId) : this.participantId$;
		}),
		switchMap(receiverId => this.userService.getUserById(parseInt(receiverId)))
	);
	public baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		public route: ActivatedRoute,
		public conversationService: ConversationService,
		public userService: UserService,
		private authService: AuthService
	) {}

	public ngOnDestroy(): void {
		this.conversationService.stopConnection();
	}

	public ngOnInit(): void {
		this.conversationService.startConnection(this.authService.getAuthToken());

		this.conversationService.addReceivePrivateMessageHandler((user, message) => {
			this.messages.push({ user, message });
		});
	}

	public onSend(senderId: number, receiverId: number): void {
		if (!this.messageControl.value) {
			return;
		}
		this.conversationService.sendPrivateMessage(
			receiverId,
			this.messageControl.value
		);
		this.messages.push({
			user: senderId,
			message: this.messageControl.value,
		});

		this.messageControl.reset();
	}
}
