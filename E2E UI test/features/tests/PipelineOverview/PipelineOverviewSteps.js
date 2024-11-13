const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const { PipelinePage } = require('../../support/pages/PipelinePage');
const navigationBar = require('../../support/components/UpperNavigationBar')
const environments = require("../../support/components/Environments");

const expect = require('chai').expect;


Given('I open the pipelineOverview page', async function () {
	await this.page.goto(environments.local.url);
	await utils.delay(500);
	let mainUrl = await this.page.url();
	expect(mainUrl).to.contain("/user")
});
Then('the pipelineOverview containst {string}',async function (string) {
	const currentPage = new PipelinePage(this.page);
	await currentPage.contains(string)
});