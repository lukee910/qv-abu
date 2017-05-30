import Spy = jasmine.Spy;

export class QuestionnaireServiceFake {
  getPreviews: Spy;

  constructor() {
    this.getPreviews = jasmine.createSpy('getPreviews');
  }
}
