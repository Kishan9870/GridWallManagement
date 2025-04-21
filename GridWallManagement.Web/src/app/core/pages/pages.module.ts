import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { ManageUserLicensesComponent } from './manage-user-licenses/manage-user-licenses.component';


@NgModule({
  declarations: [
    ManageUserLicensesComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule
  ]
})
export class PagesModule { }
