import { HttpClient, HttpXhrBackend } from '@angular/common/http';
import { AccessControlDirective } from './access-control.directive';
import { AuthService } from '@shared/services/auth.service';
import { Router } from '@angular/router';
import { TemplateRef, ViewContainerRef } from '@angular/core';

const httpClient = new HttpClient(
	new HttpXhrBackend({
		build: () => new XMLHttpRequest(),
	})
);
const router = new Router();
const authService = new AuthService(httpClient, router);
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
