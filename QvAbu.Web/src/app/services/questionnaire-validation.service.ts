import { EventEmitter, Injectable } from '@angular/core';
import { Guid } from '../models/guid';
import {
  QuestionnaireValidationPhase, ValidationState, ValidationStates,
  ValidationStateToString
} from '../models/validation-message';

@Injectable()
export class QuestionnaireValidationService {
  questionnaireValidationPhaseChange: EventEmitter<QuestionnaireValidationPhase> = new EventEmitter();

  private validationStates: { [id: string]: ValidationState };
  private validationPhase: QuestionnaireValidationPhase = null;

  constructor() { }

  initQuestionnaire(ids: Guid[]): void {
    this.validationStates = {};
    ids.forEach(_ => {
      this.validationStates[_.toString()] = ValidationState.notValidated;
    });

    this.setValidationPhase(QuestionnaireValidationPhase.init);
  }

  setQuestionState(id: Guid, state: ValidationState): void {
    if (this.validationPhase === null) {
      return;
    }

    this.validationStates[id.toString()] = state;
  }

  validate(): { [state: string]: number } {

    const result = {};
    ValidationStates.forEach(_ => {
      result[ValidationStateToString(_)] = 0;
    });

    if (!this.validationStates) {
      return result;
    }

    Object.keys(this.validationStates).forEach(id => {
      result[ValidationStateToString(this.validationStates[id])]++;
    });

    this.setValidationPhase(QuestionnaireValidationPhase.validationLocked);

    return result;
  }

  private setValidationPhase(phase: QuestionnaireValidationPhase): void {
    this.validationPhase = phase;
    this.questionnaireValidationPhaseChange.emit(phase);
  }
}
