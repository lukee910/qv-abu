import Spy = jasmine.Spy;
import { QuestionnaireValidationPhase } from './app/models/validation-message';

class Fake {
  protected createSpies(props: string[]) {
    props.forEach(prop => {
      this[prop] = <any>jasmine.createSpy(prop);
    });
  }
}

export class QuestionnaireServiceFake extends Fake {
  getPreviews: Spy = null;
  getPreview: Spy = null;
  getQuestionsForQuestionnaires: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class ApiServiceFake extends Fake {
  get: Spy = null;
  post: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class HttpFake extends Fake {
  get: Spy = null;
  post: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class EventEmitterFake<T> extends Fake {
  emit: Spy = null;
  subscribe: Spy = null;

  subscribeCallers: ((T) => void)[];

  constructor() {
    super();
    this.createSpies(Object.keys(this));

    this.subscribeCallers = [];
    this.subscribe.and.callFake(_ => this.subscribeCallers.push(<any>_));
  }
}

export class QuestionnaireValidationServiceFake extends Fake {
  questionnaireValidationPhaseChange: EventEmitterFake<QuestionnaireValidationPhase>;

  initQuestionnaire: Spy = null;
  setQuestionState: Spy = null;
  validate: Spy = null;

  constructor() {
    super();
    this.createSpies(Object.keys(this));
    this.questionnaireValidationPhaseChange = new EventEmitterFake();
  }
}

export class WindowFake extends Fake {
  scrollTo: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}
