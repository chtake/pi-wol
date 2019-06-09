import {Injectable} from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {environment} from '@env/environment';
import {Observable, Subject} from 'rxjs';
import {HostModel} from '@app/admin/hosts/models/host.model';

@Injectable({
  providedIn: 'root'
})
export class OnlineStatusService {
  private hubConnection: signalR.HubConnection;

  private subject: Subject<HostModel> = new Subject();

  constructor() {

  }

  public connect() {
    if (!this.hubConnection) {
      this.startConnection();
      this.addTransferHostStatusListener();
    }
  }

  public disconnect() {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(_ => this.hubConnection = null);
    }
  }

  public onlineStatusChanged(): Observable<HostModel> {
    return this.subject.asObservable();
  }

  private startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.signalREndpoint + 'host-status')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  };

  private addTransferHostStatusListener = () => {
    this.hubConnection.on('HostStatusChanged', (data) => {
      this.subject.next(data);
    });
  };
}
