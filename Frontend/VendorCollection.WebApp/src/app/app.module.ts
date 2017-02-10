import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { RouterModule  } from '@angular/router';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import "./rxjs-extensions";

import { ConfigurationModule } from "./configuration";
import { CoreModule } from "./core";
import { EvaluationModule } from "./evaluations";
import { ProjectModule } from "./projects";
import { UtilitiesModule } from "./utilities";
import { VendorEvaluationModule } from "./vendor-evaludations";
import { VendorModule } from "./vendors";

import { AppComponent } from './app.component';


import {
    RoutingModule,
    routedComponents
} from "./app-routing.module";

const declarables = [
    AppComponent,
    ...routedComponents
];

const providers = [

];

@NgModule({
    imports: [
        RoutingModule,

        ConfigurationModule,
        CoreModule,
        EvaluationModule,
        ProjectModule,
        UtilitiesModule,
        VendorEvaluationModule,
        VendorModule,

        BrowserModule,
        HttpModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule
    ],
    providers: providers,
    declarations: [declarables],
    exports: [declarables],
    bootstrap: [AppComponent]
})
export class AppModule { }

