import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';
import { CreateProfileComponent } from './create/create-profile.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
	},
	{
		path: ':modificationType/:user',
		component: CreateProfileComponent,
	},
	{
		path: ':modificationType/:user/:id',
		component: CreateProfileComponent,
	},
	{
		path: ':id',
		component: ProfileComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
