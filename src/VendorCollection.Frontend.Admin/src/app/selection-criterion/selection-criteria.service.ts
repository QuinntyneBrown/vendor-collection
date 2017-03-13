import { fetch } from "../utilities";
import { SelectionCriteria } from "./selection-criteria.model";

export class SelectionCriteriaService {
    constructor(private _fetch = fetch) { }

    private static _instance: SelectionCriteriaService;

    public static get Instance() {
        this._instance = this._instance || new SelectionCriteriaService();
        return this._instance;
    }

    public get(): Promise<Array<SelectionCriteria>> {
        return this._fetch({ url: "/api/selectioncriteria/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { selectionCriterion: Array<SelectionCriteria> }).selectionCriterion;
        });
    }

    public getById(id): Promise<SelectionCriteria> {
        return this._fetch({ url: `/api/selectioncriteria/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { selectionCriteria: SelectionCriteria }).selectionCriteria;
        });
    }

    public add(selectionCriteria) {
        return this._fetch({ url: `/api/selectioncriteria/add`, method: "POST", data: { selectionCriteria }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/selectioncriteria/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
