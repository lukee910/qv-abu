import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { ApiService } from './api.service';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import { Question } from '../models/questions/question';
import { Observer } from 'rxjs/Observer';
import { Observable } from 'rxjs/Observable';
import { Guid } from '../models/guid';
import { RevisionEntity } from '../models/revision-entity';

@Injectable()
export class QuestionnairesService {
  private previews: QuestionnairePreview[];

  constructor(private apiService: ApiService) { }

  public getPreviews(): Observable<QuestionnairePreview[]> {
    const observable = this.apiService.get('questionnaires/previews');
    observable.subscribe(_ => this.previews = _);
    return observable;
  }

  public getPreview(id: Guid, revision: number): Observable<QuestionnairePreview> {
    if (!this.previews) {
      return Observable.create((o: Observer<QuestionnairePreview>) => {
        this.getPreviews().subscribe(previews => {
          this.getPreview(id, revision).subscribe(p => o.next(p));
        });
      });
    }

    let preview: QuestionnairePreview = null;
    this.previews.forEach(_ => {
      if (_.id === id && _.revision === revision) {
        preview = _;
      }
    });
    return Observable.of(preview);
  }

  getQuestionsForQuestionnaires(questionnaires: RevisionEntity[]): Observable<Question[]> {
    return this.apiService.post('questionnaires/questions', questionnaires);
  }
}
