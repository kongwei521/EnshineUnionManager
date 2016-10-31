<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EnshineUnionManager.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>益生联盟数据平台 LOGIN PANEL </title>
	<script type="text/javascript" src="assets/js/jquery-1.8.2.min.js"></script>
	<script type="text/javascript" src="plugins/validate/jquery.validate.min.js"></script>
	<script type="text/javascript" src="assets/js/niceforms.js"></script>
	<link rel="stylesheet" type="text/css" media="all" href="assets/css/niceforms-default.css" />
</head>
<body>
	<form id="form1" runat="server" class="niceform">
		<div id="main_container">
			<div style="color: #71564b; margin: 0 auto; width: auto;text-align:center">
				<h1>益生联盟数据平台 管理后台登陆 </h1>
				<%--<h2 style="text-align: center; color: #71564b;">© 青岛xxxxxxx有限公司</h2>--%>
			</div>

			<div class="login_form" >
				 <div id="divLogin" >
					 <h3>——Log In——</h3>
					<%-- <a href="http://www.nisshin.com.cn" target="_blank" class="forgot_pass">了解 HXD</a>--%>
					 <fieldset>
						 <div style="border-bottom: 1px solid; width: 258px; margin-left: 127px; line-height: 20px; text-align: center;letter-spacing:14px;">
							 请输入登陆信息
						 </div>
						 <dl>
							 <dt>
								 <label for="email">用户名:</label></dt>
							 <dd>
								 <input type="text" id="txtUserName" name="txtUserName" runat="server" size="30" /></dd>
						 </dl>
						 <dl>
							 <dt>
								 <label for="password">密&nbsp;&nbsp;&nbsp; 码:</label></dt>
							 <dd>
								 <input type="password" id="txtPassword" name="txtPassword" runat="server" size="30" /></dd>
						 </dl>
						 <dl>
							 <dt>
								 <label for="password">验证码:</label></dt>
							 <dd>
								 <div style="float:left">
									  <input type="text" id="txtValidate" maxlength="5" name="txtValidate" runat="server" size="6" />
								 </div>
								 <div style="float: left; padding-top: 3px;">
									 <img id="yan" title="点击刷新验证码" style="cursor: pointer; height: 28px; width: 64px"
										 onclick="javascript:DianJi();" src="ValidateCode.aspx" />
								 </div>
							
							 </dd>
						 </dl>
						 <dl class="submit">
							 <asp:Button ID="submit" Text="登 陆" runat="server" />
						 </dl>
					 </fieldset>
				 </div>
				 <div id="divIeAlert" style="display:none">
					   <h3>——友情提示——</h3> 
					<fieldset>
						 <div style="border-bottom: 1px solid;height:30px;background-color: #D9EDF7;border-color: #BCE8F1;color: #3A87AD;line-height:30px;font-size:14px; ">
							你知道你的 Internet Explorer 版本是过时的了吗？
						 </div>
						 <span style="line-height:30px;padding-left:30px;font-size:12px; " >
											 为了更好的体验我们网站查询效果，建议您将浏览器升级到最新版本的Internet Explorer 9 以上或者选择另外一个web浏览器，最流行的web浏览器在下面的列表可以找到。
						 </span>
						 <div class="browser"> 
							 <ul ><li><a href="http://www.microsoft.com/zh-cn/download/internet-explorer.aspx" target="_blank"> <img src="assets/images/ie.png" /><br />Internet<br /> Explorer 9</a></li>
								  <li><a href="http://www.firefox.com.cn/" target="_blank"> <img src="assets/images/firefox.png" /><br />Firefox浏览器</a></li>
								  <li><a href="http://www.google.cn/chrome/" target="_blank"> <img src="assets/images/Chrome.png" /><br />Chrome浏览器</a></li>
								  <li><a href="http://www.opera.com/zh-cn" target="_blank">  <img src="assets/images/opera.png" /><br />Opera浏览器</a></li>
								  <li><a href="http://www.apple.com/cn/safari/" target="_blank">  <img src="assets/images/Safari.png" /><br />Safari浏览器</a></li>
								  <li> <a href="http://ie.sogou.com/" target="_blank"> <img src="assets/images/sogou.png" /><br />搜狗浏览器</a></li>
								  <li><a href="http://www.maxthon.cn/" target="_blank"> <img src="assets/images/Maxthon.png" /><br />遨游浏览器</a></li>
								  <li> <a href="http://browser.qq.com/" target="_blank"> <img src="assets/images/qq.png" /><br />QQ浏览器</a></li>
								   <li> <a href="http://www.theworld.cn/v5/" target="_blank"> <img src="assets/images/world.png" /><br />世界之窗<br />浏览器</a></li>
							</ul>
							
						 </div>
					 </fieldset>
				 </div>
			</div>
			<div class="footer_login">

				<div class="left_footer_login">Copyright 2016©正益移动科技有限公司. All Rights Reserved.</div>

			</div>
		</div>
	</form>
	<link href="plugins/msgbox/jquery.msgbox.css" rel="stylesheet" />
	<script src="plugins/msgbox/jquery.msgbox.min.js"></script>
	<script src="assets/js/SeachCommon.js"></script>
	 		 <script type="text/javascript">
	 		 	 $(function () {
	 		 	 	 var userAgent = window.navigator.userAgent.toLowerCase();
	 		 	 	 $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
	 		 	 	 $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
	 		 	 	 $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
	 		 	 	 if ($.browser.msie6 == true || $.browser.msie7 == true || $.browser.msie8 == true) {
	 		 	 	 	 $("#divLogin").hide();
	 		 	 	 	 $("#divIeAlert").show();
	 		 	 	 }
	 		 	 	 else {
	 		 	 	 	 $("#divLogin").show();
	 		 	 	 	 $("#divIeAlert").hide();
	 		 	 	 }
	 		 	 });
			  </script>
</body>
</html>
