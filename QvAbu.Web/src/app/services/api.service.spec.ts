import { TestBed, inject, async } from '@angular/core/testing';

import { ApiService } from './api.service';
import { HttpFake } from '../../fakes';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';

describe('ApiService', () => {
  let testee: ApiService;
  let httpFake: HttpFake;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ApiService,
        {provide: HttpClient, useClass: HttpFake}
      ]
    });
  });

  beforeEach(inject([ApiService, HttpClient], (t: ApiService, http: HttpClient) => {
    testee = t;
    httpFake = <any>http;
    environment.urlBase = 'urlBase/';
  }));

  it('should make an API GET call', async(() => {
    // Arrange
    const response = new HttpResponse({
      body: 'body'
    });
    httpFake.get.and.returnValue(Observable.of(response));
    let result: any = undefined;

    // Act
    testee.get('route').subscribe(_ => result = _);

    // Assert
    expect(result).toBe(response);
    expect(httpFake.get).toHaveBeenCalledWith('urlBase/api/route');
  }));

  it('should make an API POST call', async(() => {
    // Arrange
    const response = new HttpResponse({
      body: 'body'
    });
    const requestBody = 'requestBody';
    httpFake.post.and.returnValue(Observable.of(response));
    let result: any = undefined;

    // Act
    testee.post('route', requestBody).subscribe(_ => result = _);

    // Assert
    expect(result).toBe(response);
    expect(httpFake.post).toHaveBeenCalledWith('urlBase/api/route', requestBody);
  }));
});
