<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebReceipt.Receipt" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Вход в программу</title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-addon.js" type="text/javascript"></script>
    <script src="../../Scripts/jlinq.js" type="text/javascript"></script>
    <script src="Script.js" type="text/javascript"></script>
</head>
<body class="receipt">
    <div class="receipt">
        <div class="caption"></div>
        <div class="table">
            <div class="head">
                <table rules="all">
                    <tbody>
                        <tr>
                            <td class="articleid">
                                Код
                            </td>
                            <td class="quant">
                                Кол-во
                            </td>
                            <td class="discount">
                                Скидка
                            </td>
                            <td class="price">
                                Цена
                            </td>
                            <td class="button">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="body">
                <table rules="all">
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="foot">
                <table rules="all">
                    <tbody>
                        <tr>
                            <td class="articleid">
                                <input type="text" id="ArticleId" readonly="readonly" />
                            </td>
                            <td class="quant">
                                <input type="text" value="" disabled="disabled" />
                            </td>
                            <td class="discount">
                                <input type="text" value="" disabled="disabled" />
                            </td>
                            <td class="price">
                            </td>
                            <td class="button">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <table class="button">
            <tbody>
                <tr>
                    <td>
                        <input type="image" class="zero" src="Images/Panel/0.gif" alt="0" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/1.gif" alt="1" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/2.gif" alt="2" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/3.gif" alt="3" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/4.gif" alt="4" />
                    </td>
                    <td>
                        <input type="image" class="dot" src="Images/Panel/dot.gif" alt="," />
                    </td>
                    <td>
                        <input type="image" class="search" src="Images/Panel/search.bmp" alt="S" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/5.gif" alt="5" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/6.gif" alt="6" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/7.gif" alt="7" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/8.gif" alt="8" />
                    </td>
                    <td>
                        <input type="image" class="digit" src="Images/Panel/9.gif" alt="9" />
                    </td>
                    <td>
                        <input type="image" class="backspace" src="Images/Panel/back.gif" alt="C" />
                    </td>
                    <td>
                        <input type="image" class="sales" src="Images/Panel/sales.gif" alt="H" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="2">
                        <input type="image" class="check" src="Images/Panel/check.bmp" alt="V" />
                    </td>
                    <td>
                        <input type="image" class="undo" src="Images/Panel/undo.bmp" alt="U" />
                    </td>
                    <td>
                        <input type="image" class="exit" src="Images/Panel/exit.jpg" alt="E" />
                    </td>
                    <td colspan="2" rowspan="2">
                        <input type="image" class="send" src="Images/Panel/send.bmp" alt="S" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="image" class="refresh" src="Images/Panel/refresh.bmp" alt="R" />
                    </td>
                    <td>
                        <input type="image" class="clear" src="Images/Panel/delete.bmp" alt="1" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="select">
        <div class="caption">
            <p>
                Данный код имеет несколько позиций
            </p>
            <p>
                укажите нужную из них
            </p>
        </div>
        <div class="table">
            <div class="head">
                <table rules="all">
                    <tbody>
                        <tr>
                            <td class="attr">
                                ПрИнфо
                            </td>
                            <td class="attrinfo">
                                Инфо
                            </td>
                            <td class="store">
                                Склад
                            </td>
                            <td class="quant">
                                Кол-во
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="body">
                <table rules="all">
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <table class="button">
            <tbody>
                <tr>
                    <td class="cancel">
                        <input type="button" value="Отменить" />
                    </td>
                    <td class="ok">
                        <input type="button" value="Выбрать" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="info">
        <div class="table">
            <table rules="all">
                <tbody>
                    <tr class="articleid">
                        <td class="property">
                            Код
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="article">
                        <td class="property">
                            Артикул
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="decription">
                        <td class="property">
                            Описание
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="attr">
                        <td class="property">
                            ПрИнфо
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="attrinfo">
                        <td class="property">
                            Инфо
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="type">
                        <td class="property">
                            Вид товара
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="producer">
                        <td class="property">
                            Производитель
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="store">
                        <td class="property">
                            Склад
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="state">
                        <td class="property">
                            Статус
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="group">
                        <td class="property">
                            Группа
                        </td>
                        <td class="pie" />
                    </tr>
                    <tr class="id">
                        <td class="property">
                            Сектор
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="price">
                        <td class="property">
                            Цена
                        </td>
                        <td class="value" />
                    </tr>
                    <tr class="quant">
                        <td class="property">
                            На складе
                        </td>
                        <td class="value" />
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="ok">
            <input type="button" value="Ok" />
        </div>
    </div>
</body>
</html>
