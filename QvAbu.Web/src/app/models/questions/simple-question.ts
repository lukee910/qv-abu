import { Question } from './question';
import { SimpleAnswer } from './simple-answer';

export class SimpleQuestion extends Question {
  answers: SimpleAnswer[];
  simpleQuestionType: SimpleQuestionType;
}

export enum SimpleQuestionType {
  singleChoice = 0,
  multipleChoice = 1,
  trueFalse = 2
}
