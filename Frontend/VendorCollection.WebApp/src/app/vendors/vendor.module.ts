import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { VendorComponent } from './vendor.component';

const declarables = [VendorComponent];
const providers = [];

@NgModule({
    imports: [CommonModule],
    exports: [declarables],
    declarations: [declarables],
	providers: providers
})
export class VendorModule { }
