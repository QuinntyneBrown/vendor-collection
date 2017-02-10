import { Component, ChangeDetectionStrategy, Input, OnInit } from "@angular/core";
import { VendorActions } from "../actions";
import { AppStore } from "../store";
import { Router } from "@angular/router";   

@Component({
    template: require("./vendor-list-page.component.html"),
    styles: [require("./vendor-list-page.component.scss")],
    selector: "vendor-list-page",
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorListPageComponent implements OnInit {
    constructor(private _vendorActions: VendorActions, private _store: AppStore, private _router:Router) { }

    ngOnInit() {
        this._vendorActions.get(); 
    }
    
}
