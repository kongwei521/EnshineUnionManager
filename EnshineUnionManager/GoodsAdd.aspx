<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAdd.aspx.cs" ValidateRequest="false"
    Inherits="EnshineUnionManager.GoodsAdd" %>
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
    <script charset="utf-8" type="text/javascript" src="../kindeditor/kindeditor.js"></script>
    <script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
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
                                            <i class="icon-zoom-in"></i>商品信息添加 <span>This is the place where Search started</span>
                                        </h1>
                                        <br />
                                        <div id="live" class="tab-pane active">
                                            <div class='row-fluid'>
                                                <div class='span12 widget'>
                                                    <div class='widget-header'>
                                                        <span class='title'>
                                                            <i class='icol-blog'></i>商品信息管理
                                                        </span>
                                                        <div style='float: right; padding-top: 4px; padding-right: 4px;'>
                                                            <input type='button' class='btn  btn-primary' value='商品列表管理'
                                                                onclick="window.location.href = 'GoodsManager.aspx?mid=<%=Request["mid"]%>&type=<%=Request["type"]%>'" />
                                                        </div>
                                                    </div>
                                                    <div class='widget-content summary-list'>
                                                        <table class='table table-bordered table-striped'>

                                                            <tbody>
                                                                <tr class="even gradeC">
                                                                    <td style="width: 120px;">商品标题：</td>
                                                                    <td style="text-align: left">
                                                                        <input id="txtTitle" runat="server" class="span8" type="text" /></td>

                                                                </tr>
                                                                         <tr class="even gradeC">
                                                                  <td>普通/推荐商品<br/>商品分类/：
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="drpSaleGoodsSort" runat="server"  Width="135">
                                                                             <asp:ListItem Value="-普通/推荐商品-"></asp:ListItem>
                                                                            <asp:ListItem Value="P">普通商品</asp:ListItem>
                                                                            <asp:ListItem Value="T">推荐商品</asp:ListItem>
                                                                        </asp:DropDownList>  /	 <asp:DropDownList ID="drpGoodsSort"  Width="135"  runat="server"></asp:DropDownList>
                                                                      	
                                                                    </td>
                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td>是否生效/首页显示：</td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="drpValidate" runat="server" Width="200">
                                                                            <asp:ListItem Value="-请选择是否生效-"></asp:ListItem>
                                                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                                                            <asp:ListItem Value="N">否</asp:ListItem>
                                                                        </asp:DropDownList>/	
																		<asp:DropDownList ID="drpSetIndex" Width="200" runat="server">
                                                                            <asp:ListItem Value="-是否首页显示-"></asp:ListItem>
                                                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                                                            <asp:ListItem Value="N">否</asp:ListItem>
                                                                        </asp:DropDownList></td>

                                                                </tr>
                                                                <tr class="even gradeC">
                                                                    <td>商品编码<br />
                                                                        /产品规格/生成厂家：</td>
                                                                    <td style="text-align: left">
                                                                        		
                                                                        <input id="txtGoodsCode" runat="server" class="span2" type="text" />
                                                                          /				
                                                                        <input id="txtGoodsSpec" runat="server" class="span2" type="text" />
																																																																					   /				
                                                                        <input id="txtGoodsCompany" runat="server" class="span2" type="text" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td>价格/成本价/库存数量
																		<%--<br />
                                                                        /金卡/银卡会员价--%>：</td>
                                                                    <td style="text-align: left">
                                                                        <input id="txtGoodsPrice" runat="server" class="span2" type="text" />
                                                                        /	
                                                                        <input id="txtGoodsCost" runat="server" class="span2" type="text" />
                                                                        /	
                                                                        <input id="txtStockNum" runat="server" class="span2" type="text" />
                                                                      <%--  /	
                                                                        <input id="txtGoldPrice" runat="server" class="span2" type="text" />
                                                                        /	
                                                                        <input id="txtSilverprice" runat="server" class="span2" type="text" />
                                                     --%>               </td>
                                                                </tr>
                                                                <tr class="even gradeC">
                                                                    <td>是否特价促销商品<br />
                                                                        /购买所获积分：</td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="drpSales" runat="server" Width="200">
                                                                            <asp:ListItem Value="-是否特价促销商品-"></asp:ListItem>
                                                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                                                            <asp:ListItem Value="N">否</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        /	
                                                                        <input id="txtGetGoodPoint"  runat="server" class="span2" type="text" /> [友情提示:选择否]
                                                                    </td>
                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td>是否积分兑换商品<br />
                                                                        /兑换所需积分：</td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="drpExchangeGood" runat="server" Width="200">
                                                                            <asp:ListItem Value="-是否积分兑换商品-"></asp:ListItem>
                                                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                                                            <asp:ListItem Value="N">否</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        /	
                                                                        <input id="txtExchangePoint" disabled="disabled" runat="server" class="span2" type="text" />[友情提示:选择否]
                                                                    </td>
                                                                </tr>
                                                                  <tr class="even gradeC">
                                                                    <td>是否限购商品<br />
                                                                        /限购数量：</td>
                                                                    <td style="text-align: left">
                                                                        <asp:DropDownList ID="drpXianGou" runat="server" Width="200">
                                                                            <asp:ListItem Value="-是否限购商品-"></asp:ListItem>
                                                                            <asp:ListItem Value="Y">是</asp:ListItem>
                                                                            <asp:ListItem Value="N">否</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        /	
                                                                        <input id="txtXianGouNumber" disabled="disabled" runat="server" class="span2" type="text" />[友情提示:选择否]
                                                                    </td>
                                                                </tr>
                                                          
                                                                        <tr class="even gradeC">
                                                                    <td>商品到期日期：</td>
                                                                    <td style="text-align: left">
                                                                    <input id="txtExpireDate" runat="server" class="span2" type="text" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td>商品内容描述：</td>
                                                                    <td style="text-align: left">
                                                                        <textarea id="txtFckContent" runat="server" style="width: 98%; height: 250px;"></textarea>
                                                                    </td>
                                                                </tr>

                                                                <tr class="even gradeC">
                                                                    <td>商品图片：</td>
                                                                    <td style="text-align: left">
                                                                        <div style="float: left; padding-right: 5px;">
                                                                            <input type="file" runat="server" style="height: 26px; width: 220px;" onchange="PhotoType();"
                                                                                id="fUpLoad" />
                                                                            <input type="hidden" id="HFurl" runat="server" />
                                                                            <asp:Button ID="iUpLoad" CssClass="btn btn-info" Text="上传" runat="server" OnClick="iUpLoad_Click" />
                                                                            <br />
                                                                            <strong>允许上传商品图片格式:GIF、JPG、JPEG、BMP<br />
                                                                                &nbsp;单张商品图片上传不能超过4096KB(4M)</strong>
                                                                        </div>

                                                                        <img style="width: 197px; height: 197px;" id="iShowPhoto" src="~/assets/images/nophoto.gif"
                                                                            runat="server" />
                                                                    </td>

                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td rowspan="2">分成比例：</td>
                                                                    <td style="text-align: left">
                                                                        <input type="radio" value="0" id="rpSelect1" name="rpSelect" checked="checked" />
                                                                        默认分成
                        <input type="radio" value="1" id="rpSelect2" name="rpSelect" />自定义分成   &nbsp;&nbsp;&nbsp;<span style="color: red; font-weight: bold">(分成比例（销售价-成本价）*75%*20%/10%)</span>
                                                                    </td>

                                                                </tr>
                                                                <tr class="even gradeC">
                                                                    <td style="float: left; text-align: left">
                                                                        <div id="vvip1" runat="server">
                                                                            <table style="width: 300px;">
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV1 &nbsp;   一级会员：&nbsp;   <span id="sp1" runat="server" style="color: red; font-weight: bold">分成比例 （销售价-成本价）*75%* 20%=0.00元</span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV2 &nbsp;   二级会员：&nbsp;   <span id="sp2" runat="server" style="color: red; font-weight: bold">分成比例 （销售价-成本价）*75%* 20%=0.00元</span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV3 &nbsp;   三级会员：&nbsp;   <span id="sp3" runat="server" style="color: red; font-weight: bold">分成比例 （销售价-成本价）*75%* 10%=0.00元</span></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div id="vvip2" runat="server">
                                                                            <input type="hidden" id="ticheng" runat="server" />
                                                                            <table style="width: 300px;">
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV1 &nbsp;   一级会员：&nbsp;   <span style="color: red; font-weight: bold">分成比例（销售价-成本价）*75%*</span><input id="Text1" runat="server" value="20" class="span3" type="text" />
                                                                                        <span id="spz1" runat="server" style="color: red; font-weight: bold">%=0.00元</span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV2 &nbsp;   二级会员：&nbsp;   <span style="color: red; font-weight: bold">分成比例（销售价-成本价）*75%*</span><input id="Text2" runat="server" value="20" class="span3" type="text" /><span id="spz2" runat="server" style="color: red; font-weight: bold">%=0.00元</span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="border: 1px solid #f4cfcf;">LV3 &nbsp;   三级会员：&nbsp;   <span style="color: red; font-weight: bold">分成比例（销售价-成本价）*75%*</span><input id="Text3" value="10" runat="server" class="span3" type="text" /><span id="spz3" runat="server" style="color: red; font-weight: bold">%=0.00元</span></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>

                                                                </tr>
                                                                <tr class="even gradeX">
                                                                    <td colspan="2">

                                                                        <div style="float: left; text-align: left">
                                                                            <span style="color: red; font-weight: bold">友情提示：	</span>
                                                                            <br />
                                                                            <strong>第一步：先点击浏览选择要上传的案例图片，然后点击上传按钮进行上传。<br />
                                                                                上传成功会出现提示，右边则显示出上传的商品图片。<br />

                                                                                第二部：然后进行添加信息。最后点击确定，则上传商品图片和信息添加成功。</strong>
                                                                        </div>

                                                                        <asp:Button ID="btnAddGoods" Width="80" CssClass="btn btn-info" Text="添加" runat="server" OnClick="btnAddGoods_Click" />

                                                                        &nbsp;&nbsp;&nbsp;			
														<asp:HiddenField ID="hfDel" runat="server" />
                                                                        <asp:Button ID="btnUpdateGoods" Width="80" CssClass="btn btn-info" Text="修改" runat="server" OnClick="btnUpdateGoods_Click" />
                                                                        &nbsp;&nbsp;&nbsp;	
														<input type="button" class="btn btn-info" id="btnReset" runat="server"
                                                            style="width: 80px; height: 30px;" value="清除" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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

        <!-- Uniform Script -->
        <script src="plugins/uniform/jquery.uniform.min.js"></script>

        <link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
        <script src="plugins/msgbox/jquery.msgbox.min.js"></script>
					<link href="assets/css/jquery.datetimepicker.css" rel="stylesheet" />
					<script src="assets/js/jquery.datetimepicker.js"></script>
        <script src="assets/js/SeachCommon.js"></script>

        <p id="back-to-top">
            <a href="#top" title="返回顶部">
                <img src="assets/images/top_arrow.png" style="border: 0;" /></a>
        </p>
    </form>
</body>
</html>
