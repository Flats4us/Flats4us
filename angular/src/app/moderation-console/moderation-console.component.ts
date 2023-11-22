import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { MatTable } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-moderation-console',
	templateUrl: './moderation-console.component.html',
	styleUrls: ['./moderation-console.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ModerationConsoleComponent {}
