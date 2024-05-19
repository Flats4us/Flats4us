import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StartMapModule } from './start-map/start-map.module';
import { StartComponent } from './start.component';

const routes: Routes = [{ path: '', component: StartComponent }];

@NgModule({
	imports: [RouterModule.forChild(routes), StartMapModule],
	exports: [RouterModule],
})
export class StartRoutingModule {}
