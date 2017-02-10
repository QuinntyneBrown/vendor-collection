import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService, LoginPageComponent } from "./core";

import { VendorListPageComponent } from "./vendors";


export const routes: Routes = [
    {
        path: '',
        component: VendorListPageComponent,
        canActivate: [AuthGuardService]
    },
    {
        path: "login",
        component: LoginPageComponent
    }
];

export const RoutingModule = RouterModule.forRoot([
    ...routes
]);

export const routedComponents = [
    LoginPageComponent,
    VendorListPageComponent
];