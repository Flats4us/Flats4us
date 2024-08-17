import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddRealEstateComponent } from './components/add/add-real-estate.component';
import { RealEstateComponent } from './real-estate.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { RealEstateDetailsComponent } from './components/details/real-estate-details.component';
import { PermissionsGuard } from '@shared/services/permissions.guard';
import { AuthModels } from '@shared/models/auth.models';

const routes: Routes = [
	{
		path: 'add',
		component: AddRealEstateComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.VERIFIED_OWNER],
		},
	},
	{
		path: 'edit/:id',
		component: AddRealEstateComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.VERIFIED_OWNER],
		},
	},
	{
		path: ':user',
		component: RealEstateComponent,
		children: [{ path: ':id', component: RealEstateDetailsComponent }],
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.VERIFIED_OWNER,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.MODERATOR,
			],
		},
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RealEstateRoutingModule {}
