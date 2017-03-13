import { Vendor } from "./vendor.model";
import { VendorService } from "./vendor.service";

const template = require("./vendor-list.component.html");
const styles = require("./vendor-list.component.scss");

export class VendorListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _vendorService: VendorService = VendorService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const vendors: Array<Vendor> = await this._vendorService.get();
        for (var i = 0; i < vendors.length; i++) {
			let el = this._document.createElement(`ce-vendor-item`);
			el.setAttribute("entity", JSON.stringify(vendors[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-vendor-list", VendorListComponent);
