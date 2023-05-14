import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StartMapComponent } from './start-map/start-map.component';
import { StartMapModule } from './start-map/start-map.module';
import { StartComponent } from './start.component';

const routes: Routes = [
	{ path: '', component: StartComponent },
	{ path: 'map', component: StartMapComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes), StartMapModule],
	exports: [RouterModule],
})
export class StartRoutingModule {}
