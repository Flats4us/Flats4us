import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentsComponent } from './rents.component';
import { RentsDetailsComponent } from './components/details/rents-details.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

const routes: Routes = [
	{
		path: ':user',
		component: RentsComponent,
		children: [
			{ path: ':id', component: RentsDetailsComponent },
			{ path: '**', component: NotFoundComponent },
		],
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RentsRoutingModule {}
