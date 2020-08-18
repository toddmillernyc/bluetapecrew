"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_redux_1 = require("react-redux");
var LayoutModelStore = require("../store/LayoutModel");
var PreHeader = /** @class */ (function (_super) {
    __extends(PreHeader, _super);
    function PreHeader() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    PreHeader.prototype.componentDidMount = function () {
        this.props.requestLayoutModel();
    };
    PreHeader.prototype.render = function () {
        var contactEmail = this.props.layoutModel.contactEmail;
        var contactPhone = this.props.layoutModel.contactPhone;
        return (React.createElement(React.Fragment, null,
            React.createElement("div", { className: "pre-header" },
                React.createElement("div", { className: "container" },
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-md-6 col-sm-6 additional-shop-info" },
                            React.createElement("ul", { className: "list-unstyled list-inline" },
                                contactPhone && React.createElement("li", null,
                                    React.createElement("i", { className: "fa fa-phone" }),
                                    React.createElement("span", null, contactPhone)),
                                contactEmail && React.createElement("li", null,
                                    React.createElement("i", { className: "fa fa-envelope" }),
                                    React.createElement("a", { href: "{contactEmail}" }, contactEmail)))),
                        React.createElement("div", { className: "col-md-6 col-sm-6 additional-nav" },
                            React.createElement("ul", { className: "list-unstyled list-inline pull-right" },
                                React.createElement("li", null,
                                    React.createElement("a", { "asp-controller": "Manage", "asp-action": "Index", id: "manage-account-header-link" }, "My Account")),
                                React.createElement("li", null,
                                    React.createElement("a", { href: "~/cart", id: "cart-header-link" }, "Shopping Cart")),
                                React.createElement("li", null, "Login Partial Goes Here"))))))));
    };
    return PreHeader;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.layoutModel; }, LayoutModelStore.actionCreators)(PreHeader);
//# sourceMappingURL=PreHeader.js.map