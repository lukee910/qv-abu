import Spy = jasmine.Spy;

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
  getQuestionsForQuestionnaire: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class ApiServiceFake extends Fake {
  get: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class HttpFake extends Fake {
  get: Spy = null;

  constructor () {
    super();
    this.createSpies(Object.keys(this));
  }
}

export class EventEmitterFake extends Fake {
  emit: Spy = null;
  subscribe: Spy = null;

  constructor() {
    super();
    this.createSpies(Object.keys(this));
  }
}
