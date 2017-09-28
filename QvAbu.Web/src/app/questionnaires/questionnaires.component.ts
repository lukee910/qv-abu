import { Component, OnInit } from '@angular/core';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';
import { QuestionnairesService } from '../services/questionnaires.service';

@Component({
  selector: 'app-questionnaires',
  templateUrl: './questionnaires.component.html',
  styleUrls: ['./questionnaires.component.scss']
})
export class QuestionnairesComponent implements OnInit {
  questionnaires: QuestionnairePreview[];
  nameRegex = /[^a-zA-Z0-9]/;

  constructor(private questionnairesService: QuestionnairesService) { }

  ngOnInit() {
    this.questionnairesService.getPreviews()
      .subscribe(_ => {
        this.questionnaires = _;
      });
  }
}
