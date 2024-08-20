import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOfferComponent } from './components/add-offer/add-offer.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { OfferComponent } from './offer.component';
import { OfferDetailsComponent } from './components/details/offer-details.component';
import { WatchedOffersComponent } from './components/watched-offers/watched-offers.component';
import { AuthGuard } from '@shared/services/auth.guard';
import { AuthModels } from '@shared/models/auth.models';
import { PermissionsGuard } from '@shared/services/permissions.guard';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'add' },
	{
		path: 'add',
		component: AddOfferComponent,
		canActivate: [AuthGuard, PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.VERIFIED_OWNER],
		},
	},
	{
		path: 'watched',
		component: WatchedOffersComponent,
		canActivate: [AuthGuard, PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.VERIFIED_STUDENT],
		},
	},
	{
		path: ':user',
		component: OfferComponent,
		children: [{ path: ':id', component: OfferDetailsComponent }],
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.UNVERIFIED_STUDENT,
				AuthModels.UNVERIFIED_OWNER,
				AuthModels.VERIFIED_OWNER,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.MODERATOR,
				AuthModels.NOT_LOGGED_IN,
			],
		},
	},
	{
		path: 'details',
		component: OfferComponent,
		children: [{ path: ':id', component: OfferDetailsComponent }],
	},
	{ path: '**', component: NotFoundComponent },
];
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)],
})
export class OfferRoutingModule {}
