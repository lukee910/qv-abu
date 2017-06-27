import { TestBed, inject, async } from '@angular/core/testing';

import { ApiService } from './api.service';
import { HttpFake } from '../../fakes';
import { Http, Response, ResponseOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';

describe('ApiService', () => {
  let testee: ApiService;
  let httpFake: HttpFake;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ApiService,
        {provide: Http, useClass: HttpFake}
      ]
    });
  });

  beforeEach(inject([ApiService, Http], (t: ApiService, http: Http) => {
    testee = t;
    httpFake = <any>http;
    environment.urlBase = 'urlBase/';
  }));

  it('should make an API GET call', async(() => {
    // Arrange
    const response = new Response(new ResponseOptions({
      body: 'body'
    }));
    httpFake.get.and.returnValue(Observable.of(response));
    let result: Response = undefined;

    // Act
    testee.get('route').subscribe(_ => result = _);

    // Assert
    expect(result).toBe(response);
    expect(httpFake.get).toHaveBeenCalledWith('urlBase/api/route');
  }));
});
