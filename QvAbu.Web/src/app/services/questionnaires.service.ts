import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { Observable } from 'rxjs/Observable';
import { ApiService } from './api.service';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import { Question } from '../models/questions/question';

@Injectable()
export class QuestionnairesService {
  private previews: QuestionnairePreview[];

  constructor(private apiService: ApiService) { }

  public getPreviews(): Observable<QuestionnairePreview[]> {
    return this.apiService.get('questionnaires/previews')
                          .map((_: Response) => this.previews = _.json());
  }

  public getPreview(id: string, revision: number): Observable<QuestionnairePreview> {
    let preview: QuestionnairePreview = null;
    this.previews.forEach(_ => {
      if (_.id === id && _.revision === revision) {
        preview = _;
      }
    });
    return Observable.of(preview);
  }

  getQuestionsForQuestionnaire(id: string, revision: number): Observable<Question[]> {
    return this.apiService.get('questionnaires/' + id + '/' + revision + '/questions')
                          .map((_: Response) => _.json());
  }
}
