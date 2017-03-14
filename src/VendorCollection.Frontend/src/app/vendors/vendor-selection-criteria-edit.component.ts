import { VendorSelectionCriteria } from "./vendor-selection-criteria.model";
import { SelectionCriteria } from "../selection-criterion";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { VendorSelectionCriteriaAddOrUpdateEvent } from "./vendor.actions";

const template = require("./vendor-selection-criteria-edit.component.html");
const styles = require("./vendor-selection-criteria-edit.component.scss");

export class VendorSelectionCriteriaEditComponent extends HTMLElement {
    constructor() {
        super();
        this.onAddOrUpdateClick = this.onAddOrUpdateClick.bind(this);
    }

    static get observedAttributes() {
        return [
            "selection-criteria",
            "vendor-selection-criteria"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
    private _bind() {
        this._nameElement.innerText = this.selectionCriteria.name;
	}

	private _setEventListeners() {

    }

    private disconnectedCallback() {

    }

    public onAddOrUpdateClick() {
        const selectionCriteria = {
            selectionCriteria: this.selectionCriteria,
            id: this.selectionCriteria.id
        } as VendorSelectionCriteria;

        this.dispatchEvent(new VendorSelectionCriteriaAddOrUpdateEvent(selectionCriteria));
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "selection-criteria":
                this.selectionCriteria = JSON.parse(newValue);
                break;
        }
    }

    private selectionCriteria: SelectionCriteria;

    private get _nameElement(): HTMLElement { return this.querySelector(".selection-criteria-name") as HTMLElement; }
}

customElements.define(`ce-vendor-selection-criteria-edit`,VendorSelectionCriteriaEditComponent);
