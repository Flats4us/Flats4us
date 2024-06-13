import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class ThemeService {
	private logoUrl$: BehaviorSubject<string> = new BehaviorSubject(
		'./assets/logoFlats4Us.svg'
	);

	private darkMode: BehaviorSubject<boolean> = new BehaviorSubject(false);

	public darkMode$: Observable<boolean> = this.darkMode.asObservable();

	public isDarkMode() {
		return this.darkMode.getValue();
	}

	public setDarkMode(isDarkMode: boolean) {
		this.darkMode.next(isDarkMode);
		if (isDarkMode) {
			document.body.classList.remove('light');
			document.body.classList.add('dark');
			document.body.classList.add('dark');
			this.logoUrl$.next('./assets/logoFlats4Us_dark.svg');
		} else {
			document.body.classList.remove('dark');
			document.body.classList.add('light');
			this.logoUrl$.next('./assets/logoFlats4Us.svg');
		}
	}

	public getLogoUrl(): Observable<string> {
		return this.logoUrl$.asObservable();
	}
}
