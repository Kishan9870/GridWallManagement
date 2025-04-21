import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponent } from './core.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    loadChildren: () =>
      import('./pages/pages.module').then((m) => m.PagesModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CoreRoutingModule {}
