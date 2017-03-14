import { Contact } from "./contact.model";
import { ContactService } from "./contact.service";

const template = require("./contact-list.component.html");
const styles = require("./contact-list.component.scss");

export class ContactListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _contactService: ContactService = ContactService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const contacts: Array<Contact> = await this._contactService.get();
        for (var i = 0; i < contacts.length; i++) {
			let el = this._document.createElement(`ce-contact-item`);
			el.setAttribute("entity", JSON.stringify(contacts[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-contact-list", ContactListComponent);
