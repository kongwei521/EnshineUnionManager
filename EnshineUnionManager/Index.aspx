<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EnshineUnionManager.Index" %>
	<%@ Register Src="~/MenuList.ascx" TagPrefix="uc1" TagName="MenuList" %>

<!DOCTYPE html>
<!--[if lt IE 7]> <html class="lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]>    <html class="lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]>    <html class="lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!-->
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<!--<![endif]-->
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap Stylesheet -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" media="screen" />
    <link rel="stylesheet" href="assets/css/daterangepicker.css" />
    <!-- Uniform Stylesheet -->
    <link rel="stylesheet" href="plugins/uniform/css/uniform.default.css" />
    <!-- Main Layout Stylesheet -->
    <link rel="stylesheet" href="assets/css/fonts/icomoon/style.css" media="screen" />
    <link rel="stylesheet" href="assets/css/main-style.css" media="screen" />
    <!-- Charts Layout Stylesheet -->
    <link href="assets/css/echartsHome.css" rel="stylesheet" />
    <script src="assets/js/esl.js"></script>
    <script src="assets/js/codemirror.js"></script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
    <script src="assets/js/html5shiv.min.js"></script>
    <script src="assets/js/respond.min.js"></script>
    <![endif]-->
    <title>益生联盟数据平台 Web Client Search Platform </title>
