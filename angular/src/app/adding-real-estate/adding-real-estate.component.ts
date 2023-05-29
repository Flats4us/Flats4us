import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-adding-real-estate',
	templateUrl: './adding-real-estate.component.html',
	styleUrls: ['./adding-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddingRealEstateComponent {
	public selected = '';

	public selectedFiles: File[] = [];

	public onFileSelected(event: any) {
		const files: FileList = event.target.files;
		for (let i = 0; i < files.length; i++) {
			this.selectedFiles.push(files[i]);
		}
	}
}
