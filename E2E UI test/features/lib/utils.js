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
      delay: async function (time){
        return new Promise(function(resolve){
          setTimeout(resolve, time)
        });
      },
      highlightElement: async function (page, selector) {
        // Evaluate the page and add a red border to the element matching the selector
        page.evaluate((selector) => {
          const element = document.querySelector(selector);
          if (element) {
            // Add a red border around the element
            element.style.border = '5px solid red';
            element.style.position = 'relative'; // Ensure the element is positioned to show the border correctly
            element.style.zIndex = '9999'; // Make sure the border is visible on top
          }
        }, selector);
      },
}