import FormField from '../models/FormField'

export default class SettingsForm {

    static get siteInputs() { 
        return [
            new FormField("siteTitle", "Title"),
            new FormField("siteUrl", "Url"),
            new FormField("siteLogoUrl", "Logo Url")
    ]}
    
    static get emailinputs() { 
        return [
            new FormField("smtpHost", "Host"),
            new FormField("smtpPort", "Port"),
            new FormField("smtpUsername", "Username"),
            new FormField("smtpPassword", "Password")
    ]}

    static get contactInputs() { 
        return [
            new FormField("contactEmailAddress", "Email"),
            new FormField("contactPhoneNumber", "Phone"),
    ]}

    static get shippingInputs() {
        return [
            new FormField("freeShippingThreshold", "Free Shipping Threshold"),
            new FormField("flatShippingRate", "Flat Shipping Rate"),
        ]
    }
}