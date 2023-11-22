import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ModerationConsoleRoutingModule } from './moderation-console-routing.module';
import { ModerationConsoleComponent } from './moderation-console.component';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
	declarations: [ModerationConsoleComponent],
	imports: [
		CommonModule,
		ModerationConsoleRoutingModule,
		MatTableModule,
		MatButtonModule,
		MatCardModule,
		MatIconModule,
		MatSnackBarModule,
	],
	exports: [ModerationConsoleComponent],
})
export class ModerationConsoleModule {}
