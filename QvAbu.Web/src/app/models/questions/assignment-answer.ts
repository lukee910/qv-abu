import { Answer } from './answer';
import { AssignmentOption } from './assignment-option';

export class AssignmentAnswer extends Answer {
  correctOptionId: string;
  correctOption: AssignmentOption;
}
