import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'argumentStatus',
})
export class ArgumentStatusPipe implements PipeTransform {
	public transform(argumentStatus: number): string {
		switch (argumentStatus) {
			case 0:
				return 'Moderation-console.statutes0';
			case 1:
				return 'Moderation-console.statutes1';
			case 2:
				return 'Moderation-console.statutes2';
			default:
				return '';
		}
	}
}
