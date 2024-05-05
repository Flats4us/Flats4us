import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModerationConsoleComponent } from './moderation-console.component';
import { PropertiesVerificationComponent } from './components/properties-verification/properties-verification.component';
import { UsersVerificationComponent } from './components/users-verification/users-verification.component';
import { ProblemsVerificationComponent } from './components/problems-verification/problems-verification.component';
import { DisputeComponent } from './components/dispute/dispute.component';
import { DisputesConversationComponent } from '@shared/components/disputes-conversation/disputes-conversation.component';

const routes: Routes = [
	{
		path: '',
		component: ModerationConsoleComponent,
		children: [
			{
				path: 'verify',
				children: [
					{ path: 'problems', component: ProblemsVerificationComponent },
					{ path: 'properties', component: PropertiesVerificationComponent },
					{ path: 'users', component: UsersVerificationComponent },
				],
			},
			{
				path: 'disputes',
				component: DisputeComponent,
				children: [{ path: ':id', component: DisputesConversationComponent }],
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ModerationConsoleRoutingModule {}
