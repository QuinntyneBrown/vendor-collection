import { Contact } from "./contact.model";
import { ContactService } from "./contact.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { VendorService, Vendor } from "../vendors";

const template = require("./contact-edit.component.html");
const styles = require("./contact-edit.component.scss");

export class ContactEditComponent extends HTMLElement {
    constructor(
		private _contactService: ContactService = ContactService.Instance,
        private _router: Router = Router.Instance,
        private _vendorService: VendorService = VendorService.Instance
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

        let promises:Array<Promise<any>> = [this._vendorService.get()];

        if (this.contactId)
            promises.push(this._contactService.getById(this.contactId));              

        const results = await Promise.all(promises);
        
        const vendors: Array<Vendor> = results[0];
        
        for (let i = 0; i < vendors.length; i++) {
            let option = document.createElement("option");
            option.textContent = vendors[i].name;
            option.value = vendors[i].id;
            this._vendorSelectElement.appendChild(option);
        }

        if (this.contactId) {
            const contact: Contact = results[1];                
            this._firstnameInputElement.value = contact.firstname;  
            this._lastnameInputElement.value = contact.lastname; 
            this._titleInputElement.value = contact.title;
            this._phoneNumberInputElement.value = contact.phoneNumber;
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
            vendorId: this._vendorSelectElement.value,
            firstname: this._firstnameInputElement.value,
            lastname: this._lastnameInputElement.value,
            title: this._titleInputElement.value,
            twitter: this._twitterInputElement.value,
            linkedIn: this._linkedinInputElement.value,
            phoneNumber: this._phoneNumberInputElement.value,
            mobile: this._mobileInputElement.value
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


    private get _firstnameInputElement(): HTMLInputElement { return this.querySelector(".contact-firstname") as HTMLInputElement; }
    private get _lastnameInputElement(): HTMLInputElement { return this.querySelector(".contact-lastname") as HTMLInputElement; }
    private get _titleInputElement(): HTMLInputElement { return this.querySelector(".contact-title") as HTMLInputElement; }
    private get _twitterInputElement(): HTMLInputElement { return this.querySelector(".contact-twitter") as HTMLInputElement; }
    private get _linkedinInputElement(): HTMLInputElement { return this.querySelector(".contact-linkedin") as HTMLInputElement; }
    private get _phoneNumberInputElement(): HTMLInputElement { return this.querySelector(".contact-phone-nubmer") as HTMLInputElement; }
    private get _mobileInputElement(): HTMLInputElement { return this.querySelector(".contact-mobile") as HTMLInputElement; }
    private get _vendorSelectElement(): HTMLSelectElement { return this.querySelector("select") as HTMLSelectElement; }

}

customElements.define(`ce-contact-edit`,ContactEditComponent);
