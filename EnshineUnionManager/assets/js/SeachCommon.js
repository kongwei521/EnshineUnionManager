//刷新验证码
function DianJi() {
	$("#yan").attr("src", "ValidateCode.aspx" + "?" + Math.random());
}
//获取当前时间
var myDate = new Date(), str = '';
var year = myDate.getFullYear();
var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
var day = ("0" + myDate.getDate()).slice(-2);
var h = ("0" + myDate.getHours()).slice(-2);
var m = ("0" + myDate.getMinutes()).slice(-2);
str = year + "-" + month + "-" + day + " " + h + ":" + m;

$(document).ready(function () {
 
	//用户登陆check
	if ($.fn.validate) {
		//验证用户名
		jQuery.validator.addMethod("userNameCheck", function (value, element) {
			return this.optional(element) || /^[0-9a-zA-Z]\w{2,20}$/.test(value);
		}, "请输入3-20位字母开头的字母或数字和下划线");
		var icon = "<div class='iconerror'><img src='assets/images/unchecked.jpg'/></div>";
		$("#form1").validate({
			errorElement: "font",
			errorClass: "infoerror",
			errorPlacement: function (error, element) {
				error.appendTo(element.closest("dd"));
			},
			rules: {
				txtUserName:
				{
					required: true,
					userNameCheck: true
				},
				txtPassword:
				{
					required: true,
					minlength: 6
				},
				txtValidate:
				{
					required: true,
					minlength: 5
				}
			},
			messages:
			{
				"txtUserName": {
					required: icon + "<div class='iconerror'>请输入用户名</div>",
					userNameCheck: icon + "<div class='iconerror'>请输入正确的用户名</div>"
				},
				"txtPassword": {
					required: icon + "<div class='iconerror'>请输入密码</div>",
					minlength: icon + "<div class='iconerror'>密码长度至少为6位</div>"
				},
				"txtValidate": {
					required: icon + "<div class='iconerror'>请输入验证码</div>",
					minlength: icon + "<div class='iconerror'>验证码长度为5位</div>"
				}
			},
			submitHandler: function (form) {

				if ($("#txtValidate").val().toUpperCase() != getCookie("ImageV")) {
					$.msgbox("<span style='font-size:14px;line-height:30px;'>验证码输入错误,请检查后在输入。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
				}
				else {
					var editvalue = $("#txtUserName").val().toUpperCase() + "_" + $("#txtPassword").val();
					$.ajax({
						type: "post",
						url: "EnshineUnionHandler.ashx",
						data: "Login=" + encodeURIComponent(editvalue),
						datatype: "json",
						async: false,
						success: function (returnData, textstatus, xmlhttprequest) {
							switch (returnData) {
								//成功
								case "Success":
									window.location.href = "Index.aspx";
									break;
									//失败
								case "LoginFailure":
									$.msgbox("<span style='font-size:13px;line-height:30px;'>对不起您的账号或密码-不正确,请检查。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
									break;
								default: break;
							}
						},
						error: function (errorinfo) {
							alert("登陆请求数据错误。");
						}
					});
				}
			}
		});
	}

	if ($('#txtTimeSelect').length > 0) {
		//时间选择
		$('#txtTimeSelect').daterangepicker();
	}

	if ($('#txtExpireDate').length > 0) {
		$('#txtExpireDate').datetimepicker({ timepicker: false, format: 'Y/m/d' });
	}

	//处理点击菜单追加css效果
	var mid = decodeURIComponent(GetQueryString("mid"));
	if (mid != null) {
		$("#topmenu" + mid.split(',')[0]).attr("class", "active");
		$("#sonmenu" + mid.split(',')[1]).attr("class", "active");
	}

	switch (window.location.pathname.split('/')[1]) {
		case "Index.aspx":
			$("#topmenu1").attr("class", "active");
			$("#sonmenu2").attr("class", "active");
			break;
		case "AdminManager.aspx":
			ShowAdminInfo();
			break;
		case "UserinfoManager.aspx":
			ShowUserInfo("");
			SearchUserListInfo();
			break;
		case "PerfectUserInfo.aspx":			
			$("#txtTel").change( function (e) {
				var regex = /^0?(13\d|14[5,7]|15[0-3,5-9]|17[0,6-8]|18\d)\d{8}$/ig;
				if (!regex.test($("#txtTel").val())) {
					$.msgbox("<span style='font-size:13px;line-height:30px;'>手机号格式不正确。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
					$("#txtTel").val('');
				}
			});

			$("#txtInvitedCode").change(function (e) {
				var regex = /^0?(13\d|14[5,7]|15[0-3,5-9]|17[0,6-8]|18\d)\d{8}$/ig;
				if (!regex.test($("#txtInvitedCode").val())) {
					$.msgbox("<span style='font-size:13px;line-height:30px;'>上级邀请手机号格式不正确。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
					$("#txtInvitedCode").val('');
				}
				else {
					//$.ajax({
					//	type: "post",
					//	url: "EnshineUnionHandler.ashx",
					//	data: "GetShangJiUserInfo=" + encodeURIComponent($("#txtInvitedCode").val()),
					//	datatype: "json",
					//	async: false,
					//	success: function (returnData, textstatus, xmlhttprequest) {
					//		$("#txtShangJiGuDong,#hfShangJiGuDong").val(returnData.split(',')[0]);
					//		$("#txtShangJiZhanZhang,#hfShangJiZhanZhang").val(returnData.split(',')[1]);
					//	},
					//	error: function (errorinfo) {
					//		alert("获取上级股东/上级站长数据请求错误。");
					//	}
					//});
				}
			});
			$("#drpJueSe").change(function () {
			    var juese = $("#drpJueSe").val();
			    if (juese == '-请选择角色-')
			        $.msgbox("<span style='font-size:13px;line-height:30px;'>请选择角色。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			    else $("#txtJueSe").val(juese);
			}
                );
			//$("#drpGuDongJibie").change(function () {
			//    var gudongjibie = $("#drpGuDongJibie").val();
			//    if (gudongjibie == '-请选择股东级别-')
			//        $.msgbox("<span style='font-size:13px;line-height:30px;'>请选择股东级别。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			//    else $("#txtGuDongJibie").val(gudongjibie);
			//}
   //     );
			break;
		case "AdManager.aspx":
			ShowAdInfo();
			AddAdInfo();
			break;
		case "NewsNoticesManager.aspx":
			ShowNoticesInfo();
			break;
		case "NewsSortManager.aspx":
			ShowNewsSortInfo();
			AddNewsSortInfo();
			break;
		case "NewsCommentManager.aspx":
			ShowNewsCommentInfo();
			break;
		//case "HuoDongManager.aspx":
		//	ShowHuoDongInfo("");
		//	SearchHuoDongListInfo();
		//	AddHuoDongInfo();
		//	break;
		//case "NewHuoDongSortManager.aspx":
		//	ShowHuoDongSortInfo();
		//	AddHuoDongSortInfo();
		//	break;
		//case "JoinHuoDongManager.aspx":
		//	ShowJoinHuoDong();
		//	break;
		//case "JoinHuoDongInfoManager.aspx":
		//	ShowJoinHuoDongInfo();
		//	break;
		case "JoinGoodsHuoDongManager.aspx":
			SearchJoinGoodsHuoDongListInfo();
			ShowJoinGoodsHuoDongInfo("");
				break;
		case "GoodsAdd.aspx":

			$("#vvip2").hide(); $("#ticheng").val('');
			var getEditID = GetQueryString("upid");
			if (getEditID != null || getEditID != undefined) {
				$.ajax({
					type: "get",
					url: "EnshineUnionHandler.ashx",
					data: "GetTiChengPoint=" + getEditID,
					datatype: "json",
					async: false,
					success: function (returnData, textstatus, xmlhttprequest) {
						var typecheck = returnData.split('/');
						if (typecheck[0] == 0) {
							$("#vvip1").show(); $("#vvip2").hide();
							$("#rpSelect1").attr("checked", "checked");
							$("#rpSelect2").removeAttr("checked");
							var price = $("#txtGoodsPrice").val();
							var costprice = $("#txtGoodsCost").val();;
							$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
							$("#ticheng").val("0/20/20/10");
						}
						else {
							$("#rpSelect2").attr("checked", "checked");
							$("#rpSelect1").removeAttr("checked");
							$("#vvip1").hide(); $("#vvip2").show();
						}
						if (typecheck[4] == 'Y')
							$("#txtGetGoodPoint").removeAttr("disabled");
						else $("#txtGetGoodPoint").attr("disabled", "disabled");
						if (typecheck[5] == 'Y') $("#txtExchangePoint").removeAttr("disabled");
						else $("#txtExchangePoint").attr("disabled", "disabled");
						if (typecheck[6] == 'Y') $("#txtXianGouNumber").removeAttr("disabled");
						else $("#txtXianGouNumber").attr("disabled", "disabled");
					},
					error: function (errorinfo) {
						alert("商品分成比例数据请求错误。");
					}
				});
			}

			$("#txtGoodsPrice,#txtGoodsCost").change(function () {

				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
			}
			);
			$("#Text1,#Text2,#Text3").change(function () {

				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
				$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
				//alert($("#ticheng").val());
			}
	);
			$("input:radio[name='rpSelect']").change(function () {
				var var_name = $("input[name='rpSelect']:checked").val();
				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				if (var_name == 1) {
					$("#vvip1").hide(); $("#vvip2").show();
					$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
					$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
					$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
					$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
					//alert($("#ticheng").val());
				} else {
					$("#vvip1").show(); $("#vvip2").hide();
					$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
					$("#ticheng").val("0/20/20/10");
				}
			});


			$("#drpSales").change(function () {
				var xiangou = $("#drpSales").val();
				if (xiangou == 'Y')
					$("#txtGetGoodPoint").removeAttr("disabled");
				else $("#txtGetGoodPoint").attr("disabled", "disabled");
			}
);
			$("#drpExchangeGood").change(function () {
				var xiangou = $("#drpExchangeGood").val();
				if (xiangou == 'Y')
					$("#txtExchangePoint").removeAttr("disabled");
				else $("#txtExchangePoint").attr("disabled", "disabled");
			}
);

			$("#drpXianGou").change(function () {
				var xiangou = $("#drpXianGou").val();
				if (xiangou == 'Y')
					$("#txtXianGouNumber").removeAttr("disabled");
				else $("#txtXianGouNumber").attr("disabled", "disabled");
			}
			);
			break;
		case "GoodsManager.aspx":
			//ShowGoodsInfo(GetQueryString("type").toString().split('?')[0]+',,,,');
		//	alert(GetQueryString("mid").toString().split('?')[0]);
		//	alert(GetQueryString("type").toString().split('?')[0]);
			ShowGoodsInfo(GetQueryString("mid").toString().split('?')[0]+';'+GetQueryString("type").toString().split('?')[0] + ';;;;');
			SearchGoodsInfo();
			break;

		case "GoodsSortManager.aspx":
			ShowGoodsSortInfo();
			AddGoodsSortInfo();
			break;
		case "GroupBuyAdd.aspx":

			$("#vvip2").hide(); $("#ticheng").val('');
			var getEditID = GetQueryString("upid");
			if (getEditID != null || getEditID != undefined) {
				$.ajax({
					type: "get",
					url: "EnshineUnionHandler.ashx",
					data: "GetGroupTiChengPoint=" + getEditID,
					datatype: "json",
					async: false,
					success: function (returnData, textstatus, xmlhttprequest) {
						var typecheck = returnData.split('/');
						if (typecheck[0] == 0) {
							$("#vvip1").show(); $("#vvip2").hide();
							$("#rpSelect1").attr("checked", "checked");
							$("#rpSelect2").removeAttr("checked");
							var price = $("#txtGoodsPrice").val();
							var costprice = $("#txtGoodsCost").val();;
							$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
							$("#ticheng").val("0/20/20/10");
						}
						else {
							$("#rpSelect2").attr("checked", "checked");
							$("#rpSelect1").removeAttr("checked");
							$("#vvip1").hide(); $("#vvip2").show();
						}

					},
					error: function (errorinfo) {
						alert("商品分成比例数据请求错误。");
					}
				});
			}

			$("#txtGoodsPrice,#txtGoodsCost").change(function () {

				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
			}
			);
			$("#Text1,#Text2,#Text3").change(function () {

				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
				$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
				//alert($("#ticheng").val());
			}
	);
			$("input:radio[name='rpSelect']").change(function () {
				var var_name = $("input[name='rpSelect']:checked").val();
				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				if (var_name == 1) {
					$("#vvip1").hide(); $("#vvip2").show();
					$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
					$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
					$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
					$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
					//alert($("#ticheng").val());
				} else {
					$("#vvip1").show(); $("#vvip2").hide();
					$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
					$("#ticheng").val("0/20/20/10");
				}
			});


			break;
		case "GoodsPackageAdd.aspx":
			$("#vvip2").hide(); $("#ticheng").val('');
			var getEditID = GetQueryString("upid");
			if (getEditID != null || getEditID != undefined) {
				$.ajax({
					type: "get",
					url: "EnshineUnionHandler.ashx",
					data: "GetGoodsPackTiChengPoint=" + getEditID,
					datatype: "json",
					async: false,
					success: function (returnData, textstatus, xmlhttprequest) {
						var typecheck = returnData.split('/');
						if (typecheck[0] == 0) {
							$("#vvip1").show(); $("#vvip2").hide();
							$("#rpSelect1").attr("checked", "checked");
							$("#rpSelect2").removeAttr("checked");
							var price = $("#txtGoodsPrice").val();
							var costprice = $("#txtGoodsCost").val();;
							$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
							$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
							$("#ticheng").val("0/20/20/10");
						}
						else {
							$("#rpSelect2").attr("checked", "checked");
							$("#rpSelect1").removeAttr("checked");
							$("#vvip1").hide(); $("#vvip2").show();
						}

					},
					error: function (errorinfo) {
						alert("商品分成比例数据请求错误。");
					}
				});
			}
 		$("#txtGoodsPrice,#txtGoodsCost").change(function () {
				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
				$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
			}
			);
			$("#Text1,#Text2,#Text3").change(function () {

				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
				$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
				$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
				$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
				//alert($("#ticheng").val());
			}
	);
			$("input:radio[name='rpSelect']").change(function () {
				var var_name = $("input[name='rpSelect']:checked").val();
				var price = $("#txtGoodsPrice").val();
				var costprice = $("#txtGoodsCost").val();;
				if (var_name == 1) {
					$("#vvip1").hide(); $("#vvip2").show();
					$("#spz1").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text1").val() / 100)).toFixed(2) + "元");
					$("#spz2").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text2").val() / 100)).toFixed(2) + "元");
					$("#spz3").html("%=" + ((price - costprice) * (75 / 100) * ($("#Text3").val() / 100)).toFixed(2) + "元");
					$("#ticheng").val("1/" + $("#Text1").val() + "/" + $("#Text2").val() + "/" + $("#Text3").val());
					//alert($("#ticheng").val());
				} else {
					$("#vvip1").show(); $("#vvip2").hide();
					$("#sp1").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp2").html("分成比例（销售价-成本价）*75%*20%=" + ((price - costprice) * (75 / 100) * (20 / 100)).toFixed(2) + "元");
					$("#sp3").html("分成比例（销售价-成本价）*75%*10%=" + ((price - costprice) * (75 / 100) * (10 / 100)).toFixed(2) + "元");
					$("#ticheng").val("0/20/20/10");
				}
			});


			$("#drpSales").change(function () {
				var xiangou = $("#drpSales").val();
				if (xiangou == 'Y')
					$("#txtGetGoodPoint").removeAttr("disabled");
				else $("#txtGetGoodPoint").attr("disabled", "disabled");
			}
);
			$("#drpExchangeGood").change(function () {
				var xiangou = $("#drpExchangeGood").val();
				if (xiangou == 'Y')
					$("#txtExchangePoint").removeAttr("disabled");
				else $("#txtExchangePoint").attr("disabled", "disabled");
			}
);

			$("#drpXianGou").change(function () {
				var xiangou = $("#drpXianGou").val();
				if (xiangou == 'Y')
					$("#txtXianGouNumber").removeAttr("disabled");
				else $("#txtXianGouNumber").attr("disabled", "disabled");
			}
			);
			break;
		case "GoodsPackageManager.aspx":
			ShowGoodsPackageInfo("");
			SearchGoodsPackageInfo();
			break;
		case "GroupBuyManager.aspx":
        	ShowGroupGoodsInfo();
          //  alert(GetQueryString("type").toString().split('?')[0] );
			//	ShowGroupBuyInfo(GetQueryString("type").toString().split('?')[0]+',,,,');
        	ShowGroupBuyInfo(GetQueryString("mid").toString().split('?')[0] + ';' + GetQueryString("type").toString().split('?')[0] + ';;;;');

			break;
		case "OrderListManager.aspx":
			ShowOrderListInfo("");
			SearchOrderListInfo();
			break;
		//case "PlantDoctorSortManger.aspx":
		//	ShowPlantDoctorSortInfo();
		//	AddPlantDoctorSortInfo();
		//	break;
		//case "PlantDoctorManager.aspx":
		//	ShowPlantDoctorInfo();
		//	break;
		case "UserRechargeAdd.aspx":
			$("#txtRecTel").change('input', function (e) {
				var regex = /^0?(13\d|14[5,7]|15[0-3,5-9]|17[0,6-8]|18\d)\d{8}$/ig;
				if (!regex.test($("#txtRecTel").val())) {
					$.msgbox("<span style='font-size:13px;line-height:30px;'>充值手机号格式不正确。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
					$("#txtRecTel").val('');
				}
				else {
					$.ajax({
						type: "post",
						url: "EnshineUnionHandler.ashx",
						data: "GetUserInfo=" + encodeURIComponent($("#txtRecTel").val()),
						datatype: "json",
						async: false,
						success: function (returnData, textstatus, xmlhttprequest) {
							if (returnData == "NoData") {
								$("#txtRecUserName").val(returnData);
								$("#hfuserid").val(returnData);
							}
							else {
								$("#txtRecUserName").val(returnData.split(',')[0]);
								$("#hfuserid").val(returnData.split(',')[1]);
							}
						},
						error: function (errorinfo) {
							alert("用户信息数据请求错误。");
						}
					});
				}
			});
			$("#txtRecMoney").change('input', function (e) {
				var regex = /^(?:(?:0(?=(?:0?\.)|$))|[1-9])\d*(?:\.(?:(?:0(?=[1-9]))|[1-9])\d?)?$/ig;
				if (!regex.test($("#txtRecMoney").val())) {
					$.msgbox("<span style='font-size:13px;line-height:30px;'>充值金额格式不正确。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
					$("#txtRecMoney").val('');
				}
			});
			break;
		case "UserRecharge.aspx":
			ShowUserRechargeInfo("");
			SearchUserRechargeListInfo(); break;
		case "UserRoleManager.aspx":
			ShowUserRoleInfo();
			break;
		////case "MemberManager.aspx":
		////	ShowMemberInfo();
		////	AddMemberInfo();
		////	break;
		////case "MyPayMoney.aspx":
		////	ShowMyPayMoneyInfo();
		////	break;
		////case "MyPoint.aspx":
		////	ShowMyPointInfo();
		////	break;
		case "FinanceMonthReport.aspx":
			ShowFinanceMonthListInfo("");
			SearchFinanceMonthListInfo();
			break;

		case "FenXiaoExtractRport.aspx":
			ShowFenXiaoExtractInfo("");
			SearchFenXiaoExtracListInfo();
			break;
		case "MemberExtractReport.aspx":
			ShowMemberExtractInfo("");
			SearchMemberExtractListInfo();
			break;
		case "UserExtractMoenyManager.aspx":
			ShowUserExtractListInfo();
			break;
		case "ShopSetManager.aspx":
			ShowShopInfo();
			AddShopInfo();
			break;
		case "CoCheckExport.aspx":
			ShowSaleCheckInfo("");
			SearchSaleCheckInfo();
			break;
		case "GoodsShipManager.aspx":
			ShowGoodsShipInfo("");
			SearchGoodsShipInfo();
			break;

		case "UserAreaTotal.aspx":
			ShowUserAreaListInfo();
			break;
		case "UserAgeSexTotal.aspx":
			ShowUserAgeSexListInfo();
			break;

		case "GoodSalesTotal.aspx":
			ShowGoodSalesTotalListInfo();
			break;
		case "GoodSortSalesTotal.aspx":
			ShowGoodSortSalesTotalListInfo();
			break;
		case "GoodAreaSalesTotal.aspx":
			ShowGoodAreaSalesTotalListInfo();
			break;
		case "GoodSalesSearchTotal.aspx":
			ShowGoodSalesSearchTotalListInfo();
			break;
			//g)	用户订单份分类消费分析
		case "UserOrderTypeFenXi.aspx":
			ShowUserOrderTypeFenXiListInfo();
			break;
		case "UserOrderSalesFenXi.aspx":
			ShowUserOrderSalesFenXiListInfo();
			break;

		case "MessageViewManager.aspx":
			ShowMessageViewListInfo();
			break;

	    case "MemberJiBieFenXi.aspx":
	        ShowMemberJiBieFenXiListInfo();
	        break;
            //库存管理 商品库龄分析
	    case "GoodsStockAgeFenXi.aspx":
	        ShowGoodsStockAgeFenXiListInfo();
	        break;
            ///发货商品管理
              case "GoodsDeliveryManager.aspx":
	        ShowGoodsDeliveryListInfo();
	        break;
            //剩余商品库存
              case "GoodsSyStockManager.aspx":
	        ShowGoodsSyStockListInfo();
	        break;
            case "GoodsInWareHouse.aspx":
		 
			ShowGoodsInWareHouseInfo("");
			SearchGoodsInWareHouseInfo();
			break;
	}

	$("#divScrool2").css("overflow-x", "scroll").height(16).width($("#divScrool1").width() + 17);
	//设置一个高度16的divScrool2层 长度与divScrool1层一致
	$("#divScrool2").html($("#divScrool1").html()); //并给divScrool2层填充内容

	$("#divScrool2").scroll(function () {
		$("#divScrool1")[0].scrollLeft = $("#divScrool2")[0].scrollLeft;
		//拉动divScrool2层滚动条，divScrool1层滚动条同步被改变
	});

	//当滚动条的位置处于距顶部100像素以下时，跳转链接出现，否则消失
	$(window).scroll(function () {
		if ($(window).scrollTop() > 100)
			$("#back-to-top").fadeIn(1500);
		else
			$("#back-to-top").fadeOut(1500);
	});

	//当点击跳转链接后，回到页面顶部位置
	$("#back-to-top").click(function () {
		$('body,html').animate({ scrollTop: 0 }, 1000);
		return false;
	});
});

function SetSonClass(menuid) {
	alert(menuid.id);
	$("#" + menuid.id).attr("class", "active");
}
var editor;
if (window.location.pathname.split('/')[1] == "NewsNoticesAdd.aspx" || window.location.pathname.split('/')[1] == "GoodsAdd.aspx"
|| window.location.pathname.split('/')[1] == "PlantDoctorAdd.aspx" || window.location.pathname.split('/')[1] == "NewAdAdd.aspx"
	|| window.location.pathname.split('/')[1] == "NewHuoDongAdd.aspx" || window.location.pathname.split('/')[1] == "GroupBuyAdd.aspx"
    ||window.location.pathname.split('/')[1] == "GoodsPackageAdd.aspx" ) {
	KindEditor.ready(function (K) {
		editor = K.create('#txtFckContent', {
			cssPath: '../kindeditor/plugins/code/prettify.css',
			uploadJson: '../kindeditor/asp.net/upload_json.ashx',
			fileManagerJson: '../kindeditor/asp.net/file_manager_json.ashx',
			resizeType: 1, pasteType: 2, //allowImageRemote: false,
			allowPreviewEmoticons: false,
			//items: [
			//	'source', 'preview', 'undo', 'redo', 'fontsize', 'justifyleft', '|', 'justifycenter', 'justifyright', 'clearhtml', 'image', 'multiimage'],
			afterChange: function () {
				K('.word_count2').html(this.count('text'));
			}
		});
	});
}
//判断是否选择了图片格式类型				 
function PhotoType() {
	var x = $("#fUpLoad");;
	if (!x || !x.val()) return;
	var patn = /\.jpg$|\.jpeg$|\.gif$|\.bmp$/i;
	if (patn.test(x.val())) {//如果已经选择了并且类型是图片则浏览不可用
		$("<%=iUpLoad.ClientID%>").disabled = false;
	}
	else {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>允许上传图片格式：GIF|JPG|JPEG|BMP|。。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("<%=btnAddNotices.ClientID%>").disabled = 'disabled'; return false;
	}
	if (x.val() == "" & x.val() == null) {
		alert('请选择要上传的图片。');
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择要上传的图片。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("<%=btnAddNotices.ClientID%>").disabled = 'disabled'; return false;
	}
}
/**************管理员信息列表信息显示**********************/
function AdminInfoValidate() {
	if ($("#txtlogname").val() == "" || $("#txtlogname").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>用户名不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtlogname").focus();
		return false;
	}
	if ($("#txtPass").val() == "" || $("#txtPass").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>密码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtPass").focus();
		return false;
	}
		//else if ($("#ddldepart").val() == "-所属部门-") {
		//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择部门?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		//	return false;
		//}
	else if ($("#ddlrole").val() == "-所属角色-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择角色?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}

}
function ClearAdminInfo() {
	$("#txtlogname,#txtPass").val(""); editor.html("");
	$("#ddldepart").val('-所属部门-');
	$("#ddlrole").val('-所属角色-');
}
function ShowAdminInfoHtml(returnData) {
	$("#divShowAdminInfoManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowAdminInfoManager").html("<tr class='error'><td colspan='10'>暂无管理员信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowAdminInfoManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowAdminInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowAdminInfo=" + "all" + "&currPage=1",
		datatype: "html",
	//	async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowAdminInfoHtml(returnData);
		},
		error: function (errorinfo) {
			alert("管理员信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowAdminInfo");
	})
}
function DeleteAdminInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此管理员信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteAdminInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '管理员信息成功。'); ShowAdminInfoHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '管理员信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowAdminInfo");

	})
}

/*******************用户信息删查询*****************************/
function SearchUserListInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = $("#txtNickName").val() + "," + $("#txtName").val() + "," + $("#txtInvitedcode").val();
		ShowUserInfo(searchwhere);
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtNickName").val(""); $("#txtName").val(""); $("#txtInvitedcode").val("");
	});
}

