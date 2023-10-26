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
	public comments: any[] = [];
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

	constructor() {
		this.comments = [
			{ text: ' 1', tags: ['Tag1', 'Tag2'] },
			{ text: ' 2', tags: ['Tag3'] },
		];
	}

	public addComment() {
		const newComment = {
			text: this.commentText,
			tags: this.selectedTags,
		};
		this.comments.push(newComment);

		// Очистите текст комментария и сбросьте выбранные теги
		this.commentText = '';
		this.selectedTags = [];
	}
}
