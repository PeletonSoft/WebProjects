<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebReceipt.Login" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Вход в программу</title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-addon.js" type="text/javascript"></script>
    <script src="../../Scripts/jlinq.js" type="text/javascript"></script>
    <script src="Load.js" type="text/javascript"></script>
</head>
<body class="login">
    <div class="full">
        <div class="caption">
            <p>
                Выберите имя пользователя
            </p>
            <p>
                и введите пароль
            </p>
        </div>
        <table class="user">
            <tbody>
                <tr class="shop">
                    <td class="caption">
                        Магазин
                    </td>
                    <td class="combobox">
                        <select class="shop" />
                    </td>
                </tr>
                <tr class="user">
                    <td class="caption">
                        Сотрудник
                    </td>
                    <td class="combobox">
                        <select class="user" />
                    </td>
                </tr>
                <tr class="password">
                    <td class="caption">
                        Пароль
                    </td>
                    <td class="combobox">
                        <input class="password" type="text" readonly="readonly" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="button">
            <tbody>
                <tr>
                    <td>
                        <input type="image" class="digit1" src="Images/1.BMP" alt="1" />
                    </td>
                    <td>
                        <input type="image" class="digit2" src="Images/2.BMP" alt="2" />
                    </td>
                    <td>
                        <input type="image" class="digit3" src="Images/3.BMP" alt="3" />
                    </td>
                    <td>
                        <input type="image" class="digit4" src="Images/4.BMP" alt="4" />
                    </td>
                    <td>
                        <input type="image" class="digit5" src="Images/5.BMP" alt="5" />
                    </td>
                    <td>
                        <input type="image" class="backspace" src="Images/back.jpg" alt="C" />
                    </td>
                    <td>
                        <input type="image" class="exit" src="Images/exit.jpg" alt="X" />
                    </td>
                    <td>
                        <input type="image" class="enter" src="Images/enter.jpg" alt="E" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="error"/>
    </div>
</body>
</html>
