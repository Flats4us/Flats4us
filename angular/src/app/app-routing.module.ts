import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: 'auth',
		loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
	},
	{
		path: 'passwordChange',
		loadChildren: () =>
			import('./settings/components/passwordChange/passwordChange.module').then(
				(m) => m.PasswordChangeModule
			),
	},
	{
		path: 'settings',
		loadChildren: () =>
			import('./settings/settings.module').then((m) => m.SettingsModule),
	},
	{
		path: 'emailChange',
		loadChildren: () =>
			import('./settings/components/emailChange/emailChange.module').then(
				(m) => m.EmailChangeModule
			),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
