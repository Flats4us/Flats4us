import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { StartMapComponent } from './start-map.component';

@NgModule({
	declarations: [StartMapComponent],
	imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class StartMapModule {}
