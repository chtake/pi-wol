import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {HostsComponent} from './hosts/hosts.component';
import {RouterModule, Routes} from '@angular/router';
import {SharedModule} from '../../shared/shared.module';
import {HostService} from '@app/admin/hosts/services/host.service';
import {HostsFormComponent} from './hosts-form/hosts-form.component';
import {ArpService} from '@app/admin/hosts/services/arp.service';
import {WolService} from '@app/admin/hosts/services/wol.service';
import {OnlineStatusService} from '@app/admin/hosts/services/online-status.service';

const routes: Routes = [
  {
    path: '',
    component: HostsComponent,
  },
  {
    path: 'hosts',
    children: [
      {
        path: '',
        component: HostsComponent
      },
      {
        path: 'add',
        component: HostsFormComponent,
        data: {state: 'add'}
      },
      {
        path: ':id',
        component: HostsFormComponent,
        data: {state: 'update'}
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
  providers: [HostService, ArpService, WolService, OnlineStatusService],
  declarations: [HostsComponent, HostsFormComponent]
})
export class HostsModule {
}
