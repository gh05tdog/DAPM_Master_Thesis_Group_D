const { setWorldConstructor, setDefaultTimeout } = require('@cucumber/cucumber');

setDefaultTimeout(30 * 1000);

//attach: function used for adding attachments to hooks/steps
class World {
	constructor({ attach }) {
		this.attach = attach;
	}
}
setWorldConstructor(World);
