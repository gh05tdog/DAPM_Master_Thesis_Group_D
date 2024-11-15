const { BasePage } = require("./BasePage");
const utils = require('../../lib/utils');


exports.PipelinePage = class PipelinePage extends BasePage{
    constructor(page) {
        super(page);
        this.page = page;

        this.headerCSS = '[data-qa="header"]';
        this.sidebarCSS = '[data-qa="Sidebar"]';
        this.mainWindowCSS= '[data-qa="mainWindow"]';
    }
    async contains(element){
        await this.page.waitForSelector(this[element+'CSS'])
        await utils.highlightElement(this.page,this[element+'CSS'])
    }
}