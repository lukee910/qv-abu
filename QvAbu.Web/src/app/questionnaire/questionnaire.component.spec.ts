import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionnaireComponent } from './questionnaire.component';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

describe('QuestionnaireComponent', () => {
  let component: QuestionnaireComponent;

  const id = 'id';
  const revision = 0;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
    })
    .compileComponents();
  }));

  beforeEach(() => {
    component = new QuestionnaireComponent(<ActivatedRoute>{
      params: Observable.of({
        id: id,
        revision: revision
      })
    });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
