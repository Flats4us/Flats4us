import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ThemeService } from '@shared/services/theme.service';
import { environment } from 'src/environments/environment.prod';
import { BaseComponent } from '../base/base.component';

@Component({
	selector: 'app-footer',
	templateUrl: './footer.component.html',
	styleUrls: ['./footer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FooterComponent extends BaseComponent {
	public version = `${environment.commitDate} ${environment.commitHash}`;
	public logoUrl$ = this.themeService.getLogoUrl();
	constructor(private themeService: ThemeService) {
		super();
	}
}
