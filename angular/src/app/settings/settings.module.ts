import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailChangeComponent } from './components/emailChange/emailChange.component';
import { PasswordChangeComponent } from './components/passwordChange/passwordChange.component';
import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [],
	imports: [
		CommonModule,
		RouterModule.forChild([
			{ path: 'emailChange', component: EmailChangeComponent },
		]),
		RouterModule.forChild([
			{ path: 'passwordChange', component: PasswordChangeComponent },
		]),
	],
})
export class SettingsModule {}
