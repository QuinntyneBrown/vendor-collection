import { Component, Input, OnInit } from "@angular/core";
import { VendorActions } from "../actions";
import { Router, ActivatedRoute } from "@angular/router";
import { AppStore } from "../store";

@Component({
    template: require("./vendor-edit-page.component.html"),
    styles: [require("./vendor-edit-page.component.scss")],
    selector: "vendor-edit-page"
})
export class VendorEditPageComponent { 
    constructor(private _vendorActions: VendorActions, 
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _store: AppStore
    ) { }

    ngOnInit() {
        this._vendorActions.getById({ id: this._activatedRoute.snapshot.params["id"] });
    }

    public get entity$() {
        return this._store.vendorById$(this._activatedRoute.snapshot.params["id"]);
    }

    public onSubmit($event: any) {
        this._vendorActions.add({
            id: $event.value.id,
            name: $event.value.name
        });

        setTimeout(() => { this._router.navigate(["/vendors"]); }, 0);
        
    }

    public onCancel() {
        setTimeout(() => { this._router.navigate(["/vendors"]); }, 0);
    }
}
