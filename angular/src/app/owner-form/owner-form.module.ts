import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatCardModule} from "@angular/material/card";
import {MatRadioModule} from "@angular/material/radio";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {OwnerFormComponent} from "./owner-form.component";
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [OwnerFormComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatRadioModule,
    MatCheckboxModule,
    FormsModule,
  ],
  exports: [OwnerFormComponent],
})
export class OwnerFormModule { }
