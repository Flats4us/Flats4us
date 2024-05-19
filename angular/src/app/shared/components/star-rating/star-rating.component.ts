import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
	selector: 'app-star-rating',
	standalone: true,
	imports: [CommonModule, MatIconModule],
	templateUrl: './star-rating.component.html',
	styleUrls: ['./star-rating.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StarRatingComponent {
	@Input() public avgRating = 0;
	public starsScale: number[] = [1, 2, 3, 4, 5];
}
