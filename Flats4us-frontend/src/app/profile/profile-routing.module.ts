import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { AuthGuard } from '@shared/services/auth.guard';

import { SurveyComponent } from '../shared/components/survey/survey.component';
import { AddOpinionComponent } from './add-opinion/add-opinion.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { ProfileComponent } from './profile.component';
import { PermissionsGuard } from '@shared/services/permissions.guard';
import { AuthModels } from '@shared/models/auth.models';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'details/my' },
	{
		path: 'details/:id',
		component: ProfileComponent,
		canActivate: [AuthGuard, PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.UNVERIFIED_STUDENT,
				AuthModels.UNVERIFIED_OWNER,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.VERIFIED_OWNER,
				AuthModels.MODERATOR,
			],
		},
	},
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
		canActivate: [AuthGuard, PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.VERIFIED_OWNER,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.NOT_LOGGED_IN,
			],
		},
	},
	{
		path: ':modificationType',
		component: CreateProfileComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.UNVERIFIED_STUDENT,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.UNVERIFIED_OWNER,
				AuthModels.VERIFIED_OWNER,
				AuthModels.MODERATOR,
				AuthModels.NOT_LOGGED_IN,
			],
		},
	},
	{
		path: ':modificationType/:user',
		component: CreateProfileComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.VERIFIED_STUDENT,
				AuthModels.VERIFIED_OWNER,
				AuthModels.MODERATOR,
				AuthModels.NOT_LOGGED_IN,
			],
		},
	},
	{
		path: ':id/opinion/add',
		component: AddOpinionComponent,
		canActivate: [AuthGuard, PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.VERIFIED_STUDENT,
				AuthModels.VERIFIED_OWNER,
			],
		},
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
