const { Given, When, Then } = require('@cucumber/cucumber');
const utils = require('../../lib/utils');
const { PipelinePage } = require('../../support/pages/PipelinePage');
const navigationBar = require('../../support/components/UpperNavigationBar')
const environments = require("../../support/components/Environments");

const expect = require('chai').expect;


