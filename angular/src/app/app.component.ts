import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
	public title = 'angular';

	constructor(private translate: TranslateService) {
		translate.setDefaultLang('pl');
	  }
	
	  changeLanguage(lang: 'pl' | 'en') {
		this.translate.use(lang);
	  }
}
