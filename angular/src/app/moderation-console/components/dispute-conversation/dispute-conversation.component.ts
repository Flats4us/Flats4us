import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
	FormControl,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { map, Observable } from 'rxjs';
import { IConversation } from '@shared/models/conversation.models';
import { ActivatedRoute } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';

@Component({
	selector: 'app-dispute-conversation',
	standalone: true,
	imports: [
		CommonModule,
		FormsModule,
		MatFormFieldModule,
		MatIconModule,
		MatInputModule,
		ReactiveFormsModule,
		MatButtonModule,
		MatDialogModule,
		MatMenuModule,
	],
	templateUrl: './dispute-conversation.component.html',
	styleUrls: ['./dispute-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeConversationComponent {
	public conversationId$: Observable<string>;
	public messageControl = new FormControl();
	public currentUser = 'Moderator';

	public messages: IConversation[] = [
		{ sender: 'User1', message: 'Msg' },
		{ sender: 'User2', message: 'Msg..' },
		{ sender: 'Moderator', message: 'Msg' },
	];

	constructor(public route: ActivatedRoute) {
		this.conversationId$ = route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
	}

	public onSend(): void {
		if (!this.messageControl.value) {
			return;
		}

		this.messages.push({
			sender: this.currentUser,
			message: this.messageControl.value,
		});
		this.messageControl.reset();
	}
}
