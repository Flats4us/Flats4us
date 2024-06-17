import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IMessage } from '@shared/models/conversation.models';
import { IMyProfile } from '@shared/models/user.models';
import { AuthService } from '@shared/services/auth.service';
import { UserService } from '@shared/services/user.service';
import { map, Observable, of, switchMap } from 'rxjs';

import { environment } from '../../../../environments/environment.prod';
import { ConversationService } from '../../services/conversation.service';
import { formatDate } from '@angular/common';

@Component({
	selector: 'app-conversations-conversation',
	templateUrl: './messages-conversation.component.html',
	styleUrls: ['./messages-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessagesConversationComponent implements OnInit, OnDestroy {
	public messageControl = new FormControl();

	public messages: { user: number; message: string; timestamp: Date }[] = [];
	public conversationId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('conversationId') ?? '')
	);
	protected messages$: Observable<IMessage[]> = this.conversationId$.pipe(
		switchMap(value => {
			return value === 'new'
				? of([])
				: this.conversationService.getMessages(value);
		})
	);
	public currentUser$: Observable<IMyProfile> = this.userService.getMyProfile();
	public participantId$: Observable<string> = this.conversationId$.pipe(
		switchMap(id => this.conversationService.getParticipantId(id))
	);
	public otherUser$ = this.route.paramMap.pipe(
		switchMap(params => {
			const receiverId = params.get('receiverId');
			return receiverId ? of(receiverId) : this.participantId$;
		}),
		switchMap(receiverId => this.userService.getUserById(receiverId))
	);
	public baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		public route: ActivatedRoute,
		public conversationService: ConversationService,
		public userService: UserService,
		private authService: AuthService,
		private router: Router
	) {}

	public ngOnInit(): void {
		this.conversationService.startConnection(this.authService.getAuthToken());

		this.conversationService.addReceivePrivateMessageHandler(
			(user, message, timestamp) => {
				this.messages.push({ user, message, timestamp });
			}
		);
	}

	public ngOnDestroy(): void {
		this.conversationService.stopConnection();
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
			timestamp: new Date(),
		});

		this.messageControl.reset();
	}

	public showProfile(id?: number) {
		this.router.navigate(['profile', 'details', id]);
	}

	protected readonly formatDate = formatDate;
}
