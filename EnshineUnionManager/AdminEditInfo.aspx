<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEditInfo.aspx.cs" Inherits="EnshineUnionManager.AdminEditInfo" %>

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

									<!--查询条件-->
									<div class="widget-box">
										<h1>
											<i class="icon-zoom-in"></i>管理员信息编辑 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div id="live" class="tab-pane active">
											<div class='row-fluid'>
												<div class='span12 widget'>
													<div class='widget-header'>
														<span class='title'>管理员信息编辑
														</span>
														<div style='float: right; padding-top: 4px; padding-right: 4px;'>
															<input type='button' class='btn  btn-primary' value='管理员信息管理'
																onclick="window.location.href = 'AdminManager.aspx?mid=<%=Request["mid"]%>'" />
														</div>
													</div>
													<div class="widget-content nopadding">
														<table class="table table-bordered table-striped">
															<tbody>
																<tr class="odd gradeX">
																	<td style="width: 100px;">用户名：</td>
																	<td style="text-align: left;">
																		<input id="txtlogname" runat="server" class="span7" />
																	</td>
																	<td style="width: 100px;">姓名：</td>
																	<td style="text-align: left">
																		<input id="txttruename" runat="server" class="span10" type="text" />
																	</td>
																</tr>
																<tr class="even gradeC">
																	<td>密码：</td>
																	<td style="text-align: left">
																		<input id="txtPass" runat="server" class="span7" type="text" />
																	</td>

																	<td>电话：</td>
																	<td style="text-align: left">
																		<input id="txtTel" runat="server" maxlength="11" class="span10" type="text" />
																	</td>

																</tr>

																<tr class="odd gradeX">


																	<td>角色：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="ddlrole" runat="server" Width="140">
																		</asp:DropDownList>

																	</td>
																	<td>是否禁用：</td>
																	<td style="text-align: left">
																		<asp:CheckBox ID="cbifdisable" runat="server" Width="100px" /></td>
													 
																</tr>
																<tr class="odd gradeX">

																	<td colspan="4">
																		<asp:Button ID="btnAddAdmin" Width="80" CssClass="btn btn-info" Text="添加" runat="server" OnClick="btnAddNotices_Click" />

																		&nbsp;&nbsp;&nbsp;			
							
																								<asp:Button ID="btnUpdateAdmin" Width="80" CssClass="btn btn-info" Text="修改" runat="server" OnClick="btnUpdateNotices_Click" />
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
