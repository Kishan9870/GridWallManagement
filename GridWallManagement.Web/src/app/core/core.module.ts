import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { CoreComponent } from './core.component';
import { FooterComponent } from './shared/layout/footer/footer.component';
import { HeaderComponent } from './shared/layout/header/header.component';
import { SidebarComponent } from './shared/layout/sidebar/sidebar.component';

@NgModule({
  declarations: [
    CoreComponent,
    SidebarComponent,
    FooterComponent,
    HeaderComponent,
  ],
  imports: [CommonModule, CoreRoutingModule],
})
export class CoreModule {}
