import { async, TestBed } from '@angular/core/testing';

import { QuestionnaireComponent } from './questionnaire.component';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { QuestionnaireServiceFake } from '../../fakes';
import { Question, QuestionType } from '../models/questions/question';

describe('QuestionnaireComponent', () => {
  let component: QuestionnaireComponent;
  let questionnaireService: QuestionnaireServiceFake;

  const id = 'id';
  const revision = 0;
  const name = 'name';

  beforeEach(async(() => {
    TestBed.configureTestingModule({
    })
    .compileComponents();
  }));

  beforeEach(() => {
    questionnaireService = new QuestionnaireServiceFake();
    component = new QuestionnaireComponent(<ActivatedRoute>{
      params: Observable.of({
        id: id,
        revision: revision,
        name: name
      })
    }, <any>questionnaireService);
  });

  it('should load the route params', () => {
    // Arrange
    // In beforeEach

    // Act
    // Instantiate, in beforeEach

    // Assert
    expect(component.revision).toBe(revision);
    expect(component.id).toBe(id);
    expect(component.name).toBe(name);
  });

  it('should load the questions for the questionnaire', () => {
    // Arrange
    const questions: Question[] = [{
      id: 'id1',
      revision: 1,
      text: 'text1',
      questionType: QuestionType.textQuestion
    }];
    questionnaireService.getQuestionsForQuestionnaire.and.returnValue(Observable.of(questions));

    // Act
    component.ngOnInit();

    // Assert
    expect(component.questions).toEqual(questions);
  });
});
