import {Directive} from '@angular/core';
import {AbstractControl, NG_VALIDATORS, ValidationErrors, Validator} from '@angular/forms';

@Directive({
  selector: '[appIsValidMacAddr]',
  providers: [{provide: NG_VALIDATORS, useExisting: IsValidMacAddrValidator, multi: true}]
})
export class IsValidMacAddrValidator implements Validator {
  registerOnValidatorChange(fn: () => void): void {
  }

  validate(control: AbstractControl): ValidationErrors | null {

    if (!control.value || control.value === '') {
      return null;
    }

    if (/^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$/.test(control.value)) {
      return null;
    }
    return {IsValidMacAddr: true};
  }

}
