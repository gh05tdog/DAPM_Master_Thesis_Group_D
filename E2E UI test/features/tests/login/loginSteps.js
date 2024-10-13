const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const loginWindow = require('../../support/windows/loginWindow');
const navigationBar = require('../../support/components/UpperNavigationBar')
const expect = require('chai').expect;

When(
	'I type {string} value in the {string} field',
	async function (value, textField) {
		switch (textField) {
			case 'username':
				await utils.clickAndTypeText(
					this.page,
					loginWindow.textFields.username,
					value
				);
				break;
			case 'password':
				await utils.clickAndTypeText(
					this.page,
					loginWindow.textFields.password,
					value
				);
                await utils.delay(1000);
				break;
		}
	}
);

When('I submit by clicking the login button', async function () {
	await utils.click(this.page, loginWindow.buttons.login);
});

Then('I validate that navigation bar contains message {string}', async function(expectedMessage){
    let message = await utils.getText(this.page, navigationBar["title"]);
    expect(message).to.contain(expectedMessage);
	await utils.delay(100);
})
Then('I validate redirection to the KeycloakLoginPage',async  function () {
	let mainUrl = await this.page.url();
	expect(mainUrl).to.contain("/realms/test/protocol/openid-connect/auth?client_id")
	console.log(mainUrl);
  });
Then('I validate that alert message {string} is shown',async  function (expectedMessage) {
	let message = await utils.getText(this.page, loginWindow.errormessage);
	expect(message).to.contain(expectedMessage)
  });