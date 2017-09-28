import { QuestionnaireComponent } from './questionnaire.component';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { QuestionnaireServiceFake, QuestionnaireValidationServiceFake, WindowFake } from '../../fakes';
import { Question } from '../models/questions/question';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { TextQuestion } from '../models/questions/text-question';

describe('QuestionnaireComponent', () => {
  let testee: QuestionnaireComponent;
  let questionnaireService: QuestionnaireServiceFake;
  let validationService: QuestionnaireValidationServiceFake;
  let window: WindowFake;

  const id = 'id';
  const revision = 0;

  beforeEach(() => {
    questionnaireService = new QuestionnaireServiceFake();
    validationService = new QuestionnaireValidationServiceFake();
    window = new WindowFake();
    testee = new QuestionnaireComponent(<ActivatedRoute><any>{
      params: Observable.of({
        id: id,
        revision: revision
      })
    }, <any>questionnaireService, <any>validationService, <any>window);
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
    const questions: Question[] = [new TextQuestion('id1', 1)];
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

    const args = validationService.initQuestionnaire.calls.argsFor(0);
    expect(args.length).toBe(1);
    expect(args[0][0]).toEqual(questions[0]);
  });

  it('should validate the questionnaire on validate', () => {
    // Arrange
    const validationResult = {
      'valid': 1,
      'invalid': 2,
      'info': 3,
      'notValidated': 4
    };
    validationService.validate.and.returnValue(validationResult);

    // Act
    testee.validate();
    expect(window.scrollTo).toHaveBeenCalledWith(0, 0);

    // Assert
    expect(testee.validationResult).toBe(validationResult);
  });

  it('should get the alert class for a valid state', () => {
    // Arrange
    testee.validationResult = {
      'valid': 2,
      'invalid': 0,
      'info': 1,
      'notValidated': 0
    };

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toEqual('success');
  });

  it('should get the alert class for an invalid state', () => {
    // Arrange
    testee.validationResult = {
      'valid': 2,
      'invalid': 1,
      'info': 1,
      'notValidated': 0
    };

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toEqual('danger');
  });

  it('should get the alert class for an incomplete valid state', () => {
    // Arrange
    testee.validationResult = {
      'valid': 2,
      'invalid': 0,
      'info': 1,
      'notValidated': 1
    };

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toEqual('warning');
  });

  it('should get the alert class for an incomplete invalid state', () => {
    // Arrange
    testee.validationResult = {
      'valid': 2,
      'invalid': 1,
      'info': 1,
      'notValidated': 1
    };

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toEqual('danger');
  });
});