//用户信息一览
function ShowUserInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,,";
	} else { searchwhere = searchinfo; }

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowUserInfoHtml(returnData);
		},
		error: function (errorinfo) {
			alert("用户信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowUserInfo");
	})


}
//显示用户信息
function ShowUserInfoHtml(returnData) {
	$("#divShowUserInfoManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowUserInfoManager").html("<tr class='error'><td colspan='11'>暂无用户信息。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowUserInfoManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function UserInfoValidate() {

	if ($("#txtNickName").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>昵称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtNickName").focus();
		return false;
	}
	else if ($("#txtName").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>姓名不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtName").focus();
		return false;
	}
	else if ($("#txtPass").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>密码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtPass").focus();
		return false;
	}
	else if ($("#txtTel").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>电话不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTel").focus();
		return false;
	}
	else if ($("#drpJueSe").val() == "-请选择角色-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了角色?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	//else if ($("#drpGuDongJibie").val() == "-请选择股东级别-") {
	//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择股东级别?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	//	return false;
	//}

	else if ($("#drpHuiYuanJIbie").val() == "-请选择会员级别-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择会员级别。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}
	else if ($("#drpSex").val() == "-请选择性别-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了性别?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#seachprov").val() == "请选择") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择省/区。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}
	else if ($("#seachcity").val() == "请选择") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择市/区。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#seachdistrict").val() == "请选择") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择县/区。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}
	else if ($("#txtAddress").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>地址不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtAddress").focus();
		return false;
	}	else if ($("#txtInvitedCode").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>邀请码(邀请手机号)不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtInvitedCode").focus();
		return false;
	}
    
	else if ($("#drpSelectShop").val() == "-请选择门店-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择门店。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}

    else if ($("#txtPayPwd").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>支付密码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}
     else if ($("#txtHighstGudong").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>最高股东手机号不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}
}
function ClearUserInfo() {
	$("#txtNickName").val(""); $("#txtName").val(""); $("#txtTel").val(""); $("#txtEmail").val("");
	$("#txtPass").val(""); $("#drpSex").val('-请选择性别-'); $("#txtAreacity").val(""); $("#txtAddress").val("");
	$("#seachprov").val('请选择'); $("#seachcity").val('请选择'); $("#seachdistrict").val('请选择');
	$("#txtCardNo").val(""); $("#txtHomeNum").val(""); $("#txtHomeInfo").val(""); $("#txtPlantNum").val("");
	$("#txtPlantSort").val(""); $("#txtRemarks").val(""); $("#drpSelectShop").val('-请选择门店-');
}
//删除用户信息
function DeleteUserInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteUserInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除[' + delinfo.split(',')[1] + ']用户信息成功。'); ShowUserInfoHtml(returnData);
		},
		error: function () {
			alert('删除[' + delinfo.split(',')[1] + ']用户信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,,", "ShowUserInfo");
	})
}


