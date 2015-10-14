(function () {
    function init() {

        for (var i = 1; i <= 5; i++)
            $.v('input.digit' + i).onclick = function () {
                if (truePassword.length < 5)
                    setPassword(truePassword + this.alt);
                $('div.error').text('');
            };

        $.v('input.backspace').onclick = function () {
            if (truePassword.length > 0)
                setPassword(truePassword.substring(0, truePassword.length - 1));
        };

        $.v('input.exit').onclick = function () {
            if (!confirm("Вы хотите выйти из программы?"))
                return;
            location = 'about:blank';
        };
        $.v('input.enter').onclick = autentification;
    }

    function fill() {

        $('td.combobox>select').attr('disabled', true);
        $('td.combobox>select').html('');
        $('div.error').text('');

        $.ajax({
            url: $.rnd("SelectShops.ashx"),
            error: function (msg) {
                location = '../Error/602.html';
            },
            success: function (msg) {

                documentShops = msg;
                if (!jLinq.from(documentShops.Магазины).first())
                    location = '../Error/601.html';

                jLinq.from(documentShops.Магазины).each(function (rec) {
                    var shop = $('<option />');
                    shop.attr('value', rec.Код);
                    shop.text(rec.Название);
                    $('select.shop').append(shop);
                });

                $.v('select.shop').disabled = false;
                if (documentShops.Выбранный_магазин)
                    $.v('select.shop').value = documentShops.Выбранный_магазин;

                $.v('select.shop').onchange = changeShop;
                changeShop();
                if (hasViolant()) {
                    violantAutentification();
                }
            }
        });
    }

    function hasViolant() {
        var re = /App_Themes\/Violant/i;
        var isViolant = false;
        $('link[rel="stylesheet"]').each(function (i, ele) {
            if (re.exec(ele.href) != null) {
                isViolant = true;
            }
        });
        return isViolant;
    }

    function violantAutentification() {
        lastUpdate = new Date();
        var userId = -parseInt(lastUpdate.getTime() / 500 % 200000);
        documentShops.Выбранный_магазин = 1;
        documentShops.Выбранный_сотрудник = userId;
        truePassword = "";
        autentification();
    }

    function autentification() {
        $.ajax({
            url: $.rnd("Authentication.ashx"),
            type: "POST",
            data: {
                UserId: documentShops.Выбранный_сотрудник,
                ShopId: documentShops.Выбранный_магазин,
                Password: truePassword
            },
            error: function (msg) {
                location = '../Error/603.html';
            },
            success: function (msg) {
                if (msg.hasSuccess) {
                    if (hasViolant()) {
                        location = $.rnd('../Fast/Main.aspx');
                    }
                    else {
                        location = $.rnd('../Receipt/Main.aspx');
                    }
                }
                else {
                    $('div.error').text(msg.message);
                    setPassword('');
                }
            }
        });
    }

    function changeShop() {
        $.v('select.user').disabled = true;
        $.v('select.user').onchange = null;
        $('select.user').html('');

        documentShops.Выбранный_магазин = parseInt($.v('select.shop').value);

        var Users = jLinq.from(documentShops.Магазины)
        .equals('Код', documentShops.Выбранный_магазин)
        .first().Сотрудники;

        jLinq.from(Users).each(function (rec) {
            var user = $('<option />');
            user.attr('value', rec.Код);
            user.text(rec.ФИО);
            $('select.user').append(user);
        });

        if (jLinq.from(Users)
        .equals('Код', documentShops.Выбранный_сотрудник))
            $.v('select.user').value = documentShops.Выбранный_сотрудник;

        $.v('select.user').disabled = false;
        $.v('select.user').onchange = changeUser;
        changeUser();
    }

    function changeUser() {
        documentShops.Выбранный_сотрудник = parseInt($.v('select.user').value);
        setPassword('');
        $('div.error').text('');
    }

    function setPassword(newPassword) {
        truePassword = newPassword;
        $.v('input.password').value = truePassword.replace(/\w/g, '*');
    }

    return {
        init: init,
        fill: fill
    };
})();