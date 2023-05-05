import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddingRealEstateComponent} from "./adding-real-estate.component";

const routes: Routes = [{ path: 'addingRealEstate', component: AddingRealEstateComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AddingRealEstateRoutingModule {}
