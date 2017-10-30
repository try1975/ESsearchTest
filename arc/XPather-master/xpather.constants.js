var xpatherHTML = '\
	<xpather id="xpather">\
		<form id="xpather-form">\
			<input id="xpather-xpath" type="text" placeholder="enter XPath…" autocomplete="off" spellcheck="false" />\
		</form> \
		<xpather id="xpather-result"></xpather>\
		<xpather id="xpather-sidebar-toggler"></xpather>\
	</xpather>\
	<xpather id="xpather-sidebar">\
		<xpather id="xpather-sidebar-spacer"></xpather>\
		<xpather id="xpather-sidebar-entries"></xpather>\
	</xpather>\
	<xpather id="xpather-apisidebar">\
			<fieldset>\
				<legend>Настройки парсинга:</legend>\
				<div><label for="xpather-name">Наименование ТРУ</label><br>\
				<input type="text" name="xpather-name" id="xpather-name" placeholder="enter XPath…" autocomplete="off" spellcheck="false" /><br>\
				<input type="text" name="xpather-name-result" id="xpather-name-result" placeholder="XPath result…" autocomplete="off" spellcheck="false"/></div>\
				<div><label for="xpather-name">Стоимость ТРУ</label><br>\
				<input type="text" name="xpather-price" id="xpather-price" placeholder="enter XPath…" autocomplete="off" spellcheck="false" /><br>\
				<input type="text" name="xpather-price-result" id="xpather-price-result" placeholder="XPath result…" autocomplete="off" spellcheck="false"/></div>\
				<button id="xpather-save">Save</button>\
			</fieldset>\
	</xpather>';

var functionsWithShortcuts = {
	'sw': ['starts-with'],
	'co': ['contains'],
	'ew': ['ends-with'],
	'uc': ['upper-case'],
	'lc': ['lower-case'],
	'no': ['not']
}

var selectorsWithShortcuts = {
	'@c': ['class'],
	'@i': ['id'],
	'@t': ['title'],
	'@s': ['style'],
	'@h': ['href']
}

var tagsWithShortcuts = {
	'd': ['div'],
	's': ['span']
}

var attributes = ['id', 'class', 'itemprop', 'role', 'time', 'rel', 'style'];
var attributesLength = attributes.length;