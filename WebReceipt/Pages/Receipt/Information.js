(function () {
    function init() {
        $.v('body > div.info div.ok input').onclick = function () {
            receipt.show();
        };
    }
    function fill(id) {
        $.ajax({
            url: $.rnd("InfoByPositionId.ashx"),
            data: { positionId: id },
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                infoContent = msg;
                fillTable();
            }
        });

    }
    function fillTable() {
        $('body > div.info tr.articleid td.value').text(infoContent.Код_артикула);
        $('body > div.info tr.article td.value').text(infoContent.Артикул);
        $('body > div.info tr.decription td.value').text(infoContent.Описание);
        $('body > div.info tr.attr td.value').text(infoContent.ПрИнфо);
        $('body > div.info tr.attrinfo td.value').text(infoContent.Инфо);
        $('body > div.info tr.type td.value').text(infoContent.Вид_товара);
        $('body > div.info tr.producer td.value').text(infoContent.Производитель);
        $('body > div.info tr.store td.value').text(infoContent.Склад);
        $('body > div.info tr.state td.value').text(infoContent.Статус);
        $('body > div.info tr.group td.value').text(infoContent.Группа);
        $('body > div.info tr.pie td.value').text(infoContent.Сектор);
        $('body > div.info tr.price td.value').text(infoContent.Цена.toFixed(2));
        $('body > div.info tr.quant td.value').text(infoContent.Количество.toFixed(2));
    }
    function show() {
        $('body > div').hide();
        $('body > div.info').show();
    }
    return {
        init: init,
        fill: fill,
        show: show
    }
})();
