import { TranslateService } from '@ngx-translate/core';
import { GetDescriptionDirective } from './get-description.directive';

const testElement = {
	nativeElement: document.createElement('div'),
};

let translate: TranslateService;

describe('GetDescriptionDirective', () => {
	it('should create an instance', () => {
		const directive = new GetDescriptionDirective(testElement, translate);
		expect(directive).toBeTruthy();
	});
});
