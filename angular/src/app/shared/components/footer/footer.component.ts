import { ChangeDetectionStrategy, Component } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Component({
	selector: 'app-footer',
	templateUrl: './footer.component.html',
	styleUrls: ['./footer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FooterComponent {
	public version = `${environment.commitDate} ${environment.commitHash}`;
}
