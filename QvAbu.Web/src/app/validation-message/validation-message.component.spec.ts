import { ValidationMessageComponent } from './validation-message.component';
import { ValidationMessage, ValidationState } from '../models/validation-message';

describe('ValidationMessageComponent', () => {
  let testee: ValidationMessageComponent;

  beforeEach(() => {
    testee = new ValidationMessageComponent();
  });

  it('should return success for valid', () => {
    // Arrange
    testee.message = new ValidationMessage('msg', ValidationState.valid);

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toBe('success');
  });

  it('should return danger for invalid', () => {
    // Arrange
    testee.message = new ValidationMessage('msg', ValidationState.invalid);

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toBe('danger');
  });

  it('should return warning for notValidated', () => {
    // Arrange
    testee.message = new ValidationMessage('msg', ValidationState.notValidated);

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toBe('warning');
  });

  it('should return info for info', () => {
    // Arrange
    testee.message = new ValidationMessage('msg', ValidationState.info);

    // Act
    const result = testee.getAlertClass();

    // Assert
    expect(result).toBe('info');
  });
});
