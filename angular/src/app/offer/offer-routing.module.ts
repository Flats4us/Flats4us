import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOfferComponent } from './add-offer/add-offer.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: '/start' },
	{
		path: 'add-offer',
		component: AddOfferComponent,
	},
];
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)],
})
export class OfferRoutingModule {}
