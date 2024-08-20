import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { PermissionsGuard } from '@shared/services/permissions.guard';
import { AuthModels } from '@shared/models/auth.models';
import { AuthGuard } from '@shared/services/auth.guard';

const routes: Routes = [
	{
		path: 'login',
		component: LoginComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.NOT_LOGGED_IN],
		},
	},
	{
		path: 'register',
		component: RegisterComponent,
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [AuthModels.NOT_LOGGED_IN],
		},
	},
	{
		path: 'forgot-password',
		component: ForgotPasswordComponent,
		canActivate: [AuthGuard],
	},
	{
		path: 'reset-password',
		component: ResetPasswordComponent,
		canActivate: [AuthGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AuthRoutingModule {}
