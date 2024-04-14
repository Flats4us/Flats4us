import { registerLocaleData } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import localePl from '@angular/common/locales/pl';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterModule } from '@shared/components/footer/footer.module';
import { HeaderModule } from '@shared/components/header/header.module';
import { NotFoundComponent } from '@shared/components/not-found/not-found.component';
import { AuthInterceptor } from '@shared/services/auth.interceptor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatCardModule } from '@angular/material/card';

registerLocaleData(localePl);
import { FindRoommateModule } from './find-roommate/find-roommate.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';


export function HttpLoaderFactory(http: HttpClient) {
	return new TranslateHttpLoader(http);
  }

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
		MatNativeDateModule,
		MatCardModule,
		FindRoommateModule,
		TranslateModule.forRoot({
			loader: {
			  provide: TranslateLoader,
			  useFactory: HttpLoaderFactory,
			  deps: [HttpClient],
			}
		  })
	],
	providers: [
		{ provide: LOCALE_ID, useValue: 'pl' },
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthInterceptor,
			multi: true,
		},
	],
	bootstrap: [AppComponent],
})
export class AppModule {}
