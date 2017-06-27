import { AssignmentQuestionComponent } from './assignment-question.component';

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
});