/*******************广告信息增删查询*****************************/

//广告信息一览
function ShowAdInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowAdInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowAdHtml(returnData);
		},
		error: function (errorinfo) {
			alert("广告信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "all", "ShowAdInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtTitle").val(""); $("#txtCompany").val(""); editor.html(""); $("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");

		$("#divShowAdManager").html('');
		$("#divShowAdManager").html("<tr class='error'><td colspan='8'>暂无广告信息，请添加。</td></tr>");
	});
}
//显示广告信息
function ShowAdHtml(returnData) {
	$("#divShowAdManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowAdManager").html("<tr class='error'><td colspan='8'>暂无广告信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowAdManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
//添加广告信息
function AddAdInfo() {

	if ($("#txtTitle").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>广告标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
	else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtGoodsCode").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品编码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCode").focus();
		return false;
	}
	else if (editor != undefined) {
		if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>广告内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			editor.focus(); return false;
		}
	}
}
//清除广告信息
function ClearAdInfo() {
	$("#txtTitle").val(""); $("#txtCompany").val(""); editor.html(""); $("#HFurl").val("");
	$("#hfDel").val(""); $("#iShowPhoto").val(""); $("#drpSetIndex").val('-是否首页显示-');
}
//删除广告信息
function DeleteAdInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteAdInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']广告信息成功。'); ShowAdHtml(returnData);
		},
		error: function () {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']广告信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowAdInfo");
	})
}

/**************新闻信息列表信息显示**********************/
function NoticesValidate() {
	if ($("#txtTitle").val() == "" || $("#txtTitle").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>新闻标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}

		//else if ($("#drpValidate").val() == "-请选择是否生效-") {
		//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否生效?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		//	return false;
		//}
		//else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		//	return false;
		//}
	else if ($("#drpNewsSort").val() == "-请选择新闻类型-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了新闻类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtSource").val() == "" || $("#txtSource").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>新闻来源不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtSource").focus();
		return false;
	}
	else if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>新闻内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		editor.focus(); return false;
	}
}
function ClearNotices() {
	$("#txtTitle,#txtSource").val(""); editor.html("");
	$("#drpNewsSort").val('-请选择新闻类型-');
	//$("#drpValidate").val('-请选择是否生效-'); $("#drpSetIndex").val('-是否首页显示-');
	$("#HFurl,#HFurl1,#HFurl2").val(""); $("#hfDel,#hfDel1,#hfDel2").val("");
	$("#iShowPhoto,#iShowPhoto1,#iShowPhoto2").val("");
}
function ShowNoticesHtml(returnData) {
	$("#divShowNoticesManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowNoticesManager").html("<tr class='error'><td colspan='9'>暂无新闻/公告信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowNoticesManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowNoticesInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowNoticesInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowNoticesHtml(returnData);
		},
		error: function (errorinfo) {
			alert("新闻/公告信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNoticesInfo");
	})
}
function DeleteNoticesInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteNoticesInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + decodeURIComponent(delinfo.split(',')[1]) + '新闻/公告信息成功。'); ShowNoticesHtml(returnData)
		},
		error: function () {
			alert('删除：' + decodeURIComponent(delinfo.split(',')[1]) + '新闻/公告信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNoticesInfo");

	})
}

/*******************新闻分类信息增删改查询*****************************/
function ShowNewsSortHtml(returnData) {
	$("#divShowNewsSortManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowNewsSortManager").html("<tr class='error'><td colspan='3'>暂无新闻分类信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowNewsSortManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowNewsSortInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowNewsSortInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowNewsSortHtml(returnData);
		},
		error: function (errorinfo) {
			alert("新闻分类信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsSortInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtNewsSort").val("");
		//$("#divShowNewsSortManager").html('');
		//$("#divShowNewsSortManager").html("<tr class='error'><td colspan='3'>暂无新闻分类信息，请添加。</td></tr>");
	});
}
function AddNewsSortInfo() {
	$("#btnAddNewsSort").click(function () {
		if ($("#txtNewsSort").val() == "" || $("#txtNewsSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>新闻分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			$.ajax({
				type: "post",
				data: "AddNewsSortInfo=" + encodeURIComponent($("#txtNewsSort").val()) + "&currPage=1",
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					ShowNewsSortHtml(returnData); $("#txtNewsSort").val('');
				},
				error: function () {
					alert('添加：' + $("#txtNewsSort").val() + '新闻分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsSortInfo");
	})
	$("#btnReset").click(function () {
		$("#txtNewsSort").val('');
	});
}
//编辑新闻分类信息
function EditNewsSortInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfNewsSortId").val(splitinfo[0]);

	$("#txtEditNewsSort").val(splitinfo[1]);

	$("#btnEditNewsSort").click(function () {
		if ($("#txtEditNewsSort").val() == "" || $("#txtEditNewsSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>新闻分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var updateinfo = $("#hfNewsSortId").val() + "," + $("#txtEditNewsSort").val();
			$.ajax(
			{
				type: "post",
				data: "EditNewsSortInfo=" + encodeURIComponent(updateinfo),
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditNewsSort").val() + "]信息修改成功。</span>", {
						type: "info", buttons: [
					{ type: "submit", value: "确定" }
						]
					}, function (result) {
						ShowNewsSortHtml(returnData); $("#btnClose").click();
					});
				},
				error: function () {
					alert('修改：' + $("#txtEditNewsSort").val() + '新闻分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsSortInfo");
	})
	$("#btnResetNewsSort").click(function () {
		$("#txtEditNewsSort").val('');

		//$("#hfNewsSortId").val('');
	});
}
//删除新闻分类信息
function DeleteNewsSortInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeleteNewsSortInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + delinfo.split(',')[1] + ']新闻分类信息成功。'); ShowNewsSortHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']新闻分类信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsSortInfo");
	})
}
/*******************新闻评论信息删查询*****************************/

function ShowNewsCommentHtml(returnData) {
	$("#divShowNewsCommentManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowNewsCommentManager").html("<tr class='error'><td colspan='6'>暂无新闻/公告评论信息。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowNewsCommentManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowNewsCommentInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowNewsCommentInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowNewsCommentHtml(returnData);
		},
		error: function (errorinfo) {
			alert("新闻/公告信息评论数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsCommentInfo");
	})
}
function DeleteNewsCommentInfo(delinfo) {
	//alert(decodeURIComponent(delinfo.split(',')[1]));
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此评论信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteNewsCommentInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + decodeURIComponent(delinfo.split(',')[1]) + ']新闻/公告评论信息成功。'); ShowNewsCommentHtml(returnData)
		},
		error: function () {
			alert('删除：[' + decodeURIComponent(delinfo.split(',')[1]) + ']新闻/公告评论信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowNewsCommentInfo");

	})
}
/*******************发布活动信息增删查询*****************************/
function SearchHuoDongListInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = $("#txtHuoDongTitle").val() + "," + $("#drpActive").val();
		ShowHuoDongInfo(searchwhere);
	});
	//清除查询条件
	$("#btnClear").click(function () {
		$("#txtHuoDongTitle").val(""); //$("#drpActive").val('-请选择审核状态-');
	});
}
//发布活动信息一览
function ShowHuoDongInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,";
	} else { searchwhere = searchinfo; }

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowHuoDongInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowHuoDongHtml(returnData);
		},
		error: function (errorinfo) {
			alert("发布活动信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowHuoDongInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtTitle").val(""); $("#txtPeople").val(""); $("#txtKeyWord").val(""); $("#txtSummary").val("");
		editor.html(""); $("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val(""); $("#txtTimeSelect").val("");
		//	$("#drpSetIndex").val('-是否首页显示-');
	});
}
//显示活动信息
function ShowHuoDongHtml(returnData) {
	$("#divShowHuoDongManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowHuoDongManager").html("<tr class='error'><td colspan='11'>暂无发布活动信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowHuoDongManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
//添加活动信息
function AddHuoDongInfo() {

	if ($("#txtTitle").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>活动标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
	else if ($("#txtPeople").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>活动发布者不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtPeople").focus();
		return false;
	}
	else if ($("#txtTimeSelect").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>活动时间不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTimeSelect").focus();
		return false;
	}
	else if ($("#txtKeyWord").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>活动关键词不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtKeyWord").focus();
		return false;
	}
		//else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		//	return false;
		//}
	else if (editor != undefined) {
		if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>活动内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			editor.focus(); return false;
		}
	}
}
//清除活动信息
function ClearHuoDongInfo() {
	$("#txtTitle").val(""); $("#txtPeople").val(""); $("#txtKeyWord").val(""); $("#txtSummary").val("");
	editor.html(""); $("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
	$("#txtTimeSelect").val("");// $("#drpSetIndex").val('-是否首页显示-');
}

//审核活动信息
function ActiveHuoDongInfo(atcinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要审核通过此活动？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "ActiveHuoDongInfo=" + atcinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('标题为:[' + atcinfo.split(',')[1] + ']活动审核成功。'); ShowHuoDongHtml(returnData);
		},
		error: function () {
			alert('标题为:[' + atcinfo.split(',')[1] + ']活动审核失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,", "ShowPlanInfo");
	})
}
//删除活动信息
function DeleteHuoDongInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteHuoDongInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']方案信息成功。'); ShowHuoDongHtml(returnData);
		},
		error: function () {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']方案信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,", "ShowHuoDongInfo");
	})
}

/*******************活动分类信息增删改查询*****************************/
function ShowHuoDongSortHtml(returnData) {
	$("#divShowHuoDongSortManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowHuoDongSortManager").html("<tr class='error'><td colspan='3'>暂无活动分类信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowHuoDongSortManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowHuoDongSortInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowHuoDongSortInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowHuoDongSortHtml(returnData);
		},
		error: function (errorinfo) {
			alert("活动分类信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowHuoDongSortInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtHuoDongSort").val("");
	});
}
function AddHuoDongSortInfo() {
	$("#btnAddHuoDongSort").click(function () {
		if ($("#txtHuoDongSort").val() == "" || $("#txtHuoDongSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>活动分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			$.ajax({
				type: "post",
				data: "AddHuoDongSortInfo=" + encodeURIComponent($("#txtHuoDongSort").val()) + "&currPage=1",
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					ShowHuoDongSortHtml(returnData); $("#txtHuoDongSort").val('');
				},
				error: function () {
					alert('添加：' + $("#txtHuoDongSort").val() + '活动分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowHuoDongSortInfo");
	})
	$("#btnReset").click(function () {
		$("#txtHuoDongSort").val('');
	});
}
//编辑活动分类信息
function EditHuoDongSortInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfHuoDongSortId").val(splitinfo[0]);

	$("#txtEditHuoDongSort").val(splitinfo[1]);

	$("#btnEditHuoDongSort").click(function () {
		if ($("#txtEditHuoDongSort").val() == "" || $("#txtEditHuoDongSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>活动分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var updateinfo = $("#hfHuoDongSortId").val() + "," + $("#txtEditHuoDongSort").val();
			$.ajax(
			{
				type: "post",
				data: "EditHuoDongSortInfo=" + encodeURIComponent(updateinfo),
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditHuoDongSort").val() + "]信息修改成功。</span>", {
						type: "info", buttons: [
					{ type: "submit", value: "确定" }
						]
					}, function (result) {
						ShowHuoDongSortHtml(returnData); $("#btnClose").click();
					});
				},
				error: function () {
					alert('修改：' + $("#txtEditHuoDongSort").val() + '活动分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowHuoDongSortInfo");
	})
	$("#btnResetHuoDongSort").click(function () {
		$("#txtEditHuoDongSort").val('');

		//$("#hfHuoDongId").val('');
	});
}
//删除活动分类信息
function DeleteHuoDongSortInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeleteHuoDongSortInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
	//	async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + delinfo.split(',')[1] + ']活动分类信息成功。'); ShowHuoDongSortHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']活动分类信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowHuoDongSortInfo");
	})
}
/**************参与活动信息删查询************************/
//参与活动信息信息一览
function ShowJoinHuoDong() {
	//$("#btnSearch").click(function ()
	//{																							
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowJoinHuoDong=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowJoinHuoDongHtml(returnData);
		},
		error: function (errorinfo) {
			alert("参与活动信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowJoinHuoDong");
	})

}

function ShowJoinHuoDongHtml(returnData) {
	$("#divShowJoinHuoDongManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowJoinHuoDongManager").html("<tr class='error'><td colspan='7'>暂无参与活动信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowJoinHuoDongManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

//删除讨论信息
function DeleteJoinHuoDong(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteJoinHuoDongInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除参与活动:[' + delinfo.split(',')[1] + ']的信息成功。'); ShowJoinHuoDongHtml(returnData);
		},
		error: function () {
			alert('删除参与活动:[' + delinfo.split(',')[1] + ']的信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowJoinHuoDong");
	})
}


/**************参与活动报名信息删查询************************/
function ShowJoinHuoDongInfo() {
	//$("#btnSearch").click(function ()
	//{																							
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowJoinHuoDongInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowJoinHuoDongInfoHtml(returnData);
		},
		error: function (errorinfo) {
			alert("参与活动信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowJoinHuoDongInfo");
	})

}

