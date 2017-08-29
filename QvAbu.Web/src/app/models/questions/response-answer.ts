import { Answer } from './answer';
import { SimpleAnswer } from './simple-answer';
import { AssignmentAnswer } from './assignment-answer';
import { TextAnswer } from './text-answer';
import { Guid } from '../guid';

abstract class ResponseAnswer<TAnswer extends Answer, TResponse> {
  public answer: TAnswer;
  public value: TResponse;
}

export class SimpleResponseAnswer extends ResponseAnswer<SimpleAnswer, boolean> { }
export class TextResponseAnswer extends ResponseAnswer<TextAnswer, string> { }
export class AssignmentResponseAnswer extends ResponseAnswer<AssignmentAnswer, Guid> { }
