import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOfferComponent } from './components/add-offer/add-offer.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'add' },
	{
		path: 'add',
		component: AddOfferComponent,
	},
];
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)],
})
export class OfferRoutingModule {}
