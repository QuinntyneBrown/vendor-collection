import { Injectable } from '@angular/core';
import { ConnectionBackend, RequestOptions, RequestOptionsArgs, Http, Request, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { Storage } from "../utilities";

@Injectable()
export class OAuthHttpService extends Http {

    constructor(backend: ConnectionBackend, defaultOptions: RequestOptions, private _storage: Storage) {
        super(backend, defaultOptions);
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        return super.request(url, options);
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response> {        
        return super.get(url, this.setHeaders(options, true));
    }

    post(url: string, body: string, options?: RequestOptionsArgs, addArrayBuffer = false): Observable<Response> {
        return super.post(url, body, this.setHeaders(options, true, addArrayBuffer));
    }

    put(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        return super.put(url, body, this.setHeaders(options));
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        return super.delete(url, this.setHeaders(options));
    }

    setHeaders(options?: RequestOptionsArgs, addJsonHeader = false, addArrayBuffer = false): RequestOptionsArgs {
        if (options == null) {
            options = new RequestOptions();
        }

        if (options.headers == null) {
            options.headers = new Headers();
        }

        let token = this._storage.get({ name: "accessToken" });
        
        if (token) {
            options.headers.append('Authorization', `Bearer ${token}`);
        }

        if (addJsonHeader && options.headers.has('Accept') === false) {
            options.headers.append('Accept', 'application/json');

        }
        return options;
    }
}