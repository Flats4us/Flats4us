import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOpinionComponent } from './add-opinion.component';

const routes: Routes = [{ path: '', component: AddOpinionComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AddOpinionRoutingModule {}
