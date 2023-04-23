import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainSiteComponent } from './main-site/main-site/main-site.component';
import { OwnerFormComponent } from './owner-form/owner-form.component';

const routes: Routes = [
	{ path: 'start', component: MainSiteComponent },
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
		path: 'ownerForm',
		component: OwnerFormComponent,
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
