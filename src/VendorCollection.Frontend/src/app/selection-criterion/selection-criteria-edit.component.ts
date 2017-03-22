import { SelectionCriteria } from "./selection-criteria.model";
import { SelectionCriteriaService } from "./selection-criteria.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";


const template = require("./selection-criteria-edit.component.html");
const styles = require("./selection-criteria-edit.component.scss");

export class SelectionCriteriaEditComponent extends HTMLElement {
    constructor(
		private _selectionCriterionervice: SelectionCriteriaService = SelectionCriteriaService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["selection-criteria-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.selectionCriteriaId ? "Edit Selection Criteria": "Create Selection Criteria";

        this.htmlDescriptionEditor = new EditorComponent(this._descriptionInputElement);

        if (this.selectionCriteriaId) {
            const selectionCriteria: SelectionCriteria = await this._selectionCriterionervice.getById(this.selectionCriteriaId);                
            this._nameInputElement.value = selectionCriteria.name;            
            this.htmlDescriptionEditor.setHTML(selectionCriteria.description);  
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
        const selectionCriteria = {
            id: this.selectionCriteriaId,
            name: this._nameInputElement.value,
            description: this.htmlDescriptionEditor.text
        } as SelectionCriteria;
        
        await this._selectionCriterionervice.add(selectionCriteria);
		this._router.navigate(["selection-criteria","list"]);
    }

    public async onDelete() {        
        await this._selectionCriterionervice.remove({ id: this.selectionCriteriaId });
		this._router.navigate(["selection-criteria","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["selection-criteria", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "selection-criteria-id":
                this.selectionCriteriaId = newValue;
				break;
        }        
    }

    public htmlDescriptionEditor: EditorComponent;

    public selectionCriteriaId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".selection-criteria-name") as HTMLInputElement; }
    private get _descriptionInputElement(): HTMLInputElement { return this.querySelector(".selection-criteria-description") as HTMLInputElement; }
}

customElements.define(`ce-selection-criteria-edit`,SelectionCriteriaEditComponent);
