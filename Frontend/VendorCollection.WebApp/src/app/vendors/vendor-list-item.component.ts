import { Component, ChangeDetectionStrategy, Input, OnInit } from "@angular/core";

@Component({
    template: require("./vendor-list-item.component.html"),
    styles: [require("./vendor-list-item.component.scss")],
    selector: "vendor-list-item",
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorListItemComponent implements OnInit { 
    ngOnInit() {

    }
}
