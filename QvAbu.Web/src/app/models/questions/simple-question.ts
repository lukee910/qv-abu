import { Question } from './question';
import { SimpleAnswer } from './simple-answer';
import { Guid } from '../guid';

export class SimpleQuestion extends Question {
  answers: SimpleAnswer[];
  simpleQuestionType: SimpleQuestionType;

  constructor(id: Guid) {
    super();
    this.id = id;
  }
}

export enum SimpleQuestionType {
  singleChoice = 0,
  multipleChoice = 1,
  trueFalse = 2
}
