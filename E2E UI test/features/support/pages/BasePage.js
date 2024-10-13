const {upperNavigationBar} = require("../components/UpperNavigationBar");
const {environment} = require("../components/Environments");

exports.BasePage = class BasePage {
    constructor(page) {
        this.page = page;
        this.typeDelay = 30;
    }
    async goto(){
        await this.page.goto(environment[local].url);
    }

    async logout(){
        await this.page.logout()
    }
}