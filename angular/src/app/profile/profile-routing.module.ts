import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from '../shared/components/survey/survey.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { AuthGuard } from '@shared/services/auth.guard';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
		canActivate: [AuthGuard],
	},
	{
		path: ':modificationType/:user',
		component: CreateProfileComponent,
	},
	{
		path: ':modificationType',
		component: CreateProfileComponent,
	},
	{
		path: ':modificationType/:user/:id',
		component: CreateProfileComponent,
		canActivate: [AuthGuard],
	},
	{
		path: ':id',
		component: ProfileComponent,
		canActivate: [AuthGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
