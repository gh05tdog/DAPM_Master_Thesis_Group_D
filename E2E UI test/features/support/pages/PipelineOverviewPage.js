const { BasePage } = require("./BasePage");

exports.PipelineOverviewPage = class PipelineOverviewPage extends BasePage{
    constructor(page) {
        super(page);
        this.page = page;


        this.organizationListCSS= '[data-qa="organizationList"]';
        this.repositoryListCSS= '[data-qa="repositoryList"]';
        this.organizationCSS= '[data-qa="organization"]';
        this.repositoryCSS= '[data-qa="repository"]';


    }
    
    async validateOrganizationListIsDisplayed(){
        await this.page.waitForSelector(this.organizationListCSS);
    }
}