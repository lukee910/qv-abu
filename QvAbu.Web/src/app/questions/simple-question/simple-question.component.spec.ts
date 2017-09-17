import { SimpleQuestionComponent } from './simple-question.component';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';
import { SimpleAnswer } from '../../models/questions/simple-answer';
import { SimpleResponseAnswer } from '../../models/questions/response-answer';
import { QuestionType } from '../../models/questions/question';
import { ValidationState } from '../../models/validation-message';
import { QuestionnaireValidationServiceFake } from '../../../fakes';

describe('SimpleQuestionComponent', () => {
  let component: SimpleQuestionComponent;
  let validationServiceFake: QuestionnaireValidationServiceFake;

  beforeEach(() => {
    validationServiceFake = new QuestionnaireValidationServiceFake();
    component = new SimpleQuestionComponent(<any>validationServiceFake);
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    component.question = new SimpleQuestion('questionId');
    component.question.answers = [];
    component.question.simpleQuestionType = SimpleQuestionType.singleChoice;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.subtitle).toBe('Kreuzen sie die zutreffende Aussage an.');
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    component.question = new SimpleQuestion('questionId');
    component.question.answers = [<SimpleAnswer>{
      text: 'text',
      id: 'id',
      isCorrect: false
    }, <SimpleAnswer>{
      text: 'text',
      id: 'id',
      isCorrect: true
    }];
    component.question.simpleQuestionType = SimpleQuestionType.multipleChoice;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.subtitle).toBe('Kreuzen sie die zutreffenden 1 Aussagen an.');
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    component.question = new SimpleQuestion('questionId');
    component.question.answers = [];
    component.question.simpleQuestionType = SimpleQuestionType.trueFalse;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.subtitle).toBe('Kreuzen sie die zutreffenden Aussagen an.');
  });

  it('should init the responses', () => {
    // Arrange
    component.question = new SimpleQuestion('questionId');
    const answers = [{
      id: 'id1',
      isCorrect: false,
      text: 'text1'
    }, {
      id: 'id2',
      isCorrect: true,
      text: 'text2'
    }];
    component.question.answers = answers;
    component.question.simpleQuestionType = SimpleQuestionType.trueFalse;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.responses).toEqual([new SimpleResponseAnswer(answers[0]), new SimpleResponseAnswer(answers[1])]);
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
    component.question = {
      id: 'id',
      revision: 1,
      text: 'text',
      type: QuestionType.simpleQuestion,
      simpleQuestionType: undefined,
      answers: answers
    };
    component.responses = [{
      value: false,
      answer: null
    }, {
      value: true,
      answer: null
    }];

    // Act
    component.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith('id', ValidationState.valid);
  });
});
