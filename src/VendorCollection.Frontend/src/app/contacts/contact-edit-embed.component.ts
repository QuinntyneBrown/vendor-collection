import { Contact } from "./contact.model";
import { EditorComponent } from "../shared";
import { ContactAdd, ContactEdit, ContactDelete } from "./contact.actions";

const template = require("./contact-edit-embed.component.html");
const styles = require("./contact-edit-embed.component.scss");

export class ContactEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
			"contact",
			"contact-id"
		];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.contact ? "Edit Contact": "Create Contact";

        if (this.contact) {                
			this._firtnameInputElement.value = this.contact.firstname;  
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
		this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
		this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        //const contact = {
        //    id: this.contact != null ? this.contact.id : null,
        //    name: this._nameInputElement.value
        //} as Contact;
		
		//this.dispatchEvent(new ContactSaveEvent(contact)); 	       
    }

    public onDelete() {        
        //const contact = {
        //    id: this.contact != null ? this.contact.id : null,
        //    name: this._nameInputElement.value
        //} as Contact;

        //this.dispatchEvent(new ContactDeleteEvent(contact)); 		
    }

	public contact: Contact;

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "contact-id":
                this.contactId = newValue;
                break;
            case "contact":
                this.contact = JSON.parse(newValue);
                if (this.parentNode) {
                    this.contactId = this.contact.id;
                    this._firtnameInputElement.value = this.contact.firstname != undefined ? this.contact.firstname : "";
                    this._titleElement.textContent = this.contactId ? "Edit contact" : "Create contact";
                }
                break;
        }           
    }


    public contactId: any;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _firtnameInputElement(): HTMLInputElement { return this.querySelector(".contact-name") as HTMLInputElement;}
}

customElements.define(`ce-contact-edit-embed`,ContactEditEmbedComponent);
