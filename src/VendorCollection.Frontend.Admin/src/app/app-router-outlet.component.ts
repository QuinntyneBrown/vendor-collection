import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "vendor-list", authRequired: true },
            { path: "/vendor/edit/:vendorId", name: "vendor-edit", authRequired: true },
            { path: "/vendor/create", name: "vendor-edit", authRequired: true },
            { path: "/vendor/list", name: "vendor-list", authRequired: true },
            { path: "/vendor/edit/:vendorId/tab/:tabIndex", name: "vendor-edit", authRequired: true },
            { path: "/vendor/create/tab/:tabIndex", name: "vendor-edit", authRequired: true },

            { path: "/selection-criteria/edit/:selectionCriteriaId", name: "selection-criteria-edit", authRequired: true },
            { path: "/selection-criteria/create", name: "selection-criteria-edit", authRequired: true },
            { path: "/selection-criteria/list", name: "selection-criteria-list", authRequired: true },

            { path: "/document/edit/:documentId", name: "document-edit", authRequired: true },
            { path: "/document/create", name: "document-edit", authRequired: true },
            { path: "/document/list", name: "document-list", authRequired: true },

            { path: "/contact/edit/:contactId", name: "contact-edit", authRequired: true },
            { path: "/contact/create", name: "contact-edit", authRequired: true },
            { path: "/contact/list", name: "contact-list", authRequired: true },

            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }

        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);