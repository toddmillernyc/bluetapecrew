"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Header = void 0;
var React = require("react");
exports.Header = function (_a) {
    var _b = _a.initial, initial = _b === void 0 ? 0 : _b;
    // since we pass a number here, clicks is going to be a number.
    // setClicks is a function that accepts either a number or a function returning
    // a number
    return (React.createElement("div", { className: "header" },
        React.createElement("div", { className: "container" },
            React.createElement("a", { className: "site-logo", href: "#" },
                React.createElement("img", { src: "img/logo.png", alt: "Metronic Shop UI" })),
            React.createElement("a", { href: "#", className: "mobi-toggler" },
                React.createElement("i", { className: "fa fa-bars" })),
            React.createElement("p", null, "cart"),
            React.createElement("div", { className: "header-navigation" },
                React.createElement("ul", null)))));
};
//# sourceMappingURL=Header.js.map