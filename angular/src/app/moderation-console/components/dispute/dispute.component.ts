import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
	selector: 'app-dispute',
	standalone: true,
	imports: [
		CommonModule,
		MatButtonModule,
		MatIconModule,
		MatTabsModule,
		RouterLink,
		RouterOutlet,
	],
	templateUrl: './dispute.component.html',
	styleUrls: ['./dispute.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeComponent {}
