import { VendorDocument } from "./vendor-document.model";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { Document } from "../documents";

const template = require("./vendor-document-edit.component.html");
const styles = require("./vendor-document-edit.component.scss");

export class VendorDocumentEditComponent extends HTMLElement {
    constructor() {
        super();
        this.onCheck = this.onCheck.bind(this);
        this.onAddOrUpdateClick = this.onAddOrUpdateClick.bind(this);
    }

    static get observedAttributes() {
        return [
            "document",
            "vendor-document",
            "tab-index"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
    private _bind() {
        this._nameElement.innerText = this.document.name;
        rome(this._completionDateElement, { time: false });

        if (this.vendorDocument) {
            this._completionDateElement.value = this.vendorDocument.completionDate;
        }
	}

    private _setEventListeners() {
        this._vendorDocumentSaveButton.addEventListener("click", this.onAddOrUpdateClick);
    }

    private disconnectedCallback() {
    }

    public onCheck() {

    }

    public onAddOrUpdateClick() {
        alert("?");
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "document":
                this.document = JSON.parse(newValue);                
                break;

            case "vendor-document":
                if (newValue)
                    this.vendorDocument = JSON.parse(newValue);
                break;

            case "tab-index":
                this.customTabIndex = newValue;
                break;
        }        
    }

    private document: Document;
    private vendorDocument: VendorDocument;
    private customTabIndex: any;

    private get _nameElement(): HTMLElement { return this.querySelector(".document-name") as HTMLElement; }
    private get _completionDateElement(): HTMLInputElement { return this.querySelector(".vendor-document-completion-date") as HTMLInputElement; }
    private get _vendorDocumentSaveButton(): HTMLElement { return this.querySelector(".vendor-document-save-button") as HTMLElement; }
}

customElements.define(`ce-vendor-document-edit`,VendorDocumentEditComponent);
