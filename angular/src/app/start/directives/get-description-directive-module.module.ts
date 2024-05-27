import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { GetDescriptionDirective } from './get-description.directive';

@NgModule({
	declarations: [GetDescriptionDirective],
	imports: [CommonModule, TranslateModule],
	exports: [GetDescriptionDirective],
	providers: [TranslateService],
})
export class GetDescriptionDirectiveModule {}
