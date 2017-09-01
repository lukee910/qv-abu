import { AssignmentQuestionComponent } from './assignment-question.component';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { QuestionType } from '../../models/questions/question';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';

describe('AssignmentQuestionComponent', () => {
  let component: AssignmentQuestionComponent;

  beforeEach(() => {
    component = new AssignmentQuestionComponent();
  });

  it('should convert to char', () => {
    // Arrange
    const int = 1;
    const char = 'b';

    // Act
    const result = component.toChar(int);

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

    component.question = question;

    // Act
    component.setResponseValue({
      id: 'id',
      text: 'text'
    }, answer1);
    component.setResponseValue({
      id: 'id2',
      text: 'text'
    }, answer2);

    // Assert
    expect(component.responses).toEqual(expectedResponses);
  });
});
