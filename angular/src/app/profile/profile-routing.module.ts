import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';
import { EditOwnerProfileComponent } from './owner/edit/edit-profile.component';
import { CreateOwnerProfileComponent } from './owner/create/create-profile.component';
import { EditStudentProfileComponent } from './student/edit/edit-profile.component';
import { CreateStudentProfileComponent } from './student/create/create-profile.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
	},
	{
		path: 'student/edit/:id',
		component: EditStudentProfileComponent,
	},
	{
		path: 'owner/edit/:id',
		component: EditOwnerProfileComponent,
	},
	{
		path: 'student/create',
		component: CreateStudentProfileComponent,
	},
	{
		path: 'owner/create',
		component: CreateOwnerProfileComponent,
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
