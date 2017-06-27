import { SimpleQuestionComponent } from './simple-question.component';
import { SimpleQuestion } from '../../models/questions/simple-question';
import { QuestionType } from '../../models/questions/question';
import { SimpleAnswer } from '../../models/questions/simple-answer';

describe('SimpleQuestionComponent', () => {
  let component: SimpleQuestionComponent;

  beforeEach(() => {
    component = new SimpleQuestionComponent();
  });

  it('should load the number of correct answers', () => {
    // Arrange
    component.question = <SimpleQuestion>{
      id: 'id',
      revision: 1,
      text: 'text',
      isMultipleChoice: true,
      isNumberOfAnswersGiven: true,
      type: QuestionType.simpleQuestion,
      answers: <SimpleAnswer[]>[{
        text: 'text',
        id: 'id',
        isCorrect: false
      }, {
        text: 'text',
        id: 'id',
        isCorrect: true
      }]
    };

    // Act
    component.ngOnInit();

    // Assert
    expect(component.correctAnswersCount).toBe(1);
  });
});
