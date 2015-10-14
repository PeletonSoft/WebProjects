(function () {
    //----------------------------------------------------------------
    //
    // selectInput - выделение нового текстового поля
    //      jNewInput - jQuery обвертка нового элемента
    //      makeAjax  - надобность в обновлении предыдущей позиции
    //----------------------------------------------------------------
    var jCurrInput;
    function selectInput(jNewInput, makeAjax) {
        if (jCurrInput && jCurrInput[0] != jNewInput[0]) {
            jCurrInput.removeClass('selected');
            checkChanged();
            if (jCurrInput.is('#ArticleId')) {
                if ($('div.receipt #ArticleId').attr('value')) {
                    if (makeAjax)
                        infoByArticleId();
                }
            }
            else
                if ($.asBool(jCurrInput.attr('serverModified'))) {
                    if (makeAjax)
                        updatePosition();
                }
        }

        jCurrInput = jNewInput;
        jCurrInput.addClass('selected');
        jCurrInput.attr({ modified: false });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function setModify(val) {
        jCurrInput.attr({
            value: val,
            modified: true
        });
    }
    //----------------------------------------------------------------
    //
    // addDigit - добавление символа цифры (1-9) в ячейку
    //
    //----------------------------------------------------------------
    function addDigit(digit) {
        if (!$.asBool(jCurrInput.attr('modified'))) {
            setModify(digit);
            return;
        }
        var val = jCurrInput.attr('value');
        var DotPos = val.indexOf('.');
        var LenVal = val.length;
        if (
                jCurrInput.is('#ArticleId') && LenVal < 9 ||
                jCurrInput.is('td.quant input') && (
                    DotPos >= 0 && DotPos > LenVal - 3 ||
                    DotPos < 0 && LenVal < 9) ||
                jCurrInput.is('td.discount input') && (
                DotPos >= 0 && DotPos > LenVal - 3 ||
                DotPos < 0 && LenVal < 2))

            setModify(val + digit);
    }
    //----------------------------------------------------------------
    //
    // addZero - добавление символа нуля в ячейку
    //
    //----------------------------------------------------------------
    function addZero() {

        if (!$.asBool(jCurrInput.attr('modified'))) {
            if (!jCurrInput.is('#ArticleId'))
                setModify(0);
            return;
        }

        var val = jCurrInput.attr('value');
        var DotPos = val.indexOf('.');
        var LenVal = val.length;


        if (LenVal == 0) {
            if (!jCurrInput.is('#ArticleId'))
                setModify(0);
            return;
        }

        if (
        jCurrInput.is('#ArticleId') && LenVal < 9 ||
        jCurrInput.is('td.quant input') && (
            DotPos >= 0 && DotPos > LenVal - 2 ||
            DotPos < 0 && LenVal < 9 && val > 0) ||
        jCurrInput.is('td.discount input') && (
            DotPos >= 0 && DotPos > LenVal - 3 ||
            DotPos < 0 && LenVal < 2 && val > 0))

            setModify(val + '0');
    }
    //----------------------------------------------------------------
    //
    // addBack - удаление последнего символа в строке
    //
    //----------------------------------------------------------------
    function addBack() {
        if (!$.asBool(jCurrInput.attr('modified'))) {
            setModify('');
            return;
        }

        var val = jCurrInput.attr('value');
        if (val.length > 0)
            setModify(val.substring(0, val.length - 1));
    }
    //----------------------------------------------------------------
    //
    // addDot - добавление десятичной точки
    //
    //----------------------------------------------------------------
    function addDot() {
        if (!$.asBool(jCurrInput.attr('modified'))) {
            if (!jCurrInput.is('#ArticleId'))
                setModify('0.');
            return;
        }

        if (jCurrInput.is('#ArticleId'))
            return;

        var val = jCurrInput.attr('value');
        var DotPos = val.indexOf('.');
        var LenVal = val.length;

        if (DotPos >= 0)
            return;

        if (LenVal == 0) {
            setModify('0.');
            return;
        }

        setModify(val + '.');
    };
    //----------------------------------------------------------------
    //
    //  preCheckChanged - предварительный контроль данных введенных в ячейку
    //
    //----------------------------------------------------------------
    function preCheckChanged() {
        jCurrInput.attr({ modified: false });

        if (jCurrInput.attr('value') == jCurrInput.attr('oldValue'))
            return;

        if (jCurrInput.is('#ArticleId')) {
            var ParseInt = parseInt(jCurrInput.attr('value'));

            if (isNaN(ParseInt) || ParseInt <= 0 || ParseInt > 1e9)
                jCurrInput.attr('value', jCurrInput.attr('oldValue'));
            else {
                jCurrInput.attr({
                    value: ParseInt,
                    oldValue: jCurrInput.attr('value')
                });
            }
            return;
        }

        var ParseFloat = parseFloat(jCurrInput.attr('value'));

        if (isNaN(ParseFloat))
            jCurrInput.attr('value', jCurrInput.attr('oldValue'));
        else {
            if (jCurrInput.is('td.quant input')) {
                if (ParseFloat < 0.005)
                    ParseFloat = parseFloat(jCurrInput.attr('oldValue'));
                if (ParseFloat > 1e9)
                    ParseFloat = 1e9;
            }
            if (jCurrInput.is('td.discount input')) {
                if (ParseFloat <= -0.005)
                    ParseFloat = 0.;
                if (ParseFloat > 90)
                    ParseFloat = 90;
            }

            jCurrInput.attr('value', ParseFloat.toFixed(2));
            jCurrInput.attr('oldValue', jCurrInput.attr('value'));
        }

        jCurrInput.attr('serverModified',
        jCurrInput.attr('value') != jCurrInput.attr('serverValue'));
    }
    //----------------------------------------------------------------
    //
    //  checkChanged - контроль данных введенных в ячейку
    //
    //----------------------------------------------------------------
    function checkChanged() {
        preCheckChanged();
        if (jCurrInput.is('#ArticleId'))
            return;
        if (!$.asBool(jCurrInput.attr('serverModified')))
            return;
        var id = $.v(jCurrInput.parents('div.body table tr')).id;
        var pos = jLinq.from(receiptContent.Чек)
        .equals('ID', parseInt(id)).first();
        if (jCurrInput.is('td.quant input'))
            pos.Количество = parseFloat(jCurrInput.attr('value'));
        if (jCurrInput.is('td.discount input'))
            pos.Скидка = parseFloat(jCurrInput.attr('value'));
    };
    //----------------------------------------------------------------
    //
    //  
    //
    //----------------------------------------------------------------
    function selectNext() {
        if (jCurrInput.is('#ArticleId'))
            return;
        var rIndex = $.v(jCurrInput.parents('div.body table tr')).rowIndex;
        var rCount = $.v('div.receipt div.body table').rows.length;
        var id = $.v(jCurrInput.parents('div.body table tr')).id;
        if (jCurrInput.is('td.quant input')) {
            selectInput($('tr#' + id + ' td.discount input'), false);
            return;
        }
        if (jCurrInput.is('td.discount input'))
            if (rIndex == rCount - 1) {
                selectInput($('input#ArticleId'), false);
                return;
            }
            else {
                var newId = $.v(jCurrInput.parents('div.body table')).rows[rIndex + 1].id;
                selectInput($('tr#' + newId + ' td.quant input'), false);
                return;
            }
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function checkClick() {
        checkChanged();
        if (jCurrInput.is('#ArticleId')) {
            if ($('div.receipt #ArticleId').attr('value'))
                infoByArticleId();
        }
        else {
            if ($.asBool(jCurrInput.attr('serverModified')))
                updatePosition();
            selectNext();
        }
    }
    //----------------------------------------------------------------
    //
    //  
    //
    //----------------------------------------------------------------
    function updatePosition() {
        var id = $.v(jCurrInput.parents('div.body table tr')).id;
        var pos = jLinq.from(receiptContent.Чек)
        .equals('ID', parseInt(id)).first();

        $.ajax({
            url: $.rnd("ContentUpdate.ashx"),
            type: "POST",
            data: {
                positionId: pos.ID,
                quant: pos.Количество,
                discount: pos.Скидка
            },
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                newId = msg.positionId;
                var newPos = jLinq.from(receiptContent.Чек)
                .equals('ID', parseInt(id)).first();
                newPos.Количество = msg.quant;
                newPos.Скидка = msg.discount;
                newPos.Цена_со_скидкой = msg.price;

                $('div.receipt tr#' + newId + ' td.quant input').attr({
                    value: newPos.Количество.toFixed(2),
                    serverModified: false,
                    modified: false,
                    oldValue: newPos.Количество.toFixed(2),
                    serverValue: newPos.Количество.toFixed(2)
                });
                $('div.receipt tr#' + newId + ' td.discount input').attr({
                    value: newPos.Скидка.toFixed(2),
                    serverModified: false,
                    modified: false,
                    oldValue: newPos.Скидка.toFixed(2),
                    serverValue: newPos.Скидка.toFixed(2)
                });
                $('div.receipt tr#' + newId + ' td.price')
                .html(newPos.Цена_со_скидкой.toFixed(2));
                displayResult();
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function infoByArticleId() {
        var newArticleId = parseInt($('div.receipt input#ArticleId').attr('value'));
        $.ajax({
            url: $.rnd("InfoByArticleId.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                articleId: newArticleId
            },
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                $('div.receipt #ArticleId').attr({
                    value: '',
                    oldValue: '',
                    modified: false
                });
                if (msg.error) {
                    selectInput($('div.receipt input#ArticleId'), false);
                    alert(msg.message);
                    selectInput($('div.receipt input#ArticleId'), false);
                    return;
                }
                else
                    addNewPosition(msg);
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function addNewPosition(info) {
        // выйти если нет места для новой позиции
        if (info.warning)
            alert(info.message);

        if (info.posCount > 1) {
            //$.location($.rnd('../Select/Main.aspx'), { articleId: info.articleId });
            //$.location(('../Select/Main.aspx'), { articleId: info.articleId });
            select.show();
            select.fill(info.articleId);
            return;
        }
        insertNewPosition(info.storeInfoId);
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function insertNewPosition(storeInfoId) {
        $.ajax({
            url: $.rnd("ContentInsert.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                storeInfoId: storeInfoId
            },
            success: function (msg) {
                var rec = {
                    ID: msg.positionId,
                    Код_артикула: msg.articleId,
                    Код_размещения: msg.storeInfoId,
                    Количество: msg.quant,
                    Скидка: msg.discount,
                    Цена_со_скидкой: msg.price
                };
                receiptContent.Чек[receiptContent.Чек.length] = rec;
                receiptContent.Номер_чека = msg.receiptNumber;
                fillTable();
                selectInput($('div.receipt tr#' + msg.positionId + ' td.quant input'), false);
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function deletePosition() {
        if (!confirm('Вы уверены, что хотите удалить данную позицию?'))
            return;
        var id = $.v($(this).parents('div.body table tr')).id;

        $.ajax({
            url: $.rnd("ContentDelete.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                positionId: id
            },
            success: function (msg) {

                selectInput($('div.receipt input#ArticleId'), false);

                receiptContent.Чек =
                jLinq.from(receiptContent.Чек)
                    .where(function (r) { return r.ID != msg.positionId })
                    .select();

                receiptContent.Номер_чека = msg.receiptNumber;
                fillTable();
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function clear() {
        if (!receiptContent.Номер_чека)
            return;
        if (!confirm('Вы уверены, что хотите очистить весь чек?'))
            return;

        $.ajax({
            url: $.rnd("ContentClear.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                fill();
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function send() {
        if (!receiptContent.Номер_чека)
            return;
        checkClick();
        if (!confirm('Вы уверены, что хотите отправить данных чек на кассу?'))
            return;

        $.ajax({
            url: $.rnd("ContentSend.ashx"),
            type: "POST",
            /*
            error: function (msg) {
            location = '../Error/604.html';
            },
            */
            success: function (msg) {
                if (msg.error)
                    alert(msg.message)
                else
                    alert('Чек был успешно отправлен.')
                fill();
            }
        });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------

    function infoPosition() {
        var id = $.v($(this).parents('div.body table tr')).id;
        information.fill(id);
        information.show();
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function init() {

        $('body > div.receipt table.button input.digit')
            .bind('click', function () { addDigit(parseInt(this.alt)); });

        $.v('body > div.receipt table.button input.zero').onclick = addZero;
        $.v('body > div.receipt table.button input.backspace').onclick = addBack;
        $.v('body > div.receipt table.button input.dot').onclick = addDot;
        $.v('body > div.receipt table.button input.check').onclick = checkClick;

        $.v('body > div.receipt table.button input.refresh').onclick = fill;
        $.v('body > div.receipt table.button input.clear').onclick = clear;
        $.v('body > div.receipt table.button input.send').onclick = send;

        $.v('body > div.receipt table.button input.undo').onclick = function () {
            jCurrInput.attr({
                value: jCurrInput.attr('oldValue'),
                oldValue: jCurrInput.attr('oldValue'),
                modified: false
            });
        };

        $.v('body > div.receipt table.button input.exit').onclick = function () {
            if (!confirm('Вы уверены, что хотите завершить сеанс работы?'))
                return;
            location = $.rnd('../Login/Main.aspx');
        };

        $.v('body > div.receipt table.button input.sales').onclick = function () {
            location = $.rnd('../SalesHistory/Main.aspx');
        };

        $.v('body > div.receipt table.button input.search').onclick = function () {
            location = $.rnd('../SearchArticle/Main.aspx');
        };
        fill();
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function fill() {
        $('div.receipt div.body tbody').html('');
        $('div.receipt #ArticleId').attr({ modified: false });
        $('input#ArticleId').addClass('selected');
        selectInput($('div.receipt input#ArticleId'));

        $.ajax({
            url: $.rnd("Content.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                receiptContent = msg;
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
        $('div.receipt div.body tbody').html('');
        displayResult();
        jLinq.from(receiptContent.Чек).each(
                function (rec) {
                    appendPosition(rec);
                });
        $('div.receipt #ArticleId')
                .add('div.receipt div.body td.quant input')
                .add('div.receipt div.body td.discount input')
                .bind('focus', function (e) { selectInput($(this), true); });
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function displayResult() {
        if (receiptContent.Номер_чека) {
            var listSum = jLinq.from(receiptContent.Чек)
            .select(function (rec) { return rec.Количество * rec.Цена_со_скидкой });
            var sum = jLinq.from(listSum).sum().result;
            $('div.receipt div.foot td.price').html(sum.toFixed(2))
            $('div.receipt div.foot td.button').html('№' + receiptContent.Номер_чека);
        }
        else {
            $('div.receipt div.foot td.price').html((0).toFixed(2));
            $('div.receipt div.foot td.button').html('');
        }
        document.title = receiptContent.Магазин + '- ' + receiptContent.Сотрудник;
        $('div.receipt div.caption').text(document.title);
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    function appendPosition(rec) {

        $('div.receipt div.body tbody')
        .append($('<tr />')
            .attr('id', rec.ID)
            .append($('<td />')
                .addClass('articleid')
                .append($('<input />')
                    .attr({
                        disabled: 'disabled',
                        value: rec.Код_артикула
                    })
                )
            )
            .append($('<td />')
                .addClass('quant')
                .append($('<input />')
                    .attr({
                        value: rec.Количество.toFixed(2),
                        readonly: 'readonly',
                        serverModified: false,
                        modified: false,
                        oldValue: rec.Количество.toFixed(2),
                        serverValue: rec.Количество.toFixed(2)
                    })
                )
            )
            .append($('<td />')
                .addClass('discount')
                .append($('<input />')
                    .attr({
                        value: rec.Скидка.toFixed(2),
                        readonly: 'readonly',
                        serverModified: false,
                        modified: false,
                        oldValue: rec.Скидка.toFixed(2),
                        serverValue: rec.Скидка.toFixed(2)
                    })
                )
            )
            .append($('<td />')
                .addClass('price')
                .html(rec.Цена_со_скидкой.toFixed(2))
            )
            .append($('<td />')
                .addClass('button')
                .append($('<input />')
                    .attr({
                        type: 'image',
                        alt: 'i',
                        src: 'Images/Grid/info.gif'
                    })
                .addClass('info')
                .bind('click', infoPosition)
                )
                .append($('<input />')
                    .attr({
                        type: 'image',
                        alt: 'x',
                        src: 'Images/Grid/delete.gif'
                    })
                .addClass('delete')
                .bind('click', deletePosition)
                )
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
        $('div.receipt').show();
    }
    //----------------------------------------------------------------
    //
    //
    //
    //----------------------------------------------------------------
    return {
        init: init,
        show: show,
        selectInput: selectInput,
        insertNewPosition: insertNewPosition
    }
}) ();
