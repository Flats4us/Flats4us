import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddRealEstateComponent } from './components/add/add-real-estate.component';
import { RealEstateComponent } from './real-estate.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { RealEstateDetailsComponent } from './components/details/real-estate-details.component';

const routes: Routes = [
	// {
	// 	path: '',
	// 	pathMatch: 'full',
	// 	component: RealEstateComponent,
	// },
	{
		path: 'add',
		component: AddRealEstateComponent,
	},
	{
		path: 'owner',
		component: RealEstateComponent,
		children: [
			{ path: ':id', component: RealEstateDetailsComponent },
			{ path: '**', component: NotFoundComponent },
		],
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RealEstateRoutingModule {}
