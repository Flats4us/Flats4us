import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LocaleService } from '@shared/services/locale.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
	public title = 'angular';

	constructor(private localeService: LocaleService) {
		this.localeService.initLocale('pl');
	}
}
