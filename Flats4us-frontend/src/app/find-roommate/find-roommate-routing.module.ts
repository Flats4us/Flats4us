import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FindRoommateComponent } from './find-roommate.component';

const routes: Routes = [{ path: '', component: FindRoommateComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class FindRoommateRoutingModule {}
