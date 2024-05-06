import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { NotificationsComponent } from './notifications.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [NotificationsComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatSlideToggleModule,
		FormsModule,
		ReactiveFormsModule,
		TranslateModule
	],
})
export class NotificationsModule {}
