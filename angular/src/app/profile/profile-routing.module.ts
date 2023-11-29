import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { EditProfileComponent } from './edit/edit-profile.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
	},
	{
		path: 'edit/:user/:id',
		component: EditProfileComponent,
	},
	{
		path: 'create/:user',
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
