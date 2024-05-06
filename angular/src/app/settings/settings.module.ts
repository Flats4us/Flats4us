import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { EmailChangeModule } from './components/emailChange/emailChange.module';
import { HelpCenterModule } from './components/help-center/help-center.module';
import { NotificationsModule } from './components/notifications/notifications.module';
import { PasswordChangeModule } from './components/password-change/password-change.module';
import { SettingsRoutingModule } from './settings-routing.module';
import { SettingsComponent } from './settings.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [SettingsComponent],
	imports: [
		CommonModule,
		SettingsRoutingModule,
		NotificationsModule,
		EmailChangeModule,
		PasswordChangeModule,
		HelpCenterModule,
		MatCardModule,
		TranslateModule
	],
})
export class SettingsModule {}
