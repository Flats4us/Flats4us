import {
	ChangeDetectionStrategy,
	Component,
	TemplateRef,
	ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { map, Observable } from 'rxjs';
import { IConversation } from '@shared/models/conversation.models';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatSnackBar } from '@angular/material/snack-bar';

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
	public disputeDescription = '';
	@ViewChild('disputeDescriptionDialog')
	public disputeDescriptionDialog!: TemplateRef<any>;

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

	public OnSubmit() {
		this.snackBar.open('Spór zostaw zamknięty', 'Zamknij', {
			duration: 2000,
		});
		this.snackBar._openedSnackBarRef?.afterDismissed().subscribe(() => {
			this.router.navigate(['moderation-console/dispute']);
		});
	}
}