function ShowJoinHuoDongInfoHtml(returnData) {
	$("#divShowJoinHuoDongInfoManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowJoinHuoDongInfoManager").html("<tr class='error'><td colspan='14'>暂无参与活动报名信息</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowJoinHuoDongInfoManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

//删除讨论信息
function DeleteJoinHuoDongInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteJoinHuoDongInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除参与活动:[' + delinfo.split(',')[1] + ']的信息成功。'); ShowJoinHuoDongInfoHtml(returnData);
		},
		error: function () {
			alert('删除参与活动:[' + delinfo.split(',')[1] + ']的信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowJoinHuoDongInfo");
	})
}							


/*******************参加团购.预售活动信息增删查询*****************************/
function SearchJoinGoodsHuoDongListInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = $("#txtHuoDongTitle").val() + "," + $("#txtJoinName").val();
		ShowJoinGoodsHuoDongInfo(searchwhere);
	});
	//清除查询条件
	$("#btnClear").click(function () {
		$("#txtHuoDongTitle,#txtJoinName").val("");  
	});
}
//发布活动信息一览
function ShowJoinGoodsHuoDongInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,";
	} else { searchwhere = searchinfo; }

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowJoinGoodsHuoDongInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowJoinGoodsHuoDongHtml(returnData);
		},
		error: function (errorinfo) {
			alert("发布活动信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowJoinGoodsHuoDongInfo");
	})
 
}
//显示活动信息
function ShowJoinGoodsHuoDongHtml(returnData) {
	$("#divShowJoinGoodsHuoDongInfoManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowJoinGoodsHuoDongInfoManager").html("<tr class='error'><td colspan='11'>暂无参与预售/团购活动信息。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowJoinGoodsHuoDongInfoManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
 
//删除活动信息
function DeleteJoinGoodsHuoDongInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "DeleteJoinGoodsHuoDongInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']方案信息成功。'); ShowJoinGoodsHuoDongHtml(returnData);
		},
		error: function () {
			alert('删除标题为:[' + delinfo.split(',')[1] + ']方案信息失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,", "ShowJoinGoodsHuoDongInfo");
	})
}

/**************商品信息列表信息显示**********************/
function GoodsValidate() {
	if ($("#txtTitle").val() == "" || $("#txtTitle").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
     else  if ($("#drpSaleGoodsSort").val() == "-普通/推荐商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择普通/推荐商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
 

	else if ($("#drpValidate").val() == "-请选择是否生效-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否生效?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpGoodsSort").val() == "-请选择商品类型-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择商品类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtGoodsCode").val() == "" || $("#txtGoodsCode").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品编码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCode").focus();
		return false;
	}
	else if ($("#txtGoodsPrice").val() == "" || $("#txtGoodsPrice").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsPrice").focus();
		return false;
	}
	else if ($("#txtGoodsCost").val() == "" || $("#txtGoodsPrice").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品成本价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCost").focus();
		return false;
	}
	else if ($("#txtStockNum").val() == "" || $("#txtStockNum").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品库存数量不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtStockNum").focus();
		return false;
	}
	//else if ($("#txtGoldPrice").val() == "" || $("#txtGoldPrice").val() == undefined) {
	//	$.msgbox("<span style='font-size:13px;line-height:30px;'>金卡会员价不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	//	$("#txtGoldPrice").focus();
	//	return false;
	//}
	//else if ($("#txtSilverprice").val() == "" || $("#txtSilverprice").val() == undefined) {
	//	$.msgbox("<span style='font-size:13px;line-height:30px;'>银卡会员价不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	//	$("#txtSilverprice").focus();
	//	return false;
	//}
	else if ($("#drpSales").val() == "-是否特价促销商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否特价促销商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpExchangeGood").val() == "-是否积分兑换商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否积分兑换商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpXianGou").val() == "-是否限购商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否限购商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
    //else if ($("#drpGroupBy").val() == "-是否团购商品-") {
	//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否团购商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	//	return false;
	//}
    
	else if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		editor.focus(); return false;
	}
}
function ClearGoods() {
	$("#txtTitle").val(""); editor.html("");
	$("#drpGoodsSort").val('-请选择商品类型-');
	$("#drpValidate").val('-请选择是否生效-'); $("#drpSales").val('-是否特价促销商品-'); $("#drpExchangeGood").val('-是否积分兑换商品-');
	$("#drpSetIndex").val('-是否首页显示-'); $("#txtExchangePoint").val('');
	$("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
	$("#txtGoodsCode,#txtGoodsSpec,#txtGoodsCompany").val('');
  $("#drpXianGou").val('-是否限购商品-'); $("#txtXianGouNumber").val('');
  $("#txtGoodsPrice,#txtGoodsCost,#txtStockNum,#txtGoldPrice,#txtSilverprice").val('');
  $("#txtGetGoodPoint").val('');$("#drpGroupBy").val('-是否团购商品-');
}
function ShowGoodsHtml(returnData) {
	$("#divShowGoodsManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowGoodsManager").html("<tr class='error'><td colspan='14'>暂无商品信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGoodsManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function SearchGoodsInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = GetQueryString("mid").toString().split('?')[0] + ';' + GetQueryString("type").toString().split('?')[0] + ";" + $("#txtGoodsInfo").val() + ";" + $("#drpGoodsSort").val() + ";" + $("#drpGoodStatus").val() + ";" + $("#txtGoodSku").val();
		ShowGoodsInfo(searchwhere);
	});
	//清除查询条件
	$("#btnResrtSearch").click(function () {
		$("#txtGoodsInfo").val(""); $("#drpGoodsSort").val('商品分类'); $("#drpGoodStatus").val('商品状态'); $("#txtGoodSku").val("");
	});

}

function ShowGoodsInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = GetQueryString("mid").toString().split('?')[0]+';'+GetQueryString("type").toString().split('?')[0]+";;;;";
	} else { searchwhere = searchinfo; }
    //alert(searchwhere);
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGoodsHtml(returnData);
		},
		error: function (errorinfo) {
			alert("商品信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowGoodsInfo");
	})
}
function DeleteGoodsInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteGoodsInfo=" + GetQueryString("type").toString().split('?')[0]+","+ delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '商品信息成功。'); ShowGoodsHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '商品信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), GetQueryString("type").toString().split('?')[0] + ";" + ";;;;", "ShowGoodsInfo");

	})
}
//审核商品信息
function ActiveGoodsInfo(atcinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要下架此商品？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "ActiveGoodsInfo=" + GetQueryString("type").toString().split('?')[0]+","+ atcinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('标题为:[' + atcinfo.split(',')[1] + ']商品下架成功。'); ShowGoodsHtml(returnData);
		},
		error: function () {
			alert('标题为:[' + atcinfo.split(',')[1] + ']商品下架失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), GetQueryString("type").toString().split('?')[0] + ";" + ";;;;", "ShowGoodsInfo");
	})
}

/**************套餐商品信息列表信息显示**********************/
function GoodsPackageValidate() {
	if ($("#txtTitle").val() == "" || $("#txtTitle").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
 
		else if ($("#drpGoodsSort").val() == "-请选择商品类型-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择商品类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpValidate").val() == "-请选择是否生效-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否生效?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}

	else if ($("#txtGoodsCode").val() == "" || $("#txtGoodsCode").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品编码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCode").focus();
		return false;
	}
	else if ($("#txtGoodsPrice").val() == "" || $("#txtGoodsPrice").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsPrice").focus();
		return false;
	}
	else if ($("#txtGoodsCost").val() == "" || $("#txtGoodsPrice").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品成本价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCost").focus();
		return false;
	}
	else if ($("#txtStockNum").val() == "" || $("#txtStockNum").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品库存数量不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtStockNum").focus();
		return false;
	}

	else if ($("#drpSales").val() == "-是否特价促销商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否特价促销商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpExchangeGood").val() == "-是否积分兑换商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否积分兑换商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpXianGou").val() == "-是否限购商品-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否限购商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
		//else if ($("#drpGroupBy").val() == "-是否团购商品-") {
		//	$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否团购商品?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		//	return false;
		//}

	else if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>套餐商品内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		editor.focus(); return false;
	}
}
function ClearGoodsPackage() {
	$("#txtTitle").val(""); editor.html("");
	$("#drpGoodsSort").val('-请选择商品类型-');
	$("#drpValidate").val('-请选择是否生效-'); $("#drpSales").val('-是否特价促销商品-'); $("#drpExchangeGood").val('-是否积分兑换商品-');
	$("#drpSetIndex").val('-是否首页显示-'); $("#txtExchangePoint").val('');
	$("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
	$("#txtGoodsCode,#txtGoodsSpec,#txtGoodsCompany").val('');
	$("#drpXianGou").val('-是否限购商品-'); $("#txtXianGouNumber").val('');
	$("#txtGoodsPrice,#txtGoodsCost,#txtStockNum,#txtGoldPrice,#txtSilverprice").val('');
	$("#txtGetGoodPoint").val(''); $("#drpGroupBy").val('-是否团购商品-');
}
function ShowGoodsPackageHtml(returnData) {
	$("#divShowGoodsPackageManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowGoodsPackageManager").html("<tr class='error'><td colspan='14'>暂无套餐商品信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGoodsPackageManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function SearchGoodsPackageInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = GetQueryString("mid").toString().split('?')[0] + ";"+ $("#txtGoodsInfo").val() + ";" + $("#drpGoodsSort").val() + ";" + $("#drpGoodStatus").val() + ";" + $("#txtGoodSku").val();
		ShowGoodsPackageInfo(searchwhere);
	});
	//清除查询条件
	$("#btnResrtSearch").click(function () {
		$("#txtGoodsInfo").val(""); $("#drpGoodsSort").val('商品分类'); $("#drpGoodStatus").val('商品状态'); $("#txtGoodSku").val("");
	});

}

function ShowGoodsPackageInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = GetQueryString("mid").toString().split('?')[0] + ";;;;";
	} else { searchwhere = searchinfo; }
	//alert(searchwhere);
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsPackageInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGoodsPackageHtml(returnData);
		},
		error: function (errorinfo) {
			alert("套餐商品信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowGoodsPackageInfo");
	})
}
function DeleteGoodsPackageInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteGoodsPackageInfo=" +GetQueryString("mid").toString().split('?')[0]+ "," + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '套餐商品信息成功。'); ShowGoodsPackageHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '套餐商品信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), GetQueryString("mid").toString().split('?')[0] + ";;;;", "ShowGoodsPackageInfo");

	})
}
//审核商品信息
function ActiveGoodsPackageInfo(atcinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要下架此商品？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "ActiveGoodsPackageInfo=" +GetQueryString("mid").toString().split('?')[0] + "," + atcinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('标题为:[' + atcinfo.split(',')[1] + ']套餐商品下架成功。'); ShowGoodsPackageHtml(returnData);
		},
		error: function () {
			alert('标题为:[' + atcinfo.split(',')[1] + ']套餐商品下架失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this),GetQueryString("mid").toString().split('?')[0]   + ";;;;", "ShowGoodsPackageInfo");
	})
}
/*******************商品分类信息增删改查询*****************************/
function ShowGoodsSortHtml(returnData) {
	$("#divShowGoodsSortManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowGoodsSortManager").html("<tr class='error'><td colspan='4'>暂无商品分类信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else if (returnData == "Warning" || returnData == "") {
		$("#divShowGoodsSortManager").html("<tr class='error'><td colspan='4'>不能创建超过三级的商品分类,当前为第三级。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGoodsSortManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function ShowGoodsSortInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsSortInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGoodsSortHtml(returnData);
		},
		error: function (errorinfo) {
			alert("商品分类信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowGoodsSortInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtGoodsSort").val(""); $("#drpGoodstSort").val("-请选择商品类型-");
		//$("#divShowGoodsSortManager").html('');
		//$("#divShowGoodsSortManager").html("<tr class='error'><td colspan='3'>暂无商品分类信息，请添加。</td></tr>");
	});
}
function AddGoodsSortInfo() {
	$("#btnAddGoodsSort").click(function () {
		if ($("#drpGoodstSort").val() == "-请选择商品类型-") {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了商品类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			return false;
		}
		else if ($("#txtGoodsSort").val() == "" || $("#txtGoodsSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>商品分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var insertinfo = $("#drpGoodstSort").val() + "," + $("#txtGoodsSort").val();

			$.ajax({
				type: "post",
				data: "AddGoodsSortInfo=" + encodeURIComponent(insertinfo) + "&currPage=1",
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					ShowGoodsSortHtml(returnData); $("#txtGoodsSort").val(''); $("#drpGoodstSort").val("-请选择商品类型-");
				},
				error: function () {
					alert('添加：' + $("#txtGoodsSort").val() + '商品分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowGoodsSortInfo");
	})
	$("#btnReset").click(function () {
		$("#txtGoodsSort").val(''); $("#drpGoodstSort").val("-请选择商品类型-");
	});
}
//编辑商品分类信息
function EditGoodsSortInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfGoodsSortId").val(splitinfo[0]);

	$("#txtEditGoodsSort").val(splitinfo[1]);
	$('select#drpEditGoodstSort')[0].selectedIndex = splitinfo[2];
	//$('select#sel').attr('value', '4');
	//或者
	//$("select#sel option[value='4']").attr('selected', 'true');
	//$("#drpEditGoodstSort").val(splitinfo[1]);
	$("#btnEditGoodsSort").click(function () {
		if ($("#drpEditGoodstSort").val() == "-请选择商品类型-") {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了商品类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			return false;
		}
		else if ($("#txtEditGoodsSort").val() == "" || $("#txtEditGoodsSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>商品分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var updateinfo = $("#hfGoodsSortId").val() + "," + $("#drpEditGoodstSort").val() + "," + $("#txtEditGoodsSort").val();
			$.ajax(
			{
				type: "post",
				data: "EditGoodsSortInfo=" + encodeURIComponent(updateinfo),
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditGoodsSort").val() + "]信息修改成功。</span>", {
						type: "info", buttons: [
					{ type: "submit", value: "确定" }
						]
					}, function (result) {
						ShowGoodsSortHtml(returnData); $("#btnClose").click();
					});
				},
				error: function () {
					alert('修改：' + $("#txtEditGoodsSort").val() + '商品分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowGoodsSortInfo");
	})
	$("#btnResetGoodsSort").click(function () {
		$("#txtEditGoodsSort").val('');
		$("#drpEditGoodstSort").val("-请选择商品类型-");
		//$("#hfGoodsSortId").val('');
	});
}
//删除商品分类信息
function DeleteGoodsSortInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeleteGoodsSortInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {

			alert('删除：[' + delinfo.split(',')[1] + ']商品分类信息成功。'); ShowGoodsSortHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']商品分类信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowGoodsSortInfo");
	})
}

/**************团购列表信息显示及删除**********************/
function GroupBuyValidate() {
	if ($("#txtTitle").val() == "" || $("#txtTitle").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>团购标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
   else  if ($("#drpSaleGoodsSort").val() == "-团购/预售分类-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择团购/预售分类?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}

	else if ($("#txtTimeSelect").val() == "" || $("#txtTimeSelect").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>开始日期-结束日期不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
    else if ($("#txtGoodsCode").val() == "" || $("#txtGoodsCode").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>商品编码不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtGoodsCode").focus();
		return false;
	}
	else if ($("#drpValidate").val() == "-请选择是否生效-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否生效?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
    else if ($("#drpGoodsSort").val() == "-请选择商品类型-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了商品类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtQuantily").val() == "" || $("#txtQuantily").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>数量不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtGoodsPrice").val() == "" || $("#txtGoodsPrice").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#txtGoodsCost").val() == "" || $("#txtGoodsCost").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>成本价格不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
    	else if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>团购内容是必填项，请输入团购内容。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		editor.focus(); return false;
	}

}
function ClearGroupBuy() {
	$("#txtTitle").val(""); editor.html("");
	$("#txtTimeSelect").val(""); $("#txtQuantily").val("");
	$("#txtGoodsPrice,#txtGoodsCost").val(""); $("#txtGroupNum").val("");
	$("#drpValidate").val('-请选择是否生效-');

	$("#drpSetIndex").val('-是否首页显示-');$("#drpSaleGoodsSort").val('-团购/预售分类-');

	$("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
	$("#HFurl1").val(""); $("#hfDel1").val(""); $("#iShowPhoto1").val("");
	$("#HFurl2").val(""); $("#hfDel2").val(""); $("#iShowPhoto2").val("");
	$("#drpGoodsSort").val('-请选择商品类型-');
}

function ShowGroupGoodsInfo() {
	$("#btnSearch").click(function () {
		var searchwhere = GetQueryString("mid").toString().split('?')[0] + ';' + GetQueryString("type").toString().split('?')[0] + ";" + $("#txtGoodsInfo").val() + ";" + $("#drpGoodsSort").val() + ";" + $("#drpGoodStatus").val() + ";" + $("#txtGoodSku").val();
		ShowGroupBuyInfo(searchwhere);
	});
	//清除查询条件
	$("#btnResrtSearch").click(function () {
		$("#txtGoodsInfo").val(""); $("#drpGoodsSort").val('商品分类'); $("#drpGoodStatus").val('商品状态'); $("#txtGoodSku").val("");
	});

}

function ShowGroupBuyHtml(returnData) {
	$("#divShowGroupBuyManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowGroupBuyManager").html("<tr class='error'><td colspan='16'>暂无团购信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGroupBuyManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowGroupBuyInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = GetQueryString("mid").toString().split('?')[0] + ';' + GetQueryString("type").toString().split('?')[0] + ";;;;";
	} else { searchwhere = searchinfo; }
    //(searchwhere);
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGroupBuyInfo=" +encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGroupBuyHtml(returnData);
		},
		error: function (errorinfo) {
			alert("团购信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowGroupBuyInfo");
	})
}

function DeleteGroupBuyInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteGroupBuyInfo=" +GetQueryString("type").toString().split('?')[0]+","+delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除' + delinfo.split(',')[1] + '团购信息成功。'); ShowGroupBuyHtml(returnData)
		},
		error: function () {
			alert('删除' + delinfo.split(',')[1] + '团购信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), GetQueryString("type").toString().split('?')[0] + ";" + ";;;;", "ShowGroupBuyInfo");
	})
}

//审核商品信息
function ActiveGroupGoodsInfo(atcinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要下架此商品？</span>",
			{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
				if (result != "取消")
					$.ajax(
	{
		type: "post",
		data: "ActiveGroupGoodsInfo=" + GetQueryString("type").toString().split('?')[0]+","+atcinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('标题为:[' + atcinfo.split(',')[1] + ']商品下架成功。'); ShowGroupBuyHtml(returnData);
		},
		error: function () {
			alert('标题为:[' + atcinfo.split(',')[1] + ']商品下架失败。');
		}
	});
			});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), GetQueryString("type").toString().split('?')[0] + ";" + ";;;;", "ShowGroupBuyInfo");
	})
}
/**************订单列表信息及收货确认*********************/
function ShowOrderListHtml(returnData) {
	$("#divShowOrderListManager").html('');
	if (returnData == "NoData" || returnData == "") {
		var headTitle = "<tr><th ><input type='checkbox' name='option1' id='chkAll' class='uniform' onclick='selectAll();' value='全选' />选择</th>";
		headTitle += " <th>订单编号</th><th>买家姓名</th>	<th>电话</th><th>区市</th> <th>地址</th><th>订单金额</th><th>订单来源</th><th>支付方式 </th><th>下单时间 </th><th>支付时间 </th><th>订单状态</th><th>操作</th></tr>";
		headTitle += "	<tr class='error'><td colspan='13'>暂无订单列表信息。</td></tr>";
		$("#divShowOrderListManager").html(headTitle);

		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowOrderListManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
//切换不同订单状态
var saveordertype;
function OrderListShow(ordertype) {
	var searchwhere = "," + ordertype + "," + ",";
	saveordertype = ordertype;
	ShowOrderListInfo(searchwhere);
}
function SearchOrderListInfo() {

	$("#btnSearch").click(function () {
		var searchwhere = $("#txtOrderNo").val() + "," + $("#drpOrderStatus").val() + "," + $("#txtTimeSelect").val();
		ShowOrderListInfo(searchwhere);
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtOrderNo").val(""); $("#drpOrderStatus").val('订单状态'); $("#txtTimeSelect").val("");
	});
	//分页操作动作
	$(".pages").click(function () {
		var searchwhere;
		if (saveordertype == "" || saveordertype == undefined) {
			searchwhere = ",,,";
		} else { searchwhere = "," + saveordertype + "," + ","; }
		PagingActionClick($(this), searchwhere, "ShowOrderListInfo");
		return;
	})

}
function ShowOrderListInfo(searchinfo) {
	//页面初始化清空保存的订单号
	$("#hfSeleckSo").val('');
	$("#hfCheckSo").val('');
	//var timeSearch;
	//if (searchtime == "" || searchtime == undefined)
	//{
	//	timeSearch = year + "/" + month + "/" + day + " - " + year + "/" + month + "/" + day  ;
	//} else { timeSearch = searchtime; }
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,,";
	} else { searchwhere = searchinfo; }

	//页面初始化清空保存的订单号
	$("#hfSeleckSo").val('');
	$("#hfCheckSo").val('');
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowOrderListInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowOrderListHtml(returnData);
		},
		error: function (errorinfo) {
			alert("订单信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});

	$("#btnOrderOk").click(function () {
		OkOrderListInfo();
	});
	$("#btnDelOrderOk").click(function () {
		DeleteAllOrderInfo();
	});
}

//确认订单
function OkOrderListInfo() {
	// 查询选择条件
	if ($('input[type="checkbox"][name="option1"]:checked').each(function (i) {
	}).length == 0) {
		$.msgbox("<span style='font-size:14px;line-height:30px;'>请选择订单后进行发货。</span>",
		{ type: "error", buttons: [{ type: 'submit', value: '确定' }] });
		return false;
	}
	else {
		//拼接选择的订单号
		var orderno = "";
		//选择后的订单ID
		var updateSoID = "";
		var num = $('input[type="checkbox"][name="option1"]:checked');
		if (num[0].value == "全选") {
			//alert("全选保存的值:" + $("#hfSeleckSo").val());
			updateSoID = $("#hfSeleckSo").val();
		}
		else {
			for (var i = 0; i < num.length; i++) {
				orderno += num[i].value + ";";
			}
			$("#hfCheckSo").val(orderno);
			// alert("N单选加起来保存的值：" + $("#hfCheckSo").val());
			updateSoID = $("#hfCheckSo").val();

		}
		$.ajax(
{
	type: "post",
	data: "OkOrderListInfo=" + updateSoID,
	url: "EnshineUnionHandler.ashx",
	datatype: "html",
	//async: false,
	success: function (returnData, textstatus, xmlhttprequest) {
		alert('确认订单ID:' + updateSoID + '等信息发货成功。'); ShowOrderListHtml(returnData);
	},
	error: function () {
		alert('确认订单ID:' + updateSoID + '等信息失败。');
	}
});
		//清除转向页面
		$("#txtGoPage").val("");
	}
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,,", "ShowOrderListInfo");


	})
}

//删除单条订单
function DeleteOneOrderInfo(orderno) {

	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此订单？</span>",
	{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
		if (result != "取消")
			$.ajax(
{
	type: "post",
	data: "DeleteOneOrderInfo=" + orderno,
	url: "EnshineUnionHandler.ashx",
	datatype: "html",
	//async: false,
	success: function (returnData, textstatus, xmlhttprequest) {
		alert('订单号:' + orderno + '删除成功。'); ShowOrderListHtml(returnData);
	},
	error: function () {
		alert('订单号:' + orderno + '删除失败。');
	}
});
	});
		//清除转向页面
		$("#txtGoPage").val("");
 
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,,", "ShowOrderListInfo");
	})
}
	//批量删除订单
function DeleteAllOrderInfo() {
	// 查询选择条件
	if ($('input[type="checkbox"][name="option1"]:checked').each(function (i) {
	}).length == 0) {
		$.msgbox("<span style='font-size:14px;line-height:30px;'>请选择订单后进行删除。</span>",
		{ type: "error", buttons: [{ type: 'submit', value: '确定' }] });
		return false;
	}
	else {
		//拼接选择的订单号
		var orderno = "";
		//选择后的订单ID
		var updateSoID = "";
		var num = $('input[type="checkbox"][name="option1"]:checked');
		if (num[0].value == "全选") {
			//alert("全选保存的值:" + $("#hfSeleckSo").val());
			updateSoID = $("#hfSeleckSo").val();
		}
		else {
			for (var i = 0; i < num.length; i++) {
				orderno += num[i].value + ";";
			}
			$("#hfCheckSo").val(orderno);
			// alert("N单选加起来保存的值：" + $("#hfCheckSo").val());
			updateSoID = $("#hfCheckSo").val();

		}
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此订单？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
{
	type: "post",
	data: "DeleteAllOrderInfo=" + updateSoID,
	url: "EnshineUnionHandler.ashx",
	datatype: "html",
	//async: false,
	success: function (returnData, textstatus, xmlhttprequest) {
		alert('订单号:' + updateSoID + '等删除成功。'); ShowOrderListHtml(returnData);
	},
	error: function () {
		alert('订单号:' + updateSoID + '等删除失败。');
	}
});
});
	
		//清除转向页面
		$("#txtGoPage").val("");
	}
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,,", "ShowOrderListInfo");
	})
}

