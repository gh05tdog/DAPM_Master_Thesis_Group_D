const { BasePage } = require("./BasePage");
const utils = require('../../lib/utils');


exports.LoginPage = class LoginPage extends BasePage{
    constructor(page) {
        super(page);
        this.page = page;

        this.usernameCSS= '#username';
        this.passwordCSS= '#password';
        this.loginButtonCSS= '#kc-login';
        this.errormessageCSS= '#input-error-username > span';
    }
    async clickLoginButton(){
        await utils.click(this.page, this.loginButtonCSS);
    }
}