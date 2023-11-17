import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterModule } from '@shared/components/footer/footer.module';
import { HeaderModule } from '@shared/components/header/header.module';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { CurrencyPipe } from '@angular/common';

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
		MatSidenavModule,
		MatButtonModule,
		MatIconModule,
		MatMenuModule,
		CurrencyPipe,
	],
	providers: [CurrencyPipe],
	bootstrap: [AppComponent],
})
export class AppModule {}
