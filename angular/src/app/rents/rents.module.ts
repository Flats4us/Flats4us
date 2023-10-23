import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentsComponent } from './rents.component';
import { RentsRoutingModule } from './rents-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [
    RentsComponent
  ],
  imports: [
    CommonModule,
    RentsRoutingModule,
    MatButtonModule,
    MatIconModule
  ],
  exports: [
    RentsComponent
  ]
})
export class RentsModule { }
