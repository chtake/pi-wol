import {Component, OnInit} from '@angular/core';
import {INotificationViewModel} from '@app/shared/view-models/inotification.viewmodel';
import {NotificationViewModel} from '@app/shared/view-models/notificationViewModel';
import {state} from '@app/shared/state';
import {ActivatedRoute, Router} from '@angular/router';
import {ClrLoadingState} from '@clr/angular';
import {HostModel} from '@app/admin/hosts/models/host.model';
import {HostService} from '@app/admin/hosts/services/host.service';
import {NotificationTypes} from '@app/shared/view-models/notificationTypes';
import {ArpService} from '@app/admin/hosts/services/arp.service';

@Component({
  selector: 'app-hosts-form',
  templateUrl: './hosts-form.component.html',
  styleUrls: ['./hosts-form.component.css']
})
export class HostsFormComponent implements OnInit, INotificationViewModel {

  notification: NotificationViewModel = <NotificationViewModel>{};
  submitBtnState: ClrLoadingState = ClrLoadingState.DEFAULT;
  state: state;
  model: HostModel = <HostModel>{};

  constructor(
    private service: HostService,
    private arpService: ArpService,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    this.state = this.activatedRoute.snapshot.data.state;
  }

  ngOnInit() {
    if (this.state === 'update') {
      this.service.get(this.activatedRoute.snapshot.params.id).subscribe(m => {
        this.model = m;
      });
    }
  }

  onSubmit() {
    this.submitBtnState = ClrLoadingState.LOADING;

    switch (this.state) {
      case 'add':
        this.service.create(this.model).subscribe(res => {
            this.notifyUser(NotificationTypes.Success, 'Host created', res.id);
          },
          err => {
            this.notifyUser(NotificationTypes.Danger, 'Error while adding the host.');
          },
          () => {
            this.submitBtnState = ClrLoadingState.DEFAULT;
          });
        break;
      case 'update':
        this.service.update(this.model).subscribe(
          res => this.notifyUser(NotificationTypes.Success, 'Host updated', res.id),
          err => this.notifyUser(NotificationTypes.Danger, 'Error while updating the host.'),
          () => {
            this.submitBtnState = ClrLoadingState.DEFAULT;
          }
        );
        break;
    }
  }

  onAbort() {
    this.router.navigate(['/admin', 'hosts']);
  }

  ipAddressChanged(value: string) {
    const block = +value.split('.')[0];
    if (block <= 126) {
      this.model.netmask = '255.0.0.0';
    } else if (block <= 191) {
      this.model.netmask = '255.255.0.0';
    } else {
      this.model.netmask = '255.255.255.0';
    }

    this.arpService.getMac(value).subscribe(x => {
      this.model.macAddress = x.macAddress;
    });

  }

  private notifyUser(type: string, message: string, id: string = null) {
    this.notification = {
      type: type,
      message: message
    };

    setTimeout(() => {
      this.notification.message = null;
      if (id !== null) {
        this.router.navigate(['/admin', 'hosts']);
      }
    }, 2000);
  }
}
