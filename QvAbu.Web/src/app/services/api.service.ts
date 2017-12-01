import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }

  public get(url: string): Observable<any> {
    return <Observable<any>>this.http.get(environment.urlBase + 'api/' + url);
  }

  public post(url: string, body: any): Observable<any> {
    return <Observable<any>>this.http.post(environment.urlBase + 'api/' + url, body);
  }
}
