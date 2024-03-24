import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { SurveyComponent } from '../shared/components/survey/survey.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { AuthGuard } from '@shared/services/auth.guard';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';

const routes: Routes = [
	{
		path: 'survey/:survey-type',
		component: SurveyComponent,
		canActivate: [AuthGuard],
	},
	{
		path: ':modificationType',
		component: CreateProfileComponent,
	},
	{
		path: ':modificationType/:user',
		component: CreateProfileComponent,
	},
	{
		path: 'my/:id',
		component: ProfileComponent,
		canActivate: [AuthGuard],
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
