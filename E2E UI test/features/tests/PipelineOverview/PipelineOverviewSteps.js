const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const { PipelinePage } = require('../../support/pages/PipelinePage');
const navigationBar = require('../../support/components/UpperNavigationBar')
const environments = require("../../support/components/Environments");

const expect = require('chai').expect;


Given('I open the pipelineOverview page', async function () {
	const currentPage = (new PipelinePage(this.page));
	await currentPage.page.goto(environments[environments.selected].url);
	await currentPage.contains("mainWindow");
	await utils.delay(1000);
	let mainUrl = await currentPage.page.url();
	expect(mainUrl).to.contain("/user")
});
Then('the pipelineOverview containst {string}',async function (string) {
	const currentPage = new PipelinePage(this.page);

	await currentPage.contains(string)
});