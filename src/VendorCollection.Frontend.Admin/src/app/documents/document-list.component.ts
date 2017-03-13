import { Document as DocumentModel } from "./document.model";
import { DocumentService } from "./document.service";

const template = require("./document-list.component.html");
const styles = require("./document-list.component.scss");

export class DocumentListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _documentService: DocumentService = DocumentService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const documents: Array<DocumentModel> = await this._documentService.get();
        for (var i = 0; i < documents.length; i++) {
			let el = this._document.createElement(`ce-document-item`);
			el.setAttribute("entity", JSON.stringify(documents[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-document-list", DocumentListComponent);