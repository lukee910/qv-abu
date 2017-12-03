import { RevisionEntity } from '../revision-entity';

export class QuestionnaireConfig {
  questionnaires: RevisionEntity[];
  randomizeSeed: number;
  questionsCount: number;
}
