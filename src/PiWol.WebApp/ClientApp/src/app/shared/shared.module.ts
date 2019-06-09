import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {ClarityModule} from '@clr/angular';
import {IsValidIpAddrValidator} from '@app/shared/validators/is-valid-ip-addr.validator';
import {IsValidMacAddrValidator} from '@app/shared/validators/is-valid-mac-addr-validator.directive';
import {IsValidNetmaskAddrValidator} from '@app/shared/validators/is-valid-netmask.validator';

@NgModule({
  imports: [
    CommonModule, FormsModule, RouterModule, ReactiveFormsModule
  ],
  exports: [
    ClarityModule, CommonModule, FormsModule, ReactiveFormsModule, IsValidIpAddrValidator, IsValidMacAddrValidator, IsValidNetmaskAddrValidator
  ],
  declarations: [IsValidIpAddrValidator, IsValidMacAddrValidator, IsValidNetmaskAddrValidator]
})
export class SharedModule {
}
