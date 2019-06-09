import {Component, OnInit} from '@angular/core';
import {INotificationViewModel} from '@app/shared/view-models/inotification.viewmodel';
import {NotificationViewModel} from '@app/shared/view-models/notificationViewModel';
import {IpNetworkModel} from '@app/admin/settings/models/ip-network-model';
import {ClrLoadingState} from '@clr/angular';
import {Router} from '@angular/router';
import {IpAccessService} from '@app/admin/settings/services/ip-access.service';
import {NotificationTypes} from '@app/shared/view-models/notificationTypes';

@Component({
  selector: 'app-ip-access-form',
  templateUrl: './ip-access-form.component.html'
})
export class IpAccessFormComponent implements INotificationViewModel, OnInit {
  notification: NotificationViewModel = <NotificationViewModel>{};
  model: IpNetworkModel = <IpNetworkModel>{};
  submitBtnState: ClrLoadingState = ClrLoadingState.DEFAULT;

  constructor(
    private service: IpAccessService,
    private router: Router) {
  }

  ngOnInit() {
  }

  onSubmit() {
    this.submitBtnState = ClrLoadingState.LOADING;
    this.service.create(this.model).subscribe(res => {
        this.notifyUser(NotificationTypes.Success, 'Network created', res);
      },
      err => {
        this.notifyUser(NotificationTypes.Danger, 'Error while adding the host.');
      },
      () => {
        this.submitBtnState = ClrLoadingState.DEFAULT;
      });
  }

  onAbort() {
    this.router.navigate(['/admin', 'settings']);
  }

  private notifyUser(type: string, message: string, id: IpNetworkModel[] = null) {
    this.notification = {
      type: type,
      message: message
    };

    setTimeout(() => {
      this.notification.message = null;
      if (id !== null) {
        this.router.navigate(['/admin', 'settings']);
      }
    }, 2000);
  }
}
