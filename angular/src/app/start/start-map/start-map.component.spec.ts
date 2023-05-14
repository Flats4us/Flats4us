import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartMapComponent } from './start-map.component';

describe('StartMapComponent', () => {
	let component: StartMapComponent;
	let fixture: ComponentFixture<StartMapComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [StartMapComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(StartMapComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
