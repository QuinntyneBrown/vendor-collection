import { Vendor } from "./vendor.model";
import { VendorService } from "./vendor.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { SelectionCriteriaService, SelectionCriteria } from "../selection-criterion";
import { DocumentService, Document } from "../documents";
import { createElement } from "../utilities";


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
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["vendor-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
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
            var el = createElement(`<div class="checkbox-field"><input type="checkbox" /><span>${documents[i].name}</span></div>`);
            this._documentsTabElement.appendChild(el);
        }

        for (let i = 0; i < selectionCriterion.length; i++) {
            var el = createElement(`<div class="checkbox-field"><input type="checkbox" /><span>${selectionCriterion[i].name}</span></div>`);
            this._evaluationTabElement.appendChild(el);
        }

        if (this.vendorId) {
            const vendor: Vendor = results[2];                
			this._nameInputElement.value = vendor.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
		this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
		this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
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
        }        
    }

    public vendorId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".vendor-name") as HTMLInputElement; }
    private get _documentsTabElement(): HTMLElement { return this.querySelector(".documents-tab") as HTMLElement; }
    private get _evaluationTabElement(): HTMLElement { return this.querySelector(".evaluation-tab") as HTMLElement; }
}

customElements.define(`ce-vendor-edit`,VendorEditComponent);
