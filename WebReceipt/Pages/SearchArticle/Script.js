﻿function init() {
    $.v('div.caption td.exit input').onclick = function () {
        location = $.rnd('../Receipt/Main.aspx');
        return;
    }
    $.v('div.caption td.enter input').onclick = function () {
        if ($('div.loading').is(':visible'))
            return;

        $('div.table').hide();
        $('div.loading').hide();
        $('div.table div.body tbody').html('');

        var filter = $.v('div.caption td.input input').value;
        filter = filter.replace(/(^\s+)|(\s+$)/g, "");
        $.v('div.caption td.input input').value = filter;
        if (filter.length < 1)
            return;

        $.ajax({
            url: $.rnd('Content.ashx'),
            data: { filter: filter },
            /*
            error: function (msg) {
            location = '../Error/604.html';
            },
            */
            beforeSend: function () {
                $('div.loading').show();
                var textLoading = $.trim($('div.loading').text());
                $('div.loading').text(textLoading.substr(0, 10));
                loadingTimer = window.setInterval(loading, 500);

            },
            success: function (msg) {
                content = msg;
                jLinq.from(content.Список).each(function (rec) { addRow(rec); });
                $('div.table').show();
                $('div.loading').hide();
                clearInterval(loadingTimer);
            }
        });
    };
}

function loading() {
    var textLoading = $('div.loading').text();
    $('div.loading').text(
        textLoading.length > 20 ?
        textLoading.substr(0, 10) :
        textLoading + '.');
}

function addRow(rec) {
    $('div.table div.body tbody')
        .append($('<tr />')
            .append($('<td />')
                .addClass('articleid')
                .html(rec.Код_артикула)
            )
            .append($('<td />')
                .addClass('article')
                .html(rec.Артикул)
            )
            .append($('<td />')
                .addClass('store')
                .html(rec.Склад)
            )
            .append($('<td />')
                .addClass('quant')
                .html(rec.Количество.toFixed(2))
            )
        );
}

$(function () {
    init(); 
});