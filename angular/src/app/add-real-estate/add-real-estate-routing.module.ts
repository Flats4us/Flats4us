import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddRealEstateComponent } from './add-real-estate.component';

const routes: Routes = [{ path: '', component: AddRealEstateComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AddRealEstateRoutingModule {}
