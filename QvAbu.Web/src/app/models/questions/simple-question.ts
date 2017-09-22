import { Question, QuestionType } from './question';
import { SimpleAnswer } from './simple-answer';
import { Guid } from '../guid';

export class SimpleQuestion extends Question {
  answers: SimpleAnswer[];
  simpleQuestionType: SimpleQuestionType;

  constructor(id: Guid, revision: number) {
    super();
    this.id = id;

    this.type = QuestionType.simpleQuestion;
  }
}

export enum SimpleQuestionType {
  singleChoice = 0,
  multipleChoice = 1,
  trueFalse = 2
}
