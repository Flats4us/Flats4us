import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmailChangeComponent } from './components/emailChange/emailChange.component';
import { PasswordChangeComponent } from './components/passwordChange/passwordChange.component';
import { StudentSurveyComponent } from './components/student-survey/student-survey.component';

const routes: Routes = [
	{
		path: 'email-change',
		component: EmailChangeComponent,
	},
	{
		path: 'password-change',
		component: PasswordChangeComponent,
	},
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
