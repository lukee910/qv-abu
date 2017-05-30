import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { QuestionnairePreview } from '../models/questionnaire-preview';
import { Observable } from 'rxjs/Observable';
import { ApiService } from './api.service';
import 'rxjs/add/operator/map';

@Injectable()
export class QuestionnairesService {

  constructor(private apiService: ApiService) { }

  public getPreviews(): Observable<QuestionnairePreview[]> {
    return this.apiService.get('questionnaires/previews')
                          .map((_: Response) => _.json());
  }
}
