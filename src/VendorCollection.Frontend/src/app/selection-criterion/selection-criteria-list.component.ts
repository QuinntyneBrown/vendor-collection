import { SelectionCriteria } from "./selection-criteria.model";
import { SelectionCriteriaService } from "./selection-criteria.service";

const template = require("./selection-criteria-list.component.html");
const styles = require("./selection-criteria-list.component.scss");

export class SelectionCriteriaListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _selectionCriterionervice: SelectionCriteriaService = SelectionCriteriaService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const selectionCriterion: Array<SelectionCriteria> = await this._selectionCriterionervice.get();
        for (var i = 0; i < selectionCriterion.length; i++) {
			let el = this._document.createElement(`ce-selection-criteria-item`);
			el.setAttribute("entity", JSON.stringify(selectionCriterion[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-selection-criteria-list", SelectionCriteriaListComponent);
