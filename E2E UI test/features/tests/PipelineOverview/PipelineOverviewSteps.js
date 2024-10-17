const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const loginWindow = require('../../support/windows/loginWindow');
const navigationBar = require('../../support/components/UpperNavigationBar')
const expect = require('chai').expect;
const loginWindow = require('../../support/windows/loginWindow');
const environments = require("../../support/components/Environments");

Given('I log in as a user with the username {string} and the password {string}',async function (username, password) {
// Write code here that turns the phrase above into concrete actions
    await utils.delay(100);
    await this.page.goto(environments.local.url);	
    await utils.delay(100);

    await utils.clickAndTypeText(
        this.page,
        loginWindow.textFields.username,
        username
    );
    await utils.clickAndTypeText(
        this.page,
        loginWindow.textFields.password,
        password
    );

    await utils.click(this.page, loginWindow.buttons.login);
});

Then('i validate that the list {string} is displayed',async function (string) {
// Write code here that turns the phrase above into concrete actions
return 'pending';
});

Then('i validate that the list {string} contains the name {string}',async function (string, string2) {
// Write code here that turns the phrase above into concrete actions
return 'pending';
});