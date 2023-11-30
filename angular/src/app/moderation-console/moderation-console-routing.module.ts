import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModerationConsoleComponent } from './moderation-console.component';
import { StudentCardsVerificationComponent } from './components/student-cards-verification/student-cards-verification.component';
import { DisputeComponent } from './components/dispute/dispute.component';
import { DisputeConversationComponent } from './components/dispute-conversation/dispute-conversation.component';

const routes: Routes = [
	{
		path: '',
		component: ModerationConsoleComponent,
		children: [
			{ path: '', pathMatch: 'full', redirectTo: 'verification' },
			{ path: 'verification', component: StudentCardsVerificationComponent },
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
