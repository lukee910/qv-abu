import { AssignmentQuestionComponent } from './assignment-question.component';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { QuestionType } from '../../models/questions/question';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';
import { AssignmentOption } from '../../models/questions/assignment-option';
import { QuestionnaireValidationPhase, ValidationMessage, ValidationState } from '../../models/validation-message';
import { QuestionnaireValidationServiceFake } from '../../../fakes';

describe('AssignmentQuestionComponent', () => {
  let testee: AssignmentQuestionComponent;
  let validationServiceFake: QuestionnaireValidationServiceFake;

  beforeEach(() => {
    validationServiceFake = new QuestionnaireValidationServiceFake();
    testee = new AssignmentQuestionComponent(<any>validationServiceFake);
  });

  it('should init a validation message', () => {
    // Arrange

    // Act

    // Assert
    expect(testee.validationMessage).toEqual(new ValidationMessage('Nicht beantwortet', ValidationState.notValidated));
  });

  it('should convert to char', () => {
    // Arrange
    const int = 1;
    const char = 'b';

    // Act
    const result = testee.toChar(int);

    // Assert
    expect(result).toBe(char);
  });

  it('should set the response values', () => {
    // Arrange
    const question = <AssignmentQuestion> {
      id: 'id',
      revision: 1,
      text: 'text',
      type: QuestionType.assignmentQuestion,
      answers: [],
      options: []
    };

    const answer1 = <AssignmentAnswer> {
      id: 'answerId',
      text: 'answerText',
      correctOption: undefined,
      correctOptionId: 'otherId'
    };
    const answer2 = <AssignmentAnswer> {
      id: 'otherAnswerId',
      text: 'answerText',
      correctOption: undefined,
      correctOptionId: 'otherId'
    };
    const expectedResponse1 = new AssignmentResponseAnswer();
    expectedResponse1.answer = answer1;
    expectedResponse1.value = 'id';
    const expectedResponse2 = new AssignmentResponseAnswer();
    expectedResponse2.answer = answer2;
    expectedResponse2.value = 'id2';
    const expectedResponses: AssignmentResponseAnswer[] = [expectedResponse1, expectedResponse2];

    testee.question = question;
    spyOn(testee, 'validate').and.stub();

    // Act
    testee.setResponseValue({
      id: 'id',
      text: 'text'
    }, answer1);
    testee.setResponseValue({
      id: 'id2',
      text: 'text'
    }, answer2);

    // Assert
    expect(testee.responses).toEqual(expectedResponses);
    expect(testee.validate).toHaveBeenCalled();
  });

  it('should validate, all options valid', () => {
    // Arrange
    testee.question = new AssignmentQuestion('id');
    const options = [{
      id: 'optId1',
      text: 'optText1'
    }, {
      id: 'optId2',
      text: 'optText2'
    }];
    const answers = [{
      id: 'ansId1',
      text: 'ansText1',
      correctOption: options[1],
      correctOptionId: options[1].id
    }, {
      id: 'ansId2',
      text: 'ansText2',
      correctOption: options[0],
      correctOptionId: options[0].id
    }, {
      id: 'ansId3',
      text: 'ansText3',
      correctOption: options[1],
      correctOptionId: options[1].id
    }];
    testee.question.options = options;
    testee.question.answers = answers;
    testee.responses = [{
      answer: answers[0],
      value: options[1].id
    }, {
      answer: answers[1],
      value: options[0].id
    }, {
      answer: answers[2],
      value: options[1].id
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.valid);
    expect(testee.validationMessage).toEqual(new ValidationMessage('Alle Antworten richtig', ValidationState.valid));
  });

  it('should validate, one response not given', () => {
    // Arrange
    testee.question = new AssignmentQuestion('id');
    const options = [{
      id: 'optId1',
      text: 'optText1'
    }, {
      id: 'optId2',
      text: 'optText2'
    }];
    const answers = [{
      id: 'ansId1',
      text: 'ansText1',
      correctOption: options[0],
      correctOptionId: options[0].id
    }, {
      id: 'ansId2',
      text: 'ansText2',
      correctOption: options[1],
      correctOptionId: options[1].id
    }];
    testee.question.options = options;
    testee.question.answers = answers;
    testee.responses = [{
      answer: answers[0],
      value: options[1].id
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.notValidated);
    expect(testee.validationMessage).toEqual(new ValidationMessage('Nicht komplett beantwortet', ValidationState.notValidated));
  });

  it('should validate, one option wrong', () => {
    // Arrange
    testee.question = new AssignmentQuestion('id');
    const options = [{
      id: 'optId1',
      text: 'optText1'
    }, {
      id: 'optId2',
      text: 'optText2'
    }];
    const answers = [{
      id: 'ansId1',
      text: 'ansText1',
      correctOption: options[1],
      correctOptionId: options[1].id
    }, {
      id: 'ansId2',
      text: 'ansText2',
      correctOption: options[0],
      correctOptionId: options[0].id
    }, {
      id: 'ansId3',
      text: 'ansText3',
      correctOption: options[1],
      correctOptionId: options[1].id
    }];
    testee.question.options = options;
    testee.question.answers = answers;
    testee.responses = [{
      answer: answers[0],
      value: options[1].id
    }, {
      answer: answers[1],
      value: options[0].id
    }, {
      answer: answers[2],
      value: options[0].id
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.invalid);
    expect(testee.validationMessage).toEqual(new ValidationMessage('1 Antwort falsch', ValidationState.invalid));
  });

  it('should validate, three options wrong', () => {
    // Arrange
    testee.question = new AssignmentQuestion('id');
    const options = [{
      id: 'optId1',
      text: 'optText1'
    }, {
      id: 'optId2',
      text: 'optText2'
    }];
    const answers = [{
      id: 'ansId1',
      text: 'ansText1',
      correctOption: options[0],
      correctOptionId: options[0].id
    }, {
      id: 'ansId2',
      text: 'ansText2',
      correctOption: options[1],
      correctOptionId: options[1].id
    }, {
      id: 'ansId3',
      text: 'ansText3',
      correctOption: options[1],
      correctOptionId: options[1].id
    }];
    testee.question.options = options;
    testee.question.answers = answers;
    testee.responses = [{
      answer: answers[0],
      value: options[1].id
    }, {
      answer: answers[1],
      value: options[0].id
    }, {
      answer: answers[2],
      value: options[0].id
    }];

    // Act
    testee.validate();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith(testee.question, ValidationState.invalid);
    expect(testee.validationMessage).toEqual(new ValidationMessage('3 Antworten falsch', ValidationState.invalid));
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

  // TODO: ValidationLocked
});
