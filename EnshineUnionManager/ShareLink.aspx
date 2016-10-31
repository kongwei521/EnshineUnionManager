<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareLink.aspx.cs" Inherits="EnshineUnionManager.ShareLink" %>


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
										<li>
											<span title="后台首页">
												<i class="icon-home"></i>
												<span class="nav-title">后台首页</span>
											</span>
											<ul class="inner-nav">
												<li><a href="Index.aspx"><i class="icol-house"></i><span class="digit">我的后台首页</span></a></li>
											</ul>
										</li>
										<li>
											<span title="设置中心">
												<i class="icon-cog-2 "></i>
												<span class="nav-title">设置中心</span>
											</span>
											<ul class="inner-nav">
												<li><a href="AdManager.aspx"><i class="icol-keyboard"></i><span class="digit">广告设置管理</span></a></li>

												<li><a href="NewsSortManager.aspx"><i class="icol-text-signature"></i><span class="digit">新闻分类管理</span></a></li>
											 <!--<li><a href="PlantDoctorSortManger.aspx"><i class="icol-paintbrush"></i><span class="digit">植保分类管理</span></a></li>-->
												<li><a href="GoodsSortManager.aspx"><i class="icol-text-bold"></i><span class="digit">商品分类管理</span></a></li>
												<li><a href="MemberManager.aspx"><i class="icol-cog"></i>会员设置管理</a></li>
												<%--																																			<li><a href="UserRoleManager.aspx"><i class="icol-application-key"></i>角色权限管理</a></li> --%>
											</ul>
										</li>

										<li>
											<span title="发布中心">
												<i class="icon-edit"></i>
												<span class="nav-title">发布中心</span>
											</span>
											<ul class="inner-nav">
												<li><a href="NewsNoticesManager.aspx"><i class="icol-box"></i><span class="digit">新闻信息管理</span></a></li>
												<!--<li><a href="PlantDoctorManager.aspx"><i class="icol-compass"></i><span class="digit">植保医院信息管理</span></a></li>-->

											</ul>
										</li>
										<li>
											<span title="活动中心">
												<i class="icon-android"></i>
												<span class="nav-title">活动中心</span>
											</span>
											<ul class="inner-nav">
												<li><a href="HuoDongManager.aspx"><i class="icol-user"></i>活动管理信息</a></li>
												<li><a href="JoinHuoDongManager.aspx"><i class="icol-rss"></i><span class="digit">参与活动管理</span></a></li>
											</ul>
										</li>
										<li>
											<span title="分销管理">
												<i class="icon-gift"></i>
												<span class="nav-title">分销管理</span>
											</span>
											<ul class="inner-nav">
												<li><a href="UserExtractMoenyManager.aspx"><i class="icol-money"></i>会员提现管理</a></li>
												<li><a href="MemberFenXiaoTop.aspx"><i class="icol-style"></i>会员分销排行榜</a></li>
												<li><a href="MemberBuyTop.aspx"><i class="icol-text-bold"></i>会员消费排行榜</a></li>
												<li><a href="MemberAllFenXiaoTop.aspx"><i class="icol-spellcheck"></i>分销能力排行榜</a></li>
											</ul>
										</li>
										<li  >
											<span title="会员中心">
												<i class="icon-users"></i>
												<span class="nav-title">会员中心</span>
											</span>
											<ul class="inner-nav">
												<li><a href="UserinfoManager.aspx"><i class="icol-group"></i><span class="digit">会员信息管理</span></a></li>

												<li><a href="PerfectUserInfo.aspx"><i class="icol-application-edit"></i><span class="digit">会员信息完善</span></a></li>
												<li><a href="UserRecharge.aspx"><i class="icol-money-yen "></i><span class="digit">会员充值管理</span></a></li>

												<li><a href="UpdatePassWord.aspx"><i class="icol-ui-text-field-password"></i><span class="digit">修改会员密码</span></a></li>

											</ul>
										</li>
										<li>
											<span title="订单管理">
												<i class="icon-shopping-cart"></i>
												<span class="nav-title">订单管理</span>
											</span>
											<ul class="inner-nav">
												<li><a href="OrderListManager.aspx"><i class="icol-text-list-numbers"></i>订单管理信息</a></li>
																		<li><a href="GoodsShipManager.aspx"><i class="icol-delivery"></i>商品发货管理</a></li>
												<li><a href="CoCheckExport.aspx"><i class="icol-accept"></i>厂家核对报表</a></li>
					
													</ul>
										</li>
										<li>
											<span title="商品管理">
												<i class="icon-list"></i>
												<span class="nav-title">商品管理</span>
											</span>
											<ul class="inner-nav">
												<li><a href="GoodsManager.aspx"><i class="icol-timeline-marker"></i><span class="digit">商品信息管理</span></a></li>
												<!--<li><a href="GroupBuyManager.aspx"><i class="icol-cart"></i><span class="digit">团购商品管理</span></a></li>-->

											</ul>
										</li>

										<li>
											<span title="财务管理">
												<i class="icon-stats-up"></i>
												<span class="nav-title">财务管理</span>
											</span>
											<ul class="inner-nav">
												<li><a href="FinanceMonthReport.aspx"><i class="icol-chart-curve"></i>月销售金额统计</a></li>
												<li><a href="FenXiaoExtractRport.aspx"><i class="icol-chart-bar"></i>月分销提成统计</a></li>
												<li><a href="MemberExtractReport.aspx"><i class="icol-chart-pie"></i>月会员提现统计</a></li>
											</ul>
										</li>

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
									<div class="widget-box">
										<h1>
											<i class="icon-zoom-in"></i>一键分享推广链接 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div class="widget-content nopadding">


											<div class="gb_resLay">
												<div class="gb_res_t"><span>分享到</span><i></i></div>
												<div class="bdsharebuttonbox">
													<ul class="gb_resItms">
														<li><a title="分享到微信" href="#" class="s_weixin" data-cmd="weixin"></a>微信好友 </li>
														<li><a title="分享到QQ好友" href="#" class="s_sqq" data-cmd="sqq"></a>QQ好友 </li>
														<li><a title="分享到QQ空间" href="#" class="s_qzone" data-cmd="qzone"></a>QQ空间 </li>
														<li><a title="分享到腾讯微博" href="#" class="s_tqq" data-cmd="tqq"></a>腾讯微博 </li>
														<li><a title="分享到新浪微博" href="#" class="s_tsina" data-cmd="tsina"></a>新浪微博 </li>
														<li><a title="分享到人人网" href="#" class="s_renren" data-cmd="renren"></a>人人网 </li>
													</ul>
												</div>
											</div>
											<div style="margin: 0 auto; text-align: center;">

												<input id="txtName" name="txtName" runat="server" class="text-info span5" disabled="disabled" type="text" />
												<input type="button" class="btn btn-info" id="btnCopy" value="复制" />

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
					<p>Copyright 2015©正益移动科技有限公司. All Rights Reserved.</p>
				</div>
			</footer>

		</div>

		<!-- Core Scripts -->
		<script src="assets/js/jquery-1.8.2.min.js"></script>
		<script src="bootstrap/js/bootstrap.min.js"></script>

		<!-- Template Script -->
		<script src="assets/js/template.js"></script>
				<link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
		<script src="plugins/msgbox/jquery.msgbox.min.js"></script>
		<script src="assets/js/jquery.zclip.js"></script>
           <link href="assets/css/sharelink.css" rel="stylesheet" />

		<script >

			$(document).ready(function () {
				//复制分享链接
				$("#btnCopy").zclip({
					path: 'assets/js/ZeroClipboard.swf',
					copy: $('#txtName').val(),
					afterCopy: function () {
						$.msgbox("<span style='font-size:12px;line-height:30px;'>复制成功,可以在朋友圈进行分享了。</span>", { type: "info", buttons: [{ type: "submit", value: "确定" }] });

					}
				});
				//SNS 分享链接
				var vcode = "" + $("#txtName").val() + "";
				window._bd_share_config =
				{
	"common": {
		"bdSnsKey": {}, "bdText": vcode,
		"bdMini": "1",
		"bdMiniList": false,
		"bdPic": "",
		"bdStyle": "1",
		"bdSize": "16"
	}, "share": {},
	"image": {
		"viewList": ["weixin", "sqq", "qzone", "tqq", "tsina", "renren"],
		"viewText": "分享到：", "viewSize": "16"
	}, "selectShare": {
		"bdContainerClass": null,
		"bdSelectMiniList": ["weixin", "sqq", "qzone", "tqq", "tsina", "renren"]
	}
};
				with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src
					= 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];

			});

		</script>

		<p id="back-to-top">
			<a href="#top" title="返回顶部">
				<img src="assets/images/top_arrow.png" style="border: 0;" /></a>
		</p>
	</form>

</body>
</html>
