<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="EnshineUnionManager.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<style type="text/css">
		* {
	margin: 0px; padding: 0px;
}
 
.cf {
	display: block; -ms-zoom: 1;
}
.cf::after {
	height: 0px; clear: both; display: block; visibility: hidden; content: " ";
}
.cf {
	
}
.fl {
	float: left; display: inline;
}
.post_title {
	width: 100%; text-align: center; margin-top: 100px;
}
.post_img {
	margin: 0px auto; display: block;
}
#sns_share {
	margin: 100px auto; width: 210px; height: 30px; position: relative;
}
.sns_share_to {
	color: rgb(153, 153, 153); line-height: 24px;
}
.share_icon {
	transition:0.2s; width: 24px; height: 24px; overflow: hidden; margin-right: 5px; display: block; opacity: 0.8; background-image: url("share.png"); background-repeat: no-repeat; -webkit-transition: 0.2s;
}
.share_icon:hover {
	opacity: 1;
}
.share_icon em {
	width: 60px; height: 30px; line-height: 30px; margin-left: 30px; float: left; display: none;
}
.share_weixin {
	background-position: 0px 0px;
}
.share_tqqfriend {
	background-position: 0px 0px;
}
.share_tsina {
	background-position: -34px 0px;
}
.share_tqq {
	background-position: -68px 0px;
}
.share_renren {
	background-position: -102px 0px;
}
.share_tqzone {
	background-position: -136px 0px;
}
.wemcn {
	left: -80px; top: -125px; display: none; position: absolute; z-index: 4;
}
.ewmDiv {
	background: rgb(255, 255, 255); padding: 10px; border-radius: 5px; border: 2px solid rgb(220, 220, 220); border-image: none; width: 255px; height: 89px; z-index: 7; -moz-border-radius: 5px;
}
#ewmimg {
	width: 85px; height: 85px; float: left;
}
.rwmtext {
	width: 145px; text-indent: 8px; padding-top: 7px; float: left;
}
.rwmtext p {
	width: 145px; color: rgb(99, 99, 99); line-height: 25px; text-indent: 12px; font-size: 12px;
}
#ewmkg {
	background-position: -170px 0px; top: 11px; width: 13px; height: 13px; right: 12px; display: block; position: absolute;
}
#ewmkg:hover {
	background-position: -187px 0px;
}
.ewmsj {
	background-position: -200px 0px; width: 18px; height: 13px; margin-top: -2px; margin-left: 140px; position: absolute;
}

	</style>
					<link href="assets/css/sharelink.css" rel="stylesheet" />						 
</head>
<body>
    <form id="form1" runat="server">
   
 												<input name="txtName" disabled="disabled" class="text-info span5" id="txtName" type="text" value="http://121.42.179.208/vcode=2233434">
								<div class="kePublic">
 
<div class="gb_resLay">
  <div class="gb_res_t"><span>分享到</span><i></i></div>
  <div class="bdsharebuttonbox">
    <ul class="gb_resItms">
      <li> <a title="分享到微信" href="#" class="share_weixin share_icon fl bds_weixin" data-cmd="weixin"  ></a>微信好友 </li>
      <li> <a title="分享到QQ好友" href="#" class="share_tqqfriend share_icon f1 bds_sqq " data-cmd="sqq"  ></a>QQ好友 </li>
      <li> <a title="分享到QQ空间" href="#" class="share_tqzone share_icon fl bds_qzone" data-cmd="qzone" ></a>QQ空间 </li>
      <li> <a title="分享到腾讯微博" href="#" class="share_tqq share_icon fl bds_tqq" data-cmd="tqq" ></a>腾讯微博 </li>
      <li> <a title="分享到新浪微博"  class="share_tsina share_icon fl bds_tsina"  ></a>新浪微博 </li>
      <li> <a title="分享到人人网" href="#" class="share_renren share_icon fl bds_renren" data-cmd="renren"  ></a>人人网 </li>
    </ul>
  </div>
</div>
 										
										</div>
		 																					 <div class="wemcn" id="wemcn">
             <div id="ewm" class="ewmDiv clearfix">
                <div class="rwmtext">
                    <p>扫一扫，用手机观看！</p>
                    <p>用微信扫描还可以</p>
                    <p>分享至好友和朋友圈</p> 
                </div>
             </div> 
            <a class="share_icon" href="javascript:void(0)" id="ewmkg"></a>
            <i class="ewmsj share_icon"></i>
        </div>
																<script src="assets/js/jquery-1.8.2.min.js"></script>
	<script src="assets/js/sharelink.js"></script>
						<script>
			$(function () {
				var shareTitle = $("#txtName").val() + "轩枫阁";
				var sinaTitle = '分享文章 「' + shareTitle + '」 （分享自@轩枫Y_me）',
        renrenTitle = '分享文章 「' + shareTitle + '」（分享自@农航亮(356948827)）',
        tqqTitle = '分享文章 「' + shareTitle + '」（分享自@轩枫阁）',
        tqzoneTitle = '分享文章 「' + shareTitle + '」-轩枫阁（分享自@♪紫轩枫、）';
			//	var picShare = encodeURIComponent($(".post_title").data("thumb"));

				$('body').xuanfengSnsShare({
					tsina: {
						url: encodeURIComponent(window.location.href),
						title: sinaTitle,
					//	pic: picShare
					},
					renren: {
						url: encodeURIComponent(window.location.href),
						title: renrenTitle,
				//		pic: picShare
					},
					tqq: {
						url: encodeURIComponent(window.location.href),
						title: tqqTitle,
					//	pic: picShare
					},
					tqzone: {
						url: encodeURIComponent(window.location.href),
						title: tqzoneTitle,
					//	pic: picShare
					}
				});

				// 微信分享	
				$(".share_weixin").click(function () {
					$("#ewmimg").remove();
					var thisURL = window.location.href,
						strwrite = "<img id='ewmimg' class='ewmimg' src='https://chart.googleapis.com/chart?cht=qr&chs=150x150&choe=UTF-8&chld=L|4&chl=" + thisURL + "' width='85' height='85' alt='轩枫阁文章 二维码分享' />";
					$("#ewm").prepend(strwrite);
					$("#wemcn").show();
				});
				$("#ewmkg").click(function () {
					$("#wemcn").hide();
				});

			});
		</script>

    </form>

</body>
</html>
