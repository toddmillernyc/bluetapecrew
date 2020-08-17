import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';


//STATE
export interface LayoutModelState {
    layoutModel: LayoutModel,
    isLoading: boolean
}
export interface LayoutModel {
    contactEmail: string,
    contactPhone: string
}

// ACTIONS
interface RequestLayoutModelAction {
    type: 'REQUEST_LAYOUT_MODEL'
}

interface ReceiveLayoutModelAction {
    type: 'RECEIVE_LAYOUT_MODEL'
    layoutModel: LayoutModel
}

type KnownAction = RequestLayoutModelAction | ReceiveLayoutModelAction

// ACTION CREATORS 
export const actionCreators = {
    requestLayoutModel: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState()
        if (appState
            && appState.layoutModel
            && !appState.layoutModel.isLoading
            && !appState.layoutModel.layoutModel.contactEmail) {
            fetch(`layout`)
                .then(response => response.json() as Promise<LayoutModel>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_LAYOUT_MODEL', layoutModel: data })
                })
            dispatch({ type: 'REQUEST_LAYOUT_MODEL' })
        }
    }
};

// REDUCER 
const unloadedState: LayoutModelState = { layoutModel: { contactEmail: '', contactPhone: '' }, isLoading: false }

export const reducer: Reducer<LayoutModelState> = (state: LayoutModelState | undefined, incomingAction: Action): LayoutModelState => {
    
    if (state === undefined) {
        return unloadedState
    }
    const action = ((incomingAction) as any) as KnownAction;
    switch (action.type) {
        case 'REQUEST_LAYOUT_MODEL':
            return {
                layoutModel: state.layoutModel,
                isLoading: true
        }
        case 'RECEIVE_LAYOUT_MODEL':
            return {
                layoutModel: action.layoutModel,
                isLoading: true
            }
        default:
            return unloadedState
    }
}
