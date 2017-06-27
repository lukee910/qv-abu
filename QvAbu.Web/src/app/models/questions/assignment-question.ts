import { Question } from './question';
import { AssignmentOption } from './assignment-option';
import { AssignmentAnswer } from './assignment-answer';

export class AssignmentQuestion extends Question {
  options: AssignmentOption[];
  answers: AssignmentAnswer[];
}
