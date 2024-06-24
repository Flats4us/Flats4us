import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOfferComponent } from './components/add-offer/add-offer.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { OfferComponent } from './offer.component';
import { OfferDetailsComponent } from './components/details/offer-details.component';
import { WatchedOffersComponent } from './components/watched-offers/watched-offers.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'add' },
	{
		path: 'add',
		component: AddOfferComponent,
	},
	{
		path: 'watched',
		component: WatchedOffersComponent,
	},
	{
		path: ':user',
		component: OfferComponent,
		children: [{ path: ':id', component: OfferDetailsComponent }],
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
