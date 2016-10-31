<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="EnshineUnionManager.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<script src="assets/js/jquery-1.8.2.min.js"></script>
	<link href="assets/css/sharelink.css" rel="stylesheet" />
		<script type="text/javascript">
	//		var data = '{"return_code":0,"return_message":"success","data":{"data":[{"id":"1","question":"公主令牌在哪交？"},{"id":"2","question":"公主护使有什么用？"},{"id":"3","question":"角斗场在哪？"},{"id":"4","question":"北部断层在哪？"},{"id":"5","question":"欢乐令有什么用？"},{"id":"6","question":"令牌积分有什么用？"},{"id":"7","question":"南部断层在哪？"},{"id":"8","question":"大妖魔令牌交给谁？"},{"id":"9","question":"神工坊在哪？"},{"id":"10","question":"警戒妖珠有什么用？"}]}}';
			//var data = '{"result":"true","sumpoint":"1000","data":{"list":[{"Id":"2","userid":"26","getpoint":"1000","getpointreason":"注册会员获得1000积分","getpointtime":"2016/7/21 13:53:28"}],"maxsize":0,"uptime":"2016-07-21 04:30:43"}}';
			var data='{"result":"true","sumpoint":"0",data:{"list":[{"Id":"2","userid":"1","getpoint":"22","getpointreason":"dfdf","getpointtime":"2016/7/21 14:58:53"},{"Id":"3","userid":"1","getpoint":"3","getpointreason":"dfdf","getpointtime":"2016/7/21 15:00:45"}],"maxsize":0,"uptime":"2016-07-21 07:27:51"}}';
			function ShowData() {

				var obj = eval("("+data+")");		
				alert("result:" + obj["result"]);
				alert("sumpoint:" + obj["sumpoint"]);
				alert("data:" + obj["data"]);
				alert("第一个问题id：" + obj["data"][0]["id"]);
				alert("第一个内容id：" + obj["data"][0]["userid"]);
			}

		</script>
</head>
<body>
    <form id="form1" runat="server">
					<input type="button" value="ssd" onclick="ShowData();"		/>
					 												<input name="txtName" disabled="disabled" class="text-info span5" id="txtName" type="text" value="http://121.42.179.208/vcode=2233434">

   <div class="kePublic">
 
<div class="gb_resLay">
  <div class="gb_res_t"><span>分享到</span><i></i></div>
  <div class="bdsharebuttonbox">
    <ul class="gb_resItms">
      <li> <a title="分享到微信" href="#" class="bds_weixin" data-cmd="weixin"></a>微信好友 </li>
      <li> <a title="分享到QQ好友" href="#" class="bds_sqq" data-cmd="sqq"></a>QQ好友 </li>
      <li> <a title="分享到QQ空间" href="#" class="bds_qzone" data-cmd="qzone"></a>QQ空间 </li>
      <li> <a title="分享到腾讯微博" href="#" class="bds_tqq" data-cmd="tqq"></a>腾讯微博 </li>
      <li> <a title="分享到新浪微博" href="#" class="bds_tsina" data-cmd="tsina"></a>新浪微博 </li>
      <li> <a title="分享到人人网" href="#" class="bds_renren" data-cmd="renren"></a>人人网 </li>
    </ul>
  </div>
</div>
				<script>
					var vcode = "" + $("#txtName").val() + "";
					window._bd_share_config =
	{
		"common": {
			"bdSnsKey": {}, "bdText": vcode,
			"bdMini": "1",
			"bdMiniList": false,
			"bdPic": "",
			"bdStyle": "1",
			"bdSize": "16"
		}, "share": {},
		"image": {
			"viewList": ["weixin", "sqq", "qzone", "tqq", "tsina", "renren"],
			"viewText": "分享到：", "viewSize": "16"
		}, "selectShare": {
			"bdContainerClass": null,
			"bdSelectMiniList": ["weixin", "sqq", "qzone", "tqq", "tsina", "renren"]
		}
	};
					with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];


				</script>
		 
										</div>
    </form>
</body>
</html>
