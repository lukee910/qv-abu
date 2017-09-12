import { Question, QuestionType } from './question';
import { AssignmentOption } from './assignment-option';
import { AssignmentAnswer } from './assignment-answer';
import { Guid } from '../guid';

export class AssignmentQuestion extends Question {
  options: AssignmentOption[];
  answers: AssignmentAnswer[];

  constructor(id: Guid) {
    super();
    this.id = id;

    this.type = QuestionType.assignmentQuestion;
  }
}
