import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HeaderComponent } from './header.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatMenuModule } from '@angular/material/menu';
import { RouterLink } from '@angular/router';
import { StartDisputeDialogComponent } from '../start-dispute-dialog/start-dispute-dialog.component';

@NgModule({
	declarations: [HeaderComponent],
	imports: [
		CommonModule,
		MatToolbarModule,
		MatIconModule,
		MatButtonModule,
		BrowserAnimationsModule,
		MatSidenavModule,
		MatListModule,
		MatRadioModule,
		MatSlideToggleModule,
		MatMenuModule,
		RouterLink,
		StartDisputeDialogComponent,
	],
	exports: [HeaderComponent],
})
export class HeaderModule {}
