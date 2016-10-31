<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAllFenXiaoTop.aspx.cs" Inherits="EnshineUnionManager.MemberAllFenXiaoTop" %>
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
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta name="author" content="" />
    <!-- Bootstrap Stylesheet -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" media="screen" />
    <link rel="stylesheet" href="assets/css/daterangepicker.css" />

    <!-- Uniform Stylesheet -->
    <link rel="stylesheet" href="plugins/uniform/css/uniform.default.css" />
    <!-- Main Layout Stylesheet -->
    <link rel="stylesheet" href="assets/css/fonts/icomoon/style.css" media="screen" />
    <link rel="stylesheet" href="assets/css/main-style.css" media="screen" />

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
                                <a class="brand" href="#">管理后台
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
                             	<uc1:MenuList runat="server" ID="MenuList" />

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
                                    <h1>
                                        <i class="icon-list"></i>分销能力排行榜一览 <span>This is the place where Search Result started</span>
                                    </h1>
                                </div>
                                <!--查询条件-->
                                <div class="widget-box" style="padding-left: 40px;">

                                    <div class="row-fluid">

                                        <div class="span6 widget">
                                            <div class="widget-header">
                                                <span class="title">
                                                    <i class="icos-abacus"></i>分销能力排行榜Top50
                                                </span>

                                            </div>
                                            <div class="widget-content task-list">
                                                <ul>
                                                     <asp:Repeater ID="rpMemberFenXiaoAllTopTotal" runat="server">
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
                                                                             <td style="width:80px" class="borderstyle"> 
                                                                                 <i class="icol-award-star-gold"></i>
                                                                               <%#Eval("Name") %> 
                                                                             </td>						 <td></td>
                                                                           			  <td style="width:100px" class="borderstyle"> <%#Eval("Tel")%></td>				 <td></td>
																																																																										  <td   style="width:100px" class="borderstyle"> 
                                                                              用户类型:<%#Eval("MemberName")%>
                                                                            </td>				 <td></td>
                                                                           
                                                                             <td class="rightcss" style="width:auto;font-weight: bold; color: red;">下级分销数:<%#Convert.ToInt32( Eval("FenXiaoCount")) %>人 </td>
                                                                         </tr>
                                                                     </table>
                                                                 </li>

                                                             </ItemTemplate>
                                                         </asp:Repeater>
                                                    <li id="liMessage2" runat="server" visible="false" style="text-align: center; font-size: 16px; color: red; font-weight: bold">
																																																					暂无分销能力排行榜信息。
                                                    </li>
                                                </ul>
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
                    <p>Copyright 2016© 正益移动科技有限公司.All Rights Reserved.</p>
                </div>
            </footer>

        </div>

        <!-- Core Scripts -->
        <script src="assets/js/jquery-1.8.2.min.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>

        <!-- Template Script -->
        <script src="assets/js/template.js"></script>
        <script type="text/javascript" src="assets/js/date.js"></script>
        <script type="text/javascript" src="assets/js/daterangepicker.min.js"></script>
        <link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
        <script src="plugins/msgbox/jquery.msgbox.min.js"></script>

        <script src="assets/js/SeachCommon.js"></script>

        <p id="back-to-top">
            <a href="#top" title="返回顶部">
                <img src="assets/images/top_arrow.png" style="border: 0;" /></a>
        </p>
    </form>

</body>
</html>
