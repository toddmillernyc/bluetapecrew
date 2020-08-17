"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.reducer = exports.actionCreators = void 0;
// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
exports.actionCreators = {
    requestLayoutModel: function () { return function (dispatch, getState) {
        // Only load data if it's something we don't already have (and are not already loading)
        var appState = getState();
        if (appState && appState.layoutModel) {
            fetch("layout")
                .then(function (response) { return response.json(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_LAYOUT_MODEL', layoutModel: data });
            });
            dispatch({ type: 'REQUEST_LAYOUT_MODEL' });
        }
    }; }
};
// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
var unloadedState = { layoutModel: { contactEmail: '', contactPhone: '' }, isLoading: false };
exports.reducer = function (state, incomingAction) {
    if (state === undefined)
        return unloadedState;
    var action = (incomingAction);
    switch (action.type) {
        case 'REQUEST_LAYOUT_MODEL':
            return {
                layoutModel: state.layoutModel,
                isLoading: true
            };
        case 'RECEIVE_LAYOUT_MODEL':
            return {
                layoutModel: state.layoutModel,
                isLoading: true
            };
        default:
            return unloadedState;
    }
};
//# sourceMappingURL=LayoutModel.js.map