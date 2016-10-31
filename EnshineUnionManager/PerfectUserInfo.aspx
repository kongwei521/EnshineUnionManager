<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfectUserInfo.aspx.cs"
	EnableEventValidation="false" Inherits="EnshineUnionManager.PerfectUserInfo" %>

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
											<i class="icon-zoom-in"></i>完善会员信息 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div id="live" class="tab-pane active">
											<div class='row-fluid'>
												<div class='span12 widget'>
													<div class='widget-header'>
														<span class='title'>
															<i class='icol-blog'></i>完善会员信息
														</span>
																				 <div style='float: right; padding-top: 4px; padding-right: 4px;'> 
																		   <input type='button' class='btn  btn-primary'  value='会员信息管理' 
																						onclick="window.location.href = 'UserinfoManager.aspx?mid=<%=Request["mid"]%>'" />  
                                                                                 </div> 
													</div>
													<div class="widget-content nopadding">
														<table class="table table-bordered table-striped">
															<tbody>
																<tr class="odd gradeX">
																	<td style="width:100px;">昵称：</td>
																	<td style="text-align: left;">
																		<input id="txtNickName" runat="server" class="span7" />
																	</td>
																	<td style="width:100px;">姓名：</td>
																	<td style="text-align: left">
																		<input id="txtName" runat="server" class="span10" type="text" />
																	</td>
																</tr>
																<tr class="even gradeC">
																	<td>密码：</td>
																	<td style="text-align: left">
																		<input id="txtPass" runat="server"   class="span7" type="text" />
																	</td>

																	<td>电话：</td>
																	<td style="text-align: left">
																		<input id="txtTel" runat="server"  maxlength="11" class="span10" type="text" />
																	</td>

																</tr>

																		<tr class="odd gradeX"> 
																	<td>角色：</td>
																	<td style="text-align: left">
                 	<asp:DropDownList ID="drpJueSe" runat="server" Width="140">
																			<asp:ListItem>-请选择角色-</asp:ListItem>
																			<asp:ListItem Value="股东">股东</asp:ListItem>
																			<asp:ListItem Value="站长">站长</asp:ListItem>			
                                                                                	<asp:ListItem Value="会员">会员</asp:ListItem>
																		</asp:DropDownList>														<br />
																		<input id="txtJueSe" runat="server"    class="span5" type="text" />
																	</td>
																					<td>会员级别：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="drpHuiYuanJIbie" runat="server" Width="140"></asp:DropDownList>
																	</td>
																	<%--<td>股东级别：</td>
																	<td style="text-align: left">
                  	<asp:DropDownList ID="drpGuDongJibie" runat="server" Width="140">
																			<asp:ListItem>-请选择股东级别-</asp:ListItem>
																			<asp:ListItem Value="原始股东">原始股东</asp:ListItem>
																			<asp:ListItem Value="金卡股东">金卡股东</asp:ListItem>
																		</asp:DropDownList>		<br />
																		<input id="txtGuDongJibie" runat="server"    class="span10" type="text" />
																	</td>--%>

																</tr>
                                                                 
           <%--    <tr class="even gradeC">
																	<td>会员级别：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="drpHuiYuanJIbie" runat="server"></asp:DropDownList>
																	</td>

																	<td>所占股份：</td>
																	<td style="text-align: left">
																		<input id="txtSuoZhanGuFen" runat="server"  maxlength="11" class="span10" type="text" />
																	</td>

																</tr>--%>

																<tr class="odd gradeX">
																	<td>性别：</td>
																	<td style="text-align: left">
																		<asp:DropDownList ID="drpSex" runat="server" Width="140">
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
																	<td style="text-align: left;">

																		<asp:DropDownList ID="seachprov" runat="server" CssClass="span4" AutoPostBack="true" OnSelectedIndexChanged="seachprov_SelectedIndexChanged"></asp:DropDownList>
																		<asp:DropDownList ID="seachcity" runat="server" CssClass="span4" AutoPostBack="true" OnSelectedIndexChanged="seachcity_SelectedIndexChanged">
																					<asp:ListItem Value="0">请选择</asp:ListItem>
																		</asp:DropDownList>										<br />
																		<asp:DropDownList ID="seachdistrict" runat="server" CssClass="span7">
																					<asp:ListItem Value="0">请选择</asp:ListItem>
																		</asp:DropDownList><%--<br />
																		<input id="txtXiangZhen" runat="server" class="span7" type="text" />
																		<br />
																		<input id="txtXiangCun" runat="server" class="span7" type="text" />--%>
																	</td>
																	<td>地址：</td>
																	<td style="text-align: left">
																		<textarea id="txtAddress" runat="server" class="span10" style="height:80px;"></textarea>

																	</td>

																</tr>
								<%--								<tr class="odd gradeX">
																	<td>身份证号：</td>
																	<td style="text-align: left">
																		<input id="txtCardNo" runat="server" class="span7" maxlength="19" type="text" />
																	</td>
																	<td>家庭人数：</td>
																	<td style="text-align: left">
																		<input id="txtHomeNum" runat="server" class="span10" type="text" />
																	</td>

																</tr>
																<tr class="odd gradeC">
																	<td>户口簿家庭信息：</td>
																	<td style="text-align: left">
																		<textarea id="txtHomeInfo" runat="server" class="span10" style="height: 80px;"></textarea>
																		<br />
																		（包含家庭所有人姓名年龄）
																	</td>
																	<td>种植亩数：</td>
																	<td style="text-align: left">
																		<input id="txtPlantNum" runat="server" class="span10" type="text" />
																	</td>

																</tr>--%>
																<tr>
																<%--	<td>土地种植结构：</td>
																	<td style="text-align: left">
																		<textarea id="txtPlantSort" runat="server" class="span10" style="height: 50px;"></textarea>

																		<br />
																		（即各种作物具体种植面积）
																	</td--%>
																					<td>支付密码：</td>
																	<td style="text-align: left">
																						<input id="txtPayPwd"  runat="server" class="span4" type="text" /></td>


																	<td>邀请码<br />(邀请手机号)：</td>
																	<td style="text-align: left">
																		<input id="txtInvitedCode"  runat="server" class="span10" type="text" />
																	</td>
																</tr>
           <%--                                                     <tr>
																	<td>上级股东：</td>
																	<td style="text-align: left">							<input type="hidden"	 id="hfShangJiGuDong" runat="server" />
                     <input id="txtShangJiGuDong" disabled="disabled"  runat="server" class="span7" type="text" />
																	</td>

																	<td>上级站长：</td>
																	<td style="text-align: left">				<input type="hidden"	 id="hfShangJiZhanZhang" runat="server" />
																		<input id="txtShangJiZhanZhang"   disabled="disabled" runat="server" class="span10" type="text" />
																	</td>
																</tr>--%>
																<tr>
																	<td>预览头像图片:</td>
																	<td style="text-align: left">
																		<img id="iShowPhoto" src="~/assets/images/nophoto.gif"
																			runat="server" style="width: 250px; height: 197px;" /></td>
																	<td>头像图片：</td>
																	<td style="text-align: left">
																		<input type="file" runat="server" style="height: 26px; width: 180px;" onchange="PhotoType();"
																			id="fUpLoad" />
																		<input type="hidden" id="HFurl" runat="server" />		<input type="hidden" id="hfDel" runat="server" />
																		<br />
																		&nbsp;		 &nbsp;	 &nbsp;	 &nbsp;	 &nbsp;		 &nbsp;	 &nbsp;	 &nbsp;
																		<asp:Button ID="iUpLoad" CssClass="btn btn-info" Text="上传" runat="server" OnClick="iUpLoad_Click" />
																		<br />
																		<strong>允许上传活动图片格式:GIF、
																			<br />
																			JPG、JPEG、BMP
											&nbsp;单张活动<br />
																			图片上传不能超过4096KB(4M)</strong>

																	</td>

																</tr>
																		<tr>
																	<td>所属门店：</td>
																	<td style="text-align: left">
																							<asp:DropDownList ID="drpSelectShop" runat="server">
																			
																							</asp:DropDownList>
																	</td>



																	<td>备注：</td>
																	<td style="text-align: left">			
																		<textarea id="txtRemarks" runat="server" class="span10" style="height: 50px;"></textarea>

																	</td>
																</tr>
												<%--						<tr>
																	<td>支付密码：</td>
																	<td style="text-align: left">
																						<input id="txtPayPwd"  runat="server" class="span4" type="text" /></td>
																	<td>最高股东<br />手机号码：</td>
																	<td style="text-align: left">			
																		<input id="txtHighstGudong"  runat="server" class="span10" type="text" /></td>
																</tr>--%>
																<tr>
																	<td colspan="3" style="text-align: left"><span style="color: red; font-weight: bold">友情提示：	</span>
																		<br />
																		<strong>第一步：先点击浏览选择要上传的身份证图片，然后点击上传按钮进行上传。<br />
																			上传成功会出现提示，右边则显示出上传的身份证图片。<br />

																			第二部：然后进行添加信息。最后点击确定，则上传身份证图片和信息添加成功。</strong>
																	<br/>	<span style="color:red;font-weight:bold">备注：昵称/电话/邀请码 三者是一样的,都是手机号码</span>
																	</td>
																	<td>
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
