import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss']
})
export class QuestionnaireComponent implements OnInit {
  private id: string;

  constructor(private route: ActivatedRoute) {
    this.route.params.subscribe((params: Params) => this.id = params['id']);
  }

  ngOnInit() {
  }

}
