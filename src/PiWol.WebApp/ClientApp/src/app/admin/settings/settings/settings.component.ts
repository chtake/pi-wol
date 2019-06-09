import {Component, OnInit} from '@angular/core';
import {INotificationViewModel} from '@app/shared/view-models/inotification.viewmodel';
import {NotificationViewModel} from '@app/shared/view-models/notificationViewModel';
import {IpNetworkModel} from '@app/admin/settings/models/ip-network-model';
import {IpAccessService} from '@app/admin/settings/services/ip-access.service';
import {NotificationTypes} from '@app/shared/view-models/notificationTypes';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html'
})
export class SettingsComponent implements INotificationViewModel, OnInit {

  notification: NotificationViewModel = <NotificationViewModel>{};
  deletionModal = {
    open: false,
    model: <IpNetworkModel>{}
  };

  ipAccessModels: IpNetworkModel[];

  constructor(private service: IpAccessService) {
  }

  ngOnInit() {
    this.loadNetworks();
  }

  loadNetworks() {
    this.service.getAll().subscribe(x => this.ipAccessModels = x);
  }

  onDeleteConfirm() {
    this.service.delete(this.deletionModal.model).subscribe(
      ok => this.notifyUser(NotificationTypes.Success, `Declined access for ${ok.ipNetwork}.`),
      err => this.notifyUser(NotificationTypes.Danger, `Removing failed.`),
      () => this.loadNetworks()
    );
    this.deletionModal = {
      model: <IpNetworkModel>{},
      open: false
    };
  }

  onDelete(host: IpNetworkModel) {
    this.deletionModal.open = true;
    this.deletionModal.model = host;
  }

  private notifyUser(type: string, message: string) {
    this.notification = {
      type: type,
      message: message
    };

    setTimeout(() => {
      this.notification.message = null;
    }, 2000);
  }

}
