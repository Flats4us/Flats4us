import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainSiteComponent } from './main-site.component';

const routes: Routes = [{ path: 'start', component: MainSiteComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class MainSiteRoutingModule {}
