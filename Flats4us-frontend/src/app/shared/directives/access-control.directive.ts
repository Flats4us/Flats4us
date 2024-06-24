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
	@Input() public set appAccessControl(permission: IPermission | IPermission[]) {
		if (permission) {
			this.allowAccess(permission);
		} else if (!permission) {
			this.hideTemplate();
		}
	}

	private hasView = false;
	private destroyed: Subject<void> = new Subject<void>();

	constructor(
		private authService: AuthService,
		private templateRef: TemplateRef<any>,
		private viewContainer: ViewContainerRef
	) {}

	private allowAccess(permission: IPermission | IPermission[]) {
		if (permission instanceof Array) {
			this.grantAccessToMultipleRoles(permission);
		} else {
			this.grantAccessToSingleRole(permission);
		}
	}

	private grantAccessToMultipleRoles(permission: IPermission[]) {
		this.authService.accessControl$
			.pipe(takeUntil(this.destroyed))
			.subscribe(data => {
				switch (true) {
					case !permission.find(perm => perm.allLoggedIn) &&
						!permission.find(perm => perm.notLoggedIn) &&
						!!permission.find(perm => data.user === perm.user) &&
						!!permission.find(perm => data.status === perm.status):
						this.showTemplate();
						break;
					case !!permission.find(perm => perm.allLoggedIn) && data.isLoggedIn:
						this.showTemplate();
						break;
					case !!permission.find(perm => perm.notLoggedIn) && !data.isLoggedIn:
						this.showTemplate();
						break;
					default:
						this.hideTemplate();
						break;
				}
			});
	}

	private grantAccessToSingleRole(permission: IPermission) {
		this.authService.accessControl$
			.pipe(takeUntil(this.destroyed))
			.subscribe(data => {
				switch (true) {
					case !permission.allLoggedIn &&
						!permission.notLoggedIn &&
						data.user === permission.user &&
						data.status === permission.status:
						this.showTemplate();
						break;
					case permission.allLoggedIn && data.isLoggedIn:
						this.showTemplate();
						break;
					case permission.notLoggedIn && !data.isLoggedIn:
						this.showTemplate();
						break;
					default:
						this.hideTemplate();
						break;
				}
			});
	}

	private showTemplate() {
		if (!this.hasView) {
			this.viewContainer.createEmbeddedView(this.templateRef);
			this.hasView = true;
		}
	}

	private hideTemplate() {
		if (this.hasView) {
			this.viewContainer.clear();
			this.hasView = false;
		}
	}

	public ngOnDestroy(): void {
		this.destroyed.next();
		this.destroyed.complete();
	}
}
