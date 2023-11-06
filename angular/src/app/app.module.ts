import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterModule } from './shared/components/footer/footer.module';
import { HeaderModule } from './shared/components/header/header.module';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { FindRoommateModule } from './find-roommate/find-roommate.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { StartDisputeDialogComponent } from './shared/components/start-dispute-dialog/start-dispute-dialog.component';

@NgModule({
	declarations: [AppComponent, NotFoundComponent],
	imports: [
		BrowserModule,
		AppRoutingModule,
		FormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
		HeaderModule,
		FooterModule,
		FindRoommateModule,
		MatSidenavModule,
		MatButtonModule,
		MatIconModule,
		MatMenuModule,
		MatDialogModule,
		StartDisputeDialogComponent,
	],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}
