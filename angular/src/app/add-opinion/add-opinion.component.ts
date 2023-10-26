import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-add-opinion',
	templateUrl: './add-opinion.component.html',
	styleUrls: ['./add-opinion.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOpinionComponent {
	public commentText = '';
	public selectedTags: string[] = [];
	public checkedTags: Set<string> = new Set();
	public toggleCheckbox(tag: string): void {
		if (this.isChecked(tag)) {
			this.checkedTags.delete(tag);
		} else {
			this.checkedTags.add(tag);
		}
	}

	public isChecked(tag: string): boolean {
		return this.checkedTags.has(tag);
	}
}
