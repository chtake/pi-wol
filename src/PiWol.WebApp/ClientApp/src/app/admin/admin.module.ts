import {ModuleWithProviders, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin/admin.component';
import { HeaderComponent } from './header/header.component';
import {SharedModule} from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule
  ],
  declarations: [AdminComponent, HeaderComponent]
})
export class AdminModule {
  static forRoot(): ModuleWithProviders<any> {
    return {
      ngModule: AdminModule,
       providers: []
    };
  }
}
