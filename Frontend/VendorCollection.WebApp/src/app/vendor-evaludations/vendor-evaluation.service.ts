import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { VendorEvaluation } from "./vendorEvaluation.model";
import { Observable } from "rxjs";


@Injectable()
export class VendorEvaluationService {
    constructor(private _http: Http) { }

    public add(entity: VendorEvaluation) {
        return this._http
            .post(`${this._baseUrl}/api/vendorevaluation/add`, entity)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public get() {
        return this._http
            .get(`${this._baseUrl}/api/vendorevaluation/get`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public getById(options: { id: number }) {
        return this._http
            .get(`${this._baseUrl}/api/vendorevaluation/getById?id=${options.id}`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public remove(options: { id: number }) {
        return this._http
            .delete(`${this._baseUrl}/api/vendorevaluation/remove?id=${options.id}`)
            .map(data => data.json())
            .catch(err => {
                return Observable.of(false);
            });
    }

    public get _baseUrl() { return ""; }

}
