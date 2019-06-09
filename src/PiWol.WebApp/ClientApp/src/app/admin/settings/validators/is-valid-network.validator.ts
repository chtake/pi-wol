import {Directive} from '@angular/core';
import {AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors} from '@angular/forms';
import {Observable} from 'rxjs';
import {IpAccessService} from '@app/admin/settings/services/ip-access.service';
import {IpNetworkModel} from '@app/admin/settings/models/ip-network-model';

@Directive({
  selector: '[appIsValidNetwork]',
  providers: [{provide: NG_ASYNC_VALIDATORS, useExisting: IsValidNetworkValidator, multi: true}]
})
export class IsValidNetworkValidator implements AsyncValidator {

  private validatorTimeout;

  constructor(private service: IpAccessService) {
  }

  registerOnValidatorChange(fn: () => void): void {
  }

  validate(control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null>;
  validate(control: AbstractControl): ValidationErrors | null;
  validate(control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> | ValidationErrors | null {

    if (!control.value || control.value === '') {
      return null;
    }

    clearTimeout(this.validatorTimeout);

    return new Promise((resolve, reject) => {
      this.validatorTimeout = setTimeout(() => {
        this.service.check(<IpNetworkModel>{ipNetwork: control.value})
          .subscribe(
            _ => resolve(null),
            _ => resolve({IsValidNetwork: true})
          );
      }, 300);
    });
  }
}
