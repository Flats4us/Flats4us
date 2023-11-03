import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentsComponent } from './rents.component';
import { RentsDescriptionComponent } from './components/description/rents-description.component';

const routes: Routes = [
	{
		path: '',
		component: RentsComponent,
		children: [{ path: ':id', component: RentsDescriptionComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RentsRoutingModule {}
