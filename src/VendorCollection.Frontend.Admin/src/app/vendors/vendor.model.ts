import { VendorSelectionCriteria } from "./vendor-selection-criteria.model";
import { VendorDocument } from "./vendor-document.model";
import { Contact } from "../contacts";

export class Vendor { 
	public id:any;
    public name: string;
    public vendorDocuments: Array<VendorDocument> = [];
    public vendorSelectionCriterion: Array<VendorSelectionCriteria> = [];
    public contacts: Array<Contact> = [];
}
