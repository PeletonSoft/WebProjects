(function () {
    //----------------------------------------------------------------
    //
    // selectRow - выделение нужной строки
    //
    //----------------------------------------------------------------
    var jCurrInput;
    function selectRow(jNewInput) {
        if (jCurrInput && jCurrInput[0] != jNewInput[0]) {
            jCurrInput.removeClass('selected');
        }

        jCurrInput = jNewInput;
        jCurrInput.addClass('selected');
    }

    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function init() {
        $.v('body > div.select table.button td.cancel input').onclick = function () {
            receipt.selectInput($('div.receipt input#ArticleId'), false);
            receipt.show();
        };
        $.v('body > div.select table.button td.ok input').onclick = function () {
            receipt.insertNewPosition($.v(jCurrInput).id);
            receipt.show();
        };
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function fill(articleId) {
        $('body > div.select div.body tbody').html('');

        $.ajax({
            url: $.rnd("ContentSelect.ashx"),
            data: { articleId: articleId },
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                selectContent = msg;
                fillTable();
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function fillTable() {
        $('body > div.select div.body tbody').html('');
        //displayResult();
        jLinq.from(selectContent.Позиции).each(
                function (rec) {
                    appendPosition(rec);
                });
        $('body > div.select div.body tbody tr')
           .bind('click', function () { selectRow($(this)); })
        $('body > div.select div.body tbody tr')
           .bind('dblclick', $.v('body > div.select table.button td.ok input').onclick);
        if ($.h('body > div.select div.body tbody tr'))
            selectRow($($.v('body > div.select div.body tbody tr')));
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function appendPosition(rec) {
        $('body > div.select div.body tbody')
        .append($('<tr />')
	    .attr('id', rec.Код_размещения)
            .append($('<td />')
                .addClass('attr')
                .html(rec.ПрИнфо)
            )
            .append($('<td />')
                .addClass('attrinfo')
                .html(rec.Инфо)
            )
	        .append($('<td />')
                .addClass('store')
                .html(rec.Аббревиатура)
            )
            .append($('<td />')
                .addClass('quant')
                .html(rec.Количество.toFixed(2))
            )
        );


    }

    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function show() {
        $('body > div').hide();
        $('div.select').show();
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------

    return {
        init: init,
        fill: fill,
        show: show
    }
})();

