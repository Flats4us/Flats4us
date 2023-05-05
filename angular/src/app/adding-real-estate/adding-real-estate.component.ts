import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-adding-real-estate',
	templateUrl: './adding-real-estate.component.html',
	styleUrls: ['./adding-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddingRealEstateComponent {
	public selected: string = '';
}
