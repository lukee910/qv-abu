import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response } from '@angular/http';
import { environment } from '../../environments/environment';

@Injectable()
export class ApiService {

  constructor(private http: Http) { }

  public get(url: string): Observable<Response> {
    return this.http.get(environment.urlBase + 'api/' + url);
  }
}
