const { BasePage } = require("./BasePage");

exports.LoginPage = class LoginPage extends BasePage{
    constructor(page) {
        super(page);
        this.page = page;

        this.usernameCSS= '[data-qa="username"]';
        this.passwordCSS= '[data-qa="password"]';
    }
    
}