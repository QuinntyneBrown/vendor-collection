import { Document as DocumentModel } from "./document.model";
import { DocumentService } from "./document.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./document-edit.component.html");
const styles = require("./document-edit.component.scss");

export class DocumentEditComponent extends HTMLElement {
    constructor(
		private _documentService: DocumentService = DocumentService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["document-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.documentId ? "Edit Document": "Create Document";

        if (this.documentId) {
            const document: DocumentModel = await this._documentService.getById(this.documentId);                
			this._nameInputElement.value = document.name;  
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
        const document = {
            id: this.documentId,
            name: this._nameInputElement.value
        } as DocumentModel;
        
        await this._documentService.add(document);
		this._router.navigate(["document","list"]);
    }

    public async onDelete() {        
        await this._documentService.remove({ id: this.documentId });
		this._router.navigate(["document","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["document", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "document-id":
                this.documentId = newValue;
				break;
        }        
    }

    public documentId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".document-name") as HTMLInputElement;}
}

customElements.define(`ce-document-edit`,DocumentEditComponent);
