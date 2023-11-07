import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentsComponent } from './rents.component';
import { RentsDetailsComponent } from './components/details/rents-details.component';

const routes: Routes = [
	{
		path: '',
		component: RentsComponent,
		children: [{ path: ':id', component: RentsDetailsComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RentsRoutingModule {}
