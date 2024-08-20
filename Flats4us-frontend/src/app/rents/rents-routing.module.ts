import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RentsComponent } from './rents.component';
import { RentsDetailsComponent } from './components/details/rents-details.component';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { AuthModels } from '@shared/models/auth.models';
import { PermissionsGuard } from '@shared/services/permissions.guard';

const routes: Routes = [
	{
		path: ':user',
		component: RentsComponent,
		children: [{ path: ':id', component: RentsDetailsComponent }],
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.VERIFIED_STUDENT,
				AuthModels.VERIFIED_OWNER,
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
export class RentsRoutingModule {}
