import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';
import { EditProfileComponent } from './edit/edit-profile.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
	},
	{
		path: 'edit/:id',
		component: EditProfileComponent,
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
