import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { HelpCenterComponent } from './help-center.component';
import { TranslateModule } from '@ngx-translate/core';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
	declarations: [HelpCenterComponent],
	imports: [
		CommonModule,
		MatCardModule,
		TranslateModule,
		RouterLink,
		MatButtonModule,
	],
})
export class HelpCenterModule {}
