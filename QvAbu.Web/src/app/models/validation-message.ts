export class ValidationMessage {
  constructor(public text: string, public validationState: ValidationState) { }
}

export enum QuestionnaireValidationPhase {
  init,
  validationLocked
}

export enum ValidationState {
  valid,
  invalid,
  info,
  notValidated
}

export const ValidationStates = [
  ValidationState.valid,
  ValidationState.invalid,
  ValidationState.info,
  ValidationState.notValidated
];

export function ValidationStateToString(state: ValidationState): string {
  switch (state) {
    case ValidationState.valid:
      return 'valid';
    case ValidationState.info:
      return 'info';
    case ValidationState.invalid:
      return 'invalid';
    default:
      return 'notValidated';
  }
}
