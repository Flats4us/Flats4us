import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOffertComponent } from './offert/add-offert/add-offert.component';

const routes: Routes = [
	{
		path: 'add-offert',
		component: AddOffertComponent,
	},
];
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)],
})
export class OffertRoutingModule {}
