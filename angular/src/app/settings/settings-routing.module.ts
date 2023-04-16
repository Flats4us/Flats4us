import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmailChangeComponent } from './components/emailChange/emailChange.component';
import { PasswordChangeComponent } from './components/passwordChange/passwordChange.component';

const routes: Routes = [
	{
		path: 'emailChange',
		component: EmailChangeComponent,
	},
	{
		path: 'passwordChange',
		component: PasswordChangeComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SettingsRoutingModule {}
