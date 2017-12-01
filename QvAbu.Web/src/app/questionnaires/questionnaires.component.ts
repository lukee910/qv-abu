import { Component, OnInit } from '@angular/core';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-questionnaires',
  templateUrl: './questionnaires.component.html',
  styleUrls: ['./questionnaires.component.scss']
})
export class QuestionnairesComponent implements OnInit {
  questionnaires: QuestionnairePreview[];
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
      creationTimes = {};
    }
    creationTimes[id] = new Date().toISOString();
    localStorage.setItem('questionnaire.creationTimes', JSON.stringify(creationTimes));

    localStorage.setItem('questionnaire.' + id, JSON.stringify(selectedQuestionnaires));
    const title = selectedQuestionnaires.map(_ => _.name)
      .join(', ')
      .replace(this.nameRegex, '_')
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
