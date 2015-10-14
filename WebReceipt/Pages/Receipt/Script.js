$(function () {
    $.ajax({
        url: $.rnd("Receipt.js"),
        type: "GET",
        error: function (msg) {
            alert(msg);
            location = '../Error/604.html';
        },
        success: function (msg) {
            receipt = eval(msg);
            receipt.init();
            receipt.show();
        }
    });

    $.ajax({
        url: $.rnd("Select.js"),
        type: "GET",
        error: function (msg) {
            alert(msg);
            location = '../Error/604.html';
        },
        success: function (msg) {
            select = eval(msg);
            select.init();
        }
    });

    $.ajax({
        url: $.rnd("Information.js"),
        type: "GET",
        error: function (msg) {
            alert(msg);
            location = '../Error/604.html';
        },
        success: function (msg) {
            information = eval(msg);
            information.init();
        }
    });
});