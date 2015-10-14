<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Notify._Default" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Обновление чеков</title>
</head>
<body class="login">Привет</body>
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
                        <input class="password" type="password"/>
                    </td>
                </tr>
            </tbody>
        </table>
</html>
