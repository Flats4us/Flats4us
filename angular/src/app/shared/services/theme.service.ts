import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class ThemeService {
	private darkMode: BehaviorSubject<boolean> = new BehaviorSubject(false);

	public darkMode$: Observable<boolean> = this.darkMode.asObservable();

	public isDarkMode() {
		return this.darkMode.getValue();
	}

	public checkDarkMode() {
		return this.darkMode.asObservable();
	}

	public setDarkMode(isDarkMode: boolean) {
		this.darkMode.next(isDarkMode);
		if (isDarkMode) {
			document.body.classList.remove('light');
			document.body.classList.add('dark');
			document.body.classList.add('dark');
		} else {
			document.body.classList.remove('dark');
			document.body.classList.add('light');
		}
	}
}
