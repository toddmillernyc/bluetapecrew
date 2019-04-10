import React, { Component } from 'react';
import FormSection from '../components/FormSection'
import Form from '../models/SettingsForm'
import SendEmailTest from './SendEmailTest';

export default class Settings extends Component {
    
    render() {
        return(
            <div>
            <input type="hidden" value="{{settings.Id}}" />
            <div className="row">
                <div className="col-md-6">
                    <FormSection title="Site" fields={Form.siteInputs} />
                    <FormSection title="Smtp Email" fields={Form.emailinputs} />
                    <SendEmailTest />
                </div>
                <div className="col-md-6">
                    <FormSection title="Contact" fields={Form.contactInputs} />
                    <FormSection title="Shipping" fields={Form.shippingInputs} />
                </div>
            </div>
            <div className="row">
                <div className="col-md-12">
                    <h2>Content</h2>
                    <dl className="dl-horizontal">
                        <dt>About Us</dt>
                        <dd><textarea className="form-control" ng-model="settings.aboutUs"></textarea></dd>
                        <dt>Description</dt>
                        <dd><textarea rows="10" className="form-control" ng-model="settings.description"></textarea></dd>
                        <dt>Keywords</dt>
                        <dd><textarea className="form-control" ng-model="settings.keywords"></textarea></dd>
                    </dl>
                </div>
            </div>
            <div className="row">
                <div className="col-md-6">
                    <h2>PayPal Live</h2>
                    <dl className="dl-horizontal">
                        <dt>Api User Name</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalApiUsername" /></dd>
                        <dt>Client Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalClientId" /></dd>
                        <dt>Client Secret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalClientSecret" /></dd>
                    </dl>
    
                    <h2>PayPal SandBox</h2>
                    <dl className="dl-horizontal">
                        <dt>Api User Name</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalSandboxAccount" /></dd>
                        <dt>Sandbox ClientId</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalSandBoxClientId" /></dd>
                        <dt>Sanbox Secret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.paypalSandBoxSecret" /></dd>
                    </dl>
                </div>
                <div className="col-md-6">
                    <h2>External Services</h2>
    
                    <h3>FaceBook</h3>
                    <dl className="dl-horizontal">
                        <dt>Url</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.faceBookUrl" /></dd>
                        <dt>App Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.facebookAppId" /></dd>
                        <dt>Client Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.facebookClientId" /></dd>
                        <dt>Client Secret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.facebookClientSecret" /></dd>
                    </dl>
    
                    <h3>Google</h3>
                    <dl className="dl-horizontal">
                        <dt>Client Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.googleClientId" /></dd>
                        <dt>Client Secret </dt>
                        <dd><input className="form-control" type="text" ng-model="settings.googleClientSecret" /></dd>
                    </dl>
    
                    <h3>Instagram</h3>
                    <dl className="dl-horizontal">
                        <dt>Client Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.instagramClientId" /></dd>
                        <dt>Client Secret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.instagramClientSecret" /></dd>
                    </dl>
    
                    <h3>LinkedIn</h3>
                    <dl className="dl-horizontal">
                        <dt>LinkedInUrl</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.linkedInUrl" /></dd>
                    </dl>
    
                    <h3>Mail Chimp</h3>
                    <dl className="dl-horizontal">
                        <dt>Api Key</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.mailChimpApiKey" /></dd>
                        <dt>List Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.mailChimpListId" /></dd>
                    </dl>
    
                    <h3>Microsoft</h3>
                    <dl className="dl-horizontal">
                        <dt>MicrosoftClientId</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.microsoftClientId" /></dd>
                        <dt>MicrosoftClientSecret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.microsoftClientSecret" /></dd>
                    </dl>
    
                    <h3>Twitter</h3>
                    <dl className="dl-horizontal">
                        <dt>TwitterUrl</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.twitterUrl" /></dd>
                        <dt>TwitterClientId</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.twitterClientId" /></dd>
                        <dt>TwitterClientSecret</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.twitterClientSecret" /></dd>
                        <dt>Twitter Widget Id</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.twitterWidgetId" /></dd>
                    </dl>
    
                    <dl className="dl-horizontal">
                        <dt>CopyrightText</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.copyrightText" /></dd>
                        <dt>CopyrightUrl</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.copyrightUrl" /></dd>
                        <dt>CopyrightLinktext</dt>
                        <dd><input className="form-control" type="text" ng-model="settings.copyrightLinktext" /></dd>
                    </dl>
                </div>
            </div>
            <button type="button" className="btn btn-success" ng-click="saveSettings(settings)">Save</button>
        </div>
        )
    }
}
