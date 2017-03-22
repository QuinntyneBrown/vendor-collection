import { fetch } from "../utilities";
import { Vendor } from "./vendor.model";

export class VendorService {
    constructor(private _fetch = fetch) { }

    private static _instance: VendorService;

    public static get Instance() {
        this._instance = this._instance || new VendorService();
        return this._instance;
    }

    public get(): Promise<Array<Vendor>> {
        return this._fetch({ url: "/api/vendor/get", authRequired: true }).then((results: string) => {
            return (JSON.parse(results) as { vendors: Array<Vendor> }).vendors;
        });
    }

    public getById(id): Promise<Vendor> {
        return this._fetch({ url: `/api/vendor/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { vendor: Vendor }).vendor;
        });
    }

    public add(vendor) {
        return this._fetch({ url: `/api/vendor/add`, method: "POST", data: { vendor }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/vendor/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
