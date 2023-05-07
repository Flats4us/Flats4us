import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { OwnerFormComponent } from './owner-form.component';

const routes: Routes = [{ path: '', component: OwnerFormComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class OwnerFormRoutingModule {}
