import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface LayoutModelState {
    layoutModel: LayoutModel,
    isLoading: boolean
}

export interface LayoutModel {
    contactEmail: string,
    contactPhone: string
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestLayoutModelAction {
    type: 'REQUEST_LAYOUT_MODEL'
}

interface ReceiveLayoutModelAction {
    type: 'RECEIVE_LAYOUT_MODEL'
    layoutModel: LayoutModel
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestLayoutModelAction | ReceiveLayoutModelAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestLayoutModel: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.layoutModel) {
            fetch(`layout`)
                .then(response => response.json() as Promise<LayoutModel>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_LAYOUT_MODEL', layoutModel: data });
                });

            dispatch({ type: 'REQUEST_LAYOUT_MODEL' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: LayoutModelState = { layoutModel: { contactEmail: '', contactPhone: '' }, isLoading: false }

export const reducer: Reducer<LayoutModelState> = (state: LayoutModelState | undefined, incomingAction: Action): LayoutModelState => {
    if (state === undefined) return unloadedState
    const action = ((incomingAction) as any) as KnownAction;
    switch (action.type) {
        case 'REQUEST_LAYOUT_MODEL':
            return {
                layoutModel: state.layoutModel,
                isLoading: true
        }
        case 'RECEIVE_LAYOUT_MODEL':
            return {
            layoutModel: state.layoutModel,
            isLoading: true
            }
        default:
            return unloadedState
    }
}
