import { ChangeDetectionStrategy, Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';

@Component({
	selector: 'app-base',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './base.component.html',
	styleUrls: ['./base.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BaseComponent implements OnDestroy {
	protected destroyed: Subject<void> = new Subject<void>();

	protected untilDestroyed<T>() {
		return takeUntil<T>(this.destroyed);
	}

	public ngOnDestroy(): void {
		this.destroyed.next();
		this.destroyed.complete();
	}
}
