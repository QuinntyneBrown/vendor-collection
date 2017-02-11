import { NgModule } from '@angular/core';
import { ConnectionBackend, RequestOptions, RequestOptionsArgs, Http, Request, Response, Headers, XHRBackend, HttpModule } from '@angular/http';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from "./auth.service";
import { AuthGuardService } from "./auth-guard.service";
import { CurrentUserService } from "./current-user.service";
import { LoginRedirectService } from "./login-redirect.service";
import { OAuthHttpService } from "./oauth-http.service";
import { Storage } from "../utilities";

const declarables = [];
const providers = [
    {
        provide: Http,
        useFactory: (backend: XHRBackend, defaultOptions: RequestOptions, storage: Storage) =>
            new OAuthHttpService(backend, defaultOptions, storage),
        deps: [XHRBackend, RequestOptions, Storage]
    },
    OAuthHttpService,
    AuthService,
    AuthGuardService,
    CurrentUserService,
    LoginRedirectService,    
];

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
	providers: providers
})
export class CoreModule { }
