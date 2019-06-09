import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '@env/environment';
import {Observable} from 'rxjs';
import {HostModel} from '@app/admin/hosts/models/host.model';

@Injectable()
export class HostService {

  private static endpoint = '';

  constructor(private  http: HttpClient) {
    HostService.endpoint = environment.serviceEndpoint;
  }

  getAll(): Observable<HostModel[]> {
    return this.http.get<HostModel[]>(`${HostService.endpoint}wol/host`);
  }

  get(id: string) {
    return this.http.get<HostModel>(`${HostService.endpoint}wol/host/${id}`);
  }

  create(model: HostModel): Observable<HostModel> {
    return this.http.post<HostModel>(`${HostService.endpoint}wol/host`, model);
  }

  delete(model: HostModel): Observable<HostModel> {
    return this.http.delete<HostModel>(`${HostService.endpoint}wol/host/${model.id}`);
  }

  update(model: HostModel): Observable<HostModel> {
    return this.http.put<HostModel>(`${HostService.endpoint}wol/host/${model.id}`, model);
  }
}
