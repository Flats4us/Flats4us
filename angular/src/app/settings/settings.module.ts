import { NgModule } from '@angular/core';
import { SettingsRoutingModule } from './settings-routing.module';
import { EmailChangeModule } from './components/emailChange/emailChange.module';
import { PasswordChangeModule } from './components/passwordChange/passwordChange.module';
import { CommonModule } from '@angular/common';

@NgModule({
	declarations: [],
	imports: [
		CommonModule,
		SettingsRoutingModule,
		EmailChangeModule,
		PasswordChangeModule,
	],
})
export class SettingsModule {}
