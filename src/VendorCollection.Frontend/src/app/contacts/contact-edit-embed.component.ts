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
        this.onCreate = this.onCreate.bind(this);
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
			this._firstnameInputElement.value = this.contact.firstname;  
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createElement.removeEventListener("click", this.onCreate);
    }

    public onCreate() {
        this.dispatchEvent(new ContactEdit(new Contact()));
    }

    public onSave() {	
        const contact = {
            id: this.contactId,
            firstname: this._firstnameInputElement.value,
            lastname: this._lastnameInputElement.value,
            title: this._titleInputElement.value,
            twitter: this._twitterInputElement.value,
            linkedIn: this._linkedinInputElement.value,
            phoneNumber: this._phoneNumberInputElement.value,
            mobile: this._mobileInputElement.value
        } as Contact;
         	
        this.dispatchEvent(new ContactAdd(contact)); 	       
    }

    public onDelete() {        
        //const contact = {
        //    id: this.contact != null ? this.contact.id : null,
        //    name: this._nameInputElement.value
        //} as Contact;

        this.dispatchEvent(new ContactDelete(this.contact)); 		
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
                    this._firstnameInputElement.value = this.contact.firstname != undefined ? this.contact.firstname : "";                    
                    this._lastnameInputElement.value = this.contact.lastname != undefined ? this.contact.lastname: "";
                    this._titleInputElement.value = this.contact.title != undefined ? this.contact.title : "";
                    this._phoneNumberInputElement.value = this.contact.phoneNumber != undefined ? this.contact.phoneNumber : "";
                    this._phoneNumberInputElement.value = this.contact.mobile != undefined ? this.contact.mobile : "";
                    this._titleElement.textContent = this.contactId ? "Edit contact" : "Create contact";
                }
                break;
        }           
    }


    public contactId: any;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _createElement(): HTMLElement { return this.querySelector(".contact-create") as HTMLElement; }
    private get _firstnameInputElement(): HTMLInputElement { return this.querySelector(".contact-firstname") as HTMLInputElement; }
    private get _lastnameInputElement(): HTMLInputElement { return this.querySelector(".contact-lastname") as HTMLInputElement; }
    private get _titleInputElement(): HTMLInputElement { return this.querySelector(".contact-title") as HTMLInputElement; }
    private get _twitterInputElement(): HTMLInputElement { return this.querySelector(".contact-twitter") as HTMLInputElement; }
    private get _linkedinInputElement(): HTMLInputElement { return this.querySelector(".contact-linkedin") as HTMLInputElement; }
    private get _phoneNumberInputElement(): HTMLInputElement { return this.querySelector(".contact-phone-nubmer") as HTMLInputElement; }
    private get _mobileInputElement(): HTMLInputElement { return this.querySelector(".contact-mobile") as HTMLInputElement; }
}

customElements.define(`ce-contact-edit-embed`,ContactEditEmbedComponent);
