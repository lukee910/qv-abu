import { TestBed, inject, async } from '@angular/core/testing';

import { QuestionnairesService } from './questionnaires.service';
import { ApiService } from './api.service';
import { ApiServiceFake } from '../../fakes';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { Observable } from 'rxjs/Observable';
import { Question } from '../models/questions/question';
import { AssignmentQuestion } from '../models/questions/assignment-question';
import { SimpleQuestion, SimpleQuestionType } from '../models/questions/simple-question';
import { TextQuestion } from '../models/questions/text-question';
import { SimpleAnswer } from '../models/questions/simple-answer';

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

  it('should request, return and cache the previews', async(() => {
    // Arrange
    const data = [
      new QuestionnairePreview('id1', 1, 'preview1', 1),
      new QuestionnairePreview('id2', 2, 'preview2', 2)
    ];
    apiServiceFake.get.and.returnValue(Observable.of(data));
    let allResult: QuestionnairePreview[] = undefined;
    let singleResult: QuestionnairePreview = undefined;

    // Act
    testee.getPreviews().subscribe(_ => allResult = _);
    testee.getPreview('id1', 1).subscribe(_ => singleResult = _);

    // Assert
    expect(apiServiceFake.get).toHaveBeenCalledWith('questionnaires/previews');
    expect(apiServiceFake.get).toHaveBeenCalledTimes(1);
    expect(allResult).toEqual(data);
    expect(singleResult).toEqual(data[0]);
  }));

  it('should request the previews if a single preview is requested and no cache exists', async(() => {
    // Arrange
    const data = [
      new QuestionnairePreview('id1', 1, 'preview1', 1),
      new QuestionnairePreview('id2', 2, 'preview2', 2)
    ];
    apiServiceFake.get.and.returnValue(Observable.of(data));
    let result: QuestionnairePreview = undefined;

    // Act
    testee.getPreview('id1', 1).subscribe(_ => result = _);

    // Assert
    expect(apiServiceFake.get).toHaveBeenCalledWith('questionnaires/previews');
    expect(apiServiceFake.get).toHaveBeenCalledTimes(1);
    expect(result).toEqual(data[0]);
  }));

  it('should request and return the questions for a questionnaire', async(() => {
    // Arrange
    const data: Question[] = [<AssignmentQuestion>{
      id: 'id1',
      type: 0,
      revision: 0,
      text: 'text1',
      // Assignment Question:
      options: [{
        text: 'option1text',
        id: 'id1'
      }],
      answers: [{
        text: 'answer1text',
        id: 'id1',
        correctOption: null,
        correctOptionId: 'id1'
      }]
    }, <SimpleQuestion>{
      id: 'id2',
      type: 1,
      revision: 1,
      text: 'text2',
      // Simple Question:
      isNumberOfAnswersGiven: true,
      isMultipleChoice: true,
      answers: [<SimpleAnswer>{
        text: 'answer1text',
        id: 'id2',
        isCorrect: true
      }],
      simpleQuestionType: SimpleQuestionType.trueFalse
    }, <TextQuestion>{
      id: 'id3',
      type: 2,
      revision: 2,
      text: 'text1',
      // Text Question:
      answer: {
        text: 'answer1text',
        id: 'id3'
      }
    }];
    const dataAsBaseClass: Question[] = [];
    data.forEach(_ => dataAsBaseClass.push(<Question>_));
    apiServiceFake.post.and.returnValue(Observable.of(dataAsBaseClass));
    let result: Question[] = undefined;

    const questionnaires = [{
      revision: 1,
      id: 'id'
    }];

    // Act
    testee.getQuestionsForQuestionnaires(questionnaires).subscribe(_ => result = _);

    // Assert
    expect(apiServiceFake.post).toHaveBeenCalledWith('questionnaires/questions', questionnaires);
    expect(result).toEqual(data);
  }));
});
