import { Component, OnInit } from '@angular/core';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Router } from '@angular/router';
import { QuestionnaireConfig } from '../models/questions/questionnaire-config';

@Component({
  selector: 'app-questionnaires',
  templateUrl: './questionnaires.component.html',
  styleUrls: ['./questionnaires.component.scss']
})
export class QuestionnairesComponent implements OnInit {
  questionnaires: QuestionnairePreview[];
  questionsCountOptions: number[] = [0];
  questionsCountSelection = 0;
  selectedQuestionsCount = 0;
  private nameRegex = /[^a-zA-Z0-9,]/;
  private selectedQuestionnaires: {[id: string]: {[revision: number]: boolean}} = {};

  constructor(private questionnairesService: QuestionnairesService, private router: Router) { }

  ngOnInit() {
    this.questionnairesService.getPreviews()
      .subscribe(qps => {
        this.questionnaires = qps;
        this.questionnaires.forEach(qp => {
          this.selectedQuestionnaires[qp.id] = {
            [qp.revision]: false
          };
        });
      });
  }

  selectQuestionnaire(questionnaire: QuestionnairePreview): void {
    this.selectedQuestionnaires[questionnaire.id][questionnaire.revision]
      = !this.selectedQuestionnaires[questionnaire.id][questionnaire.revision];

    this.selectedQuestionsCount = 0;
    this.getSelectedQuestionnaires().forEach(_ => {
      this.selectedQuestionsCount += _.questionsCount;
    }, this);
    this.questionsCountOptions = [];
    if (this.selectedQuestionsCount > 10) {
      this.questionsCountOptions.push(10);

      if (this.selectedQuestionsCount > 25) {
        this.questionsCountOptions.push(25);

        if (this.selectedQuestionsCount > 50) {
          this.questionsCountOptions.push(50);
        }
      }
    }
    this.questionsCountOptions.push(this.selectedQuestionsCount);
    if (this.questionsCountOptions.indexOf(this.questionsCountSelection) === -1) {
      this.questionsCountSelection = this.selectedQuestionsCount;
    }
  }

  getSelectedQuestionnaires(): QuestionnairePreview[] {
    const selected: QuestionnairePreview[] = [];
    Object.keys(this.selectedQuestionnaires).forEach(id => {
      Object.keys(this.selectedQuestionnaires[id]).forEach(revision => {
        if (this.selectedQuestionnaires[id][revision]) {
          selected.push(this.questionnaires.filter(_ => _.id === id && _.revision.toString() === revision)[0]);
        }
      });
    });
    return selected;
  }

  navigate(): void {
    const id = this.uuidv4();
    const selectedQuestionnaires = this.getSelectedQuestionnaires();

    let creationTimes: any;
    try {
      creationTimes = JSON.parse(localStorage.getItem('questionnaire.creationTimes'));
    } catch {
    }
    if (!creationTimes) {
      creationTimes = {};
    }
    creationTimes[id] = new Date().toISOString();
    Object.keys(creationTimes).forEach(_ => {
      const expireDays = 7;
      if (new Date().getTime() - new Date(creationTimes[_]).getTime() > expireDays * 24 * 60 * 60 * 1000) {
        localStorage.removeItem('questionnaire.' + _);
        delete creationTimes[_];
      }
    });
    localStorage.setItem('questionnaire.creationTimes', JSON.stringify(creationTimes));

    const questionnaireConfig = new QuestionnaireConfig();
    questionnaireConfig.questionnaires = selectedQuestionnaires.map(_ => {
      return {
        id: _.id,
        revision: _.revision
      };
    });
    questionnaireConfig.randomizeSeed = Math.floor(Math.random() * 2147483647);
    questionnaireConfig.questionsCount = this.questionsCountSelection;
    localStorage.setItem('questionnaire.' + id, JSON.stringify(questionnaireConfig));
    const title = selectedQuestionnaires.map(_ => _.name)
      .join(', ')
      .split(this.nameRegex)
      .join('_')
      .substr(0, 30);
    this.router.navigate(['questionnaire', id, title]);
  }

  /* tslint:disable */
  uuidv4(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      let r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
  /* tslint:enable */
}
