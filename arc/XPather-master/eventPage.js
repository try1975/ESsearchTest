chrome.browserAction.onClicked.addListener(function(tab) {
	chrome.tabs.executeScript(null, {
		code: 'init()'
	});
});

chrome.commands.onCommand.addListener(function(command) {
	if (command === 'toggle-sidebar') {
		chrome.tabs.executeScript({
			code: 'toggleSidebar()'
		});
	} else if (command === 'input-autocomplete') {
		chrome.tabs.executeScript({
			code: 'inputAutocomplete()'
		});
	}
});

chrome.contextMenus.onClicked.addListener(function(info, tab) {
	chrome.tabs.executeScript(null, {
		code: 'currentSelection = \'' + info.selectionText.replace(/'/g, '[XPATHER]') + '\'; findXPath("' + info.menuItemId +'");'
	});
});
/*
chrome.contextMenus.create({
	id: 'getXPath',
	title: 'Get unique XPath',
	contexts: ['selection']
});
*/
chrome.contextMenus.create({
	id: 'getXPathName',
	title: 'Получить XPath для наименования',
	contexts: ['selection']
}, 
);

chrome.contextMenus.create({
	id: 'getXPathPrice',
	title: 'Получить XPath для цены',
	contexts: ['selection']
});