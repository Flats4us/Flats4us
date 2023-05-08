import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { HelpCenterComponent } from './help-center.component';

@NgModule({
	declarations: [HelpCenterComponent],
	imports: [CommonModule, MatCardModule],
})
export class HelpCenterModule {}
