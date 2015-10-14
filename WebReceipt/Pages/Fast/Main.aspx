<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebReceipt.Pages.Fast.Main" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Поиск артикулов</title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-addon.js" type="text/javascript"></script>
    <script src="../../Scripts/jlinq.js" type="text/javascript"></script>
    <script src="Load.js" type="text/javascript"></script>
</head>
<body class="fast">
    <div>Введите код</div>
    <input type="text" class="articleId" />
    <input type="button" value="Проверить"  class="articleIdButton"/>
    <table class="description"></table>
    <div>Введите количество</div>
    <input type="text" class="quant"/>
    <input type="button" value="Проверить"  class="quantButton"/>
    <div class="errorText"></div>
    <input type="button" value="Отправить на кассу"  class="send"/>
</body>
</html>
