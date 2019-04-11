import React, { Component } from 'react';
import SendEmailTest from './SendEmailTest'
import { getSitSettings, saveSiteSettings } from '../modules/Api'

export default class Settings extends Component {
    constructor(props) {
      super(props)

      this.state = {}
      this.handleInputChange = this.handleInputChange.bind(this)
      this.saveSettings = this.saveSettings.bind(this)
    }
    
    componentDidMount = async() =>  {
      var settings = await getSitSettings()
      this.setState(settings)
    }

    handleInputChange(event) {
      const target = event.target;
      const value = target.type === 'checkbox' ? target.checked : target.value;
      this.setState({[target.name]: value})
    }

    sendMail(emailAddress) {
      console.log(`"send mail ${emailAddress}`)
    }

    saveSettings = async() => {
      const response = await saveSiteSettings(this.state)
      console.log(response)
    }

    label = title => {return(<dt>{title}</dt>)}
    input = name => {
      return(<dd><input className="form-control" type="text" name={name} value={this.state[name] || ""} onChange={this.handleInputChange} /></dd>)
    }
    textarea = (name, rows) => {
      if(rows)
        return(<dd><textarea rows={rows} className="form-control" name={name} value={this.state[name] || ""} onChange={this.handleInputChange}></textarea></dd>)
      else
        return(<dd><textarea className="form-control" name={name} value={this.state[name] || ""} onChange={this.handleInputChange}></textarea></dd>)
    }

    render() {
      const label = this.label
      const input = this.input
      const textarea = this.textarea
        return(
<form>
  <div className="row">
    <div className="col-md-6">
      <h2>Site</h2>
      <dl className="dl-horizontal">
        {label("Title")}    {input("siteTitle")}
        {label("Url")}      {input("siteUrl")}
        {label("Logo Url")} {input("siteLogoUrl")}
      </dl>
      <h2>Smtp Email</h2>
      <dl className="dl-horizontal">
        {label("Host")}     {input("smtpHost")}
        {label("Port")}     {input("smtpPort")}
        {label("Username")} {input("smtpUsername")}
        {label("Password")} {input("smtpPassword")}
      </dl>
      <SendEmailTest />
    </div>
    <div className="col-md-6">
      <h2>Contact</h2>
      <dl className="dl-horizontal">
        {label("Email")} {input("contactEmailAddress")}
        {label("Phone")} {input("contactPhoneNumber")}
      </dl>
      <h2>Shipping</h2>
      <dl className="dl-horizontal">
        {label("Free Shipping Threshold")} {input("freeShippingThreshold")}
        {label("Flat Shipping Rate")}      {input("flatShippingRate")}
      </dl>
    </div>
  </div>

  <div className="row">
    <div className="col-md-12">
      <h2>Content</h2>
      <dl className="dl-horizontal">
        {label("About Us")}     {textarea("aboutUs")}
        {label("Description")}  {textarea("description", 10)}
        {label("Keywords")}     {textarea("keywords")}
      </dl>
    </div>
  </div>

  <div className="row">
    <div className="col-md-6">
      <h2>PayPal Live</h2>
      <dl className="dl-horizontal">
        {label("Api User Name")}  {input("paypalApiUsername")}
        {label("Client Id")}      {input("paypalClientId")}
        {label("Client Secret")}  {input("paypalClientSecret")}
      </dl>
      <h2>PayPal SandBox</h2>
      <dl className="dl-horizontal">
        {label("User Name")}        {input("paypalSandboxAccount")}
        {label("Sandbox ClientId")} {input("paypalSandBoxClientId")}
        {label("Sanbox Secret")}    {input("paypalSandBoxSecret")}
      </dl>
    </div>
    <div className="col-md-6">
      <h2>External Services</h2>
      <h3>FaceBook</h3>
      <dl className="dl-horizontal">
        {label("Url")}{input("faceBookUrl")}
        {label("App Id")}{input("facebookAppId")}
        {label("Client Id")}{input("facebookClientId")}
        {label("Client Secret")}{input("facebookClientSecret")}
      </dl>
      <h3>Google</h3>
      <dl className="dl-horizontal">
        {label("Client Id")}{input("googleClientId")}
        {label("Client Secret")}{input("googleClientSecret")}
      </dl>
      <h3>Instagram</h3>
      <dl className="dl-horizontal">
        {label("Client Id")}{input("instagramClientId")}
        {label("Client Secret")}{input("instagramClientSecret")}
      </dl>
      <h3>LinkedIn</h3>
      <dl className="dl-horizontal">
        {label("LinkedInUrl")}{input("linkedInUrl")}
      </dl>
      <h3>Mail Chimp</h3>
      <dl className="dl-horizontal">
        {label("Api Key")}{input("mailChimpApiKey")}
        {label("List Id")}{input("mailChimpListId")}
      </dl>
      <h3>Microsoft</h3>
      <dl className="dl-horizontal">
        {label("MicrosoftClientId")}{input("microsoftClientId")}
        {label("MicrosoftClientSecret")}{input("microsoftClientSecret")}
      </dl>
      <h3>Twitter</h3>
      <dl className="dl-horizontal">
        {label("TwitterUrl")}{input("twitterUrl")}
        {label("TwitterClientId")}{input("twitterClientId")}
        {label("TwitterClientSecret")}{input("twitterClientSecret")}
        {label("Twitter Widget Id")}{input("twitterWidgetId")}
        </dl>
      <dl className="dl-horizontal">
        {label("CopyrightText")}{input("copyrightText")}
        {label("CopyrightUrl")}{input("copyrightUrl")}
        {label("CopyrightLinktext")}{input("copyrightLinktext")}
      </dl>
    </div>
  </div>
  <button id="saveButton" type="button" className="btn btn-success" onClick={this.saveSettings}>Save</button>
</form>
)
}
}