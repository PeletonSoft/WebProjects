(function () {
    function init() {
        window.articleId = null;
        window.quant = null;
        $.v("input.articleIdButton").onclick = articleClick;
        $.v("input.quantButton").onclick = quantClick;
        $.v("input.send").onclick = sendClick;
        sendButtonVisibility(false);
    }


    function sendButtonVisibility(val) {
        if (val) {
            $.v("input.send").style.visibility = "visible";
        } else {
            $.v("input.send").style.visibility = "hidden";
        }

    }

    function fill() {
        //document.title = "Быстрый слон";
    }

    function quantClick() {

        sendButtonVisibility(false);

        if (!window.articleId) {
            setError("Код артикула еще не введен.")
            return;
        }

        var str = $.v("input.quant").value;
        var val = parseFloat(str);
        if (val > 0.005 && val < 10.005) {
            $.v("input.quant").value = val;
        } else {
            setError("Количество должно быть в пределах 10 штук.");
            return;
        }

        window.quant = null;
        getQuant(val, successCheckQuant)
    }

    function articleClick() {

        sendButtonVisibility(false);

        window.articleId = null;
        $("table.description").find("tr").remove();

        setError("")

        var str = $.v("input.articleId").value;
        var val = parseInt(str);
        if (val > 0 && val < 1000000) {
            $.v("input.articleId").value = val;
        } else {
            setError("Код введен неправильно.");
            return;
        }

        getArticleId(val, successCheckArticleId);
    }


    function sendClick() {

        if (!window.articleId) {
            setError("Код артикула еще не введен.")
            return;
        }

        if (!window.quant) {
            setError("Количество еще не введено.")
            return;
        }

        getArticleId(window.articleId, function (msg) {
            window.articleId = msg.articleId;
            getQuant(window.quant, function (pos, val) {
                window.quant = val;
                send(pos.Код_размещения);
            });
        });
    }


    function insertNewPosition(storeInfoId) {
        $.ajax({
            url: $.rnd("../Receipt/ContentInsert.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                storeInfoId: storeInfoId
            },
            success: function (msg) {
                updatePosition(msg.positionId, msg.receiptNumber);
            }
        });
    }
    //----------------------------------------------------------------

    function updatePosition(positionId, receiptNumber) {
        $.ajax({
            url: $.rnd("../Receipt/ContentUpdate.ashx"),
            type: "POST",
            data: {
                positionId: positionId,
                quant: window.quant,
                discount: 0
            },
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                sendCheque(receiptNumber);
            }
        });
    }

    function send(storeInfoId) {
        insertNewPosition(storeInfoId);
    }

    function sendCheque(receiptNumber) {
        $.ajax({
            url: $.rnd("../Receipt/ContentSend.ashx"),
            type: "POST",
            error: function (msg) {
                //location = '../Error/604.html';
            },
            success: function (msg) {
                if (msg.error) {
                    setError(msg.message);
                } else {
                    setError('Заявка была успешно отправлена. Ваш чек №' + receiptNumber + ". Приходите за товаром на кассу.");
                    clear();
                }
            }
        });
    }

    function clear() {
        window.articleId = null;
        window.quant = null;
        $.v("input.quant").value = "";
        $.v("input.articleId").value = "";
        $("table.description").find("tr").remove();
    }

    function checkArticleId() {
    }

    function getArticleId(val, result) {
        $.ajax({
            url: $.rnd("../Receipt/InfoByArticleId.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                articleId: val
            },
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                if (msg.error) {
                    setError(msg.message);
                    return;
                } else {
                    result(msg);
                }
            }
        });
    }



    function successCheckArticleId(msg) {

        window.articleId = msg.articleId;

        $.ajax({
            url: $.rnd("../Receipt/DescriptionByArticleId.ashx"),
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            data: {
                articleId: window.articleId
            },
            success: function (msg) {
                showArticleInfo(msg);
            }
        });

    }

    function showArticleInfo(data) {
        $("table.description").find("tr").remove();

        for (key in data) {
            $("table.description")
                .append($('<tr>')
                    .append($('<td>')
                        .text(key + ": " + data[key])));
        }
    }

    function setError(text) {
        var errorBox = $("div.errorText");
        errorBox.text(text);
        //errorBox.add(text);
    }

    function getQuant(val, result) {

        $.ajax({
            url: $.rnd("../Receipt/ContentSelect.ashx"),
            data: { articleId: window.articleId },
            type: "POST",
            error: function (msg) {
                location = '../Error/604.html';
            },
            success: function (msg) {
                var pos = jLinq.from(msg.Позиции)
                    .where(function (rec) { return rec.Количество >= val - 0.005; })
                    .orderBy("Количество")
                    .take(1);
                if (pos.length == 1) {
                    result(pos[0], val);
                } else {
                    setError("Заданного количества нет на складе.");
                    return;
                }
            }
        });

    }

    function successCheckQuant(pos, val) {
        setError("Количество доступно на складе.");
        window.quant = val;
        sendButtonVisibility(true);
    }

    return {
        init: init,
        fill: fill
    };
})();