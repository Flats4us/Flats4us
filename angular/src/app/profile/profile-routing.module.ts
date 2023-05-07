import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { StudentSurveyComponent } from './student-survey/student-survey.component';

const routes: Routes = [
	{
		path: 'student-survey',
		component: StudentSurveyComponent,
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
