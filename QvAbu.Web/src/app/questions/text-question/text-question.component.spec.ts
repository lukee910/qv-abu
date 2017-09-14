import { TextQuestionComponent } from './text-question.component';
import { QuestionnaireValidationServiceFake } from '../../../fakes';
import { TextQuestion } from '../../models/questions/text-question';
import { QuestionnaireValidationPhase, ValidationMessage, ValidationState } from '../../models/validation-message';
import { TextAnswer } from '../../models/questions/text-answer';

describe('TextQuestionComponent', () => {
  let testee: TextQuestionComponent;
  let validationServiceFake: QuestionnaireValidationServiceFake;

  beforeEach(() => {
    validationServiceFake = new QuestionnaireValidationServiceFake();
    testee = new TextQuestionComponent(<any>validationServiceFake);
  });

  it('should set the state of the question to "info" initially', () => {
    // Arrange
    testee.question = new TextQuestion('id1');

    // Act
    testee.ngOnInit();

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith('id1', ValidationState.info);
  });

  it('should set the state of the question to "info" when questionnaire initialized', () => {
    // Arrange
    testee.question = new TextQuestion('id2');
    const onEmitFn = validationServiceFake.questionnaireValidationPhaseChange.subscribeCallers[0];

    // Act
    onEmitFn(QuestionnaireValidationPhase.init);

    // Assert
    expect(validationServiceFake.setQuestionState).toHaveBeenCalledWith('id2', ValidationState.info);
    expect(testee.isValidationLocked()).toBeFalsy();
  });

  it('should validation lock and set message when questionnaire is validated', () => {
    // Arrange
    testee.question = new TextQuestion('id2');
    testee.question.answer = new TextAnswer();
    testee.question.answer.text = 'answer';
    const onEmitFn = validationServiceFake.questionnaireValidationPhaseChange.subscribeCallers[0];
    const expectedMessage = new ValidationMessage('answer', ValidationState.info);

    // Act
    onEmitFn(QuestionnaireValidationPhase.validationLocked);

    // Assert
    expect(validationServiceFake.setQuestionState).not.toHaveBeenCalled();
    expect(testee.isValidationLocked()).toBeTruthy();
    expect(testee.getValidationMessage()).toEqual(expectedMessage);
  });
});
