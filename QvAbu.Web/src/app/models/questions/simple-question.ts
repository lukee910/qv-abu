import { Question } from './question';
import { SimpleAnswer } from './simple-answer';

export class SimpleQuestion extends Question {
  answers: SimpleAnswer[];
  isMultipleChoice: boolean;
  isNumberOfAnswersGiven: boolean;
}
