import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageUserLicensesComponent } from './manage-user-licenses/manage-user-licenses.component';

const routes: Routes = [
  { path: '', redirectTo: 'manage-user-licenses', pathMatch: 'full' },
  { path: 'manage-user-licenses', component: ManageUserLicensesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