/*******************植物医院分类信息增删改查询*****************************/
function ShowPlantDoctorSortHtml(returnData) {
	$("#divShowPlantDoctorSortManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowPlantDoctorSortManager").html("<tr class='error'><td colspan='5'>暂无植保分类信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowPlantDoctorSortManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function ShowPlantDoctorSortInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowPlantDoctorSortInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowPlantDoctorSortHtml(returnData);
		},
		error: function (errorinfo) {
			alert("植保分类信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowPlantDoctorSortInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtPlantDoctorSort").val("");
		//$("#divShowPlantDoctorSortManager").html('');
		//$("#divShowPlantDoctorSortManager").html("<tr class='error'><td colspan='3'>暂无商品分类信息，请添加。</td></tr>");
	});
}
function AddPlantDoctorSortInfo() {
	$("#btnAddPlantDoctorSort,#btnUpPlantDoctorSort").click(function () {
		if ($("#drpPlantSort").val() == "-请选择植保上级分类-") {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了植保上级分类?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			return false;
		}

		else if ($("#txtPlantDoctorSort").val() == "" || $("#txtPlantDoctorSort").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>植保分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
			return false;
		}
	});
	//$("#btnAddPlantDoctorSort").click(function () {
	//	if ($("#txtPlantDoctorSort").val() == "" || $("#txtPlantDoctorSort").val() == undefined) {
	//		$.msgbox("<span style='font-size:13px;line-height:30px;'>植保分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	//	}
	//	else {
	//    var inserinfo=$("#txtPlantDoctorSort").val()+","+$("#HFurl").val();
	//		$.ajax({
	//			type: "post",
	//			data: "AddPlantDoctorSortInfo=" + encodeURIComponent(inserinfo) + "&currPage=1",
	//			url: "EnshineUnionHandler.ashx",
	//			datatype: "json",
	//			async: false,
	//			success: function (returnData, textstatus, xmlhttprequest) {
	//				ShowPlantDoctorSortHtml(returnData); $("#txtPlantDoctorSort").val('');
	//			},
	//			error: function () {
	//				alert('添加：' + $("#txtPlantDoctorSort").val() + '植保分类信息失败。');
	//			}
	//		});
	//	}
	//});
	////分页操作动作
	//$(".pages").click(function () {
	//	PagingActionClick($(this), "all", "ShowPlantDoctorSortInfo");
	//})
	//$("#btnReset").click(function () {
	//	$("#txtPlantDoctorSort").val('');
	//});
}
//编辑商品分类信息
function EditPlantDoctorSortInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfPlantDoctorSortId").val(splitinfo[0]);
	$("#drpPlantSort").val(splitinfo[3]);
	$("#txtPlantDoctorSort").val(splitinfo[1]);
	if (splitinfo[2] == "" || splitinfo[2] == undefined)
		$("#iShowPhoto").attr("src", "assets/images/nophoto.gif");
	else
		$("#iShowPhoto").attr("src", splitinfo[2]);
	$("#hfDel").val(splitinfo[2]);


	////$("#btnEditPlantDoctorSort").click(function () {
	////	if ($("#txtPlantDoctorSort").val() == "" || $("#txtPlantDoctorSort").val() == undefined) {
	////		$.msgbox("<span style='font-size:13px;line-height:30px;'>植保分类名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	////	}
	////	else {
	////		var updateinfo = $("#hfPlantDoctorSortId").val() + "," + $("#txtPlantDoctorSort").val()+","+$("#HFurl").val();;
	////		$.ajax(
	////		{
	////			type: "post",
	////			data: "EditPlantDoctorSortInfo=" + encodeURIComponent(updateinfo),
	////			url: "EnshineUnionHandler.ashx",
	////			datatype: "json",
	////			async: false,
	////			success: function (returnData, textstatus, xmlhttprequest) {
	////				$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditPlantDoctorSort").val() + "]信息修改成功。</span>", {
	////					type: "info", buttons: [
	////				{ type: "submit", value: "确定" }
	////					]
	////				}, function (result) {
	////					ShowPlantDoctorSortHtml(returnData); $("#btnClose").click();
	////				});
	////			},
	////			error: function () {
	////				alert('修改：' + $("#txtEditPlantDoctorSort").val() + '植保分类信息失败。');
	////			}
	////		});
	////	}
	////});
	//////分页操作动作
	////$(".pages").click(function () {
	////	PagingActionClick($(this), "all", "ShowPlantDoctorSortInfo");
	////})
	////$("#btnResetPlantDoctorSort").click(function () {
	////	$("#txtEditPlantDoctorSort").val('');

	////	$("#hfPlantDoctorSortId").val('');
	////});
}
//删除商品分类信息
function DeletePlantDoctorSortInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeletePlantDoctorSortInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + delinfo.split(',')[1] + ']植保分类信息成功。'); ShowPlantDoctorSortHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']植保分类信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowPlantDoctorSortInfo");
	})
}
function ClearPlantDoctorSort() {
	$("#txtPlantDoctorSort").val(""); $("#hfPlantDoctorSortId").val("");
	$("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
}

/**************植保医院信息列表信息显示**********************/
function PlantDoctorValidate() {
	if ($("#txtTitle").val() == "" || $("#txtTitle").val() == undefined) {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>植保医院标题不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtTitle").focus();
		return false;
	}
	else if ($("#drpPlantDoctorSort").val() == "-请选择植保类型-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>是否选择了植保类型?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}


	else if ($("#drpValidate").val() == "-请选择是否生效-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否生效?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if ($("#drpSetIndex").val() == "-是否首页显示-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择是否首页显示?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
	else if (editor.html().replace("<p>\n\t<br />\n</p>", "") == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>植保医院内容是必填项，请输入内容后再提交。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		editor.focus(); return false;
	}
}
function ClearPlantDoctor() {
	$("#txtTitle").val(""); editor.html(""); $("#txtSummary").val('');
	$("#drpPlantDoctorSort").val('-请选择植保类型-');
	$("#drpValidate").val('-请选择是否生效-'); $("#drpSetIndex").val('-是否首页显示-');
	$("#HFurl").val(""); $("#hfDel").val(""); $("#iShowPhoto").val("");
}
function ShowPlantDoctorHtml(returnData) {
	$("#divShowPlantDoctorManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowPlantDoctorManager").html("<tr class='error'><td colspan='8'>暂无植保医院信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowPlantDoctorManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowPlantDoctorInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowPlantDoctorInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowPlantDoctorHtml(returnData);
		},
		error: function (errorinfo) {
			alert("植保医院信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowPlantDoctorInfo");
	})
}
function DeletePlantDoctorInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeletePlantDoctorInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '植保医院信息成功。'); ShowPlantDoctorHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '植保医院信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowPlantDoctorInfo");

	})
}

//**************************用户充值验证**************************/
function UserRechargeValidate() {

	if ($("#txtRecTel").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>充值手机号不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtRecTel").focus();
		return false;
	}
	else if ($("#txtRecMoney").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>充值金额不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtRecMoney").focus();

		return false;
	}
else if ($("#drpHuiYuanJIbie").val() == "-请选择会员级别-") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择会员级别?</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		return false;
	}
    else if ($("#txtPoint").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请输入充值所获积分.</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	
    	$("#txtPoint").focus();return false;
	}
}
function UserPointValidate() {

	if ($("#txtRecTel").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>充值手机号不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtRecTel").focus();
		return false;
	}
	 if ($("#txtPoint").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请输入充值所获积分.</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
	
    	$("#txtPoint").focus();return false;
	}
}
function ClearUserRecharge() {
	$("#txtRecTel").val(""); $("#hfuserid").val(""); $("#txtRecMoney").val(""); $("#txtFckContent").val("");
	$("#txtRecUserName").val("");
}

//******************************用户充值列表**************************/
function SearchUserRechargeListInfo() {

	$("#btnSearch").click(function () {
		var searchwhere = $("#txtRechargeTel").val() + "," + $("#drpRechargeStatus").val() + "," + $("#txtTimeSelect").val();
		ShowUserRechargeInfo(searchwhere);
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtRechargeTel").val(""); $("#drpRechargeStatus").val('交易状态'); $("#txtTimeSelect").val("");
	});
}
function ShowUserRechargeHtml(returnData) {
	$("#divShowUserRechargeManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowUserRechargeManager").html("<tr class='error'><td colspan='8'>暂无用户充值信息，请进行充值。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowUserRechargeManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowUserRechargeInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,,";
	} else { searchwhere = searchinfo; }

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserRechargeInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowUserRechargeHtml(returnData);
		},
		error: function (errorinfo) {
			alert("用户充值信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowUserRechargeInfo");
	})
}
function CancelRechargeInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要撤销此次充值？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "CancelRecInfo=" + delinfo,
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('撤销手机号：' + delinfo.split(',')[1] + '充值信息成功。'); ShowUserRechargeHtml(returnData)
		},
		error: function () {
			alert('撤销手机号：' + delinfo.split(',')[1] + '充值信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this),",,,", "ShowUserRechargeInfo");

	})
}
function DeleteRechargeInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此次充值？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteRecInfo=" + delinfo,
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除手机号：' + delinfo.split(',')[1] + '充值信息成功。'); ShowUserRechargeHtml(returnData)
		},
		error: function () {
			alert('删除手机号：' + delinfo.split(',')[1] + '充值信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this),",,,", "ShowUserRechargeInfo");

	})
}

 


//******************************角色权限设置**************************/
function RoleValidate() {

	if ($("#txtRoleName").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>角色名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		$("#txtRoleName").focus();
		return false;
	}
	else if ($("#hfRole").val() == "") {
		$.msgbox("<span style='font-size:13px;line-height:30px;'>请选择权限。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });

		return false;
	}

}
function ClearRole() {
	$("#txtRoleName").val(""); $("#txtFckContent").val(""); $("#hfRole").val("");
}

function ShowUserRoleHtml(returnData) {
	$("#divShowRoleManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowRoleManager").html("<tr class='error'><td colspan='5'>暂无角色权限信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowRoleManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowUserRoleInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserRoleInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowUserRoleHtml(returnData);
		},
		error: function (errorinfo) {
			alert("角色信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowUserRoleInfo");
	})
}
function DeleteRoleInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteRoleInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '角色信息成功。'); ShowUserRoleHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '角色信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowUserRoleInfo");

	})
}
/*******************会员信息增删改查询*****************************/
function ShowMemberHtml(returnData) {
	$("#divShowMemberManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowMemberManager").html("<tr class='error'><td colspan='4'>暂无会员列表信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowMemberManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function ShowMemberInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMemberInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowMemberHtml(returnData);
		},
		error: function (errorinfo) {
			alert("会员列表信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMemberInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtMemberName").val(""); $("#txtDiscount").val("");
	});
}
function AddMemberInfo() {
	$("#btnAddMember").click(function () {
		if ($("#txtMemberName").val() == "" || $("#txtMemberName").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>会员名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else if ($("#txtDiscount").val() == "" || $("#txtDiscount").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>折扣率不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var addinfo = $("#txtMemberName").val() + "_" + $("#txtDiscount").val();

			$.ajax({
				type: "post",
				data: "AddMemberInfo=" + encodeURIComponent(addinfo) + "&currPage=1",
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					ShowMemberHtml(returnData); $("#txtMemberName").val(""); $("#txtDiscount").val("");
				},
				error: function () {
					alert('添加：' + $("#txtMemberName").val() + '会员信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMemberInfo");
	})
	$("#btnReset").click(function () {
		$("#txtMemberName").val(""); $("#txtDiscount").val("");
	});
}
//编辑会员信息
function EditMemberInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfMemberId").val(splitinfo[0]);

	$("#txtEditMemberName").val(splitinfo[1]);
	$("#txtEditDiscount").val(splitinfo[2]);

	$("#btnEditMember").click(function () {
		if ($("#txtEditMemberName").val() == "" || $("#txtEditMemberName").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>会员名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		} else if ($("#txtEditDiscount").val() == "" || $("#txtEditDiscount").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>会员折扣率不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var updateinfo = $("#hfMemberId").val() + "," + $("#txtEditMemberName").val() + "," + $("#txtEditDiscount").val();
			$.ajax(
			{
				type: "post",
				data: "EditMemberInfo=" + encodeURIComponent(updateinfo),
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditMemberName").val() + "]信息修改成功。</span>", {
						type: "info", buttons: [
					{ type: "submit", value: "确定" }
						]
					}, function (result) {
						ShowMemberHtml(returnData); $("#btnClose").click();
					});
				},
				error: function () {
					alert('修改：' + $("#txtEditMemberName").val() + '商品分类信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMemberInfo");
	})
	$("#btnResetMember").click(function () {
		$("#txtEditMemberName").val(""); $("#txtEditDiscount").val("");

		//$("#hfMemberId").val('');
	});
}
//删除会员信息
function DeleteMemberInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeleteMemberInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + delinfo.split(',')[1] + ']会员信息成功。'); ShowMemberHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']会员信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMemberInfo");
	})
}

//******************************我的收入明细列表**************************/

function ShowMyPayMoneyInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMyPayMoneyInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowMyPayMoneyManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowMyPayMoneyManager").html("<tr class='error'><td colspan='6'>暂无收入明细信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowMyPayMoneyManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("我的收入明细信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMyPayMoneyInfo");
	})
}

//******************************我的积分明细列表**************************/

function ShowMyPointInfo() {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMyPointInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowMyPointManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowMyPointManager").html("<tr class='error'><td colspan='4'>暂无积分明细信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowMyPointManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("我的积分明细信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowMyPointInfo");
	})
}

//******************************月销售金额报表**************************/
function SearchFinanceMonthListInfo() {

	$("#btnSearch").click(function () {
		ShowFinanceMonthListInfo($("#txtTimeSelect").val());
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtTimeSelect").val("");
	});
}
function ShowFinanceMonthListInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = "NowMonth";
	} else { searchwhere = searchinfo; }

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowwFinanceMonthListInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowFinanceReport").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowFinanceReport").html("<tr class='error'><td colspan='6'>暂无月销售金额报表统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowFinanceReport").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("月销售金额报表统计信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowwFinanceMonthListInfo");

	})


}


//******************************b)	当月分销提成金额报表**************************/

function SearchFenXiaoExtracListInfo() {

	$("#btnSearch").click(function () {
		ShowFenXiaoExtractInfo($("#txtTimeSelect").val());
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtTimeSelect").val("");
	});
}
function ShowFenXiaoExtractInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = "NowMonth";
	} else { searchwhere = searchinfo; }


	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowFenXiaoExtractInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowFenXiaoReport").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowFenXiaoReport").html("<tr class='error'><td colspan='3'>暂无当月分销提成金额报表信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowFenXiaoReport").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("当月分销提成金额报表信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowFenXiaoExtractInfo");

	})

}

//******************************b)	当月会员提现报表（**************************/

function SearchMemberExtractListInfo() {

	$("#btnSearch").click(function () {
		ShowMemberExtractInfo($("#txtTimeSelect").val());
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtTimeSelect").val("");
	});
}
function ShowMemberExtractInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = "NowMonth";
	} else { searchwhere = searchinfo; }


	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMemberExtractInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowMemberReport").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowMemberReport").html("<tr class='error'><td colspan='3'>暂无当月会员提现金额报表信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowMemberReport").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("当月会员提现金额报表信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), searchwhere, "ShowMemberExtractInfo");

	})

}
//******************************b)	a)	会员提现管理（**************************/
function ShowUserExtractHtml(returnData) {
	$("#divShowExtractListManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowExtractListManager").html("<tr class='error'><td colspan='7'>暂无会员提现管理信息。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowExtractListManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowUserExtractListInfo() {
	//页面初始化清空保存的订单号
	$("#hfSeleckExtNo").val('');
	$("#hfCheckExtNo").val('');
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserExtractListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowUserExtractHtml(returnData);
		},
		error: function (errorinfo) {
			alert("会员提现管理数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowUserExtractListInfo");

	})

	$("#btnExtractOk").click(function () {
		OkExtractListInfo();
	});
}

