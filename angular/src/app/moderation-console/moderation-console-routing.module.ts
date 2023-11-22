import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModerationConsoleComponent } from './moderation-console.component';
import { StudentCardsVerificationComponent } from './components/student-cards-verification/student-cards-verification.component';

const routes: Routes = [
	{
		path: '',
		component: ModerationConsoleComponent,
		children: [
			{ path: '', pathMatch: 'full', redirectTo: 'verification' },
			{ path: 'verification', component: StudentCardsVerificationComponent },
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ModerationConsoleRoutingModule {}
