import { Question, QuestionType } from './question';
import { TextAnswer } from './text-answer';
import { Guid } from '../guid';

export class TextQuestion extends Question {
  answer: TextAnswer;

  constructor(id: Guid) {
    super();
    this.id = id;

    this.type = QuestionType.textQuestion;
  }
}
