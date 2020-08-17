"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = exports.actionCreators = void 0;
// ACTION CREATORS 
exports.actionCreators = {
    requestLayoutModel: function () { return function (dispatch, getState) {
        var appState = getState();
        if (appState
            && appState.layoutModel
            && !appState.layoutModel.isLoading
            && !appState.layoutModel.layoutModel.contactEmail) {
            fetch("layout")
                .then(function (response) { return response.json(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_LAYOUT_MODEL', layoutModel: data });
            });
            dispatch({ type: 'REQUEST_LAYOUT_MODEL' });
        }
    }; }
};
// REDUCER 
var unloadedState = { layoutModel: { contactEmail: '', contactPhone: '' }, isLoading: false };
exports.reducer = function (state, incomingAction) {
    if (state === undefined) {
        return unloadedState;
    }
    var action = (incomingAction);
    switch (action.type) {
        case 'REQUEST_LAYOUT_MODEL':
            return {
                layoutModel: state.layoutModel,
                isLoading: true
            };
        case 'RECEIVE_LAYOUT_MODEL':
            return {
                layoutModel: action.layoutModel,
                isLoading: true
            };
        default:
            return unloadedState;
    }
};
//# sourceMappingURL=LayoutModel.js.map