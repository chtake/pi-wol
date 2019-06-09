import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class WolService {
  private static endpoint = '';

  constructor(private http: HttpClient) {
    WolService.endpoint = environment.serviceEndpoint;
  }

  wakeUp(id: string): Observable<any> {
    return this.http.get(`${WolService.endpoint}wol/wol/${id}`);
  }
}
