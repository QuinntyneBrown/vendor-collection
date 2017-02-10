import { Component, Input, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
    template: require("./vendor-edit-page.component.html"),
    styles: [require("./vendor-edit-page.component.scss")],
    selector: "vendor-edit-page"
})
export class VendorEditPageComponent { 
    constructor( 
        private _router: Router,
        private _activatedRoute: ActivatedRoute
    ) { }    
}
