import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-real-estate',
	templateUrl: './real-estate.component.html',
	styleUrls: ['./real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateComponent {}
