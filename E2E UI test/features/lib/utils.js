const expect = require('chai').expect;

module.exports = {
    
    click: async function (page, selector) {
        try {
          await page.waitForSelector(selector);
          await page.click(selector, { delay: 30 });
        } catch (error) {
          throw new Error(`Could not click on selector: ${selector}`);
        }
      },

      clickAndTypeText: async function (page, selector, text) {
        try {
          await page.waitForSelector(selector);
          await page.click(selector);
          await page.keyboard.down("Control");
          await page.keyboard.press("A");
          await page.keyboard.up("Control");
          await page.keyboard.press("Backspace");
          await page.type(selector, text, { delay: 30 });
        } catch (error) {
          throw new Error(`Could not type into selector: ${selector}`);
        }
      },

      generateRandomString: function (length) {
        return Math.random().toString(36).substring(2,(length + 2));
      },

      clickByXpath: async function (page, selector) {
        try {
          await page.waitForXPath(selector);
          const elements = await page.$x(selector);
          await elements[0].click({ delay: 20 });
        } catch (error) {}
      },

      pressKey: async function (page, key) {
        try {
          await page.keyboard.press(key, { delay: 100});
        } catch (error) {
          throw new Error(`Couldn't press key ${key} on the keyboard`);
        }
      },
      
      getText: async function (page, selector) {
        try {
          
          await page.waitForSelector(selector);
          return await page.$eval(selector, (element) => element.textContent);
        } catch (error) {
          throw new Error(`Could not get text from selector: ${selector}`);
        }
      },

      clickByText: async function (page, selector) {
        try {
          const [objectToClick] = await page.$x(
            `//*[contains(text(), '${selector}')]`
          );
          if (objectToClick) {
            await objectToClick.click();
          }
        } catch (error) {
          throw new Error(`Could not click on selector: ${selector}`);
        }
      },
      delay(time){
        return new Promise(function(resolve){
          setTimeout(resolve, time)
        });
      },
}