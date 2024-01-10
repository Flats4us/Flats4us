import {
	ChangeDetectionStrategy,
	Component,
	TemplateRef,
	ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { IConversation } from '@shared/models/conversation.models';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-dispute-conversation',
	templateUrl: './dispute-conversation.component.html',
	styleUrls: ['./dispute-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeConversationComponent extends BaseComponent {
	public conversationId$: Observable<string>;
	public messageControl = new FormControl();
	public currentUser = 'Moderator';
	public disputeDescription = '';
	@ViewChild('disputeDescriptionDialog')
	public disputeDescriptionDialog!: TemplateRef<unknown>;

	public messages: IConversation[] = [
		{ sender: 'User1', message: 'Msg' },
		{ sender: 'User2', message: 'Msg..' },
		{ sender: 'Moderator', message: 'Msg' },
	];

	constructor(
		public route: ActivatedRoute,
		private dialog: MatDialog,
		private snackBar: MatSnackBar,
		private router: Router
	) {
		super();
		this.conversationId$ = route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
	}

	public openDisputeDialog(): void {
		this.dialog.open(this.disputeDescriptionDialog, {
			width: '500px',
		});
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

	public onSubmit() {
		const ref = this.snackBar.open('Spór zostaw zamknięty', 'Zamknij', {
			duration: 2000,
		});

		ref
			.afterDismissed()
			.pipe(this.untilDestroyed())
			.subscribe(() => {
				this.router.navigate(['moderation-console/dispute']);
			});
	}
}
