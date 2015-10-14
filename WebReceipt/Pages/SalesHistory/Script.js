function init() {
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
        var sArticleId = $.v('div.caption td.input input').value;
        var articleId = parseInt(sArticleId);
        if (isNaN(articleId) || articleId <= 0 || articleId > 1e6)
            return;
        $.ajax({
            url: $.rnd('Content.ashx'),
            data: { articleId: articleId },
            error: function (msg) {
                location = '../Error/604.html';
            },
            beforeSend: function () {
                $('div.loading').show();
                var textLoading = $.trim($('div.loading').text());
                $('div.loading').text(textLoading.substr(0, 10));
                loadingTimer = window.setInterval(loading, 500);

            },
            success: function (msg) {
                content = msg;
                jLinq.from(content.История).each(function (rec) { addRow(rec); });
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
                .addClass('date')
                .html(dateFormat(DateDeserialize(rec.Дата_продажи), 'd.mm.yyyy'))
            )
            .append($('<td />')
                .addClass('receiptnumber')
                .html(rec.Номер_чека)
            )
            .append($('<td />')
                .addClass('attr')
                .html(rec.ПрИнфо)
            )
            .append($('<td />')
                .addClass('attrinfo')
                .html(rec.Инфо)
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