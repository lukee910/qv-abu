import { SimpleQuestionComponent } from './simple-question.component';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';
import { SimpleAnswer } from '../../models/questions/simple-answer';

describe('SimpleQuestionComponent', () => {
  let component: SimpleQuestionComponent;

  beforeEach(() => {
    component = new SimpleQuestionComponent();
  });

  it('should set the correct subtitle for single choice', () => {
    // Arrange
    component.question = new SimpleQuestion();
    component.question.answers = [];
    component.question.simpleQuestionType = SimpleQuestionType.singleChoice;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.subtitle).toBe('Kreuzen sie die zutreffende Aussage an.');
  });
  it('should set the correct subtitle for single choice', () => {
    // Arrange
    component.question = new SimpleQuestion();
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
    component.question = new SimpleQuestion();
    component.question.answers = [];
    component.question.simpleQuestionType = SimpleQuestionType.trueFalse;

    // Act
    component.ngOnInit();

    // Assert
    expect(component.subtitle).toBe('Kreuzen sie die zutreffenden Aussagen an.');
  });
});