</head>
<body>
    <form id="form1" runat="server">

        <div id="wrapper">
            <header id="header" class="navbar navbar-inverse">
                <div class="navbar-inner">
                    <div class="container">
                        <div class="brand-wrap pull-left">
                            <div class="brand-img">
                                <a class="brand" href="#">
                                    <%--	 管理后台--%>
									管理后台
                                </a>
                            </div>
                        </div>

                        <div id="header-right" class="clearfix">

                            <div id="dropdown-lists">
                                <a class="item" href="#">
                                    <span class="item-icon"><i class="icon-exclamation-sign"></i></span>
                                    <span id="spClientName" runat="server"></span>

                                </a>

                            </div>

                            <div id="header-functions" class="pull-right">
                                <div id="user-info" class="clearfix">
                                    今天是： <span id="spNowTime" runat="server"></span>
                                </div>
                                <div id="logout-ribbon">
                                    <a href="javascript:LoginOut('UserLogin','');"><i class="icon-off"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </header>

            <div id="content-wrap">
                <div id="content">
                    <div id="content-outer">
                        <div id="content-inner">
                               <uc1:MenuList runat="server" id="MenuList" />

                            <section id="main" class="clearfix">
                                <div id="main-header" class="page-header">
                                    <ul class="breadcrumb">
                                        <li>
                                            <i class="icon-home"></i>益生联盟数据平台 Home
                                        <span class="divider">&raquo;</span>
                                        </li>
                                        <li>
                                            <a href="#">益生联盟数据平台 Web Client Search</a>
                                        </li>
                                    </ul>

                                    <!--查询条件-->
                                    <div class="widget-box">
                                        <h1>
                                            <i class="icon-zoom-in"></i>数据统计分析 <span>This is the place where search  started</span>
                                        </h1>

                                        <div class="row-fluid">
                                            <!--本月热销产品排行榜--->
                                            <div class="span6 widget ">
                                                <div class="widget-header" style="height: 38px;">
                                                    <div style="float: left">
                                                        <span class="title">
                                                            <i class="icos-list"></i>本月热销产品排行榜
                                                        </span>
                                                    </div>
                                                    <div style="float: left; margin-top: 4px;">
                                                        <input type="text" runat="server" class="text-info" id="txtTimeSelect" style="width: 120px; font-size: 11px;" />
                                                    </div>
                                                    <div style="float: left; margin-top: 4px;">
                                                        &nbsp;&nbsp;<input type='button' class="btn btn-small btn-large"
                                                            id="btnSkuExport" runat="server" style='height: 28px; font-size: 11px;' value='导出Excel' onserverclick="btnSkuExport_ServerClick" />
                                                    </div>
                                                    &nbsp;
                                                     <div class="btn-group" style="float: left; padding-top: 5px; padding-left: 4px;">
                                                         <asp:LinkButton ID="lbTen" CssClass="btn" Style="width: 22px;" OnClick="lbPageGetData_Click" CommandArgument="1"
                                                             runat="server">1-10</asp:LinkButton>
                                                         <asp:LinkButton ID="lbTwenty" CssClass="btn"
                                                             OnClick="lbPageGetData_Click" CommandArgument="2" Style="width: 22px;" runat="server">11-20</asp:LinkButton>
                                                     </div>
                                                </div>
                                                <div class="widget-content task-list" style="height: 440px;">
                                                    <ul>
                                                        <asp:Repeater ID="rpTotalSku" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-right: 1px solid #f4cfcf;">
                                                                                <label class="checkbox">
                                                                                    <span class="badge badge-important"><%#Container.ItemIndex+1 %></span>
                                                                                </label>
                                                                            </td>
                                                                            <td></td>
                                                                            <td style="border-left: 1px solid #f4cfcf; border-right: 1px solid #f4cfcf;"><span class="text">
                                                                                <i class="icol-award-star-gold"></i>
                                                                                <%#Eval("GoodsTitle") %>
                                                                            </span></td>
                                                                            <td></td>
                                                                            <td class="rightcss"><%#Convert.ToInt32( Eval("SumQty")) %>  件</td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <li id="liMessage" runat="server" visible="false" style="text-align: center; font-size: 16px; color: red; padding-top: 210px; font-weight: bold">本月暂无热销产品排行榜信息。
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <!--本月客户购买排行榜-->
                                            <div class="span6 widget" id="spBuying">
                                                <div class="widget-header">
                                                    <span class="title">
                                                        <i class="icos-list-images"></i>本月客户购买排行榜
                                                    </span>
                                                </div>
                                                <div class="widget-content task-list" style="height: 440px;">
                                                    <ul>
                                                        <asp:Repeater ID="rpCustomerTotal" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <table>
                                                                        <tr>
                                                                            <td style="border-right: 1px solid #f4cfcf;">
                                                                                <label class="checkbox">
                                                                                    <span class="badge badge-warning"><%#Container.ItemIndex+1 %></span>
                                                                                </label>
                                                                            </td>
                                                                            <td></td>
                                                                            <td style="border-left: 1px solid #f4cfcf; border-right: 1px solid #f4cfcf;"><span class="text">
                                                                                <i class="icon-users"></i>
                                                                                <%#Eval("Name") %>  (联系人电话:<%#Eval("tel") %>)
                                                                            </span></td>
                                                                            <td></td>
                                                                            <td class="rightcss"><%#Convert.ToInt32( Eval("SumTotal")) %>  次</td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
																																																					<li id="liMessage1" runat="server" visible="false" style="text-align: center; font-size: 16px; color: red; padding-top: 210px; font-weight: bold">暂无本月客户购买排行榜信息。
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
																																														
                                        <div class="row-fluid">
                                            <div class="span12 widget">
                                                <div class="widget-header">
                                                    <span class="title" style="font-size: 14px; font-weight: bold"><i class="icos-chart"></i>商品销售分析 </span>

                                                </div>
                                                <div class="widget-content table-container">
                                                    <div id="goodsCharts" class="main"></div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12 widget" style="float: left">
                                                <div class="widget-header">
                                                    <span class="title" style="font-size: 14px; font-weight: bold;"><i class="icos-graph"></i>订单情况分析 </span>

                                                </div>
                                                <div class="widget-content table-container">
                                                    <div id="ordersCharts" class="main"></div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                      
                                    </div>

                                </div>

                            </section>
                        </div>
                    </div>
                </div>
            </div>

            <footer id="footer">
                <div class="footer-left">益生联盟数据平台  Web Client Search </div>
                <div class="footer-right">
                    <p>Copyright 2016©正益移动科技有限公司. All Rights Reserved.</p>
                </div>
            </footer>

        </div>

        <!-- Core Scripts -->
        <script src="assets/js/jquery-1.8.2.min.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>

        <!-- Template Script -->
        <script src="assets/js/template.js"></script>
        <script src="assets/js/setup.js"></script>

        <!-- Customizer, remove if not needed -->
        <script src="assets/js/customizer.js"></script>

        <!-- Uniform Script -->
        <script src="plugins/uniform/jquery.uniform.min.js"></script>

        <!--Daterangepicker Script-->
        <script type="text/javascript" src="assets/js/date.js"></script>
        <script type="text/javascript" src="assets/js/daterangepicker.min.js"></script>
        <!--msgbox Script-->
        <link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
        <script src="plugins/msgbox/jquery.msgbox.min.js"></script>

        <script src="assets/js/SeachCommon.js"></script>
        <!--echarts Script-->
        <script src="assets/js/echarts-map.js"></script>
        <script src="assets/js/EchartsJson.js"></script>
        <p id="back-to-top">
            <a href="#top" title="返回顶部">
                <img src="assets/images/top_arrow.png" style="border: 0;" /></a>
        </p>
    </form>
</body>
</html>
