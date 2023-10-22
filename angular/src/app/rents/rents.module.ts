import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentsComponent } from './rents.component';
import { RentsRoutingModule } from './rents-routing.module';



@NgModule({
  declarations: [
    RentsComponent
  ],
  imports: [
    CommonModule,
    RentsRoutingModule
  ],
  exports: [
    RentsComponent
  ]
})
export class RentsModule { }
