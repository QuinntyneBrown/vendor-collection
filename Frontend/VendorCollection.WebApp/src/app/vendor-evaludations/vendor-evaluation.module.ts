import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { VendorEvaluationComponent } from './vendor-evaluation.component';

const declarables = [VendorEvaluationComponent];
const providers = [];

@NgModule({
    imports: [CommonModule],
    exports: [declarables],
    declarations: [declarables],
	providers: providers
})
export class VendorEvaluationModule { }
