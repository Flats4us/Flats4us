import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentsComponent } from './rents.component';

const routes: Routes = [
	{
		path: '',
		pathMatch: 'full',
		component: RentsComponent,
	},
	{
		path: '',
		component: RentsComponent,
		children: [{ path: ':id', component: RentsComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RentsRoutingModule {}
