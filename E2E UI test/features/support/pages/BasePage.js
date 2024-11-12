const { upperNavigationBar } = require("../components/UpperNavigationBar");
const { environment } = require("../components/Environments");
const utils = require('../../lib/utils');


exports.BasePage = class BasePage {
    constructor(page) {
        this.page = page;
        this.typeDelay = 30;
    }
    async openBasepage(url) {    
        await utils.delay(500); 
        await this.page.goto(url);
        await utils.delay(500);
    }

    async logout() {
        await this.page.logout()
    }
}