if (!String.prototype.format) {
	String.prototype.format = function () {
		var args = arguments;
		return this.replace(/{(\d+)}/g, function (match, number) {
			return typeof args[number] != 'undefined' ? args[number] : match;
		});
	};
}

if (!String.prototype.supplant) {
	String.prototype.supplant = function (o) {
	    return this.replace(/{([^{}]*)}/g,
	        function (a, b) {
	            var r = o[b];
	            return typeof r === 'string' || typeof r === 'number' ? r : a;
	        }
	    );
	};
}


$.extend($.expr[':'], {
	fixed: function (node) {
		return $(node).css('position') === 'fixed';
	}
});

$.fn.extend({
	safeAddClass: function (classToAdd) {
		return this.each(function () {
			if (this.className !== undefined) {
				if (this.className.length === 0) {
					this.className = classToAdd;
				} else {
					this.className = this.className + ' ' + classToAdd;
				}
			}
		});
	},
	safeRemoveClass: function (classToRemove) {
		return this.each(function () {
			if (this.className !== undefined) {
				if (this.className.length === classToRemove.length) {
					this.className = '';
				} else {
					this.className = this.className.split(' ' + classToRemove).join('');
				}
			}
		});
	}
});