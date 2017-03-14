import { VendorDocument } from "./vendor-document.model";
import { VendorSelectionCriteria } from "./vendor-selection-criteria.model";

export const vendorActions = {
    ADD_OR_UPDATE_VENDOR_DOCUMENT: "[Vendor] VendorDocumentAddOrUpdate",
    ADD_OR_UPDATE_VENDOR_SELECTION_CRITERIA: "[Vendor] VendorSelectionCriteriaAddOrUpdate"    
};

export class VendorDocumentAddOrUpdateEvent extends CustomEvent {
    constructor(vendorDocument: VendorDocument ) {
        super(vendorActions.ADD_OR_UPDATE_VENDOR_DOCUMENT, {
            bubbles: true,
            cancelable: true,
            detail: { vendorDocument }
        });
    }
}


export class VendorSelectionCriteriaAddOrUpdateEvent extends CustomEvent {
    constructor(vendorSelectionCriteria: VendorSelectionCriteria) {
        super(vendorActions.ADD_OR_UPDATE_VENDOR_SELECTION_CRITERIA, {
            bubbles: true,
            cancelable: true,
            detail: { vendorSelectionCriteria }
        });
    }
}