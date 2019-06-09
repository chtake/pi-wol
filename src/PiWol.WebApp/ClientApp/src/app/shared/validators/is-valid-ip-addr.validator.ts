import {AbstractControl, NG_VALIDATORS, ValidationErrors, Validator} from '@angular/forms';
import {Directive} from '@angular/core';

@Directive({
  selector: '[appIsValidIpAddr]',
  providers: [{provide: NG_VALIDATORS, useExisting: IsValidIpAddrValidator, multi: true}]
})
export class IsValidIpAddrValidator implements Validator {
  registerOnValidatorChange(fn: () => void): void {
  }

  validate(control: AbstractControl): ValidationErrors | null {

    if (!control.value || control.value === '') {
      return null;
    }

    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(control.value)) {
      return null;
    }
    return {IsValidIpAddr: true};
  }

}

