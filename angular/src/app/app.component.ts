import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocaleService } from '@shared/services/locale.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
	public title = 'angular';

	constructor(
		private translate: TranslateService,
		private localeService: LocaleService
	) {
		translate.setDefaultLang('pl');
		this.localeService.initLocale('pl');
	}
}
