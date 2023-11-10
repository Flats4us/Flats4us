import { GetPriceDirective } from './get-price.directive';

const testElement = {
	nativeElement: document.createElement('div'),
};

describe('GetPriceDirective', () => {
	it('should create an instance', () => {
		const directive = new GetPriceDirective(testElement);
		expect(directive).toBeTruthy();
	});
});
