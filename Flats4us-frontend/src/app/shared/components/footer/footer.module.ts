import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FooterComponent } from './footer.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [FooterComponent],
	imports: [CommonModule, TranslateModule],
	exports: [FooterComponent],
})
export class FooterModule {}
