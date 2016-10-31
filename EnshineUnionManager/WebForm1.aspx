<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EnshineUnionManager.WebForm1" %>

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
							<aside id="sidebar">
								<nav id="navigation" class="collapse">
									<ul>
										<li class="active">
											<span title="General">
												<i class="icon-home"></i>
												<span class="nav-title">Order Home</span>
											</span>
											<ul class="inner-nav">
												<li><a href="Index.aspx"><i class="icol-house"></i><span class="digit">我的后台首页</span></a></li>
												<li><a href="UserinfoManager.aspx"><i class="icol-group"></i><span class="digit">用户信息管理</span></a></li>
												<li class="active"><a href="NewsNoticesManager.aspx"><i class="icol-box"></i><span class="digit">新闻信息管理</span></a></li>
												<li><a href="NewsSortManager.aspx"><i class="icol-text-signature"></i><span class="digit">新闻分类管理</span></a></li>
												<li><a href="GoodsManager.aspx"><i class="icol-timeline-marker"></i><span class="digit">商品信息管理</span></a></li>
												<li><a href="GoodsSortManager.aspx"><i class="icol-text-bold"></i><span class="digit">商品分类管理</span></a></li>

						 
												<li><a href="OrderListManager.aspx"><i class="icol-text-list-numbers"></i>订单管理信息</a></li>
									 
											</ul>
										</li>
										<li style="height: 200px">&nbsp;</li>
									</ul>
								</nav>
							</aside>

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
											<i class="icon-zoom-in"></i>新闻/公告信息添加 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div id="live" class="tab-pane active">
											<div class='row-fluid'>
												<div class='span12 widget'>
													<div class='widget-header'>
														<span class='title'>
															<i class='icol-blog'></i>新闻/公告信息管理
														</span>
														<div style='float: right; padding-top: 4px; padding-right: 4px;'>
															<input type='button' class='btn  btn-primary' value='新闻/公告列表管理'
																onclick="window.location.href = 'NewsNoticesManager.aspx'" />
														</div>
													</div>
													<div class='widget-content summary-list'>
														<table class='table table-bordered table-striped'>

															<tbody>
																<tr>
																	<td style="width: 120px;">商品标题：</td>
																	<td style="text-align: left">
																		<input id="txtTitle" runat="server" class="span8" type="text" /></td>

																</tr>
																<tr>
																	<td>是否生效/是否首页显示：</td>
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
																<tr>
																	<td>商品分类：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="drpGoodsSort" runat="server"></asp:DropDownList></td>

																</tr>
																<tr class="even gradeC">
																	<td>商品条码/价格：</td>
																	<td style="text-align: left">
																		<input id="txtGoodsCode" runat="server" class="span3" type="text" />
																		/
                                                            	<input id="txtGoodsPrice" runat="server" class="span3" type="text" />
																	</td>
																</tr>
																<tr>
																	<td>商品内容：</td>
																	<td style="text-align: left">
																		<textarea id="txtFckContent" runat="server" style="width: 98%; height: 250px;"></textarea></td>

																</tr>
																<tr>
																	<td>商品图片：</td>
																	<td style="float: left; text-align: left">
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
																<tr>
																	<td colspan="2">

																		<div style="float: left; text-align: left">
																			<span style="color: red; font-weight: bold">友情提示：	</span>
																			<br />
																			<strong>第一步：先点击浏览选择要上传的案例图片，然后点击上传按钮进行上传。<br />
																				上传成功会出现提示，右边则显示出上传的商品图片。<br />
																				<br />
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
				 
								 
									<div class="bdsharebuttonbox"><a href="#" class="bds_more" data-cmd="more"></a><a title="分享到新浪微博" href="#" class="bds_tsina" data-cmd="tsina"></a><a title="分享到腾讯微博" href="#" class="bds_tqq" data-cmd="tqq"></a><a title="分享到QQ空间" href="#" class="bds_qzone" data-cmd="qzone"></a><a title="分享到微信" href="#" class="bds_weixin" data-cmd="weixin"></a><a title="分享到QQ好友" href="#" class="bds_sqq" data-cmd="sqq"></a></div>
									<script>window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "sxssssssssssssssss", "bdMini": "1", "bdMiniList": false, "bdPic": "", "bdStyle": "1", "bdSize": "16" }, "share": {}, "image": { "viewList": ["tsina", "tqq", "qzone", "weixin", "sqq"], "viewText": "分享到：", "viewSize": "16" }, "selectShare": { "bdContainerClass": null, "bdSelectMiniList": ["tsina", "tqq", "qzone", "weixin", "sqq"] } }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>
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
		<script src="assets/js/SeachCommon.js"></script>

		<p id="back-to-top">
			<a href="#top" title="返回顶部">
				<img src="assets/images/top_arrow.png" style="border: 0;" /></a>
		</p>
	</form>
</body>
</html>
