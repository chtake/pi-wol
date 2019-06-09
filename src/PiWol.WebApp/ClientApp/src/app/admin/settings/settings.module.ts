import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SettingsComponent} from './settings/settings.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '@app/shared/shared.module';
import {IpAccessService} from '@app/admin/settings/services/ip-access.service';
import {IpAccessFormComponent} from './ip-access-form/ip-access-form.component';
import {IsValidNetworkValidator} from '@app/admin/settings/validators/is-valid-network.validator';

const routes: Routes = [
  {
    path: '',
    component: SettingsComponent,
  },
  {
    path: 'access',
    children: [
      {
        path: 'add',
        component: IpAccessFormComponent
      },
      {
        path: '',
        component: SettingsComponent
      }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [SettingsComponent, IpAccessFormComponent, IsValidNetworkValidator],
  providers: [IpAccessService]
})
export class SettingsModule {
}
