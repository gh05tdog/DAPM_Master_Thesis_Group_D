const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const { LoginPage } = require('../../support/pages/LoginPage');
const { LogoutPage } = require('../../support/pages/LogoutPage');
const navigationBar = require('../../support/components/UpperNavigationBar')
const expect = require('chai').expect;
const environments = require("../../support/components/Environments");
const upperNavigationBar = require('../../support/components/UpperNavigationBar');


When(
	'I type {string} value in the {string} field',
	async function (value, textField) {
		const loginPage = new LoginPage(this.page);

		await utils.clickAndTypeText(
			loginPage.page,
			loginPage[textField + 'CSS'],
			value
		);
		await utils.delay(1000);
	}
);

When('I submit by clicking the login button', async function () {
	const loginPage = new LoginPage(this.page);
	await loginPage.clickLoginButton();
});

Then('I validate that navigation bar contains message {string}', async function (expectedMessage) {
	let message = await utils.getText(this.page, navigationBar["headerCSS"]);
	expect(message).to.contain(expectedMessage);
	await utils.delay(100);
})
Then('I validate redirection to the KeycloakLoginPage', async function () {
	let mainUrl = await this.page.url();
	expect(mainUrl).to.contain("/realms/test/protocol/openid-connect/auth?client_id")
});
Then('I validate that alert message {string} is shown', async function (expectedMessage) {
	const loginPage = new LoginPage(this.page);
	let message = await utils.getText(this.page, loginPage.errormessageCSS);
	expect(message).to.contain(expectedMessage)
});
Given('A manager is logged in', async function () {
	const currentPage = new LoginPage(this.page);
	await currentPage.openBasepage(environments.local.url);
	await utils.clickAndTypeText(currentPage.page, currentPage.usernameCSS, "manager");
	await utils.clickAndTypeText(currentPage.page, currentPage.passwordCSS, "password");
	await Promise.all([
		await utils.click(currentPage.page, currentPage.loginButtonCSS),
		await currentPage.page.waitForNavigation()
	]);
	//Assert
	await utils.delay(500);
	let mainUrl = await this.page.url();
	expect(mainUrl).to.contain("/user")

});

When('the {string} is pressed on the header', async function (element) {
	await utils.click(this.page, upperNavigationBar[element+"CSS"]);
	await utils.delay(100);
})
Then('the logout page is loaded', async function () {
	const logoutPage = new LogoutPage(this.page);
	await logoutPage.page.waitForSelector(logoutPage["pageCSS"]);
	await utils.delay(4000);
})