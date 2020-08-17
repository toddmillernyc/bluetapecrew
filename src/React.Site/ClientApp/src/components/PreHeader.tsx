import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as LayoutModelStore from '../store/LayoutModel';

type LayoutModelProps =
    LayoutModelStore.LayoutModelState
    & typeof LayoutModelStore.actionCreators;

class PreHeader extends React.PureComponent<LayoutModelProps> {

    public componentDidMount() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestLayoutModel();
    }

    public render() {
        const contactEmail = this.props.layoutModel.contactEmail;
        return (
            <React.Fragment>
                <div className="pre-header">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-6 col-sm-6 additional-shop-info">
                                <ul className="list-unstyled list-inline">
                                    {
                                        this.props.layoutModel.contactPhone
                                            && <li><i className="fa fa-phone"></i><span>{this.props.layoutModel.contactPhone}</span></li>
                                    }
                                    {
                                        this.props.layoutModel.contactEmail
                                        && <li><i className="fa fa-envelope"></i><a href="{contactEmail}">{contactEmail}</a></li>
                                    }
                                </ul>
                            </div>
                            <div className="col-md-6 col-sm-6 additional-nav">
                                <ul className="list-unstyled list-inline pull-right">
                                    <li><a asp-controller="Manage" asp-action="Index" id="manage-account-header-link">My Account</a></li>
                                    <li><a href="~/cart" id="cart-header-link">Shopping Cart</a></li>
                                    <li>Login Partial Goes Here</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        )
    }
}

export default connect(
    (state: ApplicationState) => state.layoutModel,
    LayoutModelStore.actionCreators
)(PreHeader as any);