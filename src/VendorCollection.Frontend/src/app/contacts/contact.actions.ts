import { Contact } from "./contact.model";

export const contactActions = {
    ADD: "[Contact] Add",
    EDIT: "[Contact] Edit",
    DELETE: "[Contact] Delete",
};

export class ContactEvent extends CustomEvent {
    constructor(eventName:string, contact: Contact) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { contact }
        });
    }
}

export class ContactAdd extends ContactEvent {
    constructor(contact: Contact) {
        super(contactActions.ADD, contact);        
    }
}

export class ContactEdit extends ContactEvent {
    constructor(contact: Contact) {
        super(contactActions.EDIT, contact);
    }
}

export class ContactDelete extends ContactEvent {
    constructor(contact: Contact) {
        super(contactActions.DELETE, contact);
    }
}
