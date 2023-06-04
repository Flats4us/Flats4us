import { GetDescriptionDirective } from './get-description.directive';

const testElement = {
	nativeElement: document.createElement('div'),
};

describe('GetDescriptionDirective', () => {
	it('should create an instance', () => {
		const directive = new GetDescriptionDirective(testElement);
		expect(directive).toBeTruthy();
	});
});
