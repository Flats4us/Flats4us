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
	selector: '[appAccessControl]',
})
export class AccessControlDirective implements OnDestroy {
	@Input() public set appAccessControl(permission: IPermission | undefined) {
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
		this.authService.accessControl$
			.pipe(takeUntil(this.destroyed))
			.subscribe(data => {
				switch (true) {
					case !permission.allLoggedIn &&
						!permission.notLoggedIn &&
						data.user === permission.user &&
						data.status === permission.status &&
						!this.hasView:
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
						break;
					case permission.allLoggedIn && data.isLoggedIn && !this.hasView:
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
						break;
					case permission.notLoggedIn && !data.isLoggedIn && !this.hasView:
						this.viewContainer.createEmbeddedView(this.templateRef);
						this.hasView = true;
						break;
					default:
						if (this.hasView) {
							this.viewContainer.clear();
							this.hasView = false;
						}
						break;
				}
			});
	}

	public ngOnDestroy(): void {
		this.destroyed.next();
		this.destroyed.complete();
	}
}
