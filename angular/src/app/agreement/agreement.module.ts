import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgreementComponent } from './agreement.component';
import { AgreementRoutingModule } from './agreement-routing.module';

@NgModule({
	declarations: [AgreementComponent],
	imports: [CommonModule, AgreementRoutingModule],
})
export class AgreementModule {}
