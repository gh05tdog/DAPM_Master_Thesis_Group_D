# Group D, End2End UI test 
This repository is used to setup and run tests on the UI of the Process mining pipeline frontend implementation

It uses Puppeteer together with Jasmine and Cucumber to interpret User stories written in Gherkin into action on a webpage.

To run the test suite 
(if this is the first time running the test, do npm install)
npm run test

A report can be created with 
npm generate:report

To only run a few test, append @focus to the test 
npm run test:focus