import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModerationConsoleComponent } from './moderation-console.component';
import { DocumentVerificationComponent } from './components/id-cards-verification/document-verification.component';
import { DisputeComponent } from './components/dispute/dispute.component';
import { DisputeConversationComponent } from './components/dispute-conversation/dispute-conversation.component';
import { SurveyComponent } from '../profile/survey/survey.component';

const routes: Routes = [
	{
		path: '',
		component: ModerationConsoleComponent,
		children: [
			{ path: '', pathMatch: 'full', redirectTo: 'verification' },
			{
				path: 'verify/:verification-type',
				component: DocumentVerificationComponent,
			},
			{ path: 'dispute', component: DisputeComponent },
			{ path: 'dispute/:id', component: DisputeConversationComponent },
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ModerationConsoleRoutingModule {}
