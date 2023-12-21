import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModerationConsoleComponent } from './moderation-console.component';
import { VerificationComponent } from './components/verification/verification.component';
import { DisputeComponent } from './components/dispute/dispute.component';
import { DisputeConversationComponent } from './components/dispute-conversation/dispute-conversation.component';

const routes: Routes = [
	{
		path: '',
		component: ModerationConsoleComponent,
		children: [
			{ path: '', pathMatch: 'full', redirectTo: 'verify/user' },
			{
				path: 'verify/:verification-type',
				component: VerificationComponent,
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
