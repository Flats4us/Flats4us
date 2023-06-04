import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';

const routes: Routes = [
	{
		path: 'student-survey',
		component: SurveyComponent,
	},
	{
		path: 'owner-survey',
		component: SurveyComponent,
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
