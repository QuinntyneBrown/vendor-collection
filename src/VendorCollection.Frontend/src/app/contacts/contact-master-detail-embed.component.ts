import { ContactAdd, ContactDelete, ContactEdit, contactActions } from "./contact.actions";
import { Contact } from "./contact.model";

const template = require("./contact-master-detail-embed.component.html");
const styles = require("./contact-master-detail-embed.component.scss");

export class ContactMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onContactAdd = this.onContactAdd.bind(this);
        this.onContactEdit = this.onContactEdit.bind(this);
        this.onContactDelete = this.onContactDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "contacts"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.contactListElement.setAttribute("contacts", JSON.stringify(this.contacts));
    }

    private _setEventListeners() {
        this.addEventListener(contactActions.ADD, this.onContactAdd);
        this.addEventListener(contactActions.EDIT, this.onContactEdit);
        this.addEventListener(contactActions.DELETE, this.onContactDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(contactActions.ADD, this.onContactAdd);
        this.removeEventListener(contactActions.EDIT, this.onContactEdit);
        this.removeEventListener(contactActions.DELETE, this.onContactDelete);
    }

    public onContactAdd(e) {

        const index = this.contacts.findIndex(o => o.id == e.detail.contact.id);
        const indexBaseOnUniqueIdentifier = this.contacts.findIndex(o => o.name == e.detail.contact.name);

        if (index > -1 && e.detail.contact.id != null) {
            this.contacts[index] = e.detail.contact;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.contacts.length; ++i) {
                if (this.contacts[i].name == e.detail.contact.name)
                    this.contacts[i] = e.detail.contact;
            }
        } else {
            this.contacts.push(e.detail.contact);
        }
        
        this.contactListElement.setAttribute("contacts", JSON.stringify(this.contacts));
        this.contactEditElement.setAttribute("contact", JSON.stringify(new Contact()));
    }

    public onContactEdit(e) {
        this.contactEditElement.setAttribute("contact", JSON.stringify(e.detail.contact));
    }

    public onContactDelete(e) {
        if (e.detail.contact.Id != null && e.detail.contact.Id != undefined) {
            this.contacts.splice(this.contacts.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.contacts.length; ++i) {
                if (this.contacts[i].name == e.detail.contact.name)
                    this.contacts.splice(i, 1);
            }
        }

        this.contactListElement.setAttribute("contacts", JSON.stringify(this.contacts));
        this.contactEditElement.setAttribute("contact", JSON.stringify(new Contact()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "contacts":                
                this.contacts = JSON.parse(newValue);
                    
                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Contact> { return this.contacts; }

    private contacts: Array<Contact> = [];
    public contact: Contact = <Contact>{};
    public get contactEditElement(): HTMLElement { return this.querySelector("ce-contact-edit-embed") as HTMLElement; }
    public get contactListElement(): HTMLElement { return this.querySelector("ce-contact-list-embed") as HTMLElement; }
}

customElements.define(`ce-contact-master-detail-embed`,ContactMasterDetailEmbedComponent);
