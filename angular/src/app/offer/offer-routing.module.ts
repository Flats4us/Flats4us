import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOfferComponent } from './components/add-offer/add-offer.component';
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
];
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)],
})
export class OfferRoutingModule {}
