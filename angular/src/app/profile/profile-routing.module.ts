import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from '@shared/components/survey/survey.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
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
