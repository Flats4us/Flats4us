import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddRealEstateComponent } from './components/add/add-real-estate.component';
import { RealEstateComponent } from './real-estate.component';

const routes: Routes = [
	{
		path: '',
		pathMatch: 'full',
		component: RealEstateComponent,
	},
	{
		path: 'add',
		component: AddRealEstateComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RealEstateRoutingModule {}
