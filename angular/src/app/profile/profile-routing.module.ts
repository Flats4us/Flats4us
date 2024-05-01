import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { AuthGuard } from '@shared/services/auth.guard';

import { SurveyComponent } from '../shared/components/survey/survey.component';
import { AddOpinionComponent } from './add-opinion/add-opinion.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { ProfileComponent } from './profile.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'my' },
	{
		path: 'details/:id',
		component: ProfileComponent,
	},
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
		canActivate: [AuthGuard],
	},
	{
		path: ':modificationType',
		component: CreateProfileComponent,
	},
	{
		path: ':modificationType/:user',
		component: CreateProfileComponent,
	},
	{
		path: ':id/opinion/add',
		component: AddOpinionComponent,
		canActivate: [AuthGuard],
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
