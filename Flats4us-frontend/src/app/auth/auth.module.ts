import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component';
import { LoginModule } from './components/login/login.module';
import { RegisterModule } from './components/register/register.module';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

@NgModule({
	declarations: [AuthComponent],
	imports: [
		CommonModule,
		AuthRoutingModule,
		LoginModule,
		RegisterModule,
		ResetPasswordComponent,
	],
})
export class AuthModule {}
