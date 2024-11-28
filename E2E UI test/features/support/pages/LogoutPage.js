const { BasePage } = require("./BasePage");
const utils = require('../../lib/utils');


exports.LogoutPage = class LogoutPage extends BasePage{
    constructor(page) {
        super(page);
        this.page = page;

        this.pageCSS= '[data-qa="LogoutPage"]';
    }
    
}