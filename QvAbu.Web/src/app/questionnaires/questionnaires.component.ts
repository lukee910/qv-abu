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
  readonly colors: string[] = ['#4678B4', '#B48D46', '#142334', '#674A14', '#325681'];

  questionnaires: QuestionnairePreview[];
  questionsCountOptions: number[] = [0];
  questionsCountSelection = 0;
  selectedQuestionsCount = 0;
  nameRegex = /[^a-zA-Z0-9,]/;
  selectedQuestionnaires: {[id: string]: {[revision: number]: boolean}} = {};

  tags: {id: string, isSelected: boolean}[] = [];
  tagColours: {[id: string]: string} = {};

  constructor(private questionnairesService: QuestionnairesService, private router: Router) { }

  ngOnInit() {
    this.questionnairesService.getPreviews()
      .subscribe(qps => {
        this.questionnaires = qps;
        this.questionnaires.forEach(qp => {
          this.selectedQuestionnaires[qp.id] = {
            [qp.revision]: false
          };
          qp.tags.forEach(tag => {
            if (this.tags.filter(t => t.id === tag).length === 0) {
              this.tags.push({
                id: tag,
                isSelected: false
              });
            }

            const keys = Object.keys(this.tagColours);
            if (keys.indexOf(tag) === -1) {
              this.tagColours[tag] = this.colors[keys.length % this.colors.length];
            }
          });
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

  getQuestionnairesToShow(): QuestionnairePreview[] {
    if (!this.questionnaires) {
      return;
    }

    return this.questionnaires.filter(q => {
      return q.tags.filter(qt => {
        return this.tags.filter(t => t.id === qt && t.isSelected).length > 0;
      }).length > 0;
    });
  }

  cleanupNotShownQuestionnairesSelection(): void {
    const selected = this.getSelectedQuestionnaires();
    const shown = this.getQuestionnairesToShow();

    selected.forEach(sq => {
      if (shown.indexOf(sq) === -1) {
        this.selectQuestionnaire(sq);
      }
    });
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
