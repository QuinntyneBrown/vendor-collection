import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { EvaluationComponent } from './evaluation.component';

const declarables = [EvaluationComponent];
const providers = [];

@NgModule({
    imports: [CommonModule],
    exports: [declarables],
    declarations: [declarables],
	providers: providers
})
export class EvaluationModule { }
