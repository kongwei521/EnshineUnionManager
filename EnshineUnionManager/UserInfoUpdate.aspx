<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoUpdate.aspx.cs" EnableEventValidation="false"
	Inherits="EnshineUnionManager.UserInfoUpdate" %>

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
	<link href="assets/css/jquery.datetimepicker.css" rel="stylesheet" />
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
										<li class="active" >
											<span title="会员中心">
												<i class="icon-users"></i>
												<span class="nav-title">会员中心</span>
											</span>
											<ul class="inner-nav">
												<li class="active"><a href="UserinfoManager.aspx"><i class="icol-group"></i><span class="digit">会员信息管理</span></a></li>

												<!--<li ><a href="PerfectUserInfo.aspx"><i class="icol-application-edit"></i><span class="digit">会员信息完善</span></a></li>-->
												<li><a href="UserRecharge.aspx"><i class="icol-money-yen "></i><span class="digit">会员充值管理</span></a></li>

												<!--<li><a href="UpdatePassWord.aspx"><i class="icol-ui-text-field-password"></i><span class="digit">修改会员密码</span></a></li>-->

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

									<!--查询条件-->
									<div class="widget-box">
										<h1>
											<i class="icon-zoom-in"></i>用户信息修改 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div id="live" class="tab-pane active">
											<div class='row-fluid'>
												<div class='span12 widget'>
													<div class='widget-header'>
														<span class='title'>
															<i class='icol-blog'></i>用户信息修改
														</span>
														<div style='float: right; padding-top: 4px; padding-right: 4px;'>
															<input type='button' class='btn  btn-primary' value='用户列表管理'
																onclick="window.location.href = 'UserinfoManager.aspx'" />
														</div>
													</div>
													<div class="widget-content nopadding">
														<table class="table table-bordered table-striped">
															<tbody>
																<tr class="odd gradeX">
																	<td style="width: 120px;">昵称：</td>
																	<td style="text-align: left;">
																		<input id="txtNickName" runat="server" class="span7" />
																	</td>
																	<td>姓名：</td>
																	<td style="text-align: left">
																		<input id="txtName" runat="server" class="span10" type="text" />
																	</td>
																</tr>
																<tr class="even gradeC">
																	<td style="width: 120px;">密码：</td>
																	<td style="text-align: left">
																		<input id="txtPass" runat="server" class="span7" type="text" />
																	</td>

																	<td style="width: 120px;">电话：</td>
																	<td style="text-align: left">
																		<input id="txtTel" runat="server" class="span10" type="text" />
																	</td>

																</tr>

																<tr class="odd gradeX">
																	<td>性别：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="drpSex" runat="server">
																			<asp:ListItem>-请选择性别-</asp:ListItem>
																			<asp:ListItem Value="Y">男</asp:ListItem>
																			<asp:ListItem Value="N">女</asp:ListItem>
																		</asp:DropDownList>

																	</td>
																	<td>邮箱：</td>
																	<td style="text-align: left">
																		<input id="txtEmail" runat="server" class="span10" type="text" />
																	</td>

																</tr>
																<tr class="odd gradeC">
																	<td>区市：</td>
																	<td style="text-align: left">
					 
																		<asp:DropDownList ID="seachprov" runat="server" CssClass="span4" AutoPostBack="true" OnSelectedIndexChanged="seachprov_SelectedIndexChanged"></asp:DropDownList>
																		<asp:DropDownList ID="seachcity" runat="server" CssClass="span4" AutoPostBack="true" OnSelectedIndexChanged="seachcity_SelectedIndexChanged">
																			<asp:ListItem Value="0">请选择</asp:ListItem>
																		</asp:DropDownList>
																		<asp:DropDownList ID="seachdistrict" runat="server" CssClass="span4">
																			<asp:ListItem Value="0">请选择</asp:ListItem>
																		</asp:DropDownList>
																	</td>
																	<td>地址：</td>
																	<td style="text-align: left">
																		<textarea id="txtAddress" runat="server" class="span12" style="height: 50px;"></textarea>

																	</td>

																</tr>
																			<tr class="odd gradeX">
																	<td>身份证号：</td>
																	<td style="text-align: left">
																	<input id="txtCardNo" runat="server" class="span7" type="text" />
																	</td>
																	<td>种植亩数：</td>
																	<td style="text-align: left">
																		<input id="txtPlantNum" runat="server" class="span10" type="text" />
																	</td>

																</tr>
																<tr>
																	<td>家庭人数：</td>
																	<td style="text-align: left">
																		<input id="txtHomeNum"  runat="server" class="span7" type="text" />
																	</td>

																		<td>邀请码：</td>
																	<td style="text-align: left">
																		<input id="txtInvitedCode" disabled="disabled" runat="server" class="span7" type="text" />
																	</td>
																</tr>
																<tr>
																	<td colspan="4">
																			<asp:Button ID="btnAddUserInfo" Width="80" CssClass="btn btn-info" Text="添加"
																			runat="server" OnClick="btnAddUserInfo_Click" />						&nbsp;&nbsp;&nbsp;	
																		<asp:Button ID="btnUpdateUserInfo" Width="80" CssClass="btn btn-info" Text="修改"
																			runat="server" OnClick="btnUpdateUserInfo_Click" />
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
		<script src="assets/js/setup.js"></script>

		<!-- Customizer, remove if not needed -->
		<script src="assets/js/customizer.js"></script>

		<!-- Uniform Script -->
		<script src="plugins/uniform/jquery.uniform.min.js"></script>

		<!--Daterangepicker Script-->
		<script src="assets/js/jquery.datetimepicker.js"></script>
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
