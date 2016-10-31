using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EnshineUnionServer
{
	/// <summary>
	/// WebService 的摘要说明
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
	[System.Web.Script.Services.ScriptService]
	public class WebService : System.Web.Services.WebService
	{

		#region 用户相关
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="tel">帐号就是手机号码</param>
		/// <param name="pass">密码</param>
		[WebMethod(Description = "账号登录（用户名和密码(tel/pass)）")]
        public void userlogin(string tel, string password)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "select count(1) from UserInfo where tel='" + tel + "' and pass='" + password + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count <= 0)
				{
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"登录失败，用户名" + tel + "不存在\"}");
					return;
				}
				else
				{
					//20160304 del
					//sql = "select id,nickname,name,Address,invitedcode,usertype,point,housemoney,fenhongmoney,shopset from UserInfo  where nickname='" + user_name + "' and pass='" + password + "'";
					string selectSql = "select a.id,a.nickname,a.tel,a.name,a.sex,a.email, a.areacity,a.Address,a.invitedcode,a.usertype,b.membername,";
					selectSql += "a.point,a.housemoney,a.fenhongmoney,a.shopset,a.juese,a.gudongjibie";
                    selectSql += " from UserInfo a INNER JOIN memberset b on a.usertype=b.memberid where a.tel='" + tel + "' and a.pass='" + password + "'";

					DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
					if (dtGetUserInfo.Rows.Count > 0)
					{
                        string[] arr = { "id", "nickname", "tel", "name", "sex", "email", "areacity", "Address", "invitedcode", "usertype", "membername", "point", "housemoney", "fenhongmoney", "shopset", "juese", "gudongjibie" };
						string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
						Context.Response.Write(result);

						//	Context.Response.Write("{\"result\":\"true\",\"data\":[{\"id\":\"" + dt.Rows[0]["id"].ToString() + "\",\"nickname\":\"" + dt.Rows[0]["nickname"].ToString() + "\",\"name\":\"" + dt.Rows[0]["name"].ToString().Trim() + "\",\"Address\":\"" + dt.Rows[0]["Address"].ToString() + "\",\"invitedcode\":\"" + dt.Rows[0]["invitedcode"].ToString() + "\",\"usertype\":\"" + dt.Rows[0]["usertype"].ToString() + "\"}]}");
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"登录失败，用户名或密码错误\"}");
					}
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// i.	个人信息查询
		/// </summary>
		/// <param name="tel">帐号就是手机号码</param>
		/// <param name="pass">密码</param>
		[WebMethod(Description = "个人信息获取查询（用户名ID：如1）")]
		public void GetUserDetailsInfo(int userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string selectSql = "select a.id,a.nickname,a.name,a.Address,a.invitedcode,a.usertype,b.membername,";
				selectSql += "a.point,a.housemoney,a.fenhongmoney,a.shopset,a.juese,a.gudongjibie";
				selectSql += " from UserInfo a INNER JOIN memberset b on a.usertype=b.memberid where a.id='" + userid + "'";

				DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
				if (dtGetUserInfo.Rows.Count > 0)
				{
					string[] arr = { "id", "nickname", "name", "Address", "invitedcode", "usertype", "membername", "point", "housemoney", "fenhongmoney", "shopset", "juese", "gudongjibie" };
					string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"个人信息查询获取失败，或未知错误\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 注册用户
		/// </summary>
		/// <param name="tel"></param>
		/// <param name="user_name"></param>
		/// <param name="password"></param>
		/// <param name="sex">N:女 Y:男</param>
		/// <param name="areacity"></param>
		/// <param name="address"></param>
		/// <param name="code"></param>
		/// <param name="reg_time"></param>
		[WebMethod(Description = "注册个人信息")]
		public void userregister(string tel, string user_name, string password, string sex, string areacity,
														string address, string invitedcode, string reg_time)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select nickname,tel,invitedcode from userinfo where tel='" + tel + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"注册失败，暂无账号或帐号重复\"}");
					return;
				}
				else
				{
					//20160131根据邀请码手机判断是否存在存在则 能进行注册
					string sqlUserID = "select id,tel,invitedcode from UserInfo where tel='" + invitedcode + "'";
					DataTable dtUserID = DbHelperSQL.Query(sqlUserID.ToString()).Tables[0];
					if (dtUserID.Rows.Count > 0)
					{
						//20160131 东方汇农需要用手机号做邀请码特此改为Tel 存放
                        string insersql = "insert into UserInfo(nickname,name,pass,Sex,Address,invitedcode,addtime,tel,areacity,fenxiaoid,usertype,juese,point)";
                        insersql += "values('" + tel + "','" + user_name + "','" + password + "','" + sex + "','" + address + "','" + invitedcode + "','" + reg_time + "','" + tel + "','" + areacity + "','" + int.Parse(dtUserID.Rows[0]["id"].ToString()) + "','1','会员','1000')";
						int result = DbHelperSQL.ExecuteSql(insersql);
						if (result > 0)
						{
							//2016/07/20 add
							string getUserID = "select id from UserInfo where tel='" + tel + "'";
							DataTable dtgetUserID = DbHelperSQL.Query(getUserID.ToString()).Tables[0];
							if (dtgetUserID.Rows.Count > 0)
							{
								string upOne = @"insert into pointdetails(userid
									,getpoint
									,getpointreason
									,getpointtime) values('" + dtgetUserID.Rows[0]["id"] + "','1000','注册会员获得1000积分','" + DateTime.Now + "' )";

								if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upOne)) > 0)
								{
									Context.Response.Write("{\"result\":\"true\",\"data\":\"注册会员获得1000积分成功\"}");
								}
							}
						}
						else
							Context.Response.Write("{\"result\":\"false\",\"data\":\"注册失败，未知错误\"}");
					}
					else//20160131 //没有经过邀请码注册的用户则分销ID为NULL(必须经过邀请码)
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"注册失败，未经过邀请码注册\"}");
					}
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//验证分销码是否用户存在的								
		[WebMethod(Description = "验证此邀请码的用户是否存在")]
		public void ValidateInvitedCode(string strcode)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select count(1),ISNULL(usertype, 0)usertype  from UserInfo where tel='" + strcode + "' GROUP BY  usertype";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["usertype"].ToString()) >= 2)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"验证分销码存在,会员等级为银卡以上\"}");

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"验证分销码失败，不存在此分销码或用户级别不够\"}");
					return;
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//验证输入的支付密码是否和后台一致							
		[WebMethod(Description = "验证输入的支付密码是否和后台一致")]
		public void ValidatePayPassWord(string strBuyUserid, string strPayPwd)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select count(1) from UserInfo where id='" + strBuyUserid + "' and  paypassword='" + strPayPwd + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"支付密码正确\"}");

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"支付密码错误，请重新输入\"}");
					return;
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		//20160201	add
		/// <summary>
		/// 完善个人信息	 
		/// </summary>
		/// <param name="nickname">昵称</param>
		/// <param name="name">姓名</param>
		/// <param name="email">邮箱</param>
		/// <param name="sex">性别 Y 男 N 女</param>
		/// <param name="address">地址</param>
		/// <param name="areacity">省市区 以空格隔开</param>

		/// <param name="cardimg">身份证照片</param>
		[WebMethod(Description = "完善个人信息")]
		public void PerfectUserInfo(string tel, string nickname, string name, string email,
			char sex, string address, string areacity, string cardimg, string birthday)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string upfatherpoint = "update   UserInfo set ";
				if (!string.IsNullOrEmpty(nickname))
				{
					upfatherpoint += "nickname='" + nickname + "',";
				}
				if (!string.IsNullOrEmpty(name))
				{
					upfatherpoint += "name='" + name + "',";
				}
				if (!string.IsNullOrEmpty(email))
				{
					upfatherpoint += "email='" + email + "',";
				}
				if (!string.IsNullOrEmpty(address))
				{
					upfatherpoint += "address='" + address + "',";
				}
				//if (!string.IsNullOrEmpty(favorarray))
				//{
				//	upfatherpoint += "favorarray='" + favorarray + "',";
				//}
				upfatherpoint += "sex='" + sex + "',areacity='" + areacity + "', birthday='" + birthday + "' where tel = '" + tel + "'";
				if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upfatherpoint)) > 0)
					Context.Response.Write("{\"result\":\"true\",\"data\":\"完善个人信息成功。\",\"message\":\"0\"}");
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"完善个人信息失败。\",\"message\":\"-1\"}");
								 //20160718del
					//string upfatherpoint = "update   UserInfo set nickname='" + nickname + "',name='" + name + "',email='" + email + "',sex='" + sex + "',";
					//upfatherpoint += " address = '" + address + "',areacity='" + areacity + "',cardimg = '" + cardimg + "'  where tel = '" + tel + "'";
					//if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upfatherpoint)) > 0)
					//	Context.Response.Write("{\"result\":\"true\",\"data\":\"完善个人信息成功。\"}");
					//else
					//	Context.Response.Write("{\"result\":\"false\",\"data\":\"完善个人信息失败。\"}");
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		/// <summary>
		///  根据用户手机修改密码
		/// </summary>
		/// <param name="PhoneNumber">登录用户名</param>
		/// <param name="OldPassWord">旧密码</param>
		/// <param name="NewPassWord">新密码</param>
		[WebMethod(Description = @"修改用户密码(oldpass>旧密码)/newpass>新密码")]
		public void UpdatePassWord(string PhoneNumber, string OldPassWord, string NewPassWord)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select pass from UserInfo where tel='" + PhoneNumber + "' ";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					if (dt.Rows[0]["pass"].ToString().Trim() == OldPassWord)
					{
						string upfatherpoint = "update   UserInfo set pass='" + NewPassWord + "'  where tel = '" + PhoneNumber + "'";
						if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upfatherpoint)) > 0)
							Context.Response.Write("{\"result\":\"true\",\"data\":\"修改密码成功。\",\"message\":\"0\"}");
						else
							Context.Response.Write("{\"result\":\"false\",\"data\":\"修改密码失败。\",\"message\":\"-1\"}");
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"旧密码错误，不能修改\",\"message\":\"-1\"}");
					}
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"取得用户密码错误\",\"message\":\"-1\"}");
				}

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		/// <summary>
		///  忘记密码
		/// </summary>
		/// <param name="tel">登录用户名手机号</param>

		[WebMethod(Description = @"忘记密码后通过注册时手机号找回")]
		public void ForgetPassWord(string PhoneNumber)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select pass from UserInfo where tel='" + PhoneNumber + "' ";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"密码找回成功,[" + dt.Rows[0]["pass"] + "]请牢记\",\"message\":\"0\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"手机号不存在,或者不正确\",\"message\":\"-1\"}");
				}

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		///  根据用户手机修改收货地址
		/// </summary>
		/// <param name="PhoneNumber">登录用户名</param>
		/// <param name="areacity">省市区</param>
		/// <param name="address">详细地址</param>
		[WebMethod(Description = "修改收货地址(areacity省市区 中间以空格分割)")]
		public void UpdateAddres(string PhoneNumber, string areacity, string address)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string upfatherpoint = "update   UserInfo set areacity='" + areacity + "',address='" + address + "'  where tel = '" + PhoneNumber + "'";
				if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upfatherpoint)) > 0)
					Context.Response.Write("{\"result\":\"true\",\"data\":\"修改个人收货地址成功。\",\"message\":\"0\"}");
				else
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"修改个人收货地址失败。\",\"message\":\"-1\"}");

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160203 add
		/// <summary>
		/// 获取（一级、二级、三级会员的基本信息
		/// </summary>
		/// <param name="tel">手机号</param>
		/// <param name="level">几级</param>
		[WebMethod(Description = "获取（一级、二级、三级会员的基本信息）")]
		public void GetUserMemberInfo(string tel, int level)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = string.Empty;
				switch (level)
				{
					case 1:
						sql = "SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') a INNER JOIN UserInfo b on b.fenxiaoid=a.Id";
						break;
					case 2:
						sql = "SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg    from (SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg ";
						sql += "  from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid";
						break;
					case 3:
						sql = "SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg from (SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg ";
						sql += " from (SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "')";
						sql += " a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid) a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid";
						break;
				}
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "id", "fenxiaoid", "levelid", "nickname", "name", "tel", "headimg" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取(一级、二级、三级会员的基本信息)失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160203 add
		/// <summary>
		///获取（从一级、二级、三级会员购买订单得到的返利情况）订单必须是付款之后的。
		/// </summary>
		/// <param name="tel">手机号</param>
		/// <param name="level">几级</param>
		[WebMethod(Description = "获取（从一级、二级、三级会员购买订单得到的返利情况）订单必须是付款之后的。）")]
		public void GetMemberRebateInfo(string tel, int level)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = string.Empty;
				switch (level)
				{
					case 1:
						sql = "SELECT   a.id ,a.fenxiaoid,a.levelid, a.tel,b.recmoney,b.content,b.addtime from ( SELECT a.*, b.id as levelid, b.tel from";
						sql += "(SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN userrecharge b on a.levelid=b.getrecuserid";
						break;
					case 2:
						sql = "SELECT a.id ,a.fenxiaoid,a.levelid, a.tel,b.recmoney,b.content,b.addtime from (SELECT a.id,a.fenxiaoid, b.id as levelid, b.tel   from (SELECT a.*, b.id as levelid, b.tel   ";
						sql += "   from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid )";
						sql += "a INNER JOIN userrecharge b on a.levelid=b.getrecuserid";
						break;
					case 3:
						sql = "SELECT a.id ,a.fenxiaoid,a.levelid, a.tel,b.recmoney,b.content,b.addtime";
						sql += " from (SELECT a.id,a.fenxiaoid, b.id as levelid,b.tel from (SELECT a.id,a.fenxiaoid, b.id as levelid,b.tel";
						sql += " from (SELECT a.*, b.id as levelid,b.tel from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "')";
						sql += "a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid) a ";
						sql += "INNER JOIN UserInfo b on a.levelid=b.fenxiaoid)a INNER JOIN userrecharge b on a.levelid=b.getrecuserid";
						break;
				}
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "id", "fenxiaoid", "levelid", "tel", "recmoney", "content", "addtime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取(一级、二级、三级会员的订单返利信息)失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//20160714add
		/// <summary>
		/// 获取（一级、二级、三级会员分销提成信息
		/// </summary>
		/// <param name="tel">手机号</param>
		/// <param name="level">几级</param>
		[WebMethod(Description = "获取（一级、二级、三级会员分销提成信息）")]
		public void GetUserMemberTiChengInfo(string tel, int level)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = string.Empty;
				switch (level)
				{
					case 1:
                        sql = "SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,c.orderno,d.ordertime,d.orderprice,c.moneycode,c.money,c.moneyreason from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') ";
						sql += " a INNER JOIN UserInfo b on b.fenxiaoid=a.Id inner  JOIN moenydetails c on b.Id = c.userid INNER JOIN orders d on c.orderno=d.orderno";
						break;
					case 2:
                        sql = "SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,c.orderno,d.ordertime,d.orderprice,c.moneycode,c.money,c.moneyreason    from (SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg ";
						sql += "  from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "') a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid";
						sql += " inner  JOIN moenydetails c on b.Id=c.userid INNER JOIN orders d on c.orderno=d.orderno";
						break;
					case 3:
                        sql = "SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,c.orderno,d.ordertime,d.orderprice,c.moneycode,c.money,c.moneyreason from (SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg ";
						sql += " from (SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg from (SELECT id,fenxiaoid from UserInfo where tel='" + tel + "')";
						sql += " a INNER JOIN UserInfo b on b.fenxiaoid=a.Id)a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid) a INNER JOIN UserInfo b on a.levelid=b.fenxiaoid";
						sql += " inner  JOIN moenydetails c on b.Id=c.userid INNER JOIN orders d on c.orderno=d.orderno";
						break;
				}
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "id", "fenxiaoid", "levelid", "nickname", "name", "tel", "headimg","orderno","ordertime","orderprice", "moneycode", "money", "moneyreason" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取(一级、二级、三级会员的基本信息)失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		//2016/07/13
		/// <summary>
		/// 获取当月新增会员数据 
		/// </summary>
		[WebMethod(Description = "获取当月新增会员数据（2016/07/01~2016/07/31）")]
		public void GetNowMonthNewMember()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				DateTime dt = DateTime.Now; //当前时间 
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初 
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末 
				string selectSql = "select a.id,a.nickname,a.name,a.Address,a.invitedcode,a.usertype,b.membername,";
				selectSql += "a.point,a.housemoney,a.fenhongmoney,a.shopset,a.juese,a.gudongjibie";
				selectSql += " from UserInfo a INNER JOIN memberset b on a.usertype=b.memberid where a.addtime between'" + startMonth + "' and  '" + endMonth + "'";

				DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
				if (dtGetUserInfo.Rows.Count > 0)
				{
					string[] arr = { "id", "nickname", "name", "Address", "invitedcode", "usertype", "membername", "point", "housemoney", "fenhongmoney", "shopset", "juese", "gudongjibie" };
					string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
					Context.Response.Write(result);

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取当月新增会员数据失败或者未知错误。\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//2016/07/13
		/// <summary>
		/// 获取我的消费记录查询 
		/// </summary>
		[WebMethod(Description = "获取我的消费记录查询")]
		public void GetSearchMySalesGoodsInfo(int userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");

				string selectSql = "   SELECT a.id,a.nickname,a.name,a.tel,a.headimg,b.orderno,b.ordertime,b.orderprice from UserInfo a INNER JOIN orders b on a.id = b.buyername  where a.Id= '" + userid + "'";

				DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
				if (dtGetUserInfo.Rows.Count > 0)
				{
					string[] arr = { "id", "nickname", "name", "tel", "headimg", "orderno", "ordertime", "orderprice"};
					string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取我的消费记录数据失败或者未知错误。\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//2016/07/13
		/// <summary>
		/// 分销会员总的消费数据统计报表 
		/// </summary>
		[WebMethod(Description = "分销会员总的消费数据统计报表")]
		public void GetAllMemberSalesTotalReport(string userTel,string level)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string selectSql = string.Empty;

				if (!string.IsNullOrEmpty(level))
				{
					switch (level)
					{
						case "1":
							selectSql += " SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice					";
							selectSql += " from (SELECT id, fenxiaoid from UserInfo where tel ='" + userTel + "' )  a INNER JOIN UserInfo b on b.fenxiaoid = a.Id  INNER JOIN orders d on a.id = d.buyername	";
							break;
						case "2":
							selectSql += "	SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice		 ";
							selectSql += "from (SELECT a.*, b.id as levelid, b.nickname, b.name, b.tel, b.headimg								 ";
							selectSql += "from(SELECT id, fenxiaoid from UserInfo where tel ='" + userTel + "') a INNER JOIN UserInfo b on b.fenxiaoid = a.Id)a			 ";
							selectSql += "INNER JOIN UserInfo b on a.levelid = b.fenxiaoid INNER JOIN orders d on a.id = d.buyername		 ";
							break;
						case "3":
							selectSql += "		SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice		 	 ";
							selectSql += "	from (SELECT a.id, a.fenxiaoid, b.id as levelid, b.nickname, b.name, b.tel, b.headimg																	 ";
							selectSql += "	from(SELECT a.*, b.id as levelid, b.nickname, b.name, b.tel, b.headimg from(SELECT id, fenxiaoid from UserInfo where tel='" + userTel + "')				 ";
							selectSql += "	a INNER JOIN UserInfo b on b.fenxiaoid = a.Id)a INNER JOIN UserInfo b on a.levelid = b.fenxiaoid) a INNER JOIN UserInfo b on a.levelid = b.fenxiaoid	 	 ";
							selectSql += "	INNER JOIN orders d on a.id = d.buyername			 ";
							break;
					}
				}
				else
				{
					selectSql += "SELECT a.*, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice";
					selectSql += " from (SELECT id, fenxiaoid from UserInfo where tel='" + userTel + "')";
					selectSql += "  a INNER JOIN UserInfo b on b.fenxiaoid = a.Id  INNER JOIN orders d on a.id = d.buyername";
					selectSql += " union";
					selectSql += " SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice";
					selectSql += "  from (SELECT a.*, b.id as levelid, b.nickname, b.name, b.tel, b.headimg";
					selectSql += "  from(SELECT id, fenxiaoid from UserInfo where   tel='" + userTel + "') a INNER JOIN UserInfo b on b.fenxiaoid = a.Id)a";
					selectSql += "  INNER JOIN UserInfo b on a.levelid = b.fenxiaoid INNER JOIN orders d on a.id = d.buyername";
					selectSql += "  union";
					selectSql += "  SELECT a.id,a.fenxiaoid, b.id as levelid,b.nickname,b.name,b.tel,b.headimg,d.orderno,d.ordertime,d.orderprice";
					selectSql += "  from (SELECT a.id, a.fenxiaoid, b.id as levelid, b.nickname, b.name, b.tel, b.headimg";
					selectSql += "  from(SELECT a.*, b.id as levelid, b.nickname, b.name, b.tel, b.headimg from(SELECT id, fenxiaoid from UserInfo where tel='" + userTel + "')	 ";
					selectSql += " a INNER JOIN UserInfo b on b.fenxiaoid = a.Id)a INNER JOIN UserInfo b on a.levelid = b.fenxiaoid) a INNER JOIN UserInfo b on a.levelid = b.fenxiaoid ";
					selectSql += " INNER JOIN orders d on a.id = d.buyername";
				}

				DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
				if (dtGetUserInfo.Rows.Count > 0)
				{
					string[] arr = { "id", "nickname", "name", "tel", "headimg", "orderno", "ordertime", "orderprice" };
					string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取分销会员总的消费数据统计报表数据失败或者未知错误。\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion 用户相关

		#region 商城首页相关

		/// <summary>
        /// 首页最新商品展示
		/// 获取产品的ID\名称\图片地址\详情
		/// </summary>
        [WebMethod(Description = "首页最新商品展示")]
		public void getgoods()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				//	string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,goodscost,goodgoldprice,goodsilverprice  from goods where setindex = 'Y'";
				//20160803 del
                //string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,goodscost,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber from";
				//sql += " goods where setindex = 'Y' and iftuangou='N' order by  addtime desc";
                string sql = "SELECT a.goodsid,a.goodstitle,b.sortname,a.goodsimg,a.goodsContent, a.goodscode, a.goodsspec, ";
                sql += " a.goodsprice,a.goodscost, a.goodstock,c.buysumqty,a.goodgoldprice,a.goodsilverprice,a.goodsspec,a.ifxiangou,a.xiangounumber";
                sql += " from goods a left JOIN goodssort b on a.goodssort=b.sortid ";
                sql += " left JOIN(SELECT buygoodsid,sum(buysumqty)buysumqty,max(selectytsort)  selectytsort";
                 sql += " from ordersdetails GROUP BY buygoodsid,selectytsort) c on a.goodsid=c.buygoodsid ";
                 sql += " and a.selectytsort=c.selectytsort where a.goodsvalidate='Y' and a.selectytsort='P' ORDER BY a.addtime desc ";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
                    string[] arr = { "goodsId", "goodsTitle", "sortname", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodstock", "buysumqty", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };

					//	string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", " goodscost", "goodgoldprice", "goodsilverprice" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"首页商品列表取得失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 搜索产品
		/// 获取产品关键词的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = "任意输入产品关键词进行检索商品")]
		public void searchgoods(string goods)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,goodscost,goodstock,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber from goods where goodsTitle like '%" + goods + "%' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
                    string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodstock", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"" + goods + "商品信息检索失败或无此商品\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		////20160202	add
		///// <summary>
		///// 获取设置为首页的推荐商品
		///// 获取推荐商品的ID\名称\图片地址\详情
		///// </summary>
		//[WebMethod(Description = @"获取设置为首页的推荐商品/获取推荐商品的ID\名称\时间\图片地址\详情等")]
		//public void GetSetIndexGood()
		//{
		//	try
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		string sql = "select goodsId,goodstitle,goodsimg,goodscontent,goodsprice,goodscost,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber,";
		//		sql += "goodscode,memberdiscount,Goodsweight,Expressset,addtime from goods where setindex='y' and iftuangou='N'  order by  addtime desc";

		//		DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
		//		{
		//			for (int i = 0; i < dt.Rows.Count; i++)
		//			{
		//				dt.Rows[i]["goodscontent"] = Server.UrlEncode(dt.Rows[i]["goodscontent"].ToString());

		//			}
		//			string[] arr = { "goodsId", "goodstitle", "goodsimg", "goodscontent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber", "goodscode", "memberdiscount", "Goodsweight", "Expressset", "addtime" };
		//			string result = Common.Json.DateTableToJson(dt, arr);
		//			Context.Response.Write(result);
		//		}
		//		else
		//		{
		//			Context.Response.Write("{\"result\":\"false\",\"data\":\"获取设置为首页的推荐商品失败或无数据\"}");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
		//	}
		//}
		//2016714	add
		/// <summary>
		/// 获取设置为首页的推荐商品
		/// 获取推荐商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取设置为首页的推荐商品/获取推荐商品的ID\名称\时间\图片地址\详情等")]
		public void GetSetIndexGood()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "select goodsId,goodstitle,goodsimg,goodscontent,goodsprice,goodscost,goodstock,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber,";
				sql += "goodscode,memberdiscount,Goodsweight,Expressset,addtime from goods where  selectytsort='T'  and goodsvalidate='Y' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodscontent"] = Server.UrlEncode(dt.Rows[i]["goodscontent"].ToString());

					}
                    string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodstock", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber", "goodscode", "memberdiscount", "Goodsweight", "Expressset", "addtime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取设置为首页的推荐商品失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//20160201	add
		/// <summary>
		/// 显示商品销售较多的热销商品列表
		/// 获取商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"显示商品销售较多的热销商品列表/获取商品的ID\名称\图片地址\详情")]
		public void GetHotGoods(int number)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "SELECT a.goodsId,a.goodsTitle,a.goodsImg,a.goodsContent,a.goodsprice,a.goodscost,a.goodstock,a.goodgoldprice,a.goodsilverprice,a.goodsspec,a.ifxiangou,a.xiangounumber,b.salecount from goods a ";
				sql += "INNER JOIN (SELECT top " + number + "  COUNT(*) as salecount, a.buygoodsid from ordersdetails a INNER JOIN goods b on a.buygoodsid = b.goodsId	";
				sql += "where a.goodsvalidate='Y'  GROUP BY a.buygoodsid) b on a.goodsId = b.buygoodsid order by  a.addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
				}
                string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodstock", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber", "salecount" };
				string result = Common.Json.DateTableToJson(dt, arr);
				Context.Response.Write(result);
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//20160201	add
		/// <summary>
		/// 获取设置为首页的广告列表
		/// 获取广告的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取设置为首页的广告列表/获取商品的ID\名称\图片地址\详情")]
		public void GetSetIndexAd()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select a.adId,a.adTitle,a.adImg,a.adContent,a.addtime,b.goodsid,b.goodstitle,b.goodsimg,";
				sql += "b.goodscontent,b.goodsprice,b.goodscost,b.goodgoldprice,b.goodsilverprice,ISNULL(b.Goodsweight, 0) goodsweight from Advertisement ";
				sql += "a inner join goods b on a.goodscode=b.goodscode where a.setindex='y' order by  a.Addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["adcontent"] = Server.UrlEncode(dt.Rows[i]["adcontent"].ToString());
						dt.Rows[i]["goodscontent"] = Server.UrlEncode(dt.Rows[i]["goodscontent"].ToString());

					}
					string[] arr = { "adId", "adTitle", "adImg", "adContent", "addtime", "goodsid", "goodstitle", "goodsimg", "goodscontent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsweight" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					string sqlnodata = "select * from Advertisement where setindex='y' order by  Addtime desc";

					DataTable dtnodata = DbHelperSQL.Query(sqlnodata).Tables[0];
					if (dtnodata.Rows.Count > 0)
					{
						for (int i = 0; i < dtnodata.Rows.Count; i++)
						{
							dtnodata.Rows[i]["adcontent"] = Server.UrlEncode(dtnodata.Rows[i]["adcontent"].ToString());
						}
						string[] arr = { "adId", "adTitle", "adImg", "adContent", "addtime" };
						string result = Common.Json.DateTableToJson(dtnodata, arr);
						Context.Response.Write(result);
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"暂无广告信息\"}");
					}
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		////20160202	add
		///// <summary>
		///// 获取设置为首页的活动
		///// 获取活动的ID\名称\图片地址\详情
		///// </summary>
		//[WebMethod(Description = @"获取设置为首页的活动/获取活动的ID\名称\时间\图片地址\详情")]
		//public void GetSetIndexHuoDong()
		//{
		//	try
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		string sql = "select huodongId as ActivityId,huodongTitle as ActivityTitle,huodongsort as ActivityType,huodongPeople as ActivityPeople,huodongKeyWord as ActivityKeyWord,";
		//		sql += "	huodongDate as ActivityTime,huodongImg as ActivityPic,huodongContent as ActivityInfo,addTime from HuoDong  order by  addtime desc";

		//		DataTable dt = DbHelperSQL.Query(sql).Tables[0];
		//		if (dt.Rows.Count > 0)
		//		{
		//			for (int i = 0; i < dt.Rows.Count; i++)
		//			{
		//				dt.Rows[i]["ActivityInfo"] = Server.UrlEncode(dt.Rows[i]["ActivityInfo"].ToString());
		//			}
		//			string[] arr = { "ActivityId", "ActivityTitle", "ActivityType", "ActivityPeople", "ActivityKeyWord", "ActivityTime", "ActivityPic", "ActivityInfo", "addTime" };
		//			string result = Common.Json.DateTableToJson(dt, arr);
		//			Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");
		//		}
		//		else
		//		{
		//			Context.Response.Write("{\"result\":\"false\",\"data\":\"获取设置为首页的活动失败或无数据\",\"message\":\"-1\"}");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
		//	}
		//}

		//20160714	add
		/// <summary>
		/// 获取设置为首页的活动
		/// 获取活动的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取设置为首页的团购.预售活动/获取活动的ID\名称\时间\图片地址\详情")]
		public void GetSetIndexHuoDong()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select Id,title,groupsort,groupsortimg,starttime,endtime,quantity,price,costprice,weight,batch";
				sql += ",tuansummary,img,imgtwo,content,tichengpoint from goodstuan where validate='Y' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["tuansummary"] = Server.UrlEncode(dt.Rows[i]["tuansummary"].ToString());
						dt.Rows[i]["content"] = Server.UrlEncode(dt.Rows[i]["content"].ToString());
					}
					string[] arr = { "Id", "title", "groupsort", "groupsortimg", "starttime", "endtime", "quantity", "price",
					"costprice", "weight", "batch","tuansummary", "img", "imgtwo","content","tichengpoint" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取最新团购/预售活动公告失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		////20160202	add
		///// <summary>
		///// 获取设置为首页的团购商品
		///// 获取团购商品的ID\名称\图片地址\详情
		///// </summary>
		//[WebMethod(Description = @"获取设置为首页的团购商品/获取团购商品的ID\名称\时间\图片地址\详情等")]
		//public void GetGroupBuysGood()
		//{
		//	try
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,goodscost,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber from goods where setindex = 'Y' and iftuangou='Y' order by  addtime desc";

		//		DataTable dt = DbHelperSQL.Query(sql).Tables[0];
		//		for (int i = 0; i < dt.Rows.Count; i++)
		//		{
		//			dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
		//		}
		//		string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
		//		string result = Common.Json.DateTableToJson(dt, arr);
		//		Context.Response.Write(result);
		//	}
		//	catch (Exception ex)
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
		//	}
		//}

		//20160714	add
		/// <summary>
		/// 获取设置为首页的团购商品
		/// 获取团购商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取设置为首页的团购商品/获取团购商品的ID\名称\时间\图片地址\详情等")]
		public void GetGroupBuysGood()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select Id,title,groupsort,groupsortimg,starttime,endtime,quantity,price,costprice,weight,batch";
				sql+= ",tuansummary,img,imgtwo,content,tichengpoint from goodstuan where selectytsort = 'G' and validate='Y' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["tuansummary"] = Server.UrlEncode(dt.Rows[i]["tuansummary"].ToString());
						dt.Rows[i]["content"] = Server.UrlEncode(dt.Rows[i]["content"].ToString());
					}
					string[] arr = { "Id", "title", "groupsort", "groupsortimg", "starttime", "endtime", "quantity", "price",
					"costprice", "weight", "batch","tuansummary", "img", "imgtwo","content","tichengpoint" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取首页团购商品失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

        ///20161017
        /// <summary>
        /// 获取设置为首页的套餐商品
        /// 获取套餐商品的ID\名称\图片地址\详情
        /// </summary>
        [WebMethod(Description = @"获取设置为首页的套餐商品/获取套餐商品的ID\名称\时间\图片地址\详情等")]
        public void GetIndexGoodsPakage()
        {
            try
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "select goodsId,goodstitle,goodsimg,goodscontent,goodsprice,goodscost,goodstock,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber,";
                sql += "goodscode,memberdiscount,Goodsweight,Expressset,addtime from goodspackage where   goodsvalidate='Y' order by  addtime desc";

                DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["goodscontent"] = Server.UrlEncode(dt.Rows[i]["goodscontent"].ToString());

                    }
                    string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodstock", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber", "goodscode", "memberdiscount", "Goodsweight", "Expressset", "addtime" };
                    string result = Common.Json.DateTableToJson(dt, arr);
                    Context.Response.Write(result);
                }
                else
                {
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"获取套餐商品失败或无数据\"}");
                }
            }
            catch (Exception ex)
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
            }
        }
        //20161017
        /// <summary>
        /// 获取套餐商品详情展示详情
        ///获取套餐商品详情展示详情商品的ID\名称\图片地址\详情
        /// </summary>
        [WebMethod(Description = @"获取套餐商品详情展示详情userid:浏览人ID 记录浏览的类型")]
        public void GetGoodsPackageDetails(string goodsid, string userid)
        {
            try
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = "select a.goodsId,a.goodsTitle,a.goodsImg,a.goodsContent,a.goodsprice,a.goodscost,a.goodgoldprice,a.goodsilverprice,a.goodsspec,";
                sql += "a.ifxiangou,a.xiangounumber,b.sortName from goodspackage a INNER JOIN goodssort b on a.goodssort=b.sortId where a.goodsId = '" + goodsid + "'";
 
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //记录用户喜欢那种类型//2016/07/13
                    if (!string.IsNullOrEmpty(userid))
                    {
                        string sqlIfExist = "select count(*) countbrowser from goodtypelikes where browseruser= '" + userid + "' and goodstype='" + dt.Rows[0]["sortName"] + "'";

                        DataTable dtIfExist = DbHelperSQL.Query(sqlIfExist).Tables[0];
                        if (int.Parse(dtIfExist.Rows[0]["countbrowser"].ToString()) == 0)
                        {
                            string sqldetails = "insert into goodtypelikes(goodstype ,browseruser) values('" + dt.Rows[0]["sortName"] + "','" + userid + "')";
                            int resultdetails = DbHelperSQL.ExecuteSql(sqldetails);
                        }
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
                    }
                    string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
                    string result = Common.Json.DateTableToJson(dt, arr);
                    Context.Response.Write(result);
                }
                else
                {
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"获取套餐商品详情失败或无数据\"}");

                }
            }
            catch (Exception ex)
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
            }
        }

		#endregion 首页相关

		#region 商品相关

		/// <summary>
		/// 商品分类
		/// 获取分类ID\名称
		/// </summary>
		[WebMethod(Description = @"获取商品分类ID\名称")]
		public void getgoodsSort()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select sortId,sortName,sortfatherid from goodsSort where sortfatherid=1";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "sortId", "sortName", "sortfatherid" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"error\",\"data\":\"暂无商品分类\"}");

				}

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		/// <summary>
		/// 获取商品二级分类
		/// 获取商品分类的ID\名称 
		/// </summary>
		[WebMethod]
		public void getGoodsSonSort(string fatherid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "SELECT sortId, sortName, sortfatherid FROM goodsSort WHERE (sortFatherId = '" + fatherid + "')";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "sortId", "sortName", "sortfatherid" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"error\",\"data\":\"暂无商品二级分类\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 分类产品
		/// 按分类获取商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"对商品进行分类/获取商品的ID\名称\图片地址\详情")]
		public void catgoods(string sortId)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                //20160803 del
				//string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,goodscost,goodgoldprice,goodsilverprice,goodsspec,ifxiangou,xiangounumber from goods where goodsSort = '" + sortId + "' and iftuangou='N'";// order by  addtime desc";
                //20160728 add
              //  sql += " union  select id as goodsId, title as goodsTitle,img as goodsImg,content as goodsContent,price as goodsprice,costprice as goodscost,'0.00'goodgoldprice,'0.00'goodsilverprice,'0.00'goodsspec,''ifxiangou,''xiangounumber from goodstuan where groupsort= '" + sortId + "'    ";
                //20160803 add
                string sql = " select a.goodsId,a.goodsTitle,b.sortName,a.goodsImg,a.goodsContent,a.goodsprice,a.goodscost,a.goodstock,a.goodgoldprice,";
                sql += " a.goodsilverprice,a.goodsspec,a.ifxiangou,a.xiangounumber from goods a ";
                sql += "  INNER JOIN goodssort b on a.goodssort=b.sortId where a.goodsSort = '" + sortId + "' and a.goodsvalidate='Y' ";
                sql += " union ";
                sql += "   select a.id as goodsId, a.title as goodsTitle,b.sortName,a.img as goodsImg,a.content as goodsContent,a.price as goodsprice, ";
                sql += " a.costprice as goodscost,a.quantity as goodstock,'0.00'goodgoldprice,'0.00'goodsilverprice,'0.00'goodsspec,''ifxiangou,''xiangounumber ";
                sql += " from goodstuan  a INNER JOIN goodssort b on a.groupsort=b.sortId where a.groupsort='" + sortId + "' and a.validate='Y'  ";

                DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
                    string[] arr = { "goodsId", "goodsTitle", "sortName", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodstock", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取商品分类失败或无数据\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		/// <summary>
		/// 获取商品详情展示详情
		///获取商品详情展示详情商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取商品详情展示详情userid:浏览人ID 记录浏览的类型")]
		public void GetGoodsDetailsInfo(string goodsid, string userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select a.goodsId,a.goodsTitle,a.goodsImg,a.goodsContent,a.goodsprice,a.goodscost,a.goodgoldprice,a.goodsilverprice,a.goodsspec,";
				sql += "a.ifxiangou,a.xiangounumber,b.sortName from goods a INNER JOIN goodssort b on a.goodssort=b.sortId where a.goodsId = '" + goodsid + "'";
                //20160728add
                sql+=" union  select a.id as goodsId,a.title as  goodsTitle,a.img as goodsImg,a.content as goodsContent,";
                sql+="a.price as goodsprice,a.costprice as goodscost,'0.00' as goodgoldprice,'0.00' goodsilverprice,'0.00'goodsspec, ";
                sql += " ''ifxiangou,'' xiangounumber,b.sortName from goodstuan a INNER JOIN goodssort b on a.groupsort=b.sortId where a.id = '" + goodsid + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					//记录用户喜欢那种类型//2016/07/13
					if (!string.IsNullOrEmpty(userid))
					{
						string sqlIfExist = "select count(*) countbrowser from goodtypelikes where browseruser= '" + userid + "'and goodstype='" + dt.Rows[0]["sortName"] + "'";

						DataTable dtIfExist = DbHelperSQL.Query(sqlIfExist).Tables[0];
                        if (int.Parse(dtIfExist.Rows[0]["countbrowser"].ToString()) == 0)
						{
							string sqldetails = "insert into goodtypelikes(goodstype ,browseruser) values('" + dt.Rows[0]["sortName"] + "','" + userid + "')";
							int resultdetails = DbHelperSQL.ExecuteSql(sqldetails);
							////if (resultdetails > 0)
							////{
							////    Context.Response.Write("{\"result\":\"true\",\"data\":\"记录个人浏览商品类型成功\"}");
							////}
							////else
							////{ Context.Response.Write("{\"result\":\"false\",\"data\":\"记录个人浏览商品类型失败\"}"); }
						}
					}
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
					string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取商品详情失败或无数据\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		
		///2016/07/13
		/// <summary>
		/// 根据用户需求推荐商品 
		///根据用户需求推荐详情商品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"根据用户需求推荐商品userid:用户ID ")]
		public void GuessUserLikeGoods(int userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "	SELECT a.browseruser,a.goodstype,c.goodsId,c.goodsTitle,c.goodsImg,c.goodsContent,c.goodsprice,c.goodscost,";
				sql += "c.goodgoldprice,c.goodsilverprice,c.goodsspec,c.ifxiangou,c.xiangounumber from goodtypelikes a INNER JOIN goodssort";
				sql += " b on a.goodstype = b.sortName INNER JOIN goods c on b.sortId = c.goodssort where a.browseruser = '" + userid+"'		";
                sql += " UNION	SELECT a.browseruser,a.goodstype,c.goodsId,c.goodsTitle,c.goodsImg,c.goodsContent,c.goodsprice,c.goodscost,";
                sql += "c.goodgoldprice,c.goodsilverprice,c.goodsspec,c.ifxiangou,c.xiangounumber from goodtypelikes a INNER JOIN goodssort";
                sql += " b on a.goodstype = b.sortName INNER JOIN goodspackage c on b.sortId = c.goodssort where a.browseruser = '" + userid + "'		";
    
                
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
					string[] arr = { "browseruser", "goodstype", "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
                    //20161027 add
                    string sqlnolike = " SELECT top 10 '" + userid + "' as browseruser,b.sortName as goodstype, c.goodsId,c.goodsTitle,c.goodsImg,c.goodsContent,c.goodsprice,c.goodscost, ";
                    sqlnolike += "c.goodgoldprice,c.goodsilverprice,c.goodsspec,c.ifxiangou,c.xiangounumber from   goods c   LEFT JOIN goodssort b on c.goodssort=b.sortId ";
                     sqlnolike += " union  SELECT top 10 '" + userid + "' as browseruser,b.sortName as goodstype, c.goodsId,c.goodsTitle,c.goodsImg,c.goodsContent,c.goodsprice,c.goodscost, ";
                     sqlnolike += "c.goodgoldprice,c.goodsilverprice,c.goodsspec,c.ifxiangou,c.xiangounumber from   goodspackage c   LEFT JOIN goodssort b on c.goodssort=b.sortId ";
        
                    DataTable dtnolike = DbHelperSQL.Query(sqlnolike).Tables[0];
                    if (dtnolike.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtnolike.Rows.Count; i++)
                        {
                            dtnolike.Rows[i]["goodsContent"] = Server.UrlEncode(dtnolike.Rows[i]["goodsContent"].ToString());
                        }
                        string[] arr = { "browseruser", "goodstype", "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "goodscost", "goodgoldprice", "goodsilverprice", "goodsspec", "ifxiangou", "xiangounumber" };
                        string result = Common.Json.DateTableToJson(dtnolike, arr);
                        Context.Response.Write(result);
                    }
                    else
                    {
                        Context.Response.Write("{\"result\":\"false\",\"data\":\"根据用户需求获取推荐商品详情失败或无数据\"}");
                    }
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160130	add
		/// <summary>
		/// 判断是否为金卡或银卡会员 是 使用会员价否 使用普通价
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <param name="goodid">商品ID</param>
		/// <param name="">商品价格</param>
		[WebMethod(Description = "判断是否为金卡或银卡会员 是 使用会员价否 使用普通价")]
		public void IFBuyGoodsDiscountPrice(int userid, int goodid, decimal googsprice)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string getUsertype = "select a.usertype,b.discount  from  userinfo a inner join memberset b on a.usertype=b.memberid  where id='" + userid + "'";
				DataTable dtDiscount = DbHelperSQL.Query(getUsertype).Tables[0];
				if (dtDiscount.Rows.Count > 0)
				{
					//金银会员执行折扣
					decimal discountPrice;
					if (int.Parse(dtDiscount.Rows[0]["usertype"].ToString()) == 2 || int.Parse(dtDiscount.Rows[0]["usertype"].ToString()) == 3)
					{
						//折后价格
						discountPrice = googsprice - (googsprice * (Convert.ToDecimal(dtDiscount.Rows[0]["discount"].ToString()) / 100));
					}
					else { discountPrice = googsprice; }
					Context.Response.Write("{\"result\":\"true\",\"data\":\"" + discountPrice + "\"}");
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"取得会员折扣失败。\"}");

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//20160130	add
		/// <summary>
		/// 判断是否为特价商品   是  根据会员等级限制购买数量上限 和购买价格 否 无限制
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <param name="goodid">商品ID</param>
		/// <param name="">商品价格</param>
		/// 是特价商品会员限购6个。价格为会员折扣*商品价格 否则返回　ｎｕｌｌ
		[WebMethod(Description = "判断是否为特价商品   是  根据会员等级限制购买数量上限 和购买价格 否 无限制<br>是特价商品会员限购6个。价格为会员折扣*商品价格 否则返回　ｎｕｌｌ")]
		public void BuyTeJiaGoodsPrice(int userid, int goodid, decimal googsprice)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");

				//根据商品ID判断是否特价促销商品
				string getIfSales = "select goodsSales  from  goods   where goodsId='" + goodid + "'";
				DataTable dtSales = DbHelperSQL.Query(getIfSales).Tables[0];
				if (dtSales.Rows.Count > 0)
				{   //是特价商品
					if (Convert.ToChar(dtSales.Rows[0]["goodsSales"].ToString()) == 'Y')
					{
						string getUsertype = "select a.usertype,b.discount  from  userinfo a inner join memberset b on a.usertype=b.memberid  where id='" + userid + "'";
						DataTable dtDiscount = DbHelperSQL.Query(getUsertype).Tables[0];
						if (int.Parse(dtDiscount.Rows[0][0].ToString()) > 0)
						{
							//是会员则返回计算返回折扣价格
							if (int.Parse(dtDiscount.Rows[0]["usertype"].ToString()) == 2 || int.Parse(dtDiscount.Rows[0]["usertype"].ToString()) == 3)
							{
								var discountPrice = googsprice - (googsprice * (Convert.ToDecimal(dtDiscount.Rows[0]["discount"].ToString()) / 100));
								Context.Response.Write("{\"result\":\"true\",\"buynum\":\"6\",\"buyprice\":\"" + discountPrice + "\"}");
							}
						}
					}
					else
						Context.Response.Write("{\"result\":\"true\",\"buynum\":\"null\",\"buyprice\":\"null\"}");
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"取得特价商品折扣率失败。\"}");

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160222	add
		/// <summary>
		/// 判断是否限购商品，是否超过该商品的限购数量
		/// </summary>
		/// <param name="userid">用户ID</param>
		/// <param name="goodid">商品ID</param>
		[WebMethod(Description = "判断是否限购商品，是否超过该商品的限购数量 buystatus:W为无限购，Y可以购买，N不可以，J减少购买:data 购买商品数量信息<br/>buygoodcount 为当前购买文本框数量(触发文本框事件或者购买时触发)")]
		public void IFGoodsXianGou(int buyuserid, int goodsid, int buygoodcount, int usertype)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string getUsertype = "select goodstock,ifxiangou,xiangounumber   from  goods    where  goodsId='" + goodsid + "'";
				DataTable dtPesticide = DbHelperSQL.Query(getUsertype).Tables[0];
				if (dtPesticide.Rows.Count > 0)
				{
					//不是限购商品 返回0 为无限购
					if (dtPesticide.Rows[0]["ifxiangou"].ToString() == "Y")
					{
						//查询限购产品的限购数 金/银/普通
						string getAllBuyGoodSum = "SELECT  b.buygoodsid,SUM(b.buysumqty)buyAllSumQty from orders a INNER JOIN ordersdetails b on a.orderno=b.orderno where a.buyername='" + buyuserid + "' and  b.buygoodsid='" + goodsid + "' GROUP BY  b.buygoodsid";
						DataTable dtAllBuyGoodSum = DbHelperSQL.Query(getAllBuyGoodSum).Tables[0];
						if (dtAllBuyGoodSum.Rows.Count > 0)
						{
							//拆分限购数量
							var splitxiangou = dtPesticide.Rows[0]["xiangounumber"].ToString().Split('/');
							if (splitxiangou.Length > 1)
							{
								if (usertype == 3)    //金卡
								{
									Context.Response.Write(ReturnXianGouInfo(int.Parse(dtAllBuyGoodSum.Rows[0]["buyAllSumQty"].ToString().Trim()), int.Parse(splitxiangou[0].Trim()), buygoodcount));
								}
								else if (usertype == 2)  //银卡
								{
									Context.Response.Write(ReturnXianGouInfo(int.Parse(dtAllBuyGoodSum.Rows[0]["buyAllSumQty"].ToString()), int.Parse(splitxiangou[1].Trim()), buygoodcount));
								}
								else          //普通卡
								{
									Context.Response.Write(ReturnXianGouInfo(int.Parse(dtAllBuyGoodSum.Rows[0]["buyAllSumQty"].ToString()), int.Parse(splitxiangou[2].Trim()), buygoodcount));
								}
							}
							else
							{
								Context.Response.Write("{\"result\":\"false\",\"info\":\"限购不符合金卡/银卡/普通会员限购设置规则\"}");
							}
						}
						else //还没购买过商品
						{
							var splitxiangou = dtPesticide.Rows[0]["xiangounumber"].ToString().Split('/');
							if (splitxiangou.Length > 1)
							{
								if (usertype == 3)
								{
									Context.Response.Write(ReturnNoOrderXianGouInfo(int.Parse(splitxiangou[0].Trim()), buygoodcount));

								}
								else if (usertype == 2)
								{
									Context.Response.Write(ReturnNoOrderXianGouInfo(int.Parse(splitxiangou[1].Trim()), buygoodcount));
								}
								else
								{
									Context.Response.Write("{\"result\":\"false\",\"info\":\"限购不符合金卡/银卡/普通会员限购设置规则\"}");

								}
							}
							else
							{
								Context.Response.Write("{\"result\":\"false\",\"info\":\"限购不符合金卡/银卡/普通会员限购设置规则\"}");
							}
						}
					}
					else
					{
						if (int.Parse(dtPesticide.Rows[0]["goodstock"].ToString()) == 0)
							Context.Response.Write("{\"result\":\"false\",\"info\":\"该商品库存数量为0,不能购买。\",\"buystatus\":\"N\",\"data\":\"0\"}");
						else
						{
							var shengyusum = int.Parse(dtPesticide.Rows[0]["goodstock"].ToString());
							Context.Response.Write("{\"result\":\"false\",\"info\":\"不是限购商品，请尽情购买。\",\"buystatus\":\"W\",\"data\":\"" + shengyusum + "\"}");
						}
					}

				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"取得商品失败。\"}");

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		public string ReturnNoOrderXianGouInfo(int xiangounumber, int buygoodcount)
		{
			var resultInfo = string.Empty;
			//判断客户历史购买此商品数量，是否大于限购数量
			if (buygoodcount > xiangounumber)
			{
				int dayunumber = buygoodcount - xiangounumber;
				resultInfo = "{\"result\":\"false\",\"info\":\"现在购买总数量大于限购数量:" + dayunumber + "，请减少数量购买。\",\"buystatus\":\"N\",\"data\":\"0\"}";
			}
			//else if (buygoodcount == xiangounumber)
			//{
			//    int dayunumber = buygoodcount - xiangounumber;
			//    resultInfo = "{\"result\":\"false\",\"info\":\"现在购买总数量等于限购数量:" + dayunumber + "\",\"buystatus\":\"N\",\"data\":\"0\"}";
			//}
			else
			{
				var shengyusum = xiangounumber; //- buygoodcount;
				resultInfo = "{\"result\":\"true\",\"info\":\"剩余的限购数量:" + shengyusum + "\",\"buystatus\":\"Y\",\"data\":\"" + shengyusum + "\"}";

			}
			return resultInfo;
		}
		public string ReturnXianGouInfo(int buyAllSumQty, int xiangounumber, int buygoodcount)
		{
			var resultInfo = string.Empty;
			//判断客户历史购买此商品数量，是否大于限购数量
			if (buyAllSumQty > xiangounumber)
			{
				int dayunumber = buyAllSumQty - xiangounumber;
				resultInfo = "{\"result\":\"false\",\"info\":\"历史购买总数量大于限购数量:" + dayunumber + "，已不能购买。\",\"buystatus\":\"N\",\"data\":\"0\"}";
			}

			else
			{  //判断现在限购买量是否大于客户历史购买此商品数量 ，大于给提示
				int info = xiangounumber - buyAllSumQty;
				if (info > 0)
				{
					if (buygoodcount > info)
					{
						int jianshaonumber = buygoodcount - info;
						int kegoumainumber = info - buygoodcount;
						resultInfo = "{\"result\":\"false\",\"info\":\"购买数量大于剩余限购数量:" + jianshaonumber + "，请减少数量购买。\",\"buystatus\":\"J\",\"data\":\"" + kegoumainumber + "\"}";
					}
					else //不大于返回剩余可购买数量
					{
						var shengyusum = info;// -buygoodcount;
						resultInfo = "{\"result\":\"true\",\"info\":\"剩余的限购数量:" + shengyusum + "\",\"buystatus\":\"Y\",\"data\":\"" + shengyusum + "\"}";
					}
				}
				else
				{
					resultInfo = "{\"result\":\"false\",\"info\":\"历史购买数量等于限购数量，已不能购买。\",\"buystatus\":\"N\",\"data\":\"0\"}";

				}
			}
			return resultInfo;
		}

		//20160203 add
		/// <summary>
		/// 积分兑换商品列表显示
		/// 获取商品的ID\名称\图片地址\详情\所需积分
		/// </summary>
		[WebMethod(Description = "积分兑换商品列表显示")]
		public void GetExchangeGoodsList()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select goodsId,goodsTitle,goodsImg,goodsContent,goodsprice,exchangepoint from goods where ifexchange = 'Y'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0]; if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
					string[] arr = { "goodsId", "goodsTitle", "goodsImg", "goodsContent", "goodsprice", "exchangepoint" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"积分兑换商品列表取得失败或无数据\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160203 add
		/// <summary>
		/// 积分兑换商品
		/// </summary>
		/// <param name="buyuserid">兑换人ID</param>
		/// <param name="exchangepoint">商品兑换所需积分</param>
		/// <param name="goodsid">商品ID</param>
		[WebMethod(Description = "积分兑换商品/兑换人ID/商品兑换所需积分/商品ID")]
		public void PointExchangeGoods(string buyuserid, int exchangepoint, int goodsid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				//判断用户积分够不够
				string getUserPoint = "select point from UserInfo where id='" + buyuserid + "'";
				DataTable dt = DbHelperSQL.Query(getUserPoint).Tables[0];
				if (dt.Rows.Count > 0)
				{
					if (int.Parse(dt.Rows[0]["point"].ToString()) >= exchangepoint)
					{
						var orderno = GetOrderNumber();
						string sql = "insert into orders(orderno,orderprice ,ordersource,paymode ,buyername,ordertime ,paymenttime,orderstatus, ) values('" + orderno + "','0','积分兑换','积分兑换','"
+ buyuserid + "','" + DateTime.Now + "','" + DateTime.Now + "','1' )";
						int result = DbHelperSQL.ExecuteSql(sql);
						if (result > 0)
						{
							Context.Response.Write("{\"result\":\"true\",\"data\":\"生成订单成功\"}");
							string sqldetails = "insert into ordersdetails(orderno ,orderprice ,ordertime,buygoodsid,buysumqty,goodsprice) values('" + orderno + "','0','" + DateTime.Now + "','" + goodsid + "','1','0')";
							int resultdetails = DbHelperSQL.ExecuteSql(sqldetails);
							if (resultdetails > 0)
							{
								Context.Response.Write("{\"result\":\"true\",\"data\":\"生成订单明细成功\"}");
								var kouchujifen = int.Parse(dt.Rows[0]["point"].ToString()) - exchangepoint;
								string upPoint = "update   UserInfo set point='" + kouchujifen + "' where Id = '" + buyuserid + "'";
								DbHelperSQL.ExecuteSql(upPoint);
							}
							else
							{ Context.Response.Write("{\"result\":\"false\",\"data\":\"生成订单明细失败\"}"); }
						}
						else
						{ Context.Response.Write("{\"result\":\"false\",\"data\":\"生成订单失败\"}"); }


					}
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"兑换此商品积分不够，兑换失败\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion 商城相关

		#region 社区新闻相关

		/// <summary>
		/// 获取社区新闻分类
		/// 获取分类的ID\名称
		/// </summary>
		[WebMethod(Description = @"获取新闻分类的ID\名称")]
		public void GetNoticesSort()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select sortId,sortName from noticesSort";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "sortId", "sortName" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write("{\"result\":\"true\",\"data\":" + result + "}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取新闻分类失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		/// <summary>
		/// 根据新闻分类获取社区新闻
		/// 获取产品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"根据新闻分类获取新闻ID\名称\图片地址\详情等")]
		public void GetNotices(int Type)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select id as NewsId ,Title,newssort as Type,newssource as Author,img as Pic1,img1 as Pic2,img2 as Pic3,";
				sql += "content as Info,addTime,browses,discussnum,likenum from notices where newssort = '" + Type + "' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["Info"] = Server.UrlEncode(dt.Rows[i]["Info"].ToString());
					}
					string[] arr = { "NewsId", "Title", "Type", "Author", "Info", "Browses", "DiscussNum", "LikeNum", "Pic1", "Pic2", "Pic3", "addTime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取此新闻分类下的新闻失败或无数据\",\"message\":\"-1\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 获取社区新闻详细
		/// 获取产品的ID\名称\图片地址\详情
		/// </summary>
		[WebMethod(Description = @"获取社区新闻详细新闻ID\名称\图片地址\详情等")]
		public void GetNoticesDetailsInfo(string newsid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select id as NewsId ,Title,newssort as Type,newssource as Author,img as Pic1,img1 as Pic2,img2 as Pic3,";
				sql += "content as Info,addTime,browses,discussnum,likenum  from notices where id = '" + newsid + "' ";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					//增加浏览次数2016/07/06 add 
					DbHelperSQL.ExecuteSql("update notices set browses=browses+1 where id='" + newsid + "'");
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["Info"] = Server.UrlEncode(dt.Rows[i]["Info"].ToString());
					}
					string[] arr = { "NewsId", "Title", "Type", "Author", "Info", "Browses", "DiscussNum", "LikeNum", "Pic1", "Pic2", "Pic3", "addTime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取此新闻详细失败或无数据\",\"message\":\"-1\"}");

				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160214 add
		/// <summary>
		/// 根据新闻分类和页数显示最新社区新闻
		/// </summary>
		/// <param name="Type">新闻分类</param>
		/// <param name="PageSize">页码数</param>
		[WebMethod(Description = "根据新闻分类和页数显示最新社区新闻/新闻分类/页码数如2，2")]
		public void GetTopNewNotices(string Type, int PageSize)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				int rownumber = 10 * (PageSize - 1);
				string sql = "select top 10 id as NewsId ,Title,newssort as Type,newssource as Author,img as Pic1,img1 as Pic2,img2 as Pic3,content as Info,addTime,browses,discussnum,likenum ";
				sql += " from notices where newssort = '" + Type + "' and 	 (ID NOT IN (SELECT TOP " + rownumber + " id FROM notices ORDER BY id DESC)) ORDER BY ID  DESC ";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["Info"] = Server.UrlEncode(dt.Rows[i]["Info"].ToString());
					}
					string[] arr = { "NewsId", "Title", "Type", "Author", "Info", "Browses", "DiscussNum", "LikeNum", "Pic1", "Pic2", "Pic3", "addTime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"根据新闻分类和页数显示最新社区新闻获取失败或无数据\",\"message\":\"-1\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 获取新闻下的评论
		/// </summary>
		/// <param name="NewsId">新闻ID</param>
		[WebMethod(Description = "获取新闻下的评论")]
		public void GetNewsDicussListInfo(int NewsId)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "SELECT a.discussuserid as DisUserId ,a.discussid as DisId,b.headimg as HeadPic,b.Nickname ,a.discusscontent as DicussInfo,a.discusstime as DiscussTime";
				sql += " from noticesdiscuss a INNER JOIN UserInfo b on a.discussuserid = b.Id where a.noticeid = '" + NewsId + "'	";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{


					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["DicussInfo"] = Server.UrlEncode(dt.Rows[i]["DicussInfo"].ToString());
					}
					string[] arr = { "DisUserId", "DisId", "HeadPic", "Nickname", "DicussInfo", "DiscussTime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");

				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取信息新闻评论失败或无数据\",\"message\":\"-1\"}");
				}
			}

			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 对新闻进行评论
		/// </summary>
		/// <param name="NewsId">新闻ID</param>
		/// <param name="UserId">评论人</param>
		/// <param name="DicussInfo">评论内容</param>
		[WebMethod(Description = "对新闻进行评论")]
		public void MakeNewsDicuss(int NewsId, string UserId, string DicussInfo)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string insersql = "insert into noticesdiscuss(noticeid,discussuserid,discusscontent) values('" + NewsId + "','" + UserId + "','" + DicussInfo + "')";
				int result = DbHelperSQL.ExecuteSql(insersql);
				if (result > 0)
				{
					DbHelperSQL.ExecuteSql("update notices set discussnum=discussnum+1 where id='" + NewsId + "'");
					Context.Response.Write("{\"result\":\"true\",\"data\":\"评论成功\",\"message\":\"0\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"评论失败，未知错误\",\"message\":\"-1\"}");
				}
			}

			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 对喜欢的新闻进行点赞处理
		/// </summary>
		/// <param name="newsid">新闻ID</param>
		[WebMethod(Description = "对喜欢的新闻进行点赞处理iflike(0:喜欢,1：不喜欢(取消喜欢))")]
		public void ClickNewsIfLikes(int NewsId, string UserId, string iflike)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				//进行喜欢 不喜欢(取消处理)
				if (iflike == "0")
				{
					string sql = "SELECT count(1) as SumCount from noticeslikes where noticeid = '" + NewsId + "'	and likeuserid='" + UserId + "'";
					DataTable dt = DbHelperSQL.Query(sql).Tables[0];
					if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["SumCount"].ToString()) > 0)
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"已经点赞收藏,请勿再点赞收藏\",\"message\":\"-1\"}");
					}
					else
					{
						string insersql = "insert into noticeslikes(noticeid,likeuserid,liketime) values('" + NewsId + "','" + UserId + "','" + DateTime.Now + "')";
						int result = DbHelperSQL.ExecuteSql(insersql);
						if (result > 0)
						{
							//2016/07/06 add
							DbHelperSQL.ExecuteSql("update notices set likenum=likenum+1 where id='" + NewsId + "'");
							Context.Response.Write("{\"result\":\"true\",\"data\":\"点赞收藏成功\",\"message\":\"0\"}");
						}
						else
						{
							Context.Response.Write("{\"result\":\"false\",\"data\":\"点赞收藏失败，未知错误\",\"message\":\"-1\"}");
						}
					}
				}
				else
				{
					string insersql = "delete from noticeslikes where noticeid = '" + NewsId + "'	and likeuserid='" + UserId + "'";
					int result = DbHelperSQL.ExecuteSql(insersql);
					if (result > 0)
					{
						//取得点赞数。如果是0时，取消点赞不执行减少点赞数
						DataTable dtlikenum = DbHelperSQL.Query("select likenum from notices where id='" + NewsId + "'").Tables[0];
						if (int.Parse(dtlikenum.Rows[0]["likenum"].ToString()) > 0)
						{
							DbHelperSQL.ExecuteSql("update notices set likenum=likenum-1 where id='" + NewsId + "'");
						}
						Context.Response.Write("{\"result\":\"true\",\"data\":\"取消点赞收藏成功\",\"message\":\"0\"}");
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"取消点赞收藏失败/暂无点赞，未知错误\",\"message\":\"-1\"}");
					}
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 获取是否对新闻进行点赞过
		/// </summary>
		/// <param name="newsid">新闻ID</param>
		[WebMethod(Description = "获取是否对新闻进行点赞过(true:点过,false：没点赞)")]
		public void GetNewsIfLikes(int NewsId, string UserId)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "SELECT count(1) as SumCount from noticeslikes where noticeid = '" + NewsId + "'	and likeuserid='" + UserId + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["SumCount"].ToString()) > 0)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"true\",\"message\":\"0\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"false\",\"message\":\"0\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion 社区相关

		#region 活动相关
		//20160202	add
		/// <summary>
		/// 参加活动
		/// </summary>
		/// <param name="Userid">用户ID</param>
		/// <param name="ActivityId">参加活动ID</param>

		[WebMethod(Description = "加入首页推荐的活动")]
		public void JoinHuoDong(int Userid, int ActivityId)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "SELECT count(1) as SumCount from JoinHuoDong where huodongid = '" + ActivityId + "'	and userid='" + Userid + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["SumCount"].ToString()) > 0)
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"已经加入活动,请勿重新加入\",\"message\":\"-1\"}");
				}
				else
				{
					string insersql = "insert into JoinHuoDong(userid,huodongid,addTime) values('" + Userid + "','" + ActivityId + "','" + DateTime.Now + "')";
					int result = DbHelperSQL.ExecuteSql(insersql);
					if (result > 0)
						Context.Response.Write("{\"result\":\"true\",\"data\":\"加入活动成功\",\"message\":\"0\"}");
					else
						Context.Response.Write("{\"result\":\"false\",\"data\":\"加入活动失败，未知错误\",\"message\":\"-1\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		///  "参与活动”报名信息"[基本活动/征婚活动合二为一]
		/// </summary>
		/// <param name="ActivityId">活动的id</param>
		/// <param name="UserId">用户的id</param>
		/// <param name="ActivityType">活动的类型</param>
		/// <param name="Name">姓名</param>
		/// <param name="Telphone">电话号码</param>
		/// <param name="Sex">性别</param>
		/// <param name="Age">年龄</param>
		/// <param name="Number">活动人数</param>
		/// <param name="Job">职业</param>
		/// <param name="Interest">兴趣爱好</param>
		/// <param name="Remarks">备注</param>
		[WebMethod(Description = "参与活动”报名信息")]
		public void JoinHuoDongInfo(int ActivityId, int UserId, int ActivityType, string Name,
			string Telphone, char Sex, string Age, string Number, string Job, string Interest, string Remarks)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "SELECT count(1) as SumCount from JoinHuoDongInfo where huodongid = '" + ActivityId + "'	and userid='" + UserId + "'";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["SumCount"].ToString()) > 0)
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"已经加入活动,请勿重新加入\",\"message\":\"-1\"}");
				}
				else
				{
					string insersql = "INSERT INTO JoinHuoDongInfo (userid ,huodongid ,huodongsort,joinname,jointel ,joinsex ,age ,joinnumber ,job ,interest,remarks,addtime)";
					insersql += "values('" + UserId + "','" + ActivityId + "','" + ActivityType + "','" + Name + "','" + Telphone + "','" + Sex + "','" + Age + "','" + Number + "','" + Job + "','" + Interest + "','" + Remarks + "','" + DateTime.Now + "')";
					int result = DbHelperSQL.ExecuteSql(insersql);
					if (result > 0)
						Context.Response.Write("{\"result\":\"true\",\"data\":\"加入活动成功\",\"message\":\"0\"}");
					else
						Context.Response.Write("{\"result\":\"false\",\"data\":\"加入活动失败，未知错误\",\"message\":\"-1\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		///  20160714
		/// </summary>
		[WebMethod(Description = "获取活动等待列表")]
		public void GetWaitJoinHuoDong()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				DateTime dtNow = DateTime.Now;
				string sql = "select Id,title,groupsort,groupsortimg,starttime,endtime,quantity,price,costprice,weight,batch";
				sql += ",tuansummary,img,imgtwo,content,tichengpoint,'' as statusdate from goodstuan where  validate='Y' order by  addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["tuansummary"] = Server.UrlEncode(dt.Rows[i]["tuansummary"].ToString());
						dt.Rows[i]["content"] = Server.UrlEncode(dt.Rows[i]["content"].ToString());
						DateTime dtBegin = Convert.ToDateTime(dt.Rows[i]["starttime"]);
						if (DateTime.Compare(dtNow, dtBegin) > 0)
						{
							dt.Rows[i]["statusdate"] = "已开始：" + DateDiff(dtBegin, dtNow); ;
						}
						else
						{
							dt.Rows[i]["statusdate"] = "倒计时：" + DateDiff(dtBegin, dtNow); ;

						}
					}
					string[] arr = { "Id", "title", "groupsort", "groupsortimg", "starttime", "endtime", "quantity", "price",
					"costprice", "weight", "batch","tuansummary", "img", "imgtwo","content","tichengpoint","statusdate" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取团购/预售商品活动等待列表失败或无数据\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		private string DateDiff(DateTime DateTime1, DateTime DateTime2)
		{
			string dateDiff = null;
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new
			TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
			return dateDiff;
		}

		// /// <summary>
		// /// 20160714 del
		// /// </summary>
		// /// <param name="Userid"></param>
		//[WebMethod(Description = "获取个人参加活动记录")]
		//public void GetUserJoinHuoDongList(int Userid)
		//{
		//	try
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		string selectSql = " SELECT b.huodongId as ActivityId,b.huodongTitle as ActivityTitle,b.huodongsort as ActivityType,b.huodongPeople as ActivityPeople,";
		//		selectSql += " b.huodongKeyWord as ActivityKeyWord, b.huodongDate as ActivityTime,b.huodongImg as ActivityPic,b.huodongContent as ActivityInfo,";
		//		selectSql += " b.addTime from JoinHuoDong a INNER JOIN HuoDong b on a.huodongid=b.huodongId where a.userid='" + Userid + "'";
		//		DataTable dtGetUserInfo = DbHelperSQL.Query(selectSql.ToString()).Tables[0];
		//		if (dtGetUserInfo.Rows.Count > 0)
		//		{

		//			for (int i = 0; i < dtGetUserInfo.Rows.Count; i++)
		//			{
		//				dtGetUserInfo.Rows[i]["ActivityInfo"] = Server.UrlEncode(dtGetUserInfo.Rows[i]["ActivityInfo"].ToString());
		//			}
		//			string[] arr = { "ActivityId", "ActivityTitle", "ActivityType", "ActivityPeople", "ActivityKeyWord", "ActivityTime", "ActivityPic", "ActivityInfo", "addTime" };
		//			string result = Common.Json.DateTableToJson(dtGetUserInfo, arr);
		//			Context.Response.Write("{\"result\":\"true\",\"data\":" + result + ",\"message\":\"0\"}");

		//		}
		//		else
		//		{
		//			Context.Response.Write("{\"result\":\"false\",\"data\":\"个人参加活动记录获取失败，或未知错误\"}");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Context.Response.Clear();
		//		Context.Response.Charset = "utf-8";
		//		Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
		//		Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
		//	}
		//}

		/// <summary>
		/// 20160714 add
		/// </summary>
		/// <param name="Userid"></param>
		[WebMethod(Description = "获取个人参加活动记录")]
		public void GetUserJoinHuoDongList(int Userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select a.Id,a.title,a.groupsort,a.groupsortimg,a.starttime,a.endtime,a.quantity,a.price,a.costprice,a.weight,a.batch";
				sql += ",a.tuansummary,a.img,a.imgtwo,a.content,a.tichengpoint,b.id as joinid,b.huodongid,b.addtime as jointime from goodstuan a inner join JoinHuoDong b on a.id=b.huodongid where a.validate='Y' order by  a.addtime desc";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dt.Rows[i]["tuansummary"] = Server.UrlEncode(dt.Rows[i]["tuansummary"].ToString());
						dt.Rows[i]["content"] = Server.UrlEncode(dt.Rows[i]["content"].ToString());
					}
					string[] arr = { "Id", "title", "groupsort", "groupsortimg", "starttime", "endtime", "quantity", "price",
					"costprice", "weight", "batch","tuansummary", "img", "imgtwo","content","tichengpoint","joinid","huodongid","addtime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"个人参加团购/预售活动记录获取失败，或未知错误\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion

		#region  订单结算上级等等利润分红，用户订单信息添加，查询
		//20160130 add
		/// <summary>
		///  结算流程统计数额金额是否大于等于100否 ，提示不能购买，送货金额至少为100是  进入付款界面
		/// </summary>
		/// <param name="orderno">订单号</param>
		/// <param name="orderprice">订单总价格</param>
		/// <param name="byuserid">购买人ID</param>
		[WebMethod(Description = "结算流程统计数额金额是否大于等于100否 ，提示不能购买，送货金额至少为100是  进入付款界面")]
		public void OrderPayClearing(string orderno, decimal orderprice, int byuserid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				if (orderprice > 100)
					Context.Response.Write("{\"result\":\"true\"");
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"金额少于100,不能购买。\"}");

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		/// <summary>
		///  创建订单	及订单明细表
		/// </summary>
		/// <param name="orderno">生成订单编号</param>
		/// <param name="orderprice">订单金额 decimal(18, 2)</param>
		/// <param name="ordersource">订单来源 可为空</param>
		/// <param name="paymode">支付方式 可为空</param>
		/// <param name="buyername">购买人ID</param>
		/// <param name="ordertime">订单创建时间</param>
		/// <param name="paymenttime">支付时间</param>
		/// <param name="orderstatus">订单状态</param>
		/// <param name="shippindtime">发货时间 可为空</param>
		/// <param name="expressmode">物流方式 可为空</param>
		/// <param name="expressno">物流单号 可为空</param>
		[WebMethod(Description = " 创建订单	及订单明细表")]
		public void OrderCreate(string orderno, decimal orderprice, string ordersource,
string paymode, int buyername, DateTime ordertime, DateTime paymenttime, char orderstatus,
		DateTime shippindtime, string expressmode, string expressno)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				if (!string.IsNullOrEmpty(orderno))
				{
					string sql = "insert into orders(orderno,orderprice ,ordersource,paymode ,buyername,ordertime ,paymenttime,orderstatus,shippindtime ,expressmode,expressno) values('" + orderno + "','" + orderprice + "','" + ordersource + "','" + paymode + "','"
+ buyername + "','" + ordertime + "','" + paymenttime + "','" + orderstatus + "','" + shippindtime + "','" + expressmode + "','" + expressno + "')";
					int result = DbHelperSQL.ExecuteSql(sql);
					if (result > 0)
						Context.Response.Write("{\"result\":\"true\"}");
					else
						Context.Response.Write("{\"result\":\"false\",\"data\":\"生成订单失败\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"没有订单号，生成订单失败\"}");
				}
			}

			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 订单明细表插入(存储商品信息)
		/// </summary>
		/// <param name="orderno">订单编号</param>
		/// <param name="orderprice">订单总金额decimal(18, 2)</param>
		/// <param name="ordertime">订单生成时间</param>
		/// <param name="buygoodsid">商品ID</param>
		/// <param name="buysumqty">购买数量</param>
		/// <param name="goodsprice">商品单价decimal(18, 2)</param>

		[WebMethod(Description = "订单明细表插入(存储商品信息)")]
		public void OrderCreateDetails(string orderno, decimal orderprice, DateTime ordertime,
	int buygoodsid, int buysumqty, decimal goodsprice) //, decimal goodscost, decimal  goodgoldprice, decimal goodsilverprice)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                //取得库存看是否够
				string getGoodStock = "select goodstock,selectytsort from goods  where goodsId='" + buygoodsid + "'";
                DataTable dtGoodStock=new DataTable();
                if (DbHelperSQL.Query(getGoodStock).Tables[0].Rows.Count > 0)
                {
                    dtGoodStock = DbHelperSQL.Query(getGoodStock).Tables[0];
                }
                else
                {
                    string getgoodstuanStock = "select quantity as goodstock,selectytsort from goodstuan  where id='" + buygoodsid + "'";
                    dtGoodStock = DbHelperSQL.Query(getgoodstuanStock).Tables[0];

                }
				string sql = "insert into ordersdetails(orderno ,orderprice ,ordertime,buygoodsid,buysumqty,goodsprice,selectytsort) values('" + orderno + "','" + orderprice + "','" + ordertime + "','" + buygoodsid + "','"
+ buysumqty + "','" + goodsprice + "','"+ dtGoodStock.Rows[0]["selectytsort"].ToString() + "')";//,'" + goodscost + "','" + goodgoldprice + "','" + goodsilverprice + "')";
				int result = DbHelperSQL.ExecuteSql(sql);
				if (result > 0)
				{
					if (dtGoodStock.Rows.Count > 0 && int.Parse(dtGoodStock.Rows[0]["goodstock"].ToString()) > 0)
					{
                        //20160729 del
                        //int shengyustock = int.Parse(dtGoodStock.Rows[0]["goodstock"].ToString()) - buysumqty;
                        //string upTwo = "update   goods set goodstock='" + shengyustock + "' where goodsId = '" + buygoodsid + "'";

                        //if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upTwo)) > 0)
                        //{
                        //    Context.Response.Write("{\"result\":\"true\",\"data\":\"生成订单明细成功\"}");
                        //}
                        Context.Response.Write("{\"result\":\"true\",\"data\":\"生成订单明细成功\"}");
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"此商品库存为0,不能更新为负库存。\"}");
					}
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"生成订单明细失败\"}");
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		/// <summary>
		/// 订单状态状态修改
		///
		[WebMethod(Description = "订单状态改变 1：支付订单/2:发货订单/3:退货/退款中订单/4:完成订单/5:取消订单")]
		public void OrderStatusChange(string orderno, int status,int buyuserid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				var changeinfo = string.Empty;
				switch (status)
				{
					case 1:
						changeinfo = "支付订单";
						break;
					case 2:
						changeinfo = "发货订单";
						break;
					case 3:
						changeinfo = "退货/退款中订单";
						break;
					case 4:
						//2016/07/20 add
						string sqlgetgoodpoint = "SELECT sum(getgoodpoint)sumgetgoodpoint from 	";
						sqlgetgoodpoint += "(SELECT a.orderno, SUM(b.getgoodpoint)getgoodpoint FROM ordersdetails a INNER JOIN goods b on a.buygoodsid = b.goodsId";
						sqlgetgoodpoint += " where a.orderno = '"+orderno+"' GROUP BY a.orderno UNION ";
						sqlgetgoodpoint += "SELECT a.orderno, SUM(b.getgoodpoint)getgoodpoint FROM ordersdetails a INNER JOIN goodstuan b on a.buygoodsid = b.id";
						sqlgetgoodpoint += " where a.orderno = '" + orderno + "' GROUP BY a.orderno) a	";
						DataTable result = DbHelperSQL.Query(sqlgetgoodpoint).Tables[0];
						if (result.Rows.Count > 0)
						{
                            //20160803 add
                            if (int.Parse(result.Rows[0]["sumgetgoodpoint"].ToString()) > 0)
                            {
                                string upOne = @"insert into pointdetails(userid
									,getpoint
									,getpointreason
									,getpointtime) values('" + buyuserid + "','" + result.Rows[0]["sumgetgoodpoint"] + "','订单号:" + orderno + "所购商品获得" + result.Rows[0]["sumgetgoodpoint"] + "积分','" + DateTime.Now + "' )";

                                if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upOne)) > 0)
                                {
                                    //20160721 add
                                    DbHelperSQL.ExecuteSql("update userinfo set  point=point+'" + result.Rows[0]["sumgetgoodpoint"] + "' where id='" + buyuserid + "'");
                                    Context.Response.Write("{\"result\":\"true\",\"data\":\"订单号:" + orderno + "所购商品获得" + result.Rows[0]["sumgetgoodpoint"] + "积分成功\"}");
                                }
                            }
						}
							changeinfo = "完成订单";
						break;
					case 5:
						changeinfo = "取消订单";
						break;
				}
				string sql = "update orders set orderStatus = '" + status + "' where orderno='" + orderno + "' and buyername='"+buyuserid+"'";
				//DbHelperSQL.ExecuteSql(sql);
				if (Convert.ToInt32(DbHelperSQL.ExecuteSql(sql)) > 0)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"" + changeinfo + "成功\"}");
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"" + changeinfo + "失败\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


		//20160203 add
		/// <summary>
		/// 获取用户订单
		/// </summary>
        [WebMethod(Description = "根据用户ID获取用户订单信息 orderstatus:为空就是全部 orderstatus:0：创建订单 1：支付订单/2:发货订单/3:退货/退款中订单/4:完成订单/5:取消订单")]
		public void GetUserOrder(string userid,string  orderstatus)
		{
			try
            { //,(CONVERT(varchar(100), ordertime, 23)+'      '+CONVERT(varchar(100), ordertime, 108))ordertime
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = @"select [orderno]
			,[orderprice]
			,[ordersource]
			,[paymode]
			,[buyername]
	        ,CONVERT(varchar(100), ordertime, 120)ordertime
			,CONVERT(varchar(100), paymenttime, 120)paymenttime
			,[orderstatus]
			,CONVERT(varchar(100), shippindtime, 120)shippindtime
			,[expressmode]
			,[expressno] from orders where buyername = '" + userid + "'";
                if (!string.IsNullOrEmpty(orderstatus))
                {
                    sql += @" and orderstatus='" + orderstatus + "'";
                }
                sql += "   order by ordertime desc";
				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    var datetime = DateTime.Parse(dt.Rows[i]["ordertime"].ToString()).ToString("yyyy-MM-dd            HH:mm:ss");
                    //    dt.Rows[i]["ordertime"] = datetime;
                    //}
                    string[] arr = { "orderno", "orderprice", "ordersource", "paymode",
"buyername", "ordertime", "paymenttime","orderstatus","shippindtime","expressmode","expressno" };
                    string result = Common.Json.DateTableToJson(dt, arr);
                    Context.Response.Write(result);
                }
                else
                {
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"暂无订单信息。\"}");

                }
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}


        //20160721
        /// <summary>
        /// 获取用户订单
        /// </summary>
        [WebMethod(Description = "获取订单详细信息  ")]
        public void GetUserOrderDetails( string orderno)
        {
            try
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string sql = string.Empty;
                sql =  "SELECT a.orderno,b.goodsId,b.goodsTitle,c.sortName, b.goodsImg,b.goodsContent,b.goodsprice,b.goodscost,";
                sql += "a.buysumqty from ordersdetails a INNER JOIN goods b on a.buygoodsid=b.goodsId and a.selectytsort=b.selectytsort";
                sql += " INNER JOIN goodssort c on b.goodssort=c.sortId where a.orderno='"+orderno+"' UNION";

                sql += " SELECT a.orderno,b.id as goodsId,b.title as goodsTitle,c.sortName, b.Img as goodsImg,b.content as goodsContent,b.price as goodsprice,b.costprice as goodscost,";
                sql += " a.buysumqty from ordersdetails a INNER JOIN goodstuan b on a.buygoodsid=b.id and a.selectytsort=b.selectytsort";
                sql += " INNER JOIN goodssort c on b.groupsort=c.sortId where a.orderno='" + orderno + "'";

                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                		for (int i = 0; i < dt.Rows.Count; i++)
					{
	 
						dt.Rows[i]["goodsContent"] = Server.UrlEncode(dt.Rows[i]["goodsContent"].ToString());
					}
                    string[] arr = { "orderno", "goodsId", "goodsTitle", "sortName", "goodsImg", "goodsContent","goodsprice","goodscost","buysumqty"  };
                    string result = Common.Json.DateTableToJson(dt, arr);
                    Context.Response.Write(result);
                }
                else
                {
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"暂无订单详细信息。\"}");

                }
            }
            catch (Exception ex)
            {
                Context.Response.Clear();
                Context.Response.Charset = "utf-8";
                Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
            }
        }
		//20160203 add
		/// <summary>
		/// 获取订单中购买商品积分
		/// </summary>
		/// <param name="buyuserid">购买人</param>
		/// <param name="orderno">来源订单号</param>
		/// <param name="sumpoint">订单中所有商品的总积分</param>
		[WebMethod(Description = "获取订单中购买商品积分")]
		public void UserGetGoodPoint(string buyuserid, string orderno, int sumpoint)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                //20160803 addd
                if (sumpoint > 0)
                {
                    string upOne = @"insert into pointdetails(userid
									,getpoint
									,getpointreason
									,getpointtime) values('" + buyuserid + "','" + sumpoint + "','订单号:" + orderno + "所购商品获得" + sumpoint + "积分','" + DateTime.Now + "' )";

                    if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upOne)) > 0)
                    {
                        //20160721 add
                        DbHelperSQL.ExecuteSql("update userinfo set  point=point+'" + sumpoint + "' where id='" + buyuserid + "'");

                        Context.Response.Write("{\"result\":\"true\",\"data\":\"订单号:" + orderno + "所购商品获得" + sumpoint + "积分成功\"}");
                    }
                }
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160130 add
		/// <summary>
		/// 根据会员类型级是否分销进行分红
		//判断会员是否为银卡或金卡会员
		//否   无三级分销
		//是   三级分销
		//检测其上三级账户
		// 直属上级 转入利润20%
		// 上级的上级 转入利润20%
		// 上级的上级的上级 转入利润10% 
		/// </summary>
		/// <param name="orderno">订单号</param>
		/// <param name="orderprice">订单总价格</param>
		/// <param name="byuserid">购买人ID</param>
		/// 	<param name="ordercost">购买所有商品的总成本价</param>
		[WebMethod(Description = "orderprice,ordercost(可为0) 根据会员类型级是否分销进行分红,<br/>判断会员是否为银卡或金卡会员 否   无三级分销 是   三级分销 检测其上三级账户直属上级 转入利润20% 上级的上级 转入利润20% 上级的上级的上级 转入利润10% ")]
		public void OrderClearingMoney(string orderno, decimal orderprice, decimal ordercost, int buyuserid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select id,invitedcode,shopset from userinfo  where id='" + buyuserid + "'";
				DataTable dtDiscount = DbHelperSQL.Query(sql.ToString()).Tables[0];
				if (dtDiscount.Rows.Count > 0)
				{
					//金银会员有三级分销
					if (!string.IsNullOrEmpty(dtDiscount.Rows[0]["invitedcode"].ToString()))
					{     //根据订单取得所有商品ID进行循环得到提成
						string tichensql = "SELECT a.orderno,a.buygoodsid,b.goodsprice, b.goodscost,b.tichengpoint ";
						tichensql += " from ordersdetails a INNER JOIN goods b on a.buygoodsid=b.goodsId where a.orderno='" + orderno + "'";
						DataTable dtGoodTicheng = DbHelperSQL.Query(tichensql).Tables[0];
						if (dtGoodTicheng.Rows.Count > 0)
						{
							for (int i = 0; i < dtGoodTicheng.Rows.Count; i++)
							{
								//进行分割得到每个等级会员的分红比例
								var tichengpoint = dtGoodTicheng.Rows[i]["tichengpoint"].ToString().Split('/');
								decimal goodsprice = Convert.ToDecimal(dtGoodTicheng.Rows[i]["goodsprice"].ToString());
								decimal goodscost = Convert.ToDecimal(dtGoodTicheng.Rows[i]["goodscost"].ToString());
								//县域账户进行分红
								string sqlxianyu = "select id, fenhongmoney,ifchubei,shopset from superadmin  where shopset='" + dtDiscount.Rows[0]["shopset"].ToString() + "'";
								DataTable dtXianyu = DbHelperSQL.Query(sqlxianyu.ToString()).Tables[0];
								if (dtXianyu.Rows.Count > 0)
								{
									decimal xyFenHong = 0.00m;
									decimal xycbFenHong = 0.00m;
									for (int xy = 0; xy < dtXianyu.Rows.Count; xy++)
									{
										xyFenHong = Convert.ToDecimal(dtXianyu.Rows[xy]["fenhongmoney"]) + ((goodsprice - goodscost) * Convert.ToDecimal(25 / 100));
										xycbFenHong = Convert.ToDecimal(dtXianyu.Rows[xy]["fenhongmoney"]) + ((goodsprice - goodscost) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(50 / 100));
										if (Convert.ToChar(dtXianyu.Rows[xy]["ifchubei"]) == 'Y')
										{
											if (xyFenHong > 0) DbHelperSQL.ExecuteSql("update   superadmin set fenhongmoney='" + xyFenHong + "' where Id = '" + dtXianyu.Rows[xy]["id"].ToString() + "'");
										}
										else
										{
											if (xycbFenHong > 0) DbHelperSQL.ExecuteSql("update   superadmin set fenhongmoney='" + xyFenHong + "' where Id = '" + dtXianyu.Rows[xy]["id"].ToString() + "'");

										}
									}
								}
								decimal sumOneHouseMoney = 0.0m;
								decimal sumTwoHouseMoney = 0.0m; decimal sumThreeHouseMoney = 0.0m;
								decimal fencheng1 = (goodsprice - goodscost) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengpoint[1]) / Convert.ToDecimal(100);
								decimal fencheng2 = (goodsprice - goodscost) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengpoint[2]) / Convert.ToDecimal(100);
								decimal fencheng3 = (goodsprice - goodscost) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengpoint[3]) / Convert.ToDecimal(100);
								DataTable dtOne = ShareProfit(buyuserid.ToString());
								if (dtOne.Rows.Count > 0)
								{

									#region//更新直接上级ID的余额			如userid  5 则上级userid是3
									if (fencheng1 > 0)
									{                                                     //20160220 housemoney=>	fenhongmoney
										sumOneHouseMoney = Convert.ToDecimal(dtOne.Rows[0]["fenhongmoney"].ToString()) + fencheng1;
										//更新新注册用户第一次充值的会员类型
										string upOne = "update   UserInfo set fenhongmoney='" + sumOneHouseMoney + "' where Id = '" + dtOne.Rows[0]["fatherid"].ToString() + "'";
										if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upOne)) > 0)
										{
											//插入我的收入表中记录
											DbHelperSQL.ExecuteSql(@"insert into moenydetails([moneycode]
								 
									,[userid]
									,[money]
									,[moneyreason]
									,[moneytime],orderno,getrecuserid) values('" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "','" + dtOne.Rows[0]["fatherid"].ToString() + "','" + fencheng1 + "','分享订单号:"
																																																				 + orderno + "及购买人ID:" + buyuserid + "的利润分成','" + DateTime.Now + "','" + orderno + "','" + buyuserid + "')");

											Context.Response.Write("{\"result\":\"true\",\"data\":\"sumOneHouseMoney上级用户利润分红成功\"}");
										}
									}
									else { Context.Response.Write("{\"result\":\"false\",\"data\":\"销售价低于成本价不允许利润分红\"}"); }
									#endregion

									// 	如userid  5 则上级userid是3  userid 3 上级是否为空
									if (!string.IsNullOrEmpty(dtOne.Rows[0]["twothreefatherid"].ToString()) || dtOne.Rows[0]["twothreefatherid"].ToString() != "0")
									{
										DataTable dtTwo = ShareProfit(dtOne.Rows[0]["fatherid"].ToString());
										if (dtTwo.Rows.Count > 0)
										{
											#region // //分利润给上级的上级userID //不为空则找到上级userID
											//更新上级的上级	ID的余额
											if (fencheng2 > 0)
											{                                                   //20160220 housemoney=>	fenhongmoney
												sumTwoHouseMoney = Convert.ToDecimal(dtTwo.Rows[0]["fenhongmoney"].ToString()) + fencheng2;
												string upTwo = "update   UserInfo set fenhongmoney='" + sumTwoHouseMoney + "' where Id = '" + dtTwo.Rows[0]["fatherid"].ToString() + "'";

												if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upTwo)) > 0)
												{
													//插入我的收入表中记录
													DbHelperSQL.ExecuteSql(@"insert into moenydetails([moneycode]
								 
									,[userid]
									,[money]
									,[moneyreason]
									,[moneytime],orderno,getrecuserid) values('" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "','" + dtOne.Rows[0]["fatherid"].ToString() + "','" + fencheng2 + "','分享订单号:"
																																									 + orderno + "及购买人ID:" + buyuserid + "的利润分成','" + DateTime.Now + "','" + orderno + "','" + buyuserid + "')");


													Context.Response.Write("{\"result\":\"true\",\"data\":\"sumTwoHouseMoney上级用户利润分红成功\"}");
												}
											}
											else { Context.Response.Write("{\"result\":\"false\",\"data\":\"销售价低于成本价不允许利润分红\"}"); }
											#endregion
											if (!string.IsNullOrEmpty(dtTwo.Rows[0]["twothreefatherid"].ToString()) || dtTwo.Rows[0]["twothreefatherid"].ToString() != "0")
											{
												#region //分利润给上级的上级的上级ID
												DataTable dtThree = ShareProfit(dtTwo.Rows[0]["fatherid"].ToString());
												if (dtThree.Rows.Count > 0)
												{
													if (fencheng3 > 0)
													{ //更新上级的上级的上级ID的余额													//20160220 housemoney=>	fenhongmoney
														sumThreeHouseMoney = Convert.ToDecimal(dtThree.Rows[0]["fenhongmoney"].ToString()) + fencheng3;
														string upThree = "update   UserInfo set fenhongmoney='" + sumThreeHouseMoney + "' where Id = '" + dtThree.Rows[0]["fatherid"].ToString() + "'";
														if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upThree)) > 0)
														{
															//插入我的收入表中记录
															DbHelperSQL.ExecuteSql(@"insert into moenydetails([moneycode]
								 
									,[userid]
									,[money]
									,[moneyreason]
									,[moneytime],orderno,getrecuserid) values('" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "','" + dtOne.Rows[0]["fatherid"].ToString() + "','" + fencheng3 + "','分享订单号:"
																																					 + orderno + "及购买人ID:" + buyuserid + "的利润分成','" + DateTime.Now + "','" + orderno + "','" + buyuserid + "')");
															Context.Response.Write("{\"result\":\"true\",\"data\":\"sumThreeHouseMoney上级用户利润分红成功\"}");

														}
													}
													else { Context.Response.Write("{\"result\":\"false\",\"data\":\"销售价低于成本价不允许利润分红\"}"); }
												}
												#endregion
											}
										}
									}

								}
							}
						}
					}
					else
					{
						#region //如果没有上级邀请码则直接分红给县域储备金账户/资金账户
						//根据订单取得所有商品ID进行循环得到提成
						string tichensql = "SELECT a.orderno,a.buygoodsid,b.goodsprice, b.goodscost,b.tichengpoint ";
						tichensql += " from ordersdetails a INNER JOIN goods b on a.buygoodsid=b.goodsId where a.orderno='" + orderno + "'";
						DataTable dtGoodTicheng = DbHelperSQL.Query(tichensql).Tables[0];
						if (dtGoodTicheng.Rows.Count > 0)
						{
							for (int i = 0; i < dtGoodTicheng.Rows.Count; i++)
							{
								//县域账户进行直接分红给县域储备金账户/资金账户
								string sqlxianyu = "select id, fenhongmoney,ifchubei,shopset from superadmin  where shopset='" + dtDiscount.Rows[0]["shopset"].ToString() + "'";
								DataTable dtXianyu = DbHelperSQL.Query(sqlxianyu.ToString()).Tables[0];
								if (dtXianyu.Rows.Count > 0)
								{
									decimal xyFenHong = 0.00m;
									decimal xycbFenHong = 0.00m;
									decimal goodsprice = Convert.ToDecimal(dtGoodTicheng.Rows[i]["goodsprice"].ToString());
									decimal goodscost = Convert.ToDecimal(dtGoodTicheng.Rows[i]["goodscost"].ToString());
									for (int xy = 0; xy < dtXianyu.Rows.Count; xy++)
									{
										xyFenHong = Convert.ToDecimal(dtXianyu.Rows[xy]["fenhongmoney"]) + ((goodsprice - goodscost) * Convert.ToDecimal(25 / 100));
										xycbFenHong = Convert.ToDecimal(dtXianyu.Rows[xy]["fenhongmoney"]) + ((goodsprice - goodscost) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(50 / 100));
										if (Convert.ToChar(dtXianyu.Rows[xy]["ifchubei"]) == 'Y')
										{
											if (xyFenHong > 0) DbHelperSQL.ExecuteSql("update   superadmin set fenhongmoney='" + xyFenHong + "' where Id = '" + dtXianyu.Rows[xy]["id"].ToString() + "'");
										}
										else
										{
											if (xycbFenHong > 0) DbHelperSQL.ExecuteSql("update   superadmin set fenhongmoney='" + xyFenHong + "' where Id = '" + dtXianyu.Rows[xy]["id"].ToString() + "'");

										}
									}
								}
							}
						}
						#endregion
						Context.Response.Write("{\"result\":\"false\",\"data\":\"无三级分销,直接分红给县域储备金账户/资金账户\"}");
					}
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"取得要分红的上级邀请码失败。\"}");
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}

		}

		/// <summary>
		/// 根据用户ID的上级userid级账户余额
		/// </summary>
		/// <param name="buyuserid"></param>
		/// <returns></returns>
		public DataTable ShareProfit(string buyuserid)
		{
			DataTable retrunData = new DataTable();
			//取得上级UserID级剩余金额							//20160220 housemoney=>	fenhongmoney
			//string sql = "select  b.fenxiaoid as fatherid,isnull(a.fenhongmoney,0.00) as fenhongmoney,isnull(a.fenxiaoid,0) as twothreefatherid,a.tel";
			//sql += " from userinfo a inner join (select a.fenxiaoid  from userinfo a inner join";
			//sql += "(select invitedcode  from userinfo   where id = '" + buyuserid + "') b	 ";
			//sql += "on a.invitedcode = b.invitedcode) b on a.Id = b.fenxiaoid";
			string sql = "select  b.id as fatherid,isnull(a.fenhongmoney,0.00) as fenhongmoney,isnull(a.fenxiaoid,0) as twothreefatherid,a.tel";
			sql += " from userinfo a inner join (select a.id  from userinfo a inner join ";
			sql += "(select fenxiaoid  from userinfo   where id = '" + buyuserid + "') b ";
			sql += "on a.id = b.fenxiaoid) b on a.Id = b.id";
			DataTable dtFenhongMoney = DbHelperSQL.Query(sql.ToString()).Tables[0];
			if (dtFenhongMoney.Rows.Count > 0)
			{
				retrunData = dtFenhongMoney;
			}
			return retrunData;
		}
		#endregion

		#region 用户充值操作/获取用户余额/扣住用户余额/用户提现操作
		/// <summary>
		/// 用户充值操作
		/// </summary>
		/// <param name="recno">流水账号</param>
		/// <param name="tel">电话</param>
		/// <param name="userid">用户ID</param>
		/// <param name="recmoney">充值金额</param>
		/// <param name="content">充值内容</param>
		/// <param name="validate">充值状态</param>
		/// <param name="addtime">充值时间</param>
		[WebMethod(Description = "用户充值操作")]
		public void UserRecharge(string recno, string tel, int userid, decimal recmoney,
	 string content, char validate, DateTime addtime)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = @"insert into userrecharge([recno]
			,[tel]
			,[userid]
			,[recmoney]
			,[content]
			,[validate]
			,[addtime]) values('" + recno + "','" + tel + "','" + userid + "','" + recmoney + "','"
+ content + "','" + validate + "','" + addtime + "')";
				int result = DbHelperSQL.ExecuteSql(sql);
				if (result > 0)
				{
					//20160130	add
					//判断如果是第一次充值(充值额度)则检测邀请码确定其上级userid 对上级userid进行奖励积分
					string getOne = "select b.fenxiaoid from userrecharge a inner join userinfo b on a.userid=b.id where a.userid='" + userid + "'";
					DataTable dt = DbHelperSQL.Query(getOne).Tables[0];
					if (dt.Rows.Count == 1)
					{
						//存放会员等级
						int memberstatus = 1;
						int fatherpoint = 0;
						if (recmoney <= 200)       //单次充值200      普通会员
						{
							memberstatus = 1; fatherpoint = 0;
						}
						else if (recmoney > 200 || recmoney < 5000) //单次充值3000     银卡会员
						{
							memberstatus = 2; fatherpoint = 50;
						}
						else if (recmoney >= 5000)  //单次充值 5000     金卡会员
						{
							memberstatus = 3; fatherpoint = 100;
						}
						//更新新注册用户第一次充值的会员类型
						string upUserType = "update   UserInfo set usertype='" + memberstatus + "' where Id = '" + userid + "'";
						if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upUserType)) > 0)
							Context.Response.Write("{\"result\":\"true\"}");
						else
							Context.Response.Write("{\"result\":\"false\",\"data\":\"更新会员等级失败。\"}");
						if (fatherpoint > 0)
						{
							//查询分销父ID 的积分和userid
							string getFatherPoint = "select point,id from  userinfo  where id='" + int.Parse(dt.Rows[0]["fenxiaoid"].ToString()) + "'";
							DataTable dtPoint = DbHelperSQL.Query(getFatherPoint).Tables[0];
							if (dtPoint.Rows.Count > 0)
							{
								int point = int.Parse(dtPoint.Rows[0]["point"].ToString()) + fatherpoint;
								string upfatherpoint = "update   UserInfo set point='" + point + "' where Id = '" + int.Parse(dtPoint.Rows[0]["id"].ToString()) + "'";
								if (Convert.ToInt32(DbHelperSQL.ExecuteSql(upfatherpoint)) > 0)
									Context.Response.Write("{\"result\":\"true\"}");
								else
									Context.Response.Write("{\"result\":\"false\",\"data\":\"更新上级积分失败。\"}");
							}
							Context.Response.Write("{\"result\":\"false\",\"data\":\"更新上级积分成功\"}");
						}
					}
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"用户充值失败\"}");
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		/// <summary>
		/// 		获取用户账户余额
		/// </summary>
		/// <param name="userid"></param>
		[WebMethod(Description = "根据用户ID获取用户账户余额")]
		public void GetUserBalance(string userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select id,tel,name,housemoney from UserInfo where Id = '" + userid + "'";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];

				string[] arr = { "id", "tel", "name", "housemoney" };
				string result = Common.Json.DateTableToJson(dt, arr);
				Context.Response.Write(result);
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		/// <summary>
		/// 扣除用户余额
		/// </summary>
		/// <param name="userid"></param>
		[WebMethod(Description = "根据用户ID购买商品时扣除账号余额(账号余额-商品总金额buygoodsprice)")]
		public void SubtractUserBalance(string userid, decimal buygoodsprice)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string getsql = "select housemoney from UserInfo where Id='" + userid + "'";
				DataTable dt = DbHelperSQL.Query(getsql).Tables[0];
				if (dt.Rows.Count <= 0)
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取账户余额失败\"}");
					return;
				}
				else
				{
					if (Convert.ToDecimal(dt.Rows[0]["housemoney"]) > 0)
					{
						if (Convert.ToDecimal(dt.Rows[0]["housemoney"]) > buygoodsprice)
						{
							decimal shengxiamoney = Convert.ToDecimal(dt.Rows[0]["housemoney"]) - buygoodsprice;
							string sql = "update   UserInfo set housemoney='" + shengxiamoney + "' where Id = '" + userid + "'";
							int result = DbHelperSQL.ExecuteSql(sql);
							if (result > 0)
								Context.Response.Write("{\"result\":\"true\"}");
							else
								Context.Response.Write("{\"result\":\"false\",\"data\":\"扣除账户余额失败\"}");
						}
						else
						{
							Context.Response.Write("{\"result\":\"false\",\"data\":\"购买金额大于账户余额,无法支付,请充值后付款\"}");
						}
					}
					else
					{
						Context.Response.Write("{\"result\":\"false\",\"data\":\"账户余额不足,请充值后付款\"}");
					}
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		//20160214 add
		/// <summary>
		/// 用户提现操作
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="tel"></param>
		/// <param name="extractmoney">提现金额</param>
		/// <param name="extcardno">银行卡/支付宝等等</param>
		[WebMethod(Description = "根据用户ID及提现金额(账号余额-提现金额)生成提现清单，财务手工转账")]
		public void UserExtractMoeny(string userid, string tel, decimal extractmoney, string extcardno)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string getsql = "select fenhongmoney from UserInfo where Id='" + userid + "'";
				DataTable dt = DbHelperSQL.Query(getsql).Tables[0];
				if (dt.Rows.Count <= 0)
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"获取账户余额失败\"}");
					return;
				}
				else
				{    //20160220 由housemoney修改为fenhongmoney 分红金额字段
					if (Convert.ToDecimal(dt.Rows[0]["fenhongmoney"]) >= extractmoney)
					{
						string insersql = "insert into memberextract(extno,exttel,extuserid,extmoney,extcontent,extcardno,exttime) values('" + DateTime.Now.ToString("yymmssffff") + "','" + tel + "','" + userid + "','" + extractmoney + "','提现','" + extcardno + "','" + DateTime.Now + "')";
						int result = DbHelperSQL.ExecuteSql(insersql);
						if (result > 0)
						{
							decimal tixianmoney = Convert.ToDecimal(dt.Rows[0]["fenhongmoney"]) - extractmoney;
							string sql = "update   UserInfo set fenhongmoney='" + tixianmoney + "' where Id = '" + userid + "'";

							int extresult = DbHelperSQL.ExecuteSql(sql);
							if (extresult > 0)
								Context.Response.Write("{\"result\":\"true\",\"data\":\"提现成功等待财务审核后进行手工打款\"}");
							else
								Context.Response.Write("{\"result\":\"false\",\"data\":\"提现金额失败\"}");
						}
						else
							Context.Response.Write("{\"result\":\"false\",\"data\":\"提现金额失败，未知错误\"}");

					}
					else
						Context.Response.Write("{\"result\":\"false\",\"data\":\"提现金额大于账户余额，提现失败\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion

		#region 获取生成订单编号
		/// <summary>
		/// 获取生成订单编号
		/// </summary>
		[WebMethod(Description = "获取生成订单编号")]
		public void getOrderNo()
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                string result = DateTime.Now.ToString("yyMMddHHmmffff");
                //GetOrderNumber();
				Context.Response.Write(result);
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		private string GetOrderNumber()
		{
			string Number = DateTime.Now.ToString("yyMMddHHmmffff");//yyyyMMddHHmmssms
            return Number+ Next(1000, 1).ToString();
		}
		private static int Next(int numSeeds, int length)
		{
			// Create a byte array to hold the random value.  
			byte[] buffer = new byte[length];
			// Create a new instance of the RNGCryptoServiceProvider.  
			System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
			// Fill the array with a random value.  
			Gen.GetBytes(buffer);
			// Convert the byte to an uint value to make the modulus operation easier.  
			uint randomResult = 0x0;//这里用uint作为生成的随机数  
			for (int i = 0; i < length; i++)
			{
				randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
			}
			// Return the random number mod the number  
			// of sides. The possible values are zero-based  
			return (int)(randomResult % numSeeds);
		}
		#endregion

		#region	获取我的收入及我的积分明细
		[WebMethod(Description = "获取我的收入明细")]
		public void GetUseryPayMoneyDetails(string userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				string sql = "select * from moenydetails where userid = '" + userid + "'";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					string[] arr = { "moneycode", "userid", "orderno", "money", "moneyreason", "moneytime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					Context.Response.Write(result);
				}
				else
				{
					Context.Response.Write("{\"result\":\"false\",\"data\":\"我的收入明细获取失败或暂无收入明细。\"}");
				}
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}

		[WebMethod(Description = "获取我的积分明细")]
		public void GetUseryPointDetails(string userid)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
                //20160803 update
                string sql = "select a.*  from pointdetails a  where a.getpoint<>0 and  a.userid = '" + userid + "'";

				DataTable dt = DbHelperSQL.Query(sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
                    string[] arr = { "Id", "userid", "getpoint", "getpointreason", "getpointtime" };
					string result = Common.Json.DateTableToJson(dt, arr);
					//Context.Response.Write(result);
                    string sqlGetPoint = "select  b.point from userinfo b  where b.id = '" + userid + "'";

										var data="data:" + result;
                    Context.Response.Write("{\"result\":\"true\",\"sumpoint\":\"" +DbHelperSQL.Query(sqlGetPoint).Tables[0] .Rows[0]["point"]+ "\","+ data + "}");
				}
				else
				{
                    Context.Response.Write("{\"result\":\"false\",\"data\":\"我的积分明细获取失败或暂无积分明细\"}");
				}

			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
		#endregion

		/// <summary>
		/// 20160723 add
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="messagename"></param>
		/// <param name="messagetel"></param>
		/// <param name="messagecontent"></param>
		[WebMethod(Description = "意见反馈留言收集整理")]
		public void MessageViewInfo(string userid, string messagename, string messagetel, 	string messagecontent)
		{
			try
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
			 
				string insersql = "insert into messageview(messageuserid,messagename,messagetel,messagecontent)";
				insersql += "values('" + userid + "','" + messagename + "','" + messagetel + "','" + messagecontent + "')";
				int result = DbHelperSQL.ExecuteSql(insersql);
				if (result > 0)
				{
					Context.Response.Write("{\"result\":\"true\",\"data\":\"意见反馈留言成功\"}");
				}
				else
					Context.Response.Write("{\"result\":\"false\",\"data\":\"意见反馈留言失败，未知错误\"}");
			}
			catch (Exception ex)
			{
				Context.Response.Clear();
				Context.Response.Charset = "utf-8";
				Context.Response.AddHeader("content-type", "text/plain; charset=utf-8");
				Context.Response.Write("{\"result\":\"error\",\"data\":\"" + ex.ToString() + "\"}");
			}
		}
	}
}

