import { QuestionnaireComponent } from './questionnaire.component';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { QuestionnaireServiceFake, QuestionnaireValidationServiceFake } from '../../fakes';
import { Question, QuestionType } from '../models/questions/question';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';

describe('QuestionnaireComponent', () => {
  let testee: QuestionnaireComponent;
  let questionnaireService: QuestionnaireServiceFake;
  let validationService: QuestionnaireValidationServiceFake;

  const id = 'id';
  const revision = 0;

  beforeEach(() => {
    questionnaireService = new QuestionnaireServiceFake();
    validationService = new QuestionnaireValidationServiceFake();
    testee = new QuestionnaireComponent(<ActivatedRoute><any>{
      params: Observable.of({
        id: id,
        revision: revision
      })
    }, <any>questionnaireService, <any>validationService);
  });

  it('should load the route params', () => {
    // Arrange
    // In beforeEach

    // Act
    // Instantiate, in beforeEach

    // Assert
    expect(testee.revision).toBe(revision);
    expect(testee.id).toBe(id);
  });

  it('should load the questions and preview', () => {
    // Arrange
    const questions: Question[] = [{
      id: 'id1',
      revision: 1,
      text: 'text1',
      type: QuestionType.textQuestion
    }];
    const preview: QuestionnairePreview = {
      revision: 0,
      id: 'id',
      name: 'name',
      questionsCount: 2
    };
    questionnaireService.getPreview.and.returnValue(Observable.of(preview));
    questionnaireService.getQuestionsForQuestionnaire.and.returnValue(Observable.of(questions));

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.questions).toEqual(questions);
    expect(testee.name).toBe('name');
    expect(validationService.initQuestionnaire).toHaveBeenCalledWith(['id1' ]);
  });
});
