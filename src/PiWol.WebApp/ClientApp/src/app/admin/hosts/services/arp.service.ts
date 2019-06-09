import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '@env/environment';
import {ArpResultModel} from '@app/admin/hosts/models/arp-result.model';

@Injectable()
export class ArpService {
  private static endpoint = '';

  constructor(private  http: HttpClient) {
    ArpService.endpoint = environment.serviceEndpoint;
  }

  getMac(ipAddr: string) {
    return this.http.get<ArpResultModel>(`${ArpService.endpoint}wol/arp/?ip=${ipAddr}`);
  }
}
