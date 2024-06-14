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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { ModerationConsoleService } from './services/moderation-console.service';
import { PropertiesVerificationComponent } from './components/properties-verification/properties-verification.component';
import { UsersVerificationComponent } from './components/users-verification/users-verification.component';
import { ProblemsVerificationComponent } from './components/problems-verification/problems-verification.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { AddInterventionDialogComponent } from './components/add-intervention-dialog/add-intervention-dialog.component';
import { ChangeDisputeStatusDialogComponent } from './components/change-dispute-status-dialog/change-dispute-status-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
	declarations: [
		ModerationConsoleComponent,
		DisputeComponent,
		PropertiesVerificationComponent,
		UsersVerificationComponent,
		ProblemsVerificationComponent,
		AddInterventionDialogComponent,
		ChangeDisputeStatusDialogComponent,
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
		MatPaginatorModule,
		MatOptionModule,
		MatSelectModule,
		MatTooltipModule,
		MatSortModule,
	],
	providers: [ModerationConsoleService, MatSnackBarModule],
	exports: [ModerationConsoleComponent],
})
export class ModerationConsoleModule {}
