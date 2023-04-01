import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailChangeComponent } from './emailChange.component';
import { EmailChangeRoutingModule } from './emailChange-routing.module';
@NgModule({
	declarations: [EmailChangeComponent],
	imports: [CommonModule, EmailChangeRoutingModule],
	exports: [EmailChangeComponent],
})
export class EmailChangeModule {}
