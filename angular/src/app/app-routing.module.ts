import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NotFoundComponent } from './shared/components/not-found/not-found.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'start' },
	{
		path: 'auth',
		loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
	},
	{
		path: 'start',
		loadChildren: () => import('./start/start.module').then(m => m.StartModule),
	},
	{
		path: 'profile',
		loadChildren: () =>
			import('./profile/profile.module').then(m => m.ProfileModule),
	},
	{
		path: 'settings',
		loadChildren: () =>
			import('./settings/settings.module').then(m => m.SettingsModule),
	},
	{
		path: 'messages',
		loadChildren: () =>
			import('./messages/messages.module').then(m => m.MessagesModule),
	},
	{
		path: 'find-roommate',
		loadChildren: () =>
			import('./find-roommate/find-roommate.module').then(
				m => m.FindRoommateModule
			),
	},
	{
		path: 'offer',
		loadChildren: () => import('./offer/offer.module').then(m => m.OfferModule),
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
