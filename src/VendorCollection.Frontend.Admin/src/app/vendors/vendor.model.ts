import { VendorSelectionCriteria } from "./vendor-selection-criteria.model";
import { VendorDocument } from "./vendor-document.model";

export class Vendor { 
	public id:any;
    public name: string;
    public vendorDocuments: Array<VendorDocument> = [];
    public vendorSelectionCriterion: Array<VendorSelectionCriteria> = [];
}
