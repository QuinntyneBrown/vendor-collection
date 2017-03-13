import { fetch } from "../utilities";
import { Document } from "./document.model";

export class DocumentService {
    constructor(private _fetch = fetch) { }

    private static _instance: DocumentService;

    public static get Instance() {
        this._instance = this._instance || new DocumentService();
        return this._instance;
    }

    public get(): Promise<Array<Document>> {
        return this._fetch({ url: "/api/document/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { documents: Array<Document> }).documents;
        });
    }

    public getById(id): Promise<Document> {
        return this._fetch({ url: `/api/document/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { document: Document }).document;
        });
    }

    public add(document) {
        return this._fetch({ url: `/api/document/add`, method: "POST", data: { document }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/document/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
