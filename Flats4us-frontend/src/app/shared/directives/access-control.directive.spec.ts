import { AccessControlDirective } from './access-control.directive';
import { AuthService } from '@shared/services/auth.service';
import { TemplateRef, ViewContainerRef } from '@angular/core';

let authService: AuthService;
let templateRef: TemplateRef<any>;
let viewContainerRef: ViewContainerRef;

describe('AccessControlDirective', () => {
	it('should create an instance', () => {
		const directive = new AccessControlDirective(
			authService,
			templateRef,
			viewContainerRef
		);
		expect(directive).toBeTruthy();
	});
});
