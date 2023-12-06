import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ModerationConsoleRoutingModule } from './moderation-console-routing.module';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ModerationConsoleComponent } from './moderation-console.component';
import { DisputeComponent } from './components/dispute/dispute.component';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink, RouterOutlet } from '@angular/router';
import { DisputeConversationComponent } from './components/dispute-conversation/dispute-conversation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { VerificationComponent } from './components/verification/verification.component';
import { ModerationConsoleService } from './services/moderation-console.service';

@NgModule({
	providers: [ModerationConsoleService],
	declarations: [
		ModerationConsoleComponent,
		VerificationComponent,
		DisputeComponent,
		DisputeConversationComponent,
	],
	imports: [
		CommonModule,
		ModerationConsoleRoutingModule,
		MatTableModule,
		MatButtonModule,
		MatCardModule,
		MatIconModule,
		MatSnackBarModule,
		MatTabsModule,
		RouterLink,
		RouterOutlet,
		FormsModule,
		MatFormFieldModule,
		MatInputModule,
		ReactiveFormsModule,
		MatDialogModule,
		MatMenuModule,
	],
	exports: [ModerationConsoleComponent],
})
export class ModerationConsoleModule {}
