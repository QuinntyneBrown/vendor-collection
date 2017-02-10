import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";

import { ProjectComponent } from './project.component';

const declarables = [ProjectComponent];
const providers = [];

@NgModule({
    imports: [CommonModule],
    exports: [declarables],
    declarations: [declarables],
	providers: providers
})
export class ProjectModule { }
