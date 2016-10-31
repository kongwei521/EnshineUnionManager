<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersDetails.aspx.cs" Inherits="EnshineUnionManager.OrdersDetails" %>
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
    <!-- jquery-ui Stylesheets -->
<link rel="stylesheet" href="assets/jui/css/jquery.ui.all.css" media="screen">
<link rel="stylesheet" href="assets/jui/jquery-ui.custom.css" media="screen">
<link rel="stylesheet" href="assets/jui/timepicker/jquery-ui-timepicker.css" media="screen">

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
 											 
									<h1>
										<i class="icon-list"></i>订单列表一览 <span>This is the place where Search Result started</span>
									</h1>	
								</div>
								<!--库存余量查询数据一览内容-->
								<div id="main-content">
									<div class="row-fluid">

										<div class="span12 widget">
											<div class="widget-header">
												<span class="title" style="font-size:14px;font-weight:bold" id="spOrderno" runat="server"> </span>
												<div style='float: right; padding-top: 4px; padding-right: 4px;'>
													<input type='button' class='btn  btn-primary' value='返回订单管理' onclick="window.location.href = 'OrderListManager.aspx'" />
												</div>
											</div>
											<div class="widget-content table-container">
												<table class="table table-striped table-detail-view">
													<thead>
														<tr>
															<th colspan="2" style="font-size:14px;font-weight:bold"><i class="icol-exclamation"></i>基础信息</th>
														</tr>
													</thead>
													<tbody>
														<tr>
															<th style="width: 250px;">订单编号：</th>
															<td><span id="spONO" runat="server"></span></td>
														</tr>
														<tr>
															<th>订单状态：</th>
															<td><span id="spOrderStatus" runat="server"></span></td>
														</tr>
														<tr>
															<th >订单来源：</th>
															<td ><span id="spOrdersource" runat="server"></span></td>
														</tr>
														<tr>
															<th>下单时间：</th>
															<td><span id="spordertime" runat="server"></span></td>
														</tr>

													</tbody>
													<thead>
														<tr>
															<th colspan="2" style="font-size:14px;font-weight:bold"><i class="icol-user-business-boss"></i>收货信息</th>
														</tr>
													</thead>
													<tbody>
														<tr>
															<th>收货人姓名：</th>
															<td><span id="spName" runat="server"></span></td>
														</tr>
														<tr>
															<th>联系电话： </th>
															<td><span id="spTel" runat="server"></span></td>
														</tr>
                                                        <tr>
															<th>区市： </th>
															<td><span id="spAreacity" runat="server"></span></td>
														</tr>
														<tr>
															<th>收货地址：</th>
															<td><span id="spAddress" runat="server"></span></td>
														</tr>
													</tbody>
													<thead>
														<tr>
															<th colspan="2" style="font-size:14px;font-weight:bold"><i class="icol-world"></i>商品信息 </th>

														</tr>
													</thead>
													<tbody>
														<tr>
															<td colspan="2">

																<div class="widget-content table-container">
                                                                    <table class="table table-condensed">
																		<thead>

																			<tr>
																				<th style="border-top: 1px solid #dddddd">商品条码</th>
																				<th style="border-top: 1px solid #dddddd">商品名称</th>
																				<th style="border-top: 1px solid #dddddd">商品分类</th>
																				<th style="border-top: 1px solid #dddddd">商品图片</th>
																				<th style="border-top: 1px solid #dddddd">商品价格</th>
																			<%--	<th style="border-top: 1px solid #dddddd">优惠后价格</th>--%>
																				<th style="border-top: 1px solid #dddddd">数量</th>
																				<th style="border-top: 1px solid #dddddd">小计</th>

																			</tr>
																		</thead><tbody>
                                                                    <asp:Repeater id="rpGoogsList" runat="server" OnItemDataBound="rpGoogsList_ItemDataBound" >
                                                                        <ItemTemplate>
                                                                            	<tr>
                                                                                    <td><%#Eval("GoodsCode") %></td>
                                                                                    <td><%#Eval("GoodsTitle") %></td>
                                                                                    <td><%#Eval("GoodsSort") %></td>                                                               
                                                                                    <td><img src=" <%#Eval("GoodsImg")== "" ? "assets/images/nophoto.gif" : Eval("GoodsImg")%>" style='width:60px;height:60px;'/>
                                                                                       </td>
                                                                                    <td><%#Eval("GoodsPrice") %></td>
                                                                                    <td><%#Eval("SumQty") %></td>
                                                                                    <td><%# Convert.ToDecimal(Eval("GoodsPrice"))* int.Parse(Eval("SumQty").ToString())  %></td>
                                                                                </tr>
																	 
																		
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
																	
																													<tr>
																				<td style="text-align: right" colspan="6">商品总金额：</td>
																				<td><asp:Label id="spSumPrice" runat="server"></asp:Label></td>
                                                                                
																			</tr>
																		
																		</tbody>
																	</table>
																</div>
															</td>
														</tr>
													</tbody>
												</table>
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

