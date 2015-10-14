$(function () {
    $.ajax({
        url: $.rnd("Script.js"),
        type: "GET",
        error: function (msg) {
            alert(msg);
            location = '../Error/604.html';
        },
        success: function (msg) {
            Script = eval(msg);
            Script.init();
            Script.fill();
        }
    });
});