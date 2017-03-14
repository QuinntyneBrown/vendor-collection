import { Contact } from "./contact.model";
import { ContactService } from "./contact.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./contact-edit.component.html");
const styles = require("./contact-edit.component.scss");

export class ContactEditComponent extends HTMLElement {
    constructor(
		private _contactService: ContactService = ContactService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["contact-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.contactId ? "Edit Contact": "Create Contact";

        if (this.contactId) {
            const contact: Contact = await this._contactService.getById(this.contactId);                
			this._nameInputElement.value = contact.name;  
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
        const contact = {
            id: this.contactId,
            name: this._nameInputElement.value
        } as Contact;
        
        await this._contactService.add(contact);
		this._router.navigate(["contact","list"]);
    }

    public async onDelete() {        
        await this._contactService.remove({ id: this.contactId });
		this._router.navigate(["contact","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["contact", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "contact-id":
                this.contactId = newValue;
				break;
        }        
    }

    public contactId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".contact-name") as HTMLInputElement;}
}

customElements.define(`ce-contact-edit`,ContactEditComponent);