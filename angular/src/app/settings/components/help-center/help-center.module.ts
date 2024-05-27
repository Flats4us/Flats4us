import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { HelpCenterComponent } from './help-center.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [HelpCenterComponent],
	imports: [CommonModule, MatCardModule, TranslateModule],
})
export class HelpCenterModule {}
