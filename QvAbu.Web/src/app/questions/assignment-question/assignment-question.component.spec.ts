import { AssignmentQuestionComponent } from './assignment-question.component';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { AssignmentOption } from '../../models/questions/assignment-option';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';
import { QuestionType } from '../../models/questions/question';

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
    const expectedResponses: AssignmentResponseAnswer[] = [];

    component.question = question;

    // Act
    component.setResponseValue(new AssignmentOption(), new AssignmentAnswer());

    // Assert
    expect(component.responses).toEqual(expectedResponses);
  });
});
