<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageViewManager.aspx.cs" Inherits="EnshineUnionManager.MessageViewManager" %>

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
									 管理后台
								</a>
							</div>
						</div>

						<div id="header-right" class="clearfix"  >

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
										<i class="icon-list"></i>商品销售排行榜信息一览 <span>This is the place where Search Result started</span>
									</h1>	
								</div>
								<!--库存余量查询数据一览内容-->
								<div id="main-content">
									<div class="tab-content">
										<!--分页-->
										<div id="showPage" style="width: 650px; margin: 0 auto; display:none " class="pages">
											<div style="float: left">
												<a id="first" class="pages">首页</a>
												<a id="prev" class="pages">上页</a>
												<a id="next" class="pages">下页</a>
												<a id="last" class="pages">尾页</a>
												跳转到第<input type="text" id="txtGoPage" style="width: 45px; height: 15px; border: 1px solid" />
												页
											</div>
											<div style="margin: 0; float: left">
												&nbsp;<input type="button" class="pages btn btn-info" id="go" value="跳转" />
												&nbsp; 共<span id="SumCount"></span> 条数据，每页<span id="ItemCount"></span> 条，
												 当前<span id="Index"></span>/<span id="PageCount"></span>页
											</div>
										</div>
										<!--我的产品档案一览-->
										<div id="live" class="tab-pane active">
											 <div class='row-fluid'>
												  <div class='span12 widget'>
													   <div class='widget-header'>
															<span class='title'>
																 <i class='icol-blog'></i>商品销售排行榜信息统计
															</span>
														
													   </div>
													   <div class='widget-content summary-list'>
															<div class='row-fluid' style='padding-top: 2px;'>
																 <div class='span12 section divScrool'>
																	  <table class='table table-bordered table-striped'>
																		   <thead>
																						<tr>
																							<th>留言建议人</th>
																							<th>留言建议电话</th>
																							<th>留言建议内容</th>
																						 	<th>留言建议时间</th>
																						</tr>
																					</thead>
																		   <tbody id="divShowMessageViewManager">
																		   </tbody>
																	  </table>
																 </div>
															</div>
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
		  <script src="assets/js/SeachCommon.js"></script>

		<p id="back-to-top">
			<a href="#top" title="返回顶部">
				<img src="assets/images/top_arrow.png" style="border: 0;" /></a>
		</p>
	</form>

</body>
</html>

