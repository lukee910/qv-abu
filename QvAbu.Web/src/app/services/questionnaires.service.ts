import { Injectable } from '@angular/core';
import { QuestionnairePreview } from '../models/questionnaire-preview';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class QuestionnairesService {

  constructor() { }

  public getPreviews(): Observable<QuestionnairePreview[]> { return null; }

}
