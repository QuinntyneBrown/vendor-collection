import { Contact } from "./contact.model";

const template = require("./contact-list-embed.component.html");
const styles = require("./contact-list-embed.component.scss");

export class ContactListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
			"contacts"
		];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {		
        for (let i = 0; i < this.contacts.length; i++) {
			let el = this._document.createElement(`ce-contact-item-embed`);
			el.setAttribute("entity", JSON.stringify(this.contacts[i]));
			this.appendChild(el);
        }	
	}

	contacts:Array<Contact> = [];

	attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "contacts":
                this.contacts = JSON.parse(newValue);
                break;
        }
    }
}

customElements.define("ce-contact-list-embed", ContactListEmbedComponent);
