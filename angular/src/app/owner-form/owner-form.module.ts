import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';

import { OwnerFormComponent } from './owner-form.component';
import { OwnerFormRoutingModule } from './owner-form-routing.module';

@NgModule({
	declarations: [OwnerFormComponent],
	imports: [CommonModule, MatRadioModule, MatCardModule, OwnerFormRoutingModule],
	exports: [OwnerFormComponent],
})
export class OwnerFormModule {}
