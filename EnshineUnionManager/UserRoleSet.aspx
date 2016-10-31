<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRoleSet.aspx.cs" Inherits="EnshineUnionManager.UserRoleSet" %>
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
											<i class="icon-zoom-in"></i>角色信息添加 <span>This is the place where Search started</span>
										</h1>
										<br />
										<div id="live" class="tab-pane active">
											<div class='row-fluid'>
												<div class='span12 widget'>
													<div class='widget-header'>
														<span class='title'>
															<i class='icol-blog'></i>角色
														</span>
														<div style='float: right; padding-top: 4px; padding-right: 4px;'>
															<input type='button' class='btn  btn-primary' value='角色列表管理'
																onclick="window.location.href = 'UserRoleManager.aspx?mid=<%=Request["mid"]%>'" />
														</div>
													</div>
													<div class='widget-content summary-list'>
														<table class='table table-bordered table-striped'>

															<tbody>
																<tr>
																	<td style="width: 120px;">角色名：</td>
																	<td style="text-align: left">
																		<input id="txtRoleName" runat="server" class="span8" type="text" /></td>

																</tr>

																<tr>
																	<td>角色描述：</td>
																	<td style="text-align: left">
																		<textarea id="txtFckContent" runat="server" style="width: 98%; height: 100px;"></textarea></td>

																</tr>
																<tr>
																	<td>权限选择：</td>
																	<td style="text-align: left">
																		<ul id="treeDemo" runat="server" class="ztree"></ul>
																		<input type="hidden" id="hfRole" runat="server" />
																	</td>

																</tr>
																<tr>
																	<td colspan="2">
																		<asp:Button ID="btnAddRole" Width="80" CssClass="btn btn-info" Text="添加" OnClick="btnAddRole_Click" runat="server" />
																		&nbsp;&nbsp;&nbsp;	
																																																																					  <asp:Button ID="btnUpdateRole" Width="80" CssClass="btn btn-info" Text="修改" OnClick="btnUpdateRole_Click" runat="server" />
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
		<link href="assets/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
		<script src="assets/js/jquery.ztree.core-3.5.min.js"></script>
		<script src="assets/js/jquery.ztree.excheck-3.5.min.js"></script>
		<script type="text/javascript">

			var setting = {
				view: {
					selectedMulti: false
				},
				check: {
					enable: true
				},
				data: {
					key: {
						//将treeNode的ItemName属性当做节点名称
						name: "menuname"
					},
					simpleData: {
						//是否使用简单数据模式
						enable: true,
						//当前节点id属性  
						idKey: "menuid",
						//当前节点的父节点id属性 
						pIdKey: "fathermenuid",
						//用于修正根节点父节点数据，即pIdKey指定的属性值
						rootPId: 0
					}
				},
				callback: {
					beforeCheck: beforeCheck,
					onCheck: onCheck
				}
			};


			function beforeCheck(treeId, treeNode) {

				return (treeNode.doCheck !== false);
			}
			var arrayObj = new Array();
			function onCheck(e, treeId, treeNode) {
				if (treeNode.check_Child_State > 0) {
					arrayObj.push(treeNode.menuid);
					for (var i = 0; i < treeNode.children.length; i++) {
					  //	alert(treeNode.children[i].menuid + treeNode.children[i].menuname);

						arrayObj.push(treeNode.children[i].menuid);
					}
				}
				else {
                var fatherid;
					//选中则添加
                if (treeNode.checked == true) {
                	//不存在数组中
                	if (arrayObj.indexOf(treeNode.parentTId.split('_')[1]) == -1) {
                		fatherid = treeNode.parentTId.split('_')[1];
                		arrayObj.push(fatherid);
                	}
                	//var roleid = treeNode.parentTId.split('_')[1] + "," + treeNode.tId.split('_')[1];
                	//alert(roleid);
                	arrayObj.push(treeNode.menuid);
                	//alert(treeNode.menuid + treeNode.menuname);
                }
                	//去掉勾选则从数组中删除
                else {
                	//判断一个父节点取消则相应的子节点全部取消
                	if (treeNode.check_Child_State == 0) {
                		//先清除掉父节点然后循环去掉字节点的选中ID
                		RemoveRoleItem(treeNode.menuid);
                		//arrayObj = $.grep(arrayObj, function (value) {
                		//	return value != treeNode.menuid;
                		//});
                		//	arrayObj.splice(jQuery.inArray(treeNode.menuid, arrayObj), 1);20160323
                		for (var i = 0; i < treeNode.children.length; i++) {
                			//arrayObj.splice(jQuery.inArray(treeNode[i], arrayObj), 1);
                			RemoveRoleItem(treeNode.children[i].menuid);
                			//arrayObj = $.grep(arrayObj, function (value) {
                			//	return value != treeNode.children[i].menuid;
                			//});
                		}
                	}//清除选中的子节点及父节点
                	else {
                		//1:		换成arrayObj无效
                		//var arr = [1, 10, 11, 12, 13, 14, 2, 3, 4, 5, 6, 7];
                		//arr.splice($.inArray(4, arr), 1);
                		//alert(arr);
                		//第二种方法移除
                		//arrayObj = $.grep(arrayObj, function (value) {
                		//	return value != treeNode.menuid;
                		//});
                		//第三种方法移除
                		RemoveRoleItem(treeNode.menuid);
																		//父节点下面只有一个子节点处理(子节点取消,父节点保留)	后续再修改
                		var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                		var node = treeObj.getNodeByParam("menuid", treeNode.fathermenuid);
                		node.checked = true;
                		treeObj.updateNode(node);
                //		RemoveRoleItem(treeNode.fathermenuid);
                		//最初移除方法 无效
                		// arrayObj.splice(jQuery.inArray(treeNode.menuid, arrayObj), 1);
                	}
                }
				}
				//定义了sort的比较函数
				arrayObj = arrayObj.sort(function (a, b) {
					return a - b;
				});
			  //	alert(arrayObj.toString());
				$("#hfRole").val('');
				$("#hfRole").val(arrayObj.toString());
			}
			//获取URL请求参数
			function getQueryString(name) {
				var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
				var r = window.location.search.substr(1).match(reg);
				if (r != null) return unescape(r[2]); return null;
			}
			function RemoveRoleItem(name) {
				for (var i = 0; i < arrayObj.length; i++) {
					if (arrayObj[i] == name) {
						arrayObj.splice(i, 1);						//从下标为i的元素开始，连续删除1个元素
						i--;//因为删除下标为i的元素后，该位置又被新的元素所占据，所以要重新检测该位置
					}
				}
			}
			$(function () {
				$.post("UserRoleSet.aspx", function (json) {
					var treeObj = $.fn.zTree.init($("#treeDemo"), setting, json);
					//默认展开所有节点
					treeObj.expandAll(false);

					//如果地址有参数时则为修改选中状态
					if (getQueryString("upid") != null) {
						var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
						arrayObj = $("#hfRole").val().split(',');
						//var mid = $("#hfRole").val().split(',');
						for (var i = 0; i < arrayObj.length; i++) {
						    var node = treeObj.getNodeByParam("menuid", arrayObj[i]);
							node.checked = true;
							//node.open = true;
							//treeObj.selectNode(node);
							treeObj.updateNode(node);

						}
					}

				});
			});

		</script>
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