function OkExtractListInfo() {
	// 查询选择条件
	if ($('input[type="checkbox"][name="option1"]:checked').each(function (i) {
	}).length == 0) {
		$.msgbox("<span style='font-size:14px;line-height:30px;'>请选择后进行生成提现清单。</span>",
		{ type: "error", buttons: [{ type: 'submit', value: '确定' }] });
		return false;
	}
	else {
		//拼接选择的订单号
		var orderno = "";
		//选择后的订单ID
		var updateSoID = "";
		var num = $('input[type="checkbox"][name="option1"]:checked');
		if (num[0].value == "全选") {
			//alert("全选保存的值:" + $("#hfSeleckExtNo").val());
			updateSoID = $("#hfSeleckExtNo").val();
		}
		else {
			for (var i = 0; i < num.length; i++) {
				orderno += num[i].value + ";";
			}
			$("#hfCheckSo").val(orderno);
			//alert("N单选加起来保存的值：" + $("#hfCheckExtNo").val());
			updateSoID = $("#hfCheckExtNo").val();

		}
		$.ajax(
{
	type: "post",
	data: "OkCreateExtractListInfo=" + updateSoID,
	url: "EnshineUnionHandler.ashx",
	datatype: "html",
	//async: false,
	success: function (returnData, textstatus, xmlhttprequest) {
		alert('生成订单ID:' + updateSoID + '等提现清单成功。'); ShowUserExtractHtml(returnData);
	},
	error: function () {
		alert('生成ID为:' + delinfo.split(',')[1] + '等提现清单失败。');
	}
});
		//清除转向页面
		$("#txtGoPage").val("");
	}
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "All", "ShowUserExtractListInfo");
	})
}
/*******************店铺设置信息增删改查询*****************************/
function ShowShopHtml(returnData) {
	$("#divShowShopManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowShopManager").html("<tr class='error'><td colspan='4'>暂无门店店铺信息，请添加。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowShopManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}
function ShowShopInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowShopInfo=" + "all" + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowShopHtml(returnData);
		},
		error: function (errorinfo) {
			alert("门店店铺信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowShopInfo");
	})

	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtShopName,#txtShopTel").val("");
	});
}
function AddShopInfo() {
	$("#btnAddShop").click(function () {
		if ($("#txtShopName").val() == "" || $("#txtShopName").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>门店店铺名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else if ($("#txtShopTel").val() == "" || $("#txtShopTel").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>店铺联系方式不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var insertInfo = $("#txtShopName").val() + "," + $("#txtShopTel").val();
			$.ajax({
				type: "post",
				data: "AddShopInfo=" + encodeURIComponent(insertInfo) + "&currPage=1",
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					ShowShopHtml(returnData); $("#txtShopName,#txtShopTel").val('');
				},
				error: function () {
					alert('添加：' + $("#txtShopName").val() + '门店店铺信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		var insertInfo = $("#txtShopName").val() + "," + $("#txtShopTel").val();
		PagingActionClick($(this), insertInfo, "ShowShopInfo");
	})
	$("#btnReset").click(function () {
		$("#txtShopName,#txtShopTel").val("");
	});
}
//编辑新闻分类信息
function EditShopInfo(editinfo) {
	var splitinfo = editinfo.split(',');
	$("#hfShopId").val(splitinfo[0]);

	$("#txtEditShopName").val(splitinfo[1]);
	$("#txtEditTel").val(splitinfo[2]);
	$("#btnEditShop").click(function () {
		if ($("#txtEditShopName").val() == "" || $("#txtEditShopName").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>门店店铺名称不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		} else if ($("#txtEditTel").val() == "" || $("#txtEditTel").val() == undefined) {
			$.msgbox("<span style='font-size:13px;line-height:30px;'>店铺联系方式不能为空。</span>", { type: "error", buttons: [{ type: "submit", value: "确定" }] });
		}
		else {
			var updateinfo = $("#hfShopId").val() + "," + $("#txtEditShopName").val() + "," + $("#txtEditTel").val();
			$.ajax(
			{
				type: "post",
				data: "EditShopInfo=" + encodeURIComponent(updateinfo),
				url: "EnshineUnionHandler.ashx",
				datatype: "html",
				//async: false,
				success: function (returnData, textstatus, xmlhttprequest) {
					$.msgbox("<span style='font-size:12px;line-height:30px;'>[" + $("#txtEditShopName").val() + "]信息修改成功。</span>", {
						type: "info", buttons: [
					{ type: "submit", value: "确定" }
						]
					}, function (result) {
						ShowShopHtml(returnData); $("#btnClose").click();
					});
				},
				error: function () {
					alert('修改：' + $("#txtEditShopName").val() + '门店店铺信息失败。');
				}
			});
		}
	});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowShopInfo");
	})
	$("#btnResetShop").click(function () {
		$("#txtEditShopName,#txtEditTel").val('');

		//$("#hfShopId").val('');
	});
}
//删除新闻分类信息
function DeleteShopInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
		{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
			if (result != "取消")
				$.ajax(
	{
		type: "post",
		data: "DeleteShopInfo=" + delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：[' + delinfo.split(',')[1] + ']门店店铺信息成功。'); ShowShopHtml(returnData);
		},
		error: function () {
			alert('删除：[' + delinfo.split(',')[1] + ']门店店铺信息失败。');
		}
	});
		});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), "all", "ShowShopInfo");
	})
}
/**************厂家核对报表查询************************/
function SearchSaleCheckInfo() {

	$("#btnSearch").click(function () {
		var searchwhere = $("#drpShopSet").val() + "," + $("#txtTimeSelect").val();
		ShowSaleCheckInfo(searchwhere);
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#drpShopSet").val('县域门店'); $("#txtTimeSelect").val("");
	});
 
}
//厂家核对报表信息一览
function ShowSaleCheckInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,";
	} else { searchwhere = searchinfo; }
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowSaleCheckInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowSaleCheckHtml(returnData);
		},
		error: function (errorinfo) {
			alert("厂家核对报表数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowSaleCheckInfo");
	})

}

function ShowSaleCheckHtml(returnData) {
	$("#divShowSaleCheckReport").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowSaleCheckReport").html("<tr class='error'><td colspan='12'>暂无订单，无法生成厂家核对报表。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowSaleCheckReport").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

/**************商品发货表查询************************/
function SearchGoodsShipInfo() {

	$("#btnSearch").click(function () {
		var searchwhere = $("#txtOrderNo").val() + "," + $("#txtGoodsName").val() + "," +
				$("#txtBuyName").val() + "," + $("#txtCompany").val() + "," +
					$("#txtGuDong").val() + "," + $("#txtZhanZhang").val() + ","
					+ $("#txtTimeSelect").val() + "," + $("#drpShopSet").val()+","+$("#drpGoodShipStatus").val();
		ShowGoodsShipInfo(searchwhere);
	});
	//清除查询条件
	$("#btnReset").click(function () {
		$("#txtOrderNo,#txtGoodsName,#txtBuyName,#txtCompany").val("");  
		$("#txtGuDong,#txtZhanZhang,#txtTimeSelect").val("");
		$("#drpShopSet").val('县域门店');  		$("#drpGoodShipStatus").val('发货状态');  

	});

}
//商品发货表信息一览
function ShowGoodsShipInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ",,,,,,,,,";
	} else { searchwhere = searchinfo; }
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsShipInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGoodsShipHtml(returnData);
		},
		error: function (errorinfo) {
			alert("商品发货表数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowGoodsShipInfo");
	})
$("#btnGoodShipOk").click(function () {
		OkGoodShipListInfo();
	});
}

function ShowGoodsShipHtml(returnData) {
	$("#divShowGoodsShipManager").html('');
	if (returnData == "NoData" || returnData == "") {
		var noinfo="<tr>  <th ><input type='checkbox' name='option1' id='chkAll' class='uniform' onclick='selectAll();' value='全选' /></th>";
		noinfo += "<th>订单状态</th><th>发货单号</th><th>上级股东</th><th>上级站长</th>	<th>订单编号</th><th>订单金额</th>			";
		noinfo+="<th>买家姓名</th><th>电话</th><th>商品名称</th><th>商品编码</th><th>购买数量</th>	<th>账户余额</th><th>区市</th>	<th>详细地址</th>	";
		noinfo+="<th>厂家名称</th><th>商品价格</th><th>成本价格</th><th>商品规格</th>	<th>支付时间</th><th>购买者角色</th>	</tr>";
		$("#divShowGoodsShipManager").html(noinfo+"<tr class='error'><td colspan='20'>暂无订单，无法生成商品发货表。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGoodsShipManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function OkGoodShipListInfo() {
	// 查询选择条件
	if ($('input[type="checkbox"][name="option1"]:checked').each(function (i) {
	}).length == 0) {
		$.msgbox("<span style='font-size:14px;line-height:30px;'>请选择订单后进行发货。</span>",
		{ type: "error", buttons: [{ type: 'submit', value: '确定' }] });
		return false;
	}
	else {
		//拼接选择的订单号
		var orderno = "";
		//选择后的订单ID
		var updateSoID = "";
		var num = $('input[type="checkbox"][name="option1"]:checked');
		if (num[0].value == "全选") {
			//alert("全选保存的值:" + $("#hfSeleckSo").val());
			updateSoID = $("#hfSeleckSo").val();
		}
		else {
			for (var i = 0; i < num.length; i++) {
				orderno += num[i].value + ";";
			}
			$("#hfCheckSo").val(orderno);
			// alert("N单选加起来保存的值：" + $("#hfCheckSo").val());
			updateSoID = $("#hfCheckSo").val();

		}
		$.ajax(
{
	type: "post",
	data: "OkGoodShipInfo=" + updateSoID,
	url: "EnshineUnionHandler.ashx",
	datatype: "html",
	//async: false,
	success: function (returnData, textstatus, xmlhttprequest) {
		alert('确认订单商品ID:' + updateSoID + '等信息发货成功。'); ShowGoodsShipHtml(returnData);
	},
	error: function () {
		alert('确认订单商品ID:' + updateSoID + '等信息失败。');
	}
});
		//清除转向页面
		$("#txtGoPage").val("");
	}
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), ",,,,,,,,,","ShowGoodsShipInfo");


	})
}

 
//用户消费地区分析 
function ShowUserAreaListInfo() {
 
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserAreaListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowUserAreaManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowUserAreaManager").html("<tr class='error'><td colspan='4'>暂无用户消费地区统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowUserAreaManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用户消费地区统计信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowUserAreaListInfo");

	})

}

//用户ii.	年龄、性别等信息分析
function ShowUserAgeSexListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserAgeSexListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowUserAgeSexManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowUserAgeSexManager").html("<tr class='error'><td colspan='4'>暂无用户年龄、性别统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowUserAgeSexManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用户年龄、性别统计信息数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowUserAgeSexListInfo");

	})

}

//				商品i.	销售排行榜
function ShowGoodSalesTotalListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodSalesTotalListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodSalesTotalManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodSalesTotalManager").html("<tr class='error'><td colspan='4'>暂无商品销售排行榜统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodSalesTotalManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("商品销售排行榜数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodSalesTotalListInfo");

	})

}

//地区消费排行榜
function ShowGoodAreaSalesTotalListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodAreaSalesTotalListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodAreaSalesTotalManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodAreaSalesTotalManager").html("<tr class='error'><td colspan='4'>暂无用户地区消费排行榜统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodAreaSalesTotalManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用户地区消费排行榜数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodAreaSalesTotalListInfo");

	})

}

//商品分类排行榜
function ShowGoodSortSalesTotalListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodSortSalesListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodSortSalesTotalManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodSortSalesTotalManager").html("<tr class='error'><td colspan='3'>暂无商品分类排行榜榜统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodSortSalesTotalManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("商品分类排行榜数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodSortSalesListInfo");

	})

}

//商品销售数据检索	
function ShowGoodSalesSearchTotalListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodSalesSearchListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodSalesSearchTotalManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodSalesSearchTotalManager").html("<tr class='error'><td colspan='5'>暂无商品销售数据检索统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodSalesSearchTotalManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用商品销售数据检索数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodSalesTotalListInfo");

	})

}

//i.用户订单分类分析
function ShowUserOrderTypeFenXiListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserOrderTypeFenXiListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShoUserOrderTypeFenXiManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShoUserOrderTypeFenXiManager").html("<tr class='error'><td colspan='5'>暂无用户订单分类分析检索统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShoUserOrderTypeFenXiManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用户订单分类分析数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowUserOrderTypeFenXiListInfo");

	})

}
//ii.	用户消费分析
function ShowUserOrderSalesFenXiListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowUserOrderSalesFenXiListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowUserOrderSalesFenXiManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowUserOrderSalesFenXiManager").html("<tr class='error'><td colspan='5'>暂无用户消费分析检索统计信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowUserOrderSalesFenXiManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("用户消费分析数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowUserOrderSalesFenXiListInfo");

	})

}

//ii.	意见收集整理
function ShowMessageViewListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMessageViewListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowMessageViewManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowMessageViewManager").html("<tr class='error'><td colspan='5'>暂无意见收集整理信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowMessageViewManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("意见收集整理数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowMessageViewListInfo");

	})

}

//分销层级分析
function ShowMemberJiBieFenXiListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowMemberJiBieFenXiListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowFenXiaoFenXiManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowFenXiaoFenXiManager").html("<tr class='error'><td colspan='5'>暂无分销层级分析信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowFenXiaoFenXiManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("分销层级分析数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowMemberJiBieFenXiListInfo");

	})

}

//商品库龄分析
function ShowGoodsStockAgeFenXiListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsStockAgeFenXiListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodsStockAgeManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodsStockAgeManager").html("<tr class='error'><td colspan='7'>暂无商品库龄分析信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodsStockAgeManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("商品库龄分析数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodsStockAgeFenXiListInfo");

	})

}
//发货商品管理
function ShowGoodsDeliveryListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsDeliveryListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodsDeliveryManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodsDeliveryManager").html("<tr class='error'><td colspan='9'>暂无发货商品管理信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodsDeliveryManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("发货商品管理数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodsDeliveryListInfo");

	})

}
//剩余商品库存
function ShowGoodsSyStockListInfo() {

	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsSyStockListInfo=All&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			$("#divShowGoodsSyStockManager").html('');
			if (returnData == "NoData" || returnData == "") {
				$("#divShowGoodsSyStockManager").html("<tr class='error'><td colspan='6'>暂无剩余商品库存信息。</td></tr>");
				$("#showPage").css('display', 'none');
			}
			else {
				$("#showPage").css('display', 'block');
				$("#divShowGoodsSyStockManager").html(returnData.split('</tr>_')[0]);
				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);
				$("#ItemCount").text(page[1]);
				$("#Index").text(page[2]);
				$("#PageCount").text(page[3]);
			}
		},
		error: function (errorinfo) {
			alert("剩余商品库存数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {

		PagingActionClick($(this), "All", "ShowGoodsSyStockListInfo");

	})
}

 /**************入库商品管理***************/
function ShowGoodsInWareHouseHtml(returnData) {
	$("#divShowGoodsInWhManager").html('');
	if (returnData == "NoData" || returnData == "") {
		$("#divShowGoodsInWhManager").html("<tr class='error'><td colspan='10'>暂无入库商品管理信息。</td></tr>");
		$("#showPage").css('display', 'none');
	}
	else {
		$("#showPage").css('display', 'block');
		$("#divShowGoodsInWhManager").html(returnData.split('</tr>_')[0]);
		var page = returnData.split('</tr>_')[1].split(',');
		$("#SumCount").text(page[0]);
		$("#ItemCount").text(page[1]);
		$("#Index").text(page[2]);
		$("#PageCount").text(page[3]);
	}
}

