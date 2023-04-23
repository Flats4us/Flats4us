import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StudentSurveyComponent } from './components/student-survey/student-survey.component';

const routes: Routes = [
	{
		path: 'student-survey',
		component: StudentSurveyComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SettingsRoutingModule {}
