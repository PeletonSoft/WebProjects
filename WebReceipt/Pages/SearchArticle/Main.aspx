﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebReceipt.Pages.SearchArticle.SearchArticle" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Поиск артикула</title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-addon.js" type="text/javascript"></script>
    <script src="../../Scripts/jlinq.js" type="text/javascript"></script>
    <script src="../../Scripts/datetime.js" type="text/javascript"></script>
    <script src="Script.js" type="text/javascript"></script>   
</head>
<body class="searcharticle">
    <div class="caption">
        <table>
            <tbody>
                <tr>
                    <td class="text">
                        Введите артикул
                    </td>
                    <td class="input">
                        <input type="text" />
                    </td>
                    <td class="enter">
                        <input type="image" src="Images/enter.jpg" alt="E" />
                    </td>
                    <td class="exit">
                        <input type="image" src="Images/exit.jpg" alt="X" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="loading">Загрузка...
    </div>
    <div class="table">
        <div class="head">
            <table rules="all">
                <tbody>
                    <tr>
                        <td class="articleid">
                            Код
                        </td>
                        <td class="article">
                            Артикул
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

</body>
</html>
