import {Injectable} from '@angular/core';
import {environment} from '@env/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IpNetworkModel} from '@app/admin/settings/models/ip-network-model';

@Injectable()
export class IpAccessService {
  private static endpoint = '';

  constructor(private  http: HttpClient) {
    IpAccessService.endpoint = environment.serviceEndpoint;
  }

  getAll(): Observable<IpNetworkModel[]> {
    return this.http.get<IpNetworkModel[]>(`${IpAccessService.endpoint}access/ipnetworkmanagement`);
  }

  create(model: IpNetworkModel): Observable<IpNetworkModel[]> {
    return this.http.post<IpNetworkModel[]>(`${IpAccessService.endpoint}access/ipnetworkmanagement`, model);
  }

  delete(model: IpNetworkModel): Observable<IpNetworkModel> {
    return this.http.delete<IpNetworkModel>(`${IpAccessService.endpoint}access/ipnetworkmanagement/${encodeURIComponent(model.ipNetwork)}`);
  }

  check(model: IpNetworkModel): Observable<IpNetworkModel> {
    return this.http.post<IpNetworkModel>(`${IpAccessService.endpoint}access/ipnetworkmanagement/check`, model);
  }
}
