import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HeaderComponent } from './header.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

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
		MatMenuModule,
		MatSlideToggleModule,
	],
	exports: [HeaderComponent],
})
export class HeaderModule {}
