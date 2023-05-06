import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StudentSurveyComponent } from './components/student-survey/student-survey.component';
import { EmailChangeComponent } from './components/emailChange/emailChange.component';
import { PasswordChangeComponent } from './components/passwordChange/passwordChange.component';

const routes: Routes = [
	{
		path: 'student-survey',
		component: StudentSurveyComponent,
	},
	{
		path: 'email-change',
		component: EmailChangeComponent,
	},
	{
		path: 'password-change',
		component: PasswordChangeComponent,
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class SettingsRoutingModule {}
