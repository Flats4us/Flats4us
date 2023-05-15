import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { NotificationsComponent } from './notifications.component';

@NgModule({
	declarations: [NotificationsComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatSlideToggleModule,
		FormsModule,
		ReactiveFormsModule,
	],
})
export class NotificationsModule {}
