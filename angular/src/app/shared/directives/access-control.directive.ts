import {
	Directive,
	Input,
	OnDestroy,
	TemplateRef,
	ViewContainerRef,
} from '@angular/core';
import { IPermission } from '@shared/models/auth.models';
import { AuthService } from '@shared/services/auth.service';
import { Subject, takeUntil } from 'rxjs';

@Directive({
	standalone: true,
	selector: '[appPermissionIf]',
})
export class AccessControlDirective implements OnDestroy {
	@Input() public set appPermissionIf(permission: IPermission | undefined) {
		if (permission) {
			this.allowAccess(permission);
		} else if (!permission) {
			this.viewContainer.clear();
			this.hasView = false;
		}
	}

	private hasView = false;
	private destroyed: Subject<void> = new Subject<void>();

	constructor(
		private authService: AuthService,
		private templateRef: TemplateRef<any>,
		private viewContainer: ViewContainerRef
	) {}

	private allowAccess(permission: IPermission) {
		if (
			!permission.allLoggedIn &&
			!(permission.allLoggedIn ?? false) &&
			!permission.notLoggedIn &&
			!(permission.notLoggedIn ?? false)
		) {
			this.authService.accessControl$
				.pipe(takeUntil(this.destroyed))
				.subscribe(data => {
					if (
						data.user === permission.user &&
						data.status === permission.status &&
						!this.hasView
					) {
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
					} else if (
						!(data.user === permission.user && data.status === permission.status) &&
						this.hasView
					) {
						this.viewContainer.clear();
						this.hasView = false;
					}
				});
		} else if (permission.allLoggedIn && (permission.allLoggedIn ?? false)) {
			this.authService.isLoggedIn$
				.pipe(takeUntil(this.destroyed))
				.subscribe(data => {
					if (data && !this.hasView) {
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
					} else if (!data && this.hasView) {
						this.viewContainer.clear();
						this.hasView = false;
					}
				});
		} else if (permission.notLoggedIn && (permission.notLoggedIn ?? false)) {
			this.authService.isLoggedIn$
				.pipe(takeUntil(this.destroyed))
				.subscribe(data => {
					if (!data && !this.hasView) {
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
					} else if (data && this.hasView) {
						this.viewContainer.clear();
						this.hasView = false;
					}
				});
		}
	}

	public ngOnDestroy(): void {
		this.destroyed.next();
		this.destroyed.complete();
	}
}
