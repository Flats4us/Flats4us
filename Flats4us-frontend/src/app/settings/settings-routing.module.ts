import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmailChangeComponent } from './components/email-change/email-change.component';
import { HelpCenterComponent } from './components/help-center/help-center.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { PasswordChangeComponent } from './components/password-change/password-change.component';
import { SettingsComponent } from './settings.component';

const routes: Routes = [
	{
		path: '',
		component: SettingsComponent,
		children: [
			{ path: '', pathMatch: 'full', redirectTo: 'notifications' },
			{ path: 'notifications', component: NotificationsComponent },
			{
				path: 'email-change',
				component: EmailChangeComponent,
			},
			{
				path: 'password-change',
				component: PasswordChangeComponent,
			},
			{ path: 'help-center', component: HelpCenterComponent },
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SettingsRoutingModule {}
