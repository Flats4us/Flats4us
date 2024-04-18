import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { DetailsProfileComponent } from './details-profile.component';
import { UserService } from '@shared/services/user.service';
import { MatButtonModule } from '@angular/material/button';
import { ProfileRoutingModule } from '../profile-routing.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [DetailsProfileComponent],
	imports: [
		CommonModule,
		ProfileRoutingModule,
		MatChipsModule,
		MatCardModule,
		MatIconModule,
		MatListModule,
		MatButtonModule,
		TranslateModule,
	],
	providers: [UserService],
})
export class DetailsProfileModule {}
