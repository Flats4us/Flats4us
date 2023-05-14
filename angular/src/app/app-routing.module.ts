import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: 'auth',
		loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
	},
	{
		path: 'profile',
		loadChildren: () =>
			import('./profile/profile.module').then((m) => m.ProfileModule),
	},
	{
		path: 'settings',
		loadChildren: () =>
			import('./settings/settings.module').then((m) => m.SettingsModule),
	},
	{
		path: 'home',
		loadChildren: () =>
			import('./main-site/main-site.module').then((m) => m.MainSiteModule),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
