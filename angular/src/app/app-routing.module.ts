import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { AuthGuard } from '@shared/services/auth.guard';

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
		canActivate: [AuthGuard],
	},
	{
		path: 'settings',
		loadChildren: () =>
			import('./settings/settings.module').then(m => m.SettingsModule),
		canActivate: [AuthGuard],
	},
	{
		path: 'messages',
		loadChildren: () =>
			import('./messages/messages.module').then(m => m.MessagesModule),
		canActivate: [AuthGuard],
	},
	{
		path: 'disputes',
		loadChildren: () =>
			import('./disputes/disputes.module').then(m => m.DisputesModule),
		canActivate: [AuthGuard],
	},
	{
		path: 'real-estate',
		loadChildren: () =>
			import('./real-estate/real-estate.module').then(m => m.RealEstateModule),
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
	{
		path: 'rents',
		loadChildren: () => import('./rents/rents.module').then(m => m.RentsModule),
	},
	{
		path: 'calendar',
		loadComponent: () =>
			import('./calendar/calendar.component').then(c => c.CalendarComponent),
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
