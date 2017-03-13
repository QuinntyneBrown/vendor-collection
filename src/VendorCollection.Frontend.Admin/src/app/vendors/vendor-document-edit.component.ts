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
    }

    static get observedAttributes() {
        return [
            "document",
            "vendor-document"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
    private _bind() {
        this._nameElement.innerText = this.document.name;
	}

	private _setEventListeners() {
    }

    private disconnectedCallback() {
    }

    public onCheck() {

    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "document":
                this.document = JSON.parse(newValue);                
                break;
        }        
    }

    private document: Document;

    private get _nameElement(): HTMLElement { return this.querySelector(".document-name") as HTMLElement; }
}

customElements.define(`ce-vendor-document-edit`,VendorDocumentEditComponent);
