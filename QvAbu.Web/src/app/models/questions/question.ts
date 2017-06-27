import { RevisionEntity } from '../revision-entity';

export class Question extends RevisionEntity {
  text: string;
  type: QuestionType;
}

export enum QuestionType {
  assignmentQuestion = 0,
  simpleQuestion = 1,
  textQuestion = 2
}
