import { TestBed, inject, async } from '@angular/core/testing';

import { QuestionnairesService } from './questionnaires.service';
import { ApiService } from './api.service';
import { ApiServiceFake } from '../../fakes';
import { QuestionnairePreview } from '../models/questionnaire-preview';
import { Observable } from 'rxjs/Observable';

describe('QuestionnairesService', () => {
  let testee: QuestionnairesService;
  let apiServiceFake: ApiServiceFake;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        QuestionnairesService,
        {provide: ApiService, useClass: ApiServiceFake}
      ]
    });
  });

  beforeEach(inject([QuestionnairesService, ApiService], (t: QuestionnairesService, api: ApiService) => {
    testee = t;
    apiServiceFake = <any>api;
  }));

  it('should request and return the previews', async(() => {
    // Arrange
    const data = [
      new QuestionnairePreview('id1', 1, 'preview1', 1),
      new QuestionnairePreview('id2', 2, 'preview2', 2)
    ];
    apiServiceFake.get.and.returnValue(Observable.of({
      json: jasmine.createSpy('json').and.returnValue(data)
    }));
    let result: QuestionnairePreview[] = undefined;

    // Act
    testee.getPreviews().subscribe(_ => result = _);

    // Assert
    expect(apiServiceFake.get).toHaveBeenCalledWith('questionnaires/previews');
    expect(result).toEqual(data);
  }));
});