function SearchGoodsInWareHouseInfo() {
	$("#btnSearch").click(function () {
		var searchwhere =  $("#txtGoodsInfo").val() + ";" + $("#drpGoodsSort").val() + ";" + $("#txtExpireDate").val() + ";" + $("#txtGoodSku").val();
		ShowGoodsInWareHouseInfo(searchwhere);
	});
	//清除查询条件
	$("#btnResrtSearch").click(function () {
		$("#txtGoodsInfo").val(""); $("#drpGoodsSort").val('商品分类');  $("#txtExpireDate,#txtGoodSku").val("");
	});

}

function ShowGoodsInWareHouseInfo(searchinfo) {
	var searchwhere;
	if (searchinfo == "" || searchinfo == undefined) {
		searchwhere = ";;;;";
	} else { searchwhere = searchinfo; }
    //alert(searchwhere);
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: "ShowGoodsInWareHouseInfo=" + encodeURIComponent(searchwhere) + "&currPage=1",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			ShowGoodsInWareHouseHtml(returnData);
		},
		error: function (errorinfo) {
			alert("入库商品管理数据请求错误。");
		}
	});
	//清除转向页面
	$("#txtGoPage").val("");
	//});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this), searchwhere, "ShowGoodsInWareHouseInfo");
	})
}
function DeleteGoodsInWareHouseInfo(delinfo) {
	$.msgbox("<span style='font-size:13px;line-height:30px;'>是否要删除此信息？</span>",
{ type: "info", buttons: [{ type: "submit", value: "确定" }, { type: "submit", value: "取消" }] }, function (result) {
	if (result != "取消")
		$.ajax(
	{
		type: "post",
		data: "DeleteGoodsInWareHouseInfo=" +delinfo.split(',')[0],
		url: "EnshineUnionHandler.ashx",
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			alert('删除：' + delinfo.split(',')[1] + '入库商品信息成功。'); ShowGoodsInWareHouseHtml(returnData)
		},
		error: function () {
			alert('删除：' + delinfo.split(',')[1] + '入库商品信息失败。');
		}
	});
});
	//分页操作动作
	$(".pages").click(function () {
		PagingActionClick($(this),  ";;;;", "ShowGoodsInWareHouseInfo");

	})
}

//详细明细分页及明细页面中检索分页
//pageActionID,分页ID
//serachInfo 查询条件
//searchType 查询类型 so asn。。
function PagingActionClick(pageActionID, serachInfo, searchType) {
	//总页数大于1的情况下上下首末页可用
	if (parseFloat($("#PageCount").html()) > 1) {
		//取得控件类型是ID还是class
		var type = pageActionID.attr("id");
		//取得当前是多少页
		var thisindex = $("#Index").text();
		switch (type) {
			case 'first':
				{
					$("#txtGoPage").val("");
					pageindex = 1;
					SearchPageIndex(1, serachInfo, searchType);
					return;
				}
			case 'prev':
				{
					$("#txtGoPage").val("");
					pageindex = parseInt(thisindex) - 1;
					if (pageindex < 1) return;
					SearchPageIndex(pageindex, serachInfo, searchType);
					return;
				}
			case 'next':
				{
					$("#txtGoPage").val("");
					pageindex = parseInt(thisindex) + 1;
					if (pageindex > parseInt($("#PageCount").html())) return;
					else
						SearchPageIndex(pageindex, serachInfo, searchType);
					return;
				}
			case 'last':
				{
					var max = parseInt($("#PageCount").html());
					$("#txtGoPage").val("");
					pageindex = max;
					SearchPageIndex(max, serachInfo, searchType);
					return;
				}
			case 'go':
				{
					var _go = $("#txtGoPage").val();
					if (_go == ""||_go==undefined) {
					    $.msgbox("<span style='font-size:14px;line-height:30px;'>请输入要跳转的页数</span>",
                         { type: "error", buttons: [{ type: 'submit', value: '确定' }] });
					   pageindex = 1;
					    return false;

					}
					else {
					    pageindex = _go;
					}
					SearchPageIndex(_go, serachInfo, searchType);
					return;
				}
		}
	}
}


var searchpageindex;
//index,页面索引例如1,2,3
//searchwhere 查询条件
//wheretype 查询类型 so asn。。
function SearchPageIndex(index, searchwhere, wheretype) {
	$.ajax({
		type: "post",
		url: "EnshineUnionHandler.ashx",
		data: wheretype + "=" + encodeURIComponent(searchwhere) + "&currPage=" + index,
		datatype: "html",
		//async: false,
		success: function (returnData, textstatus, xmlhttprequest) {
			if (returnData.split('_')[0] != "") {
				$("#showPage").css('display', 'block');
				$("#divShowUserInfoManager").html(returnData.split('</tr>_')[0]);
				$("#divShowNoticesManager").html(returnData.split('</tr>_')[0]);

				$("#divShowNewsSortManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodsManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodsSortManager").html(returnData.split('</tr>_')[0]);

				$("#divShowOrderListManager").html(returnData.split('</tr>_')[0]);

				$("#divShowPlantDoctorSortManager").html(returnData.split('</tr>_')[0]);
				$("#divShowPlantDoctorManager").html(returnData.split('</tr>_')[0]);
				$("#divShowHuoDongManager").html(returnData.split('</tr>_')[0]);
				$("#divShowAdManager").html(returnData.split('</tr>_')[0]);
				$("#divShowJoinHuoDongManager").html(returnData.split('</tr>_')[0]);
				$("#divShowUserRechargeManager").html(returnData.split('</tr>_')[0]);
				$("#divShowRoleManager").html(returnData.split('</tr>_')[0]);


				$("#divShowMyPayMoneyManager").html(returnData.split('</tr>_')[0]);
				$("#divShowMyPointManager").html(returnData.split('</tr>_')[0]);

				$("#divShowMemberManager").html(returnData.split('</tr>_')[0]);
				$("#divShowFinanceReport").html(returnData.split('</tr>_')[0]);
				$("#divShowFenXiaoReport").html(returnData.split('</tr>_')[0]);
				$("#divShowMemberReport").html(returnData.split('</tr>_')[0]);
				$("#divShowExtractListManager").html(returnData.split('</tr>_')[0]);
				$("#divShowShopManager").html(returnData.split('</tr>_')[0]);

				$("#divShowSaleCheckReport").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodsShipManager").html(returnData.split('</tr>_')[0]);

					//20160723 add
				$("#divShowUserAreaManager").html(returnData.split('</tr>_')[0]);
				$("#divShowUserAgeSexManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodSalesTotalManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodAreaSalesTotalManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodSortSalesTotalManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodSalesSearchTotalManager").html(returnData.split('</tr>_')[0]);
				$("#divShoUserOrderTypeFenXiManager").html(returnData.split('</tr>_')[0]);
				$("#divShowUserOrderSalesFenXiManager").html(returnData.split('</tr>_')[0]);
				$("#divShowMessageViewManager").html(returnData.split('</tr>_')[0]);
				$("#divShowFenXiaoFenXiManager").html(returnData.split('</tr>_')[0]);

                //库存管理； 
                $("#divShowGoodsStockAgeManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodsDeliveryManager").html(returnData.split('</tr>_')[0]);
				$("#divShowGoodsSyStockManager").html(returnData.split('</tr>_')[0]);


				var page = returnData.split('</tr>_')[1].split(',');
				$("#SumCount").text(page[0]);//总条数
				$("#ItemCount").text(page[1]);//每页显示条数
				$("#Index").text(page[2]);//当前页数
				$("#PageCount").text(page[3]);//总页数
			}
			else {
				$("#showPage").css('display', 'none');
				$("#divShowUserInfoManager").html("<tr class='error'><td colspan='11'>暂无用户信息。</td></tr>");

				$("#divShowNoticesManager").html("<tr class='error'><td colspan='8'>暂无新闻/公告信息，请添加。</td></tr>");

				$("#divShowNewsSortManager").html("<tr class='error'><td colspan='3'>暂无新闻分类信息，请添加。</td></tr>");

				$("#divShowGoodsManager").html("<tr class='error'><td colspan='14'>暂无商品信息，请添加。</td></tr>");
				$("#divShowGoodsSortManager").html("<tr class='error'><td colspan='4'>暂无商品分类信息，请添加。</td></tr>");

				$("#divShowOrderListManager").html("<tr class='error'><td colspan='13'>暂无订单列表信息。</td></tr>");

				$("#divShowPlantDoctorSortManager").html("<tr class='error'><td colspan='5'>暂无植保分类信息，请添加。</td></tr>");
				$("#divShowPlantDoctorManager").html("<tr class='error'><td colspan='8'>暂无植保医院信息，请添加。</td></tr>");
				$("#divShowHuoDongManager").html("<tr class='error'><td colspan='10'>暂无发布活动信息，请添加。</td></tr>");
				$("#divShowAdManager").html("<tr class='error'><td colspan='8'>暂无广告信息，请添加。</td></tr>");
				$("#divShowJoinHuoDongManager").html("<tr class='error'><td colspan='8'>暂无参与活动信息，请添加。</td></tr>");
				$("#divShowUserRechargeManager").html("<tr class='error'><td colspan='8'>暂无用户充值信息，请进行充值。</td></tr>");
				$("#divShowRoleManager").html("<tr class='error'><td colspan='5'>暂无角色权限信息，请添加。</td></tr>");


				$("#divShowMyPayMoneyManager").html("<tr class='error'><td colspan='6'>暂无收入明细信息。</td></tr>");
				$("#divShowMyPointManager").html("<tr class='error'><td colspan='4'>暂无积分明细信息。</td></tr>");

				$("#divShowMemberManager").html("<tr class='error'><td colspan='4'>暂无会员列表信息，请添加。</td></tr>");
				$("#divShowFinanceReport").html("<tr class='error'><td colspan='6'>暂无月销售金额报表统计信息。</td></tr>");
				$("#divShowFenXiaoReport").html("<tr class='error'><td colspan='3'>暂无当月分销提成金额报表信息。</td></tr>");
				$("#divShowMemberReport").html("<tr class='error'><td colspan='3'>暂无当月会员提现金额报表信息。</td></tr>");
				$("#divShowExtractListManager").html("<tr class='error'><td colspan='7'>暂无会员提现管理信息。</td></tr>");
				$("#divShowShopManager").html("<tr class='error'><td colspan='4'>暂无门店店铺信息，请添加。</td></tr>");
				$("#divShowSaleCheckReport").html("<tr class='error'><td colspan='12'>暂无订单，无法生成厂家核对报表。</td></tr>");
				$("#divShowGoodsShipManager").html("<tr class='error'><td colspan='20'>暂无订单，无法生成商品发货表。</td></tr>");

				//20160723add
				$("#divShowUserAreaManager").html("<tr class='error'><td colspan='4'>暂无用户消费地区统计信息。</td></tr>");
				$("#divShowUserAgeSexManager").html("<tr class='error'><td colspan='4'>暂无用户年龄、性别统计信息。</td></tr>");
				$("#divShowGoodSalesTotalManager").html("<tr class='error'><td colspan='4'>暂无商品销售排行榜统计信息。</td></tr>");
				$("#divShowGoodAreaSalesTotalManager").html("<tr class='error'><td colspan='4'>暂无用户地区消费排行榜统计信息。</td></tr>");
				$("#divShowGoodSortSalesTotalManager").html("<tr class='error'><td colspan='3'>暂无商品分类排行榜榜统计信息。</td></tr>");
				$("#divShowGoodSalesSearchTotalManager").html("<tr class='error'><td colspan='5'>暂无商品销售数据检索统计信息。</td></tr>");
				$("#divShoUserOrderTypeFenXiManager").html("<tr class='error'><td colspan='5'>暂无用户订单分类分析检索统计信息。</td></tr>");
				$("#divShowUserOrderSalesFenXiManager").html("<tr class='error'><td colspan='5'>暂无用户消费分析检索统计信息。</td></tr>");
				$("#divShowMessageViewManager").html("<tr class='error'><td colspan='5'>暂无意见收集整理信息。</td></tr>");
				$("#divShowFenXiaoFenXiManager").html("<tr class='error'><td colspan='5'>暂无分销层级分析信息。</td></tr>");

				$("#divShowGoodsStockAgeManager").html("<tr class='error'><td colspan='7'>暂无商品库龄分析信息。</td></tr>");
				$("#divShowGoodsDeliveryManager").html("<tr class='error'><td colspan='9'>暂无发货商品管理信息。</td></tr>");
				$("#divShowGoodsSyStockManager").html("<tr class='error'><td colspan='6'>暂无剩余商品库存信息。</td></tr>");

			}
		},
		error: function () {
			alert("分页数据检索信息错误");
		}
	});

}

//退出登陆并清除cookies
function LoginOut(Name, Value) {
	var Now = new Date();
	Now.setTime(Now.getTime() - 10000);
	document.cookie = Name + '=' + escape(Value) + '; path=/; expires=' + Now.toGMTString() + ';';
	$.msgbox("<span style='font-size:14px;line-height:60px;'>退出登陆成功。</span>",
	{ type: "info", buttons: [{ type: "submit", value: "确定" }] }, function (result) { window.location.href = 'Login.aspx'; });
}

//获取cookies值
function getCookie(cookie_name) {
	var allcookies = document.cookie;
	var cookie_pos = allcookies.indexOf(cookie_name);   //索引的长度
	// 如果找到了索引，就代表cookie存在，
	// 反之，就说明不存在。
	if (cookie_pos != -1) {
		// 把cookie_pos放在值的开始，只要给值加1即可。
		cookie_pos += cookie_name.length + 1;
		var cookie_end = allcookies.indexOf(";", cookie_pos);
		if (cookie_end == -1) {
			cookie_end = allcookies.length;
		}
		var value = decodeURI(allcookies.substring(cookie_pos, cookie_end)); //得到cookie值
	}
	return value;
}
//获取页面参数
function GetQueryString(name) {
	var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
	var r = window.location.search.substr(1).match(reg);
	if (r != null) return (r[2]); return null;
}