import {AbstractControl, NG_VALIDATORS, ValidationErrors, Validator} from '@angular/forms';
import {Directive} from '@angular/core';

@Directive({
  selector: '[appIsValidNetmaskAddr]',
  providers: [{provide: NG_VALIDATORS, useExisting: IsValidNetmaskAddrValidator, multi: true}]
})
export class IsValidNetmaskAddrValidator implements Validator {

  private static validNetmasks = [
    '255.0.0.0',
    '255.240.0.0',
    '255.255.0.0',
    '255.255.240.0',
    '255.255.248.0',
    '255.255.252.0',
    '255.255.254.0',
    '255.255.255.0',
    '255.255.255.128',
    '255.255.255.192',
    '255.255.255.224',
    '255.255.255.240',
    '255.255.255.248',
    '255.255.255.252',
    '255.255.255.254',
    '255.255.255.255',
  ];

  registerOnValidatorChange(fn: () => void): void {
  }

  validate(control: AbstractControl): ValidationErrors | null {

    if (!control.value || control.value === '') {
      return null;
    }

    if (IsValidNetmaskAddrValidator.validNetmasks.find(x => x === control.value)) {
      return null;
    }

    return {IsValidNetmaskAddr: true};
  }

}

