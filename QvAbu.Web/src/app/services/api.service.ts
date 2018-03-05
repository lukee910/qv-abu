import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/mergeMap';

@Injectable()
export class ApiService {
  urlBase: string;

  constructor(private http: HttpClient) { }

  public get(url: string): Observable<any> {
    return this.getBaseUrl()
      .flatMap(baseUrl => <Observable<any>>this.http.get(this.urlBase + 'api/' + url));
  }

  public post(url: string, body: any): Observable<any> {
    return this.getBaseUrl()
      .flatMap(baseUrl => this.http.post(this.urlBase + 'api/' + url, body));
  }

  private getBaseUrl(): Observable<any> {
    if (this.urlBase) {
      return Observable.create(o => o.next(this.urlBase));
    } else {
      return this.http.get<string>('/assets/api-url-base.json').map(value => {
        this.urlBase = value;
        return this.urlBase;
      });
    }
  }
}
