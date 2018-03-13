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

  const presetId = 'presetId';
  const config = {
    questionnaires: [
      {id: 'questionnaire1', revision: 1},
      {id: 'questionnaire2', revision: 2}
    ],
    randomizeSeed: 1234,
    questionsCount: 4321
  };

  beforeEach(() => {
    questionnaireService = new QuestionnaireServiceFake();
    validationService = new QuestionnaireValidationServiceFake();
    window = new WindowFake();
    spyOn(localStorage, 'getItem');
    (<any>localStorage.getItem).and.returnValue(JSON.stringify(config));

    testee = new QuestionnaireComponent(<ActivatedRoute><any>{
      params: Observable.of({
        presetId: presetId
      })
    }, <any>questionnaireService, <any>validationService, <any>window);
  });

  it('should load the questionnaires from localStorage and route params', () => {
    // Arrange

    // Act
    // Instantiate, in beforeEach

    // Assert
    expect(localStorage.getItem).toHaveBeenCalledWith('questionnaire.' + presetId);
    expect(testee.config).toEqual(config);
  });

  it('should load the questions and preview', () => {
    // Arrange
    const questions: Question[] = [new TextQuestion('id1', 1)];
    const previews: QuestionnairePreview[] = [{
      revision: 0,
      id: 'id',
      name: 'name1',
      questionsCount: 2,
      tags: ['tag1', 'tag2']
    }, {
      revision: 0,
      id: 'id2',
      name: 'name2',
      questionsCount: 1,
      tags: ['tag1']
    }];
    questionnaireService.getPreview.and.returnValues(Observable.of(previews[0]), Observable.of(previews[1]));
    questionnaireService.getQuestionsForQuestionnaires.and.returnValue(Observable.of(questions));

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.questions).toEqual(questions);
    expect(testee.previews).toEqual(previews);

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
