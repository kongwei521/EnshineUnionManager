<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="EnshineUnionManager.WebForm4" %>

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

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
    <script src="assets/js/html5shiv.min.js"></script>
    <script src="assets/js/respond.min.js"></script>
    <![endif]-->
	 <title>HXD :: Web Client Search Platform </title>
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
											 <img src="assets/images/logo.png" alt="" />
										</a>
								   </div>
							  </div>

							  <div id="header-right" class="clearfix">

								   <div id="dropdown-lists">
											<a class="item" href="#">
									<span id="spClientName" runat="server" ></span>
									  Welcome To HXD Web Search
								</a>

								   </div>

								   <div id="header-functions" class="pull-right">
										<div id="user-info" class="clearfix">
											 Today： <span id="spNowTime" runat="server"></span>
										</div>
										<div id="logout-ribbon" title="退出登陆">
											 <a href="javascript:LoginOut('UserLogin','');"><i class="icon-off"></i></a>
										</div>
								   </div>
							  </div>
						 </div>
					</div>
			   </header>

			   <div id="content-wrap">
					   <section id="main" class="clearfix" style="border-left: 1px solid #ddd; border-right: 1px solid #ddd; margin-left: 40px; margin-right: 40px;">
                    <!--菜单导航-->
                    <div style="border-bottom: 1px solid #ddd">
                        <ul class="stats-container" style="margin-bottom: 5px; margin-top: 10px;">
                            <li>
                                <a href="PlanNoIndex.aspx" class="stat summary" data-target="#live">
                                    <span class="icon icon-circle bg-green">
                                        <i class="icon-home"></i>
                                    </span>
                                    <span class="digit">本月出<br />
                                        库订单
                                    </span>
                                </a>
                            </li>
                            <li>
                                <a href="MultipleSearch.aspx" class="stat summary" data-target="#math">
                                    <span class="icon icon-circle bg-blue">
                                        <i class="icon-search"></i>
                                    </span>
                                    <span class="digit">订单明<br />
                                        细查询
                                    </span>
                                </a>
                            </li>
                            <li>
                                <a href="BadProduct.aspx" class="stat summary" data-target="#fb">
                                    <span class="icon icon-circle bg-red">
                                        <i class="icon-bold"></i>
                                    </span>
                                    <span class="digit">B品实<br />
                                        时查询
                                    </span>
                                </a>
                            </li>
 <li>
                                <a href="MultipleTotalSearch.aspx" class="stat summary" data-target="#fb">
                                    <span class="icon icon-circle bg-orange">
                                        <i class="icon-file-excel"></i>
                                    </span>
                                    <span class="digit">综合汇<br />
                                        总统计
                                    </span>
                                </a>
                            </li>

                            <li>
                                <a href="UpdatePassWord.aspx" class="stat summary"   >
                                    <span class="icon icon-circle ">
                                        <i class="icon-key-2"></i>
                                    </span>
                                    <span class="digit">修改登<br />
                                        陆密码
                                    </span>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div id="main-header" class="page-header">

                        <!--查询条件-->
                        <div class="widget-box">
                            <h1>
                                <i class="icon-zoom-in"></i>出库计划单号查询 <span>This is the place where search planno info started</span>
                            </h1>
                            <br />
                            <div class="widget-content nopadding">
                                <!--存放客户ID-->
                                <input type="hidden" id="hfCustomerID" runat="server" />

                                <table class="table table-bordered table-striped">
                                    <tbody>
                                        <tr class="odd gradeX">

                                            <td>计划编号：</td>
                                            <td>
                                                <input type="text" id="txtPlanNo" class="text-info" /></td>
                                            <td>客户编号：</td>
                                            <td>
                                                <input type="text" runat="server" id="txtCustomerID" class="text-info" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr class="even gradeC">
                                            <td>委托日期：</td>
                                            <td>
                                                <input id="txtPlanTimeSelect" class="text-info" type="text" />
                                            </td>
                                            <td style="text-align: right" colspan="2">
                                                <span style="color: red">（友情提示：选择多个条件进行查询，结果会更精确。）</span>
                                                <input type="button" class="btn btn-info" id="btnSearchPlanNo"
                                                    style="width: 80px; height: 30px;" value="查询" />&nbsp;&nbsp;&nbsp;
															<input type="button" class="btn btn-info" id="btnResetPlanNo"
                                                                style="width: 80px; height: 30px;" value="清除" />

                                            </td>

                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <h1>
                            <i class="icon-list"></i>检品检针进度一览 <span>This is the place where Search Result started</span>
                        </h1>
                    </div>
                   	<!--检品检针进度一览内容-->
										<div id="main-content">
                                            <!--检品检针tab切换-->
                                           <div class="stats-container">
                                                <a href="#" class="stat summary" data-target="#openbox" data-toggle="tab" id="clickOpenbox">
                                                    <div class="alert   fade in" style="width: 155px;">
                                                        <span class="icon icon-circle bg-blue">
                                                            <i class="icon-scissor"></i>
                                                        </span>
                                                        <span class="digit">上货开箱<br />
                                                            信息
                                                        </span>
                                                    </div>
                                                </a>&nbsp;&nbsp;&nbsp;
                                            <a href="#" class="stat summary" data-target="#jianpin" data-toggle="tab" id="clickJianpin">
                                                <div class="alert   fade in" style="width: 155px;">
                                                    <span class="icon icon-circle bg-red">
                                                        <i class="icon-t-shirt"></i>
                                                    </span>
                                                    <span class="digit">检品检针<br />
                                                        信息
                                                    </span>
                                                </div>
                                            </a>&nbsp;&nbsp;&nbsp;
                                            <a href="#" class="stat summary" data-target="#jianzhen" data-toggle="tab" id="clickJianZhen">
                                                <div class="alert   fade in" style="width: 155px;">
                                                    <span class="icon icon-circle bg-green">
                                                        <i class="icon-camera"></i>
                                                    </span>
                                                    <span class="digit">直过检针<br />
                                                        信息
                                                    </span>
                                                </div>
                                            </a>
                                                &nbsp;&nbsp;
                                             <a href="#" class="stat summary" id="clickNeedle" data-target="#boxing" data-toggle="tab">
                                                   <div class="alert " style="width: 155px;">
                                                       <span class="icon icon-circle bg-orange">
                                                           <i class="icon-attachment"></i>
                                                       </span>
                                                       <span class="digit">装箱综合<br />
                                                           信息
                                                       </span>
                                                   </div>
                                               </a>
                                            </div>

										 
										</div>
                </section>
			   </div>
			  


			   <footer id="footer">
					<div class="footer-left">HXD - Web Client Search Admin</div>
					<div class="footer-right">
						 <p>Copyright 2014© HXD Web Client Search. All Rights Reserved.</p>
					</div>
			   </footer>

		  </div>

		  <!-- Core Scripts -->
		  <script src="assets/js/jquery-1.8.3.min.js"></script>
		  <script src="bootstrap/js/bootstrap.min.js"></script>

		  <!-- Template Script -->
		  <script src="assets/js/template.js"></script>
		  <script src="assets/js/setup.js"></script>
 
		  <!-- Uniform Script -->
		  <script src="plugins/uniform/jquery.uniform.min.js"></script>

		  <!--Daterangepicker Script-->
		  <script type="text/javascript" src="assets/js/date.js"></script>
		  <script type="text/javascript" src="assets/js/daterangepicker.min.js"></script>
		  <link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
		  <script src="plugins/msgbox/jquery.msgbox.min.js"></script>
		  <script src="assets/js/SearchJsonData.js"></script>
		 <script src="assets/js/Dialog.js"></script>
		 <link href="assets/css/Dialog.1.0.css" rel="stylesheet" />
		  <p id="back-to-top">
			   <a href="#top" title="返回顶部">
					<img src="assets/images/top_arrow.png" style="border: 0;" /></a>
		  </p>
	 </form>
</body>
</html>
