import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditProfileComponent } from './edit-profile.component';

const routes: Routes = [
	{ path: '', pathMatch: 'full', component: EditProfileComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class EditProfileRoutingModule {}
