import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchedOffersComponent } from './watched-offers.component';

describe('WatchedOffersComponent', () => {
	let component: WatchedOffersComponent;
	let fixture: ComponentFixture<WatchedOffersComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [WatchedOffersComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(WatchedOffersComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
