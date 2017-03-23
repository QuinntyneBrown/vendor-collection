import { Vendor } from "./vendor.model";
import { VendorService } from "./vendor.service";
import { EditorComponent, tabsEvents } from "../shared";
import { Router } from "../router";
import { SelectionCriteriaService, SelectionCriteria } from "../selection-criterion";
import { DocumentService, Document } from "../documents";
import { createElement, addOrUpdate } from "../utilities";
import { vendorActions, VendorDocumentAddOrUpdateEvent, VendorSelectionCriteriaAddOrUpdateEvent } from "./vendor.actions";
import { contactActions } from "../contacts";

const template = require("./vendor-edit.component.html");
const styles = require("./vendor-edit.component.scss");

export class VendorEditComponent extends HTMLElement {
    constructor(
        private _documentService: DocumentService = DocumentService.Instance,
        private _selectionCriteriaService: SelectionCriteriaService = SelectionCriteriaService.Instance,
        private _vendorService: VendorService = VendorService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onContactsChanged = this.onContactsChanged.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
        this.onTabSelectedIndexChanged = this.onTabSelectedIndexChanged.bind(this);
        this.onAddOrUpdateVendorDocument = this.onAddOrUpdateVendorDocument.bind(this);
        this.onAddOrUpdateVendorSelectionCriteria = this.onAddOrUpdateVendorSelectionCriteria.bind(this);
    }

    static get observedAttributes() {
        return [
            "vendor-id",
            "tab-index"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;         
        this.tabsElement.setAttribute("custom-tab-index", `${this.customTabIndex}`);   
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.vendorId ? "Edit Vendor": "Create Vendor";

        let promises: Array<Promise<any>> = [
            this._documentService.get(),
            this._selectionCriteriaService.get()
        ];

        if (this.vendorId)
            promises.push(this._vendorService.getById(this.vendorId));

        var results = await Promise.all(promises);

        const documents = results[0] as Array<Document>;
        const selectionCriterion = results[1] as Array<SelectionCriteria>;

        for (let i = 0; i < documents.length; i++) {
            let el = document.createElement(`ce-vendor-document-edit`);
            el.setAttribute("document", JSON.stringify(documents[i]));
            el.setAttribute("vendor-document", null);
            this._documentsTabElement.appendChild(el);
        }

        for (let i = 0; i < selectionCriterion.length; i++) {
            let el = document.createElement(`ce-vendor-selection-criteria-edit`);
            el.setAttribute("selection-criteria", JSON.stringify(selectionCriterion[i]));
            el.setAttribute("vendor-selection-criteria", null);
            this._evaluationTabElement.appendChild(el);
        }

        if (this.vendorId) {
            const vendor: Vendor = results[2];                
            this._nameInputElement.value = vendor.name;  
            this._titleElement.textContent = `Edit Vendor: ${vendor.name}`;            
            this._contactsMasterDetailEmbedElement.setAttribute("contacts", JSON.stringify(vendor.contacts));
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    public onTabSelectedIndexChanged(e) {
        this._router.navigate(["vendor", "edit", this.vendorId, "tab", e.detail.selectedIndex]);
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
        this._contactsMasterDetailEmbedElement.addEventListener(contactActions.CONTACTS_CHANGED, this.onContactsChanged);
        this.addEventListener(tabsEvents.SELECTED_INDEX_CHANGED, this.onTabSelectedIndexChanged);
        this.addEventListener(vendorActions.ADD_OR_UPDATE_VENDOR_DOCUMENT, this.onAddOrUpdateVendorDocument);
        this.addEventListener(vendorActions.ADD_OR_UPDATE_VENDOR_SELECTION_CRITERIA, this.onAddOrUpdateVendorSelectionCriteria);
    }

    public onContactsChanged(e) {
        this.vendor.contacts = e.detail.contacts;
    }

    public async onAddOrUpdateVendorDocument(e: VendorDocumentAddOrUpdateEvent) {
        addOrUpdate({ items: this.vendor.vendorSelectionCriterion, item: e.detail.vendorSelectionCriteria });
        await this._vendorService.add(this.vendor);
    }

    public async onAddOrUpdateVendorSelectionCriteria(e: VendorSelectionCriteriaAddOrUpdateEvent) {
        addOrUpdate({ items: this.vendor.vendorDocuments, item: e.detail.vendorDocument });
        await this._vendorService.add(this.vendor);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
        this._contactsMasterDetailEmbedElement.removeEventListener(contactActions.CONTACTS_CHANGED, this.onContactsChanged);
    }

    public async onSave() {
        const vendor = {
            id: this.vendorId,
            name: this._nameInputElement.value
        } as Vendor;
        
        await this._vendorService.add(vendor);
        this._router.navigate(["vendor","list"]);
    }

    public async onDelete() {        
        await this._vendorService.remove({ id: this.vendorId });
        this._router.navigate(["vendor","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["vendor", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "vendor-id":
                this.vendorId = newValue;
                break;
            case "tab-index":
                this.customTabIndex = newValue;
                break;
        }        
    }

    public vendorId: number;
    public vendor: Vendor;
    public customTabIndex;

    public get tabsElement(): HTMLElement { return this.querySelector("ce-tabs") as HTMLElement; }
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".vendor-name") as HTMLInputElement; }
    private get _documentsTabElement(): HTMLElement { return this.querySelector(".documents-tab") as HTMLElement; }
    private get _evaluationTabElement(): HTMLElement { return this.querySelector(".evaluation-tab") as HTMLElement; }

    private get _contactsMasterDetailEmbedElement(): HTMLElement { return this.querySelector("ce-contact-master-detail-embed") as HTMLElement; }
}

customElements.define(`ce-vendor-edit`,VendorEditComponent);
