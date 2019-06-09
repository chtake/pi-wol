import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {INotificationViewModel} from '@app/shared/view-models/inotification.viewmodel';
import {NotificationViewModel} from '@app/shared/view-models/notificationViewModel';
import {HostModel, HostStatus} from '../models/host.model';
import {HostService} from '@app/admin/hosts/services/host.service';
import {NotificationTypes} from '@app/shared/view-models/notificationTypes';
import {Router} from '@angular/router';
import {WolService} from '@app/admin/hosts/services/wol.service';
import {OnlineStatusService} from '@app/admin/hosts/services/online-status.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-hosts',
  templateUrl: './hosts.component.html'
})
export class HostsComponent implements INotificationViewModel, OnInit, OnDestroy {
  notification: NotificationViewModel = <NotificationViewModel>{};
  hostStatus = HostStatus;
  hosts: HostModel[] = [];
  deletionModal = {
    open: false,
    host: <HostModel>{}
  };
  isSmallScreen = false;

  private subscription: Subscription;

  constructor(private service: HostService,
              private wolService: WolService,
              private onlineStatus: OnlineStatusService,
              private router: Router) {
    this.getScreenResolution();
  }

  @HostListener('window:resize', ['$event'])
  getScreenResolution(event?) {
    this.isSmallScreen = (window.innerWidth < 500);
  }


  ngOnInit() {
    this.loadHosts();
    this.onlineStatus.connect();
    this.subscription = this.onlineStatus.onlineStatusChanged().subscribe(data => {
      this.hosts.find(x => x.id === data['id']).status = data['status'];
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.onlineStatus.disconnect();
  }

  onDelete(host: HostModel) {
    this.deletionModal.open = true;
    this.deletionModal.host = host;
  }

  onDeleteConfirm() {
    this.service.delete(this.deletionModal.host).subscribe(
      ok => this.notifyUser(NotificationTypes.Success, `${ok.hostname} removed.`),
      err => this.notifyUser(NotificationTypes.Danger, `Removing failed.`),
      () => this.loadHosts()
    );
    this.deletionModal = {
      host: <HostModel>{},
      open: false
    };
  }

  onEdit(host: HostModel) {
    this.router.navigate(['/admin', 'hosts', host.id]);
  }

  wakeUp(host: HostModel) {
    this.wolService.wakeUp(host.id).subscribe(
      _ => this.notifyUser(NotificationTypes.Success, 'Wake up command sent.'),
      _ => this.notifyUser(NotificationTypes.Danger, 'Wake up command failed.')
    );
  }

  private loadHosts() {
    this.service.getAll().subscribe(hosts => this.hosts = hosts);
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
