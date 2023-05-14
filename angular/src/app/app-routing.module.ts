import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NotFoundComponent } from './shared/components/not-found/not-found.component';

const routes: Routes = [
	{
		path: 'auth',
		loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
	},
	{
		path: 'start',
		loadChildren: () => import('./start/start.module').then((m) => m.StartModule),
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
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
