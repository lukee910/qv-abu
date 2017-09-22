import { SimpleQuestionComponent } from './simple-question.component';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';
import { SimpleAnswer } from '../../models/questions/simple-answer';
import { SimpleResponseAnswer } from '../../models/questions/response-answer';
import { QuestionType } from '../../models/questions/question';
import { QuestionnaireValidationPhase, ValidationMessage, ValidationState } from '../../models/validation-message';
import { QuestionnaireValidationServiceFake } from '../../../fakes';

describe('SimpleQuestionComponent', () => {
  let testee: SimpleQuestionComponent;
  let validationServiceFake: QuestionnaireValidationServiceFake;

  beforeEach(() => {
    validationServiceFake = new QuestionnaireValidationServiceFake();
    testee = new SimpleQuestionComponent(<any>validationServiceFake);
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    testee.question = new SimpleQuestion('questionId');
    testee.question.answers = [];
    testee.question.simpleQuestionType = SimpleQuestionType.singleChoice;

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.subtitle).toBe('Kreuzen sie die zutreffende Aussage an.');
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    testee.question = new SimpleQuestion('questionId');
    testee.question.answers = [<SimpleAnswer>{
      text: 'text',
      id: 'id',
      isCorrect: false
    }, <SimpleAnswer>{
      text: 'text',
      id: 'id',
      isCorrect: true
    }];
    testee.question.simpleQuestionType = SimpleQuestionType.multipleChoice;

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.subtitle).toBe('Kreuzen sie die zutreffenden 1 Aussagen an.');
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    testee.question = new SimpleQuestion('questionId');
    testee.question.answers = [];
    testee.question.simpleQuestionType = SimpleQuestionType.trueFalse;

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.subtitle).toBe('Kreuzen sie die zutreffenden Aussagen an.');
  });

  it('should init the responses', () => {
    // Arrange
    testee.question = new SimpleQuestion('questionId');
    const answers = [{
      id: 'id1',
      isCorrect: false,
      text: 'text1'
    }, {
      id: 'id2',
      isCorrect: true,
      text: 'text2'
    }];
    testee.question.answers = answers;
    testee.question.simpleQuestionType = SimpleQuestionType.trueFalse;

    // Act
    testee.ngOnInit();

    // Assert
    expect(testee.responses).toEqual([new SimpleResponseAnswer(answers[0]), new SimpleResponseAnswer(answers[1])]);
  });

  it('should set question valid on validate', () => {
    // Arrange
    const answers: SimpleAnswer[] = [{
      id: 'id1',
      text: 'text1',
      isCorrect: false
    }, {
      id: 'id2',
      text: 'text2',
      isCorrect: true
    }];
    testee.question = {
      id: 'id',
      revision: 1,
      text: 'text',
      type: QuestionType.simpleQuestion,
      simpleQuestionType: undefined,
      answers: answers
    };
    testee.responses = [{
      value: false,
      answer: null
    }, {
      value: true,
      answer: null
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.valid);
    expect(testee.validationMessage).toEqual(new ValidationMessage('Richtige Antwort', ValidationState.valid));
  });

  it('should set question invalid on validate', () => {
    // Arrange
    const answers: SimpleAnswer[] = [{
      id: 'id1',
      text: 'text1',
      isCorrect: false
    }, {
      id: 'id2',
      text: 'text2',
      isCorrect: true
    }];
    testee.question = {
      id: 'id',
      revision: 1,
      text: 'text',
      type: QuestionType.simpleQuestion,
      simpleQuestionType: undefined,
      answers: answers
    };
    testee.responses = [{
      value: true,
      answer: null
    }, {
      value: true,
      answer: null
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.invalid);
    expect(testee.validationMessage).toEqual(new ValidationMessage('Falsche Antwort', ValidationState.invalid));
  });

  it('should validation lock and set message when questionnaire is validated', () => {
    // Arrange
    const onEmitFn = validationServiceFake.questionnaireValidationPhaseChange.subscribeCallers[0];
    expect(testee.isValidationLocked()).toBeFalsy();

    // Act
    onEmitFn(QuestionnaireValidationPhase.validationLocked);

    // Assert
    expect(validationServiceFake.setQuestionState).not.toHaveBeenCalled();
    expect(testee.isValidationLocked()).toBeTruthy();
  });
});
