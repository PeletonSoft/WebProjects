(function ($) {

    $.l = function (selector, context) {
        return $(selector, context);
    };

    $.h = function (selector, context) {
        return $(selector, context).length > 0;
    };

    $.v = function (selector, context) {
        return $(selector, context)[0];
    };

    $.x = function (selector, context) {
        if (!context)
            context = document;

        var z = $();
        var xrt = XPathResult.ORDERED_NODE_SNAPSHOT_TYPE;

        var x = (context.evaluate) ?
            context.evaluate(selector, context, null, xrt, null) :
            context.ownerDocument.evaluate(selector, context, null, xrt, null);

        for (var i = 0; i < x.snapshotLength; i++)
            z = z.add(x.snapshotItem(i));
        return z;
    }

    $.x.l = function (selector, context) {
        return $.x(selector, context);
    };

    $.x.h = function (selector, context) {
        return $.x(selector, context).length > 0;
    };

    $.x.v = function (selector, context) {
        return $.x(selector, context)[0];
    };

    $.rnd = function (href) {
        var postfix = 'random=' + Math.random(); ;
        if (!href)
            return '?random=' + postfix;
        if (href.toString().indexOf('?') >= 0)
            return href.toString() + '&' + postfix;
        return href.toString() + '?' + postfix;
    };

    $.location = function (path, params, method) {
        method = method || "post"; // Set method to post by default, if not specified.

        // The rest of this code assumes you are not using a library.
        // It can be made less wordy if you use one.
        var jForm = $('<form />').
            attr({
                method: method,
                action: path
            });

        for (var key in params) {
            jForm.append(
                $('<input />')
                    .attr({
                        type: 'hidden',
                        name: key,
                        value: params[key]
                    })
                );
        }

        // Not entirely sure if this is necessary
        $('body').append(jForm);
        jForm.submit();
    };
    $.asBool = function (value) {
        if (!value || (/^false$/i).test(value)) {
            return false;
        }
        return true;
    }

})(window.jQuery);