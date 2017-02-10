import { EvalutationCriteriaItem } from "./evalutation-criteria-item.model";

export class Evalutation { 
	public id:any;
    public name: string;
    public criteria: Array<EvalutationCriteriaItem> = [];
}
