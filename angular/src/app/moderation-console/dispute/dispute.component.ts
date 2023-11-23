import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'app-dispute',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './dispute.component.html',
	styleUrls: ['./dispute.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeComponent {}
