import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyComponent } from '../shared/components/survey/survey.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { AuthGuard } from '@shared/services/auth.guard';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { DetailsProfileComponent } from './details/details-profile.component';
import { ProfileComponent } from './profile.component';
import { AddOpinionComponent } from './add-opinion/add-opinion.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'my' },
	{
		path: 'my',
		component: ProfileComponent,
	},
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
		path: 'details/student/:id',
		component: DetailsProfileComponent,
		canActivate: [AuthGuard],
	},
	{
		path: ':id/opinion/add',
		component: AddOpinionComponent,
		canActivate: [AuthGuard],
	},
	{ path: '**', component: NotFoundComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ProfileRoutingModule {}
