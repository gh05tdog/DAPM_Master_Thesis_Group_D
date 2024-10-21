const reporter = require("cucumber-html-reporter");

const options = {
    theme: "bootstrap",
    jsonFile: "output/report.json",
    output: "output/report/index.html",
    reportSuiteAsScenarios: true,
    launchReport: true,
    name: "Process mining pipelines UI Test",
    storeScreenshots: true,
    noInlineScreenshots: true,
    brandTitle: "Cucumber automatic tests",
    screenshotsDirectory: "output/report/screenshots",
    metadata: {
      Environment: "CI",
      Parallel: "Scenarios",
      Executed: "Local System",
    },
  };

  reporter.generate(options);