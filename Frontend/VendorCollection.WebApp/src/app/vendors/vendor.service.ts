import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { Vendor } from "./vendor.model";
import { Observable } from "rxjs";


@Injectable()
export class VendorService {
    constructor(private _http: Http) { }

    public add(entity: Vendor) {
        return this._http
            .post(`${this._baseUrl}/api/vendor/add`, entity)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public get() {
        return this._http
            .get(`${this._baseUrl}/api/vendor/get`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public getById(options: { id: number }) {
        return this._http
            .get(`${this._baseUrl}/api/vendor/getById?id=${options.id}`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public remove(options: { id: number }) {
        return this._http
            .delete(`${this._baseUrl}/api/vendor/remove?id=${options.id}`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public get _baseUrl() { return ""; }

}
