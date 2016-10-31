using EnshineUnionManager.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EnshineUnionManager
{
	/// <summary>
	/// EnshineUnionHandler 的摘要说明
	/// </summary>
	public class EnshineUnionHandler : IHttpHandler
	{
		/// <summary>
		/// 登陆状态
		/// </summary>
		private string status = string.Empty;
		/// <summary>
		/// 分页详细信息
		/// </summary>
		private string pageInfo = string.Empty;
		/// <summary>
		/// 每页显示条数
		/// </summary>
		private int pageSize = 20;
		/// <summary>
		/// 增加区分颜色
		/// </summary>
		private int rowCount = 0;

		/// <summary>
		/// 管理员信息
		/// </summary>
		private StringBuilder sbAdminSearchInfo = new StringBuilder();
		/// <summary>
		/// 用户信息
		/// </summary>
		private StringBuilder sbUserSearchInfo = new StringBuilder();
		//广告
		private StringBuilder sbAdSearchInfo = new StringBuilder();
		//新闻信息
		private StringBuilder sbNewsSearchInfo = new StringBuilder();
		/// <summary>
		///新闻分类
		/// </summary>
		private StringBuilder sbNewsSortSearchInfo = new StringBuilder();
		/// <summary>
		///新闻评论信息
		/// </summary>
		private StringBuilder sbNewsCommentSearchInfo = new StringBuilder();

		/// <summary>
		/// 活动信息
		/// </summary>
		private StringBuilder sbHuoDongSearchInfo = new StringBuilder();

		/// <summary>
		///活动分类
		/// </summary>
		private StringBuilder sbHuoDongSortSearchInfo = new StringBuilder();

		/// <summary>
		/// 参与活动信息
		/// </summary>
		private StringBuilder sbJoinHuoDongSearchInfo = new StringBuilder();

		/// <summary>
		/// 参与活动报名信息
		/// </summary>
		private StringBuilder sbJoinHuoDongInfoSearchInfo = new StringBuilder();
		/// <summary>
		/// 商品
		/// </summary>
		private StringBuilder sbGoodsSearchInfo = new StringBuilder();


		/// <summary>
		/// 商品分类
		/// </summary>
		private StringBuilder sbGoodsSortSearchInfo = new StringBuilder();

		/// <summary>
		/// 团购商品
		/// </summary>
		private StringBuilder sbGroupBuySearchInfo = new StringBuilder();
		/// <summary>
		/// 订单列表
		/// </summary>
		private StringBuilder sbOrderListSearchInfo = new StringBuilder();

		/// <summary>
		/// 植保分类
		/// </summary>
		private StringBuilder sbPlantDoctorSortSearchInfo = new StringBuilder();

		/// <summary>
		/// 植保医院信息
		/// </summary>
		private StringBuilder sbPlantDoctoSearchInfo = new StringBuilder();

		/// <summary>
		/// 用户充值信息
		/// </summary>
		private StringBuilder sbUserRechargeSearchInfo = new StringBuilder();

		/// <summary>
		/// 角色权限设置
		/// </summary>
		private StringBuilder sbUserRoleSearchInfo = new StringBuilder();



		/// <summary>
		/// 会员列表信息
		/// </summary>
		private StringBuilder sbMemberSearchInfo = new StringBuilder();

		/// <summary>
		/// 我的收入明细
		/// </summary>
		private StringBuilder sbMyPayMoneySearchInfo = new StringBuilder();

		/// <summary>
		/// 我的积分明细
		/// </summary>
		private StringBuilder sbMyPointSearchInfo = new StringBuilder();

		/// <summary>
		/// 月销售金额报表统计信息
		/// </summary>
		private StringBuilder sbFinanceMonthSearchInfo = new StringBuilder();
		/// <summary>
		/// 当月分销提成金额报表
		/// </summary>
		private StringBuilder sbFenXiaoExtractSearchInfo = new StringBuilder();

		/// <summary>
		/// 当月会员提现报表
		/// </summary>
		private StringBuilder sbMemberExtractSearchInfo = new StringBuilder();

		/// <summary>
		/// 会员提现管理
		/// </summary>
		private StringBuilder sbMemberExtractManagerSearchInfo = new StringBuilder();

		/// <summary>
		/// 店铺设置管理
		/// </summary>
		private StringBuilder sbShopManagerSearchInfo = new StringBuilder();
		/// <summary>
		/// 厂家核对报表
		/// </summary>
		private StringBuilder sbSaleCheckSearchInfo = new StringBuilder();
		/// <summary>
		/// 商品发货表
		/// </summary>
		private StringBuilder sbGoodsShipSearchInfo = new StringBuilder();


		/// <summary>
		/// i.	地区分析
		/// </summary>
		private StringBuilder sbUserAreaSearchInfo = new StringBuilder();
		/// <summary>
		/// ii.	年龄、性别等信息分析
		/// </summary>
		private StringBuilder sbUserAgeSexSearchInfo = new StringBuilder();

		/// <summary>
		/// i.	商品i.	销售排行榜
		/// </summary>
		private StringBuilder sbGoodSalesSearchInfo = new StringBuilder();
		/// <summary>
		/// ii.	地区消费排行榜
		/// </summary>
		private StringBuilder sbGoodAreaSalesSearchInfo = new StringBuilder();

		/// <summary>
		/// i.	商品i.iv.	商品销售数据检索
		/// </summary>
		private StringBuilder sbGoodSortSalesSearchInfo = new StringBuilder();
		/// <summary>
		/// ii.iv.	商品销售数据检索
		/// </summary>
		private StringBuilder sbGoodSalesSearchSearchInfo = new StringBuilder();


		/// <summary>
		/// i.	i.	订单分类分析
		/// </summary>
		private StringBuilder sbUserOrderTypeFenXiSearchInfo = new StringBuilder();
		/// <summary>
		/// ii.ii.	用户消费分析
		/// </summary>
		private StringBuilder sbUserOrderSalesFenXiSearchInfo = new StringBuilder();

		/// <summary>
		/// 	意见收集整理
		/// </summary>
		private StringBuilder sbMessageViewSearchInfo = new StringBuilder();

		/// <summary>
		/// 	分销层级分析
		/// </summary>
		private StringBuilder sbMemberJiBieFenXiSearchInfo = new StringBuilder();

		/// <summary>
		/// 	商品库龄分析
		/// </summary>
		private StringBuilder sbGoodsStockAgeFenXiSearchInfo = new StringBuilder();

		/// <summary>
		/// 	发货商品管理
		/// </summary>
		private StringBuilder sbGoodsDeliverySearchInfo = new StringBuilder();

		/// <summary>
		/// 	剩余商品库存
		/// </summary>
		private StringBuilder sbGoodsSyStockSearchInfo = new StringBuilder();

		/// <summary>
		/// 	入库商品管理
		/// </summary>
		private StringBuilder sbGoodsInWareHouseSearchInfo = new StringBuilder();

		/// <summary>
		/// 	套餐商品管理
		/// </summary>
		private StringBuilder sbGoodsPackageSearchInfo = new StringBuilder();
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			HttpCookie cookies = new HttpCookie("UserLogin");
			#region 处理登陆
			if (!string.IsNullOrEmpty(context.Request["Login"]))
			{
				string[] parm = context.Request["Login"].Split('_');
				//var loginPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(parm[1], "MD5");
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var login = from p in db.superadmin
											join pp in db.userrole on p.roleid equals pp.roleid
into g
											from c in g.DefaultIfEmpty()
											where p.name == parm[0] && p.pass == parm[1] //loginPwd.ToString()
											select new { p.name, p.pass, p.ifdisable, p.roleid, c.powerid, c.rolename };

					if (login.Count() > 0)
					{
						//更新登陆时间和IP
						var spUpdate = db.superadmin.Single(p => p.name == parm[0] && p.pass == parm[1]);// loginPwd.ToString());            ///	lastloginip = GetIPAddress(),
						spUpdate.lastlogintime = DateTime.Now;
						spUpdate.lastloginip = GetIPAddress();
						db.SubmitChanges();

						foreach (var item in login)
						{
							status = "Success";

							cookies["ClientName"] = context.Server.UrlEncode(item.name.Trim());
							cookies["ClientPass"] = context.Server.UrlEncode(item.pass.Trim());
							cookies["UserRole"] = item.powerid.Trim();
							cookies["IfDisable"] = item.ifdisable.ToString();
							cookies.Expires = System.DateTime.Now.AddHours(24);
							context.Response.Cookies.Add(cookies);
						}
					}
					else
					{
						status = "LoginFailure";//登陆信息错误及账号不存在
					}
					context.Response.Write(status);
				}
			}
			#endregion

			#region//修改密码
			if (!string.IsNullOrEmpty(context.Request["UpPwd"]))
			{
				//获得用户输入！ 
				string[] parm = context.Request["UpPwd"].Split('_');
				string oldPwd = parm[1]; //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(parm[1], "MD5");
				string newPwd = parm[2]; //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(parm[2], "MD5");
				string newConfirmPwd = parm[3]; //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(parm[3], "MD5");
																				//查询时候密码是否存在是否正确
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var result = from p in db.UserInfo
											 where p.tel.Trim() == parm[0] && p.pass.Trim() == oldPwd
											 select p;
					if (result.Count() > 0)
					{
						if (oldPwd != newPwd && oldPwd != newConfirmPwd)
						{
							UserInfo cl = db.UserInfo.Single(a => a.tel == parm[0]);
							cl.pass = newPwd;
							db.SubmitChanges();
							status = "Success";
						}
						else
						{
							status = "Failure";
						}
					}
				}
				context.Response.Write(status);
				context.Response.End();
			}
			#endregion

			#region 管理员查询及删除信息

			if (!string.IsNullOrEmpty(context.Request["ShowAdminInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetAdminInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteAdminInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					superadmin delNot = db.superadmin.Single(x => x.Id == Convert.ToInt32(context.Request["DeleteAdminInfo"]));
					db.superadmin.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetAdminInfo("1"));
			}
			#endregion

			#region	 角色权限设置
			if (!string.IsNullOrEmpty(context.Request["ShowUserRoleInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetUserRoleInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteRoleInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					userrole delRole = db.userrole.Single(x => x.roleid == Convert.ToInt32(context.Request["DeleteRoleInfo"]));
					db.userrole.DeleteOnSubmit(delRole);
					db.SubmitChanges();
				}
				context.Response.Write(GetUserRoleInfo("1"));
			}
			#endregion

			#region 用户信息增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowUserInfo"]) &&
					!string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowUserInfo"].Split(',');
				context.Response.Write(GetUserInfo(strParm[0], strParm[1], strParm[2], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["EditUserInfo"]))
			{
				string[] strParm = context.Request["EditUserInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					UserInfo updateOK = db.UserInfo.Single(x => x.Id == Convert.ToInt32(strParm[0]));
					//updateOK.community = int.Parse(strParm[1]);
					//updateOK.name = strParm[2];
					//updateOK.tel = strParm[3];
					db.SubmitChanges();
				}
				context.Response.Write(GetUserInfo("", "", "", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteUserInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					UserInfo delOK = db.UserInfo.Single(x => x.Id == Convert.ToInt32(context.Request["DeleteUserInfo"]));
					db.UserInfo.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetUserInfo("", "", "", "1"));
			}

			if (!string.IsNullOrEmpty(context.Request["GetUserInfo"]))
			{
				var returnUserInfo = string.Empty;
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{

					var getUserInfo = db.UserInfo.Where(x => x.tel == context.Request["GetUserInfo"]).Select(x => new { x.Id, x.name });
					if (getUserInfo.Count() > 0)
						foreach (var item in getUserInfo)
						{
							returnUserInfo = item.name.ToString() + "," + item.Id.ToString();
						}
					else
						returnUserInfo = "NoData";

				}
				context.Response.Write(returnUserInfo);
			}
			//获取上级站长 股东
			if (!string.IsNullOrEmpty(context.Request["GetShangJiUserInfo"]))
			{
				var returnUserInfo = string.Empty;
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var getUserInfo = ReturnSJGDZZ(context.Request["GetShangJiUserInfo"]);
					if (getUserInfo.Count() > 0)
					{
						foreach (var item in getUserInfo)
						{
							if (!string.IsNullOrEmpty(item.SjGuDong) && !string.IsNullOrEmpty(item.SjZhanZhang))
							{
								returnUserInfo = item.SjGuDong + "," + item.SjZhanZhang;
							}
							else
							{
								#region 邀请码的上级股东 和站长为空时
								//基本如4，3，2，1			（4是注册者,3是邀请码 ）
								//如果邀请码上级是股东 则 上级股东和站长都是同一个手机号码
								if (item.JueSe == "股东" && item.GuDongJiBie == "原始股东")    //1
								{
									returnUserInfo = item.Tel + "," + item.Tel;
								}
								else if (item.JueSe == "站长")                               //2
								{
									var shangjigudong = item.InvitedCode;//ReturnSJGDZZ(item.InvitedCode).Single().Tel;
									returnUserInfo = shangjigudong + "," + item.Tel;
								}
								//1888888888=》上级 13475329478(会员) =>18661966213(会员) =》18153236010（站长)=>17762020698股东
								else if (item.JueSe == "会员")    //邀请码上级是会员				 3
								{
									var shangji1 = ReturnSJGDZZ(item.InvitedCode).Single().Tel;
									var shangjuese = ReturnSJGDZZ(item.InvitedCode).Single().JueSe;  //在上一级依然是会员
									if (shangjuese == "会员")                         //找3上级2结果依然是3 会员		
									{
										var shangincode1 = ReturnSJGDZZ(shangji1).Single().InvitedCode;
										//	var shangincode2 = ReturnSJGDZZ(shangincode1).Single().InvitedCode;
										if (ReturnSJGDZZ(shangincode1).Single().JueSe == "站长") //		 找到3上面是2
										{
											var shangjigudong = ReturnSJGDZZ(shangincode1).Single().InvitedCode;
											returnUserInfo = shangjigudong + "," + shangincode1;
										}
										//1888888888=》上级 13475329478(会员) =>18661966213(会员)=>17762020698股东

										else         //股东		 //		 找到3上面是1
										{
											//var shangjigudong = ReturnSJGDZZ(shangincode1).Single().Tel;
											returnUserInfo = shangincode1 + "," + shangincode1; //shangjigudong + "," + shangjigudong;
										}
									}
									else if (shangjuese == "站长")      //在上一级是站长
									{
										var shangjigudong = ReturnSJGDZZ(shangji1).Single().Tel;
										returnUserInfo = shangjigudong + "," + shangji1;
									}
									else                      //在上一级是股东
									{
										var shangjigudong = ReturnSJGDZZ(shangji1).Single().Tel;
										returnUserInfo = shangjigudong + "," + shangjigudong;
									}
								}

								#endregion
							}
						}
					}
					else { returnUserInfo += context.Request["GetShangJiUserInfo"] + "," + context.Request["GetShangJiUserInfo"]; }
				}
				context.Response.Write(returnUserInfo);
			}

			#endregion

			#region 广告信息管理及删除
			if (!string.IsNullOrEmpty(context.Request["ShowAdInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowAdInfo"].Split('_');
				context.Response.Write(GetAdInfo(strParm[0], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["DeleteAdInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					Advertisement delCom = db.Advertisement.Single(x => x.AdID == Convert.ToInt32(context.Request["DeleteAdInfo"]));
					db.Advertisement.DeleteOnSubmit(delCom);
					db.SubmitChanges();
				}
				context.Response.Write(GetAdInfo("", "1"));
			}
			#endregion

			#region 新闻信息查询及删除信息

			if (!string.IsNullOrEmpty(context.Request["ShowNoticesInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetNoticesInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteNoticesInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					notices delNot = db.notices.Single(x => x.Id == Convert.ToInt32(context.Request["DeleteNoticesInfo"]));
					db.notices.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetNoticesInfo("1"));
			}
			#endregion

			#region 新闻分类增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowNewsSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowNewsSortInfo"].Split('_');
				context.Response.Write(GetNewsSortInfo(strParm[0], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["AddNewsSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["AddNewsSortInfo"].Split('_');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					noticessort addOk = new noticessort();
					addOk.sortName = strParm[0];

					addOk.addTime = DateTime.Now;
					db.noticessort.InsertOnSubmit(addOk); db.SubmitChanges();
				}
				context.Response.Write(GetNewsSortInfo("", context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["EditNewsSortInfo"]))
			{
				string[] strParm = context.Request["EditNewsSortInfo"].Split(',');

				// 产品档案SKU 修改
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					noticessort updateOK = db.noticessort.Single(x => x.sortId == Convert.ToInt32(strParm[0]));
					updateOK.sortName = strParm[1];

					db.SubmitChanges();
				}
				context.Response.Write(GetNewsSortInfo("", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteNewsSortInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					noticessort delOK = db.noticessort.Single(x => x.sortId == Convert.ToInt32(context.Request["DeleteNewsSortInfo"]));
					db.noticessort.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetNewsSortInfo("", "1"));
			}
			#endregion

			#region 新闻评论信息查询及删除信息

			if (!string.IsNullOrEmpty(context.Request["ShowNewsCommentInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetNewsCommentInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteNewsCommentInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//2016/07/06 add
					//删除评论。相应的 新闻评论数也要减少
					var newid = db.noticesdiscuss.Single(x => x.discussid == Convert.ToInt32(context.Request["DeleteNewsCommentInfo"])).noticeid;
					var discussnum = db.notices.Single(x => x.Id == newid).discussnum;
					if (discussnum > 0)
					{
						var numcount = int.Parse(discussnum.ToString()) - 1;
						notices updiscussnum = db.notices.Single(x => x.Id == newid);
						updiscussnum.discussnum = numcount;
						db.SubmitChanges();
					}
					noticesdiscuss delNot = db.noticesdiscuss.Single(x => x.discussid == Convert.ToInt32(context.Request["DeleteNewsCommentInfo"]));
					db.noticesdiscuss.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetNewsCommentInfo("1"));
			}
			#endregion

			#region 活动查询及删除
			if (!string.IsNullOrEmpty(context.Request["ShowHuoDongInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowHuoDongInfo"].Split(',');
				context.Response.Write(GetHuoDongInfo(strParm[0], strParm[1], context.Request["currPage"]));
			}
			///审核活动信息
			if (!string.IsNullOrEmpty(context.Request["ActiveHuoDongInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HuoDong delOK = db.HuoDong.Single(x => x.huodongId == Convert.ToInt32(context.Request["ActiveHuoDongInfo"]));
					delOK.huodongActive = 'Y';
					db.SubmitChanges();
				}
				context.Response.Write(GetHuoDongInfo("", "", "1"));
			}
			///删除活动信息
			if (!string.IsNullOrEmpty(context.Request["DeleteHuoDongInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HuoDong delOK = db.HuoDong.Single(x => x.huodongId == Convert.ToInt32(context.Request["DeleteHuoDongInfo"]));
					db.HuoDong.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetHuoDongInfo("", "", "1"));
			}
			#endregion

			#region 活动分类增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowHuoDongSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowHuoDongSortInfo"].Split('_');
				context.Response.Write(GetHuoDongSortInfo(strParm[0], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["AddHuoDongSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["AddHuoDongSortInfo"].Split('_');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HuoDongSort addOk = new HuoDongSort();
					addOk.sortName = strParm[0];

					addOk.addTime = DateTime.Now;
					db.HuoDongSort.InsertOnSubmit(addOk); db.SubmitChanges();
				}
				context.Response.Write(GetHuoDongSortInfo("", context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["EditHuoDongSortInfo"]))
			{
				string[] strParm = context.Request["EditHuoDongSortInfo"].Split(',');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HuoDongSort updateOK = db.HuoDongSort.Single(x => x.sortId == Convert.ToInt32(strParm[0]));
					updateOK.sortName = strParm[1];

					db.SubmitChanges();
				}
				context.Response.Write(GetHuoDongSortInfo("", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteHuoDongSortInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HuoDongSort delOK = db.HuoDongSort.Single(x => x.sortId == Convert.ToInt32(context.Request["DeleteHuoDongSortInfo"]));
					db.HuoDongSort.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetHuoDongSortInfo("", "1"));
			}
			#endregion

			#region 参与活动查询及删除
			if (!string.IsNullOrEmpty(context.Request["ShowJoinHuoDong"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowJoinHuoDong"].Split('_');
				context.Response.Write(GetJoinHuoDong(strParm[0], context.Request["currPage"]));
			}

			///删除参与活动信息
			if (!string.IsNullOrEmpty(context.Request["DeleteJoinHuoDong"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					JoinHuoDong delOK = db.JoinHuoDong.Single(x => x.Id == Convert.ToInt32(context.Request["DeleteJoinHuoDong"]));
					db.JoinHuoDong.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetJoinHuoDong("", "1"));
			}
			#endregion

			#region 参与活动报名信息显示及删除
			if (!string.IsNullOrEmpty(context.Request["ShowJoinHuoDongInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowJoinHuoDongInfo"].Split('_');
				context.Response.Write(GetJoinHuoDongInfo(strParm[0], context.Request["currPage"]));
			}

			///删除参与活动报名信息
			if (!string.IsNullOrEmpty(context.Request["DeleteJoinHuoDongInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					JoinHuoDongInfo delOK = db.JoinHuoDongInfo.Single(x => x.id == Convert.ToInt32(context.Request["DeleteJoinHuoDongInfo"]));
					db.JoinHuoDongInfo.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetJoinHuoDongInfo("", "1"));
			}
			#endregion


			#region 参与预售/团购 活动查询及删除
			if (!string.IsNullOrEmpty(context.Request["ShowJoinGoodsHuoDongInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowJoinGoodsHuoDongInfo"].Split(',');
				context.Response.Write(GetJoinGoodsHuoDongInfo(strParm[0], strParm[1], context.Request["currPage"]));
			}
 
			///删除活动信息
			if (!string.IsNullOrEmpty(context.Request["DeleteJoinGoodsHuoDongInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					JoinHuoDong delOK = db.JoinHuoDong.Single(x => x.Id == Convert.ToInt32(context.Request["DeleteJoinGoodsHuoDongInfo"]));
					db.JoinHuoDong.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetJoinGoodsHuoDongInfo("", "", "1"));
			}
			#endregion

			#region 商品查询信息及删除

			if (!string.IsNullOrEmpty(context.Request["ShowGoodsInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{

				string[] strParm = context.Request["ShowGoodsInfo"].Split(';');
                context.Response.Write(GetGoodsInfo(strParm[0], strParm[1], strParm[2], strParm[3], strParm[4], strParm[5], context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteGoodsInfo"]))
            {
                string[] strParm = context.Request["DeleteGoodsInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
                    goods delNot = db.goods.Single(x => x.goodsId == Convert.ToInt32(strParm[1]));
					db.goods.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetGoodsInfo("", strParm[0],"", "", "", "", "1"));
			}
			///审核活动信息
			if (!string.IsNullOrEmpty(context.Request["ActiveGoodsInfo"]))
            {
                string[] strParm = context.Request["ActiveGoodsInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
                    goods actOK = db.goods.Single(x => x.goodsId == Convert.ToInt32(strParm[1]));
					actOK.goodsvalidate = 'N';
					db.SubmitChanges();
				}
				context.Response.Write(GetGoodsInfo("",strParm[0],"", "", "", "", "1"));
			}
			//获取商品分销比例
			if (!string.IsNullOrEmpty(context.Request["GetTiChengPoint"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var getinfo = from p in db.goods where p.goodsId == Convert.ToInt32(context.Request["GetTiChengPoint"]) select new { p.tichengpoint, p.goodsSales, p.ifexchange, p.ifxiangou };
					foreach (var item in getinfo)
					{
						if (!string.IsNullOrEmpty(item.tichengpoint))
						{
							context.Response.Write(item.tichengpoint + "/" + item.goodsSales + "/" + item.ifexchange + "/" + item.ifxiangou);
						}
						else
						{
							context.Response.Write("0/20/20/10" + "/" + item.goodsSales + "/" + item.ifexchange + "/" + item.ifxiangou);
						}
					}
					//var tichengpoint = db.goods.Single(x => x.goodsId == Convert.ToInt32(context.Request["GetTiChengPoint"])).tichengpoint;
					//if (!string.IsNullOrEmpty(tichengpoint))
					//{
					//    context.Response.Write(tichengpoint);
					//}
					//else
					//{
					//    context.Response.Write("0/20/20/10");
					//}
				}
			}
			#endregion

			#region 套餐商品查询信息及删除

			if (!string.IsNullOrEmpty(context.Request["ShowGoodsPackageInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{

				string[] strParm = context.Request["ShowGoodsPackageInfo"].Split(';');
				context.Response.Write(GetGoodsPackageInfo(strParm[0], strParm[1], strParm[2], strParm[3], strParm[4], context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteGoodsPackageInfo"]))
			{
				string[] strParm = context.Request["DeleteGoodsPackageInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					goodspackage delNot = db.goodspackage.Single(x => x.goodsId == Convert.ToInt32(strParm[1]));
					db.goodspackage.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetGoodsPackageInfo( strParm[0],"", "", "", "",  "1"));
			}
			///审核活动信息
			if (!string.IsNullOrEmpty(context.Request["ActiveGoodsPackageInfo"]))
			{
				string[] strParm = context.Request["ActiveGoodsPackageInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					goodspackage actOK = db.goodspackage.Single(x => x.goodsId == Convert.ToInt32(strParm[1]));
					actOK.goodsvalidate = 'N';
					db.SubmitChanges();
				}
				context.Response.Write(GetGoodsPackageInfo(strParm[0], "", "", "", "",    "1"));
			}
			//获取商品分销比例
			if (!string.IsNullOrEmpty(context.Request["GetGoodsPackTiChengPoint"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var getinfo = from p in db.goodspackage where p.goodsId == Convert.ToInt32(context.Request["GetGoodsPackTiChengPoint"])
												select new { p.tichengpoint, p.goodsSales, p.ifexchange, p.ifxiangou };
					foreach (var item in getinfo)
					{
						if (!string.IsNullOrEmpty(item.tichengpoint))
						{
							context.Response.Write(item.tichengpoint + "/" + item.goodsSales + "/" + item.ifexchange + "/" + item.ifxiangou);
						}
						else
						{
							context.Response.Write("0/20/20/10" + "/" + item.goodsSales + "/" + item.ifexchange + "/" + item.ifxiangou);
						}
					}
					//var tichengpoint = db.goods.Single(x => x.goodsId == Convert.ToInt32(context.Request["GetTiChengPoint"])).tichengpoint;
					//if (!string.IsNullOrEmpty(tichengpoint))
					//{
					//    context.Response.Write(tichengpoint);
					//}
					//else
					//{
					//    context.Response.Write("0/20/20/10");
					//}
				}
			}
			#endregion

			#region 商品分类增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowGoodsSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowGoodsSortInfo"].Split('_');
				context.Response.Write(GetGoodsSortInfo(strParm[0], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["AddGoodsSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["AddGoodsSortInfo"].Split(',');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//取得选择父分类的sortpath以便更新新添加分类path
					var sortPath = db.goodssort.Single(x =>
									x.sortId == Convert.ToInt32(strParm[0])).sortPath;
					//判断三级分类只能三级
					if (sortPath.Split(',').Length <= 3)
					{
						goodssort addOk = new goodssort();
						addOk.sortFatherId = int.Parse(strParm[0]);
						addOk.sortName = strParm[1];

						addOk.addTime = DateTime.Now;
						db.goodssort.InsertOnSubmit(addOk); db.SubmitChanges();
						//取得新添加分类ID
						var sortId = db.goodssort.Max(x => x.sortId);

						//获取选择父分类的sortSonId（更新时默认本ID）
						//var sortSonId=db.plantdoctorsort.Single(x =>
						//     x.sortId == Convert.ToInt32(drpPlantSort.SelectedValue)).sortSonId;
						goodssort upSonID = db.goodssort.Single(x => x.sortId == Convert.ToInt32(strParm[0]));
						upSonID.sortSonId = sortId;
						goodssort updateOK = db.goodssort.Single(x => x.sortId == Convert.ToInt32(sortId));
						if (!string.IsNullOrEmpty(sortPath) && sortPath != "-1")
							updateOK.sortPath = sortPath + "," + sortId;
						else
						{
							updateOK.sortPath = strParm[0] + "," + sortId;
						}
						db.SubmitChanges();
						//	goodssort addOk = new goodssort();
						//addOk.sortName = strParm[0];

						//addOk.addTime = DateTime.Now;
						//db.goodssort.InsertOnSubmit(addOk); db.SubmitChanges();
						context.Response.Write(GetGoodsSortInfo("", context.Request["currPage"]));

					}
					else
					{
						context.Response.Write("Warning");
					}
				}
			}
			if (!string.IsNullOrEmpty(context.Request["EditGoodsSortInfo"]))
			{
				string[] strParm = context.Request["EditGoodsSortInfo"].Split(',');

				// 产品档案SKU 修改
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					goodssort updateOK = db.goodssort.Single(x => x.sortId == Convert.ToInt32(strParm[0]));
					updateOK.sortFatherId = int.Parse(strParm[1]);
					updateOK.sortName = strParm[2];
					if (db.goodssort.Single(x =>
					 x.sortId == int.Parse(strParm[1])).sortFatherId != int.Parse(strParm[1]))
					{
						updateOK.sortPath = db.goodssort.Single(x =>
						x.sortId == int.Parse(strParm[1])).sortPath + "," + strParm[0];
					}

					db.SubmitChanges();
					//goodssort updateOK = db.goodssort.Single(x => x.sortId == Convert.ToInt32(strParm[0]));
					//updateOK.sortName = strParm[1];

					//db.SubmitChanges();
				}
				context.Response.Write(GetGoodsSortInfo("", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteGoodsSortInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					goodssort delOK = db.goodssort.Single(x => x.sortId == Convert.ToInt32(context.Request["DeleteGoodsSortInfo"]));
					db.goodssort.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetGoodsSortInfo("", "1"));
			}
			#endregion

			#region  团购信息列表管理查询删除

			if (!string.IsNullOrEmpty(context.Request["ShowGroupBuyInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
			 
                string[] strParm = context.Request["ShowGroupBuyInfo"].Split(';');
                context.Response.Write(GetGroupBuyInfo(strParm[0], strParm[1], strParm[2], strParm[3], strParm[4], strParm[5], context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteGroupBuyInfo"]))
			{
                string[] strParm = context.Request["DeleteGroupBuyInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    goodstuan delTuant = db.goodstuan.Single(x => x.Id == Convert.ToInt32(strParm[1]));
					db.goodstuan.DeleteOnSubmit(delTuant);
					db.SubmitChanges();
				}
                context.Response.Write(GetGroupBuyInfo("",strParm[0], "", "", "", "", "1"));
			}
            ///审核活动信息
			if (!string.IsNullOrEmpty(context.Request["ActiveGroupGoodsInfo"]))
			{
                string[] strParm = context.Request["ActiveGroupGoodsInfo"].Split(',');
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    goodstuan actOK = db.goodstuan.Single(x => x.Id == Convert.ToInt32(strParm[1]));
					actOK.validate = 'N';
					db.SubmitChanges();
				}
				context.Response.Write(GetGroupBuyInfo("",strParm[0],"", "", "", "", "1"));
			}
			//获取商品分销比例
            if (!string.IsNullOrEmpty(context.Request["GetGroupTiChengPoint"]))
            {
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    var tichengpoint = db.goodstuan.Single(x => x.Id == Convert.ToInt32(context.Request["GetGroupTiChengPoint"]));
                   if (!string.IsNullOrEmpty(tichengpoint.ToString()))
                        {
                            context.Response.Write(tichengpoint);
                        }
                        else
                        {
                            context.Response.Write("0/20/20/10" );
                        }
                }
            }
			#endregion


			#region  订单列表管理查询确认

			if (!string.IsNullOrEmpty(context.Request["ShowOrderListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowOrderListInfo"].Split(',');
				context.Response.Write(GetOrderListInfo(strParm[0], strParm[1], strParm[2], context.Request["currPage"]));


			}
			if (!string.IsNullOrEmpty(context.Request["OkOrderListInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//更新订单状态
					char[] charSeparators = new char[] { ';' };
					string[] strParm = context.Request["OkOrderListInfo"].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					foreach (var soID in strParm)
					{
						orders upOrderEatSatus = db.orders.Single(x => x.orderno.Contains(soID));
						upOrderEatSatus.orderstatus = '2';
						db.SubmitChanges();
                        //发货后进行对商品库存减少
                        var goodsinfo=from p in db.ordersdetails where p.orderno==soID select p;
                        var goodstock = 0;//库存数量
                        int shengyustock = 0;
                        foreach (var ordergoods in goodsinfo)
                        {
                            if (ordergoods.selectytsort == 'P' || ordergoods.selectytsort == 'T')
                            {
                                goodstock = db.goods.Single(x => x.goodsId == ordergoods.buygoodsid && x.selectytsort == ordergoods.selectytsort).goodstock;
                                shengyustock = goodstock - ordergoods.buysumqty;
                                goods upstock = db.goods.Single(x => x.goodsId==ordergoods.buygoodsid);
                                upstock.goodstock = shengyustock;
                                db.SubmitChanges();
                            }
                            else
                            {
                                goodstock = Convert.ToInt32(db.goodstuan.Single(x => x.Id == ordergoods.buygoodsid && x.selectytsort == ordergoods.selectytsort).quantity);
                                shengyustock = goodstock - ordergoods.buysumqty;
                                goodstuan upstock = db.goodstuan.Single(x => x.Id == ordergoods.buygoodsid);
                                upstock.quantity = shengyustock;
                                db.SubmitChanges();
                            }
                        }

					}
				}
				context.Response.Write(GetOrderListInfo("", "", "", "1"));
			}
			//删除单条订单
			if (!string.IsNullOrEmpty(context.Request["DeleteOneOrderInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					orders delOK = db.orders.Single(x => x.orderno == context.Request["DeleteOneOrderInfo"]);
					db.orders.DeleteOnSubmit(delOK);
					ordersdetails deldetails = db.ordersdetails.Single(x => x.orderno == context.Request["DeleteOneOrderInfo"]);
					db.ordersdetails.DeleteOnSubmit(deldetails);
					db.SubmitChanges();

				}
				context.Response.Write(GetOrderListInfo("", "", "", "1"));
			}
			//批量删除订单
			if (!string.IsNullOrEmpty(context.Request["DeleteAllOrderInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//更新订单状态
					char[] charSeparators = new char[] { ';' };
					string[] strParm = context.Request["DeleteAllOrderInfo"].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					foreach (var soID in strParm)
					{
						orders delOK = db.orders.Single(x => x.orderno == soID);
						db.orders.DeleteOnSubmit(delOK);
						ordersdetails deldetails = db.ordersdetails.Single(x => x.orderno == soID);
						db.ordersdetails.DeleteOnSubmit(deldetails);
						db.SubmitChanges();
					}
				}
				context.Response.Write(GetOrderListInfo("", "", "", "1"));
			}
			#endregion

			#region 植保分类增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowPlantDoctorSortInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowPlantDoctorSortInfo"].Split('_');
				context.Response.Write(GetPlantDoctorSortInfo(strParm[0], context.Request["currPage"]));
			}

		 
			if (!string.IsNullOrEmpty(context.Request["DeletePlantDoctorSortInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					plantdoctorsort delOK = db.plantdoctorsort.Single(x => x.sortId == Convert.ToInt32(context.Request["DeletePlantDoctorSortInfo"]));
					db.plantdoctorsort.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetPlantDoctorSortInfo("", "1"));
			}
			#endregion

			#region 植保医院信息查询及删除信息

			if (!string.IsNullOrEmpty(context.Request["ShowPlantDoctorInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetPlantDoctorInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeletePlantDoctorInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					plantdoctor delNot = db.plantdoctor.Single(x => x.Id == Convert.ToInt32(context.Request["DeletePlantDoctorInfo"]));
					db.plantdoctor.DeleteOnSubmit(delNot);
					db.SubmitChanges();
				}
				context.Response.Write(GetPlantDoctorInfo("1"));
			}
			#endregion

			#region 用户充值显示
			if (!string.IsNullOrEmpty(context.Request["ShowUserRechargeInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowUserRechargeInfo"].Split(',');
				context.Response.Write(GetUserRechargeListInfo(strParm[0], strParm[1], strParm[2], context.Request["currPage"]));
			}
			//撤销充值
			if (!string.IsNullOrEmpty(context.Request["CancelRecInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					string[] strParm = context.Request["CancelRecInfo"].Split(',');
					var usermoney = db.UserInfo.SingleOrDefault(x => x.tel == strParm[1]).housemoney;
					var userpoint = db.UserInfo.SingleOrDefault(x => x.tel == strParm[1]).point;
					var recmoeny = db.userrecharge.SingleOrDefault(x => x.recno == strParm[0]).recmoney;
					var recpoint = db.pointdetails.SingleOrDefault(x => x.recno == strParm[0]).getpoint;
					UserInfo delNot = db.UserInfo.Single(x => x.tel == strParm[1]);
					if (usermoney > 0) { delNot.housemoney = Convert.ToDecimal(usermoney) - Convert.ToDecimal(recmoeny); }
					if (userpoint > 0) { delNot.point = userpoint - recpoint; }
					db.SubmitChanges();
					userrecharge upNot = db.userrecharge.Single(x => x.recno == strParm[0]);

					upNot.validate = 'C';
					db.SubmitChanges();
				}
				context.Response.Write(GetUserRechargeListInfo("", "", "", "1"));
			}
			//删除撤销后的充值记录
			if (!string.IsNullOrEmpty(context.Request["DeleteRecInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					string[] strParm = context.Request["DeleteRecInfo"].Split(',');
					userrecharge delNot = db.userrecharge.Single(x => x.recno == strParm[0]);

					db.userrecharge.DeleteOnSubmit(delNot);
					////moenydetails delMoney = db.moenydetails.Single(x => x.recno == strParm[0]);

					////db.moenydetails.DeleteOnSubmit(delMoney);
					pointdetails delPoint = db.pointdetails.Single(x => x.recno == strParm[0]);

					db.pointdetails.DeleteOnSubmit(delPoint);
					db.SubmitChanges();
				}
				context.Response.Write(GetUserRechargeListInfo("", "", "", "1"));
			}
			#endregion

			#region	 角色权限设置
			if (!string.IsNullOrEmpty(context.Request["ShowUserRoleInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetUserRoleInfo(context.Request["currPage"]));

			}
			if (!string.IsNullOrEmpty(context.Request["DeleteRoleInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					userrole delRole = db.userrole.Single(x => x.roleid == Convert.ToInt32(context.Request["DeleteRoleInfo"]));
					db.userrole.DeleteOnSubmit(delRole);
					db.SubmitChanges();
				}
				context.Response.Write(GetUserRoleInfo("1"));
			}
			#endregion

			#region 会员增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowMemberInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowMemberInfo"].Split('_');
				context.Response.Write(GetMemberInfo(strParm[0], context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["AddMemberInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["AddMemberInfo"].Split('_');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					memberset addOk = new memberset();
					addOk.membername = strParm[0];
					addOk.discount = strParm[1];
					addOk.addTime = DateTime.Now;
					db.memberset.InsertOnSubmit(addOk); db.SubmitChanges();
				}
				context.Response.Write(GetMemberInfo("", context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["EditMemberInfo"]))
			{
				string[] strParm = context.Request["EditMemberInfo"].Split(',');

				// 产品档案SKU 修改
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					memberset updateOK = db.memberset.Single(x => x.memberid == Convert.ToInt32(strParm[0]));
					updateOK.membername = strParm[1];
					updateOK.discount = strParm[2];
					db.SubmitChanges();
				}
				context.Response.Write(GetMemberInfo("", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteMemberInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					memberset delOK = db.memberset.Single(x => x.memberid == Convert.ToInt32(context.Request["DeleteMemberInfo"]));
					db.memberset.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetMemberInfo("", "1"));
			}
			#endregion

			#region 我的收入明细/积分明细
			if (!string.IsNullOrEmpty(context.Request["ShowMyPayMoneyInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				HttpCookie getCookies = context.Request.Cookies["UserLogin"];
				context.Response.Write(GetMyPayMoneyInfo(context.Request["currPage"], getCookies["ClientName"]));
			}
			if (!string.IsNullOrEmpty(context.Request["ShowMyPointInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				HttpCookie getCookies = context.Request.Cookies["UserLogin"];
				context.Response.Write(GetMyPointInfo(context.Request["currPage"], getCookies["ClientName"]));
			}
			#endregion

			#region  当月销售金额报表/当月分销提成金额报表/会员提现报表报表/会员提现管理及生成提现清单
			//当月销售金额报表
			if (!string.IsNullOrEmpty(context.Request["ShowwFinanceMonthListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowwFinanceMonthListInfo"].Split(',');
				context.Response.Write(GetFinanceMonthListInfo(strParm[0], context.Request["currPage"]));

			}
			//当月分销提成金额报表
			if (!string.IsNullOrEmpty(context.Request["ShowFenXiaoExtractInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowFenXiaoExtractInfo"].Split(',');
				context.Response.Write(GetFenXiaoExtractInfo(strParm[0], context.Request["currPage"]));
			}
			//当月c)	会员提现报表报表
			if (!string.IsNullOrEmpty(context.Request["ShowMemberExtractInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowMemberExtractInfo"].Split(',');
				context.Response.Write(GetMemberExtractInfo(strParm[0], context.Request["currPage"]));
			}
			//会员提现管理及生成提现清单
			if (!string.IsNullOrEmpty(context.Request["ShowUserExtractListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetUserExtractListInfo(context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["OkCreateExtractListInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//更新订单状态
					char[] charSeparators = new char[] { ';' };
					string[] strParm = context.Request["OkCreateExtractListInfo"].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					StringBuilder sbExtractList = new StringBuilder();
					foreach (var soID in strParm)
					{
						var getInfo = from a in db.memberextract
													join b in db.UserInfo on new { extuserid = a.extuserid } equals new { extuserid = b.Id }
													where
														a.extno == soID.Trim()
													select new
													{
														b.name,
														a.exttel,
														a.extmoney,
														a.extcardno
													};
						foreach (var item in getInfo)
						{
							var info = "姓名:" + item.name.Trim() + "电话:" + item.exttel.Trim() + "提现金额:" + item.extmoney + "转账账号:" + item.extcardno;
							sbExtractList.Append(info + "<br/>");
						}

					}
					extractlist exinsert = new extractlist();
					exinsert.extlistid = DateTime.Now.ToString("yyHHmmssffff");
					exinsert.extlistinfo = sbExtractList.ToString();
					exinsert.createtime = DateTime.Now;
					db.extractlist.InsertOnSubmit(exinsert); db.SubmitChanges();
				}
				context.Response.Write(GetUserExtractListInfo("1"));
			}
			#endregion

			#region 门店店铺增删改查询
			if (!string.IsNullOrEmpty(context.Request["ShowShopInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetShopInfo("", context.Request["currPage"]));
			}

			if (!string.IsNullOrEmpty(context.Request["AddShopInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["AddShopInfo"].Split(',');

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					shopset addOk = new shopset();
					addOk.shopname = strParm[0];
					addOk.shoptel = strParm[1];
					addOk.addtime = DateTime.Now;
					db.shopset.InsertOnSubmit(addOk); db.SubmitChanges();
				}
				context.Response.Write(GetShopInfo("", context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["EditShopInfo"]))
			{
				string[] strParm = context.Request["EditShopInfo"].Split(',');

				// 产品档案SKU 修改
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					shopset updateOK = db.shopset.Single(x => x.shopCode == Convert.ToInt32(strParm[0]));
					updateOK.shopname = strParm[1];
					updateOK.shoptel = strParm[2];
					db.SubmitChanges();
				}
				context.Response.Write(GetShopInfo("", "1"));
			}
			if (!string.IsNullOrEmpty(context.Request["DeleteShopInfo"]))
			{

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					shopset delOK = db.shopset.Single(x => x.shopCode == Convert.ToInt32(context.Request["DeleteShopInfo"]));
					db.shopset.DeleteOnSubmit(delOK);
					db.SubmitChanges();
				}
				context.Response.Write(GetShopInfo("", "1"));
			}
			#endregion

			#region 厂家核对报表查询
			if (!string.IsNullOrEmpty(context.Request["ShowSaleCheckInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowSaleCheckInfo"].Split(',');

				context.Response.Write(GetSaleCheckInfo(strParm[0], strParm[1], context.Request["currPage"]));
			}

			#endregion

			#region 商品发货表查询
			if (!string.IsNullOrEmpty(context.Request["ShowGoodsShipInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				string[] strParm = context.Request["ShowGoodsShipInfo"].Split(',');

				context.Response.Write(GetGoodsShipInfo(strParm[0], strParm[1], strParm[2], strParm[3],
										strParm[4], strParm[5], strParm[6], strParm[7], strParm[8], context.Request["currPage"]));
			}
			if (!string.IsNullOrEmpty(context.Request["OkGoodShipInfo"]))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//更新订单状态
					char[] charSeparators = new char[] { ';' };
					string[] strParm = context.Request["OkGoodShipInfo"].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
					var shipno = "FH" + DateTime.Now.ToString("HHmmssffff");
					foreach (var soID in strParm)
					{
						ordersdetails upOrderEatSatus = db.ordersdetails.Single(x => x.ordersid == int.Parse(soID));
						upOrderEatSatus.goodshipstatus = '1';
						upOrderEatSatus.goodshipno = shipno;
						db.SubmitChanges();
					}
				}
				context.Response.Write(GetGoodsShipInfo("", "", "", "", "", "", "", "", "", "1"));
			}
			#endregion

			#region //h)	用户信息分析
			//i.	地区分析
			if (!string.IsNullOrEmpty(context.Request["ShowUserAreaListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
	 
				context.Response.Write(GetUserAreaInfo(context.Request["currPage"]));
			}
			//ii.	年龄、性别等信息分析
			if (!string.IsNullOrEmpty(context.Request["ShowUserAgeSexListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetUserAgeSexInfo(context.Request["currPage"]));
			}
			#endregion

			#region //h)	商品销售信息分析
			//i.	i.	销售排行榜
			if (!string.IsNullOrEmpty(context.Request["ShowGoodSalesTotalListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{

				context.Response.Write(GetGoodSalesInfo(context.Request["currPage"]));
			}
			//ii.	iii.	地区消费排行榜
			if (!string.IsNullOrEmpty(context.Request["ShowGoodAreaSalesTotalListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetGoodAreaSalesInfo(context.Request["currPage"]));
			}

			//i.	i.	ii.	分类排行榜
			if (!string.IsNullOrEmpty(context.Request["ShowGoodSortSalesListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{

				context.Response.Write(GetGoodSortSalesInfo(context.Request["currPage"]));
			}
			//ii.	iv.	商品销售数据检索
			if (!string.IsNullOrEmpty(context.Request["ShowGoodSalesSearchListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetGoodSalesSearchInfo(context.Request["currPage"]));
			}

			#endregion


			#region g)	用户订单分析
			//i.	i.	订单分类分析
			if (!string.IsNullOrEmpty(context.Request["ShowUserOrderTypeFenXiListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{

				context.Response.Write(GetUserOrderTypeFenXiSearchInfo(context.Request["currPage"]));
			}
			//ii.ii.	用户消费分析
			if (!string.IsNullOrEmpty(context.Request["ShowUserOrderSalesFenXiListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetUserOrderSalesFenXiSearchInfo(context.Request["currPage"]));
			}
			#endregion

			//ii.	意见收集整理
			if (!string.IsNullOrEmpty(context.Request["ShowMessageViewListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
			{
				context.Response.Write(GetMessageViewSearchInfo(context.Request["currPage"]));
			}

            //分销层级分析
            if (!string.IsNullOrEmpty(context.Request["ShowMemberJiBieFenXiListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
            {
                context.Response.Write(GetMemberJiBieFenXiSearchInfo(context.Request["currPage"]));
            }
            #region 库存管理
            //商品库龄分析
            if (!string.IsNullOrEmpty(context.Request["ShowGoodsStockAgeFenXiListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
            {
                context.Response.Write(GetGoodsStockAgeFenXiSearchInfo(context.Request["currPage"]));
            }
            //发货商品管理
            if (!string.IsNullOrEmpty(context.Request["ShowGoodsDeliveryListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
            {
                context.Response.Write(GetGoodsDeliverySearchInfo(context.Request["currPage"]));
            }
            //剩余商品库存
            if (!string.IsNullOrEmpty(context.Request["ShowGoodsSyStockListInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
            {
                context.Response.Write(GetGoodsSyStockSearchInfo(context.Request["currPage"]));
            }
            //入库商品信息及删除

            if (!string.IsNullOrEmpty(context.Request["ShowGoodsInWareHouseInfo"]) && !string.IsNullOrEmpty(context.Request["currPage"]))
            {

                string[] strParm = context.Request["ShowGoodsInWareHouseInfo"].Split(';');
                context.Response.Write(GetGoodsInWareHouseInfo(strParm[0], strParm[1], strParm[2], strParm[3], context.Request["currPage"]));

            }
            if (!string.IsNullOrEmpty(context.Request["DeleteGoodsInWareHouseInfo"]))
            {
                string[] strParm = context.Request["DeleteGoodsInWareHouseInfo"].Split(',');
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    goods delNot = db.goods.Single(x => x.goodsId == Convert.ToInt32(strParm[1]));
                    db.goods.DeleteOnSubmit(delNot);
                    db.SubmitChanges();
                }
                context.Response.Write(GetGoodsInfo("", strParm[0], "", "", "", "", "1"));
            }
         
            #endregion
            DateTime dt = DateTime.Now;
			DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
			DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末

			#region // 每天商品销售数统计
			if (!string.IsNullOrEmpty(context.Request["goodsCharts"]))
			{
				//图例名称
				var legend = string.Empty;
				//饼状图数据 省份及统计的数据
				StringBuilder sbPieData = new StringBuilder();
				sbPieData.Append("[");
				//using (var writer = new StreamWriter(@"E:\linq.sql", false, Encoding.UTF8))
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					//	db.Log = writer;

					List<DaysGoodsSum> getDaysGoodsData = (
																	 from t0 in db.orders
																	 join tt in db.ordersdetails on t0.orderno equals tt.orderno
																	 join t1 in db.goods on new { buygoodsid = tt.buygoodsid } equals new { buygoodsid = t1.goodsId }
																	 where t0.ordertime >= dt.Date.Date &&
																												 t0.ordertime <= DateTime.Parse(dt.Date.ToString("yyyy-MM-dd 23:59:59"))

																	 group new { t1, tt } by new
																	 {
																		 t1.goodstitle
																	 } into g
																	 select new DaysGoodsSum
																	 {
																		 GoodsTitle = g.Key.goodstitle,
																		 GoodsSum = g.Sum(p => p.tt.buysumqty)
																	 }).ToList<DaysGoodsSum>();
					if (getDaysGoodsData.Count > 0)
					{
						//设置数据
						foreach (var item in getDaysGoodsData)
						{
							legend += "'" + item.GoodsTitle + ":" + item.GoodsSum + "件',";
							sbPieData.Append("{value:'" + item.GoodsSum + "',name:'" + item.GoodsTitle + ":" + item.GoodsSum + "件'},");
						}
						var getPieData = sbPieData.ToString().TrimEnd(',') + "]_[" + legend.TrimEnd(',') + "]";

						//将List转换为Json数据并Response返回新对象
						context.Response.Write(getPieData.ToJson());
					}
					else
					{
						var getPieData = "[{value:'0',name:'暂无商品销售:0件'}]_['暂无商品销售:0件']";

						//	将List转换为Json数据并Response返回新对象
						context.Response.Write(getPieData.ToJson());
					}
				}


			}
			#endregion

			#region // 本月每天订单数统计
			if (!string.IsNullOrEmpty(context.Request["ordersCharts"]))
			{
				//图表的category是字符串数组形式显示
				List<string> categoryList = new List<string>();//{"周一","周二", "周三", "周四", "周五","周六"};
																											 //图表的series数据为一个对象数组 需定义一个series的类
				List<Series> seriesList = new List<Series>();

				//Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
				List<string> legendList = new List<string>();
				//设置legend数组
				legendList.Add("每天订单数(单)"); //这里的名称必须和series类里面的name保持一致

				//定义一个Series对象
				Series seriesObj = new Series();
				seriesObj.name = "每天订单数(单)";
				seriesObj.type = "bar"; //柱状图显示,可以是其他
				seriesObj.data = new List<int>(); //初始化seriesObj.data 否则data.Add(x)添加时会报错

				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					List<MonethOrdersNo> getMonthData = (from t in db.orders
																							 where
																								 t.ordertime >= startMonth.Date.Date && t.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59"))

																							 group t by new
																							 {
																								 Day = t.ordertime.Date
																							 } into g
																							 select new MonethOrdersNo
																							 {
																								 Day = g.Key.Day,
																								 DayOrderNo = g.Count()
																							 }).OrderBy(x => x.Day).ToList<MonethOrdersNo>();
					//设置数据
					for (int i = 0; i < getMonthData.Count(); i++)
					{
						//加入category数组
						categoryList.Add(getMonthData[i].Day.Value.ToString("yyyy-MM-dd"));
						//为series序列数组中data添加数据
						seriesObj.data.Add(getMonthData[i].DayOrderNo);
					}

				}
				//将sereis对象压入sereis数组列表内
				seriesList.Add(seriesObj);

				//结果显示需要category和series、legend多个对象 因此new一个新的对象来封装返回的多个对象
				var newObj = new
				{
					category = categoryList,
					series = seriesList,
					legend = legendList
				};
				//将List转换为Json数据并Response返回新对象
				context.Response.Write(newObj.ToJson());
			}
			#endregion
			context.Response.End();
		}
		/// <summary>
		/// 管理员信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetAdminInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getAdminData = SearchDataClass.GetSearchAdminInfoData();
			if (getAdminData.ToList().Count > 0)
			{
				pageInfo = getAdminData.ToList().Count + "," + pageSize + "," + currPage + "," + (getAdminData.ToList().Count % pageSize == 0 ?
(getAdminData.ToList().Count / pageSize) : (getAdminData.ToList().Count / pageSize + 1));

				var lstGetNoticesItemData = (getAdminData.ToList().Count > pageSize ? getAdminData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getAdminData.ToList()).ToList();
				foreach (var item in lstGetNoticesItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbAdminSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbAdminSearchInfo.Append(@"<td><a class='btn btn-info' href='AdminEditInfo.aspx?mid=3,7&&upid=" + item.AdminID + "'>编辑</a> ");
					sbAdminSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteAdminInfo('" + item.AdminID + "," + item.AdminName + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbAdminSearchInfo.Append(@"<td>" + item.AdminName + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.AdminPassWord + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.RoleName + "</td>");


					var strVal = item.IfDisable == 'Y' ? "禁用" : "启用";
					sbAdminSearchInfo.Append(@"<td>" + strVal + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.TrueName + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.ContactTel + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.Lastlogintime + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.Lastloginip + "</td>");
					sbAdminSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbAdminSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}
		/// <summary>
		/// 检索用户信息数据增删改
		/// </summary>
		/// <param name="strCommunityC">社区名称</param>
		/// <param name="chValidate">是否有效</param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetUserInfo(string strNickName, string strName, string strInvitedcode, string currPage)
		{
			var returnResult = string.Empty;
			var getUserData = SearchDataClass.GetSearchUserInfoData(strNickName, strName, strInvitedcode);
			if (getUserData.ToList().Count > 0)
			{
				pageInfo = getUserData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserData.ToList().Count % pageSize == 0 ?
(getUserData.ToList().Count / pageSize) : (getUserData.ToList().Count / pageSize + 1));

				var lstGetUserItemData = (getUserData.ToList().Count > pageSize ? getUserData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserData.ToList()).ToList();
				foreach (var item in lstGetUserItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserSearchInfo.Append(@"<tr class='" + cssName + "'>");
					var editinfo = item.ID + "," + item.Nickname + "," + item.Name + "," + item.Tel;
                    sbUserSearchInfo.Append(@"<td><a class='btn btn-info' href='PerfectUserInfo.aspx?upid=" + item.ID + "&mid=8,9'>编辑</a>  ");
					var delinfo = item.ID + "," + item.Nickname;
					sbUserSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteUserInfo('" + delinfo + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbUserSearchInfo.Append(@"<td>" + item.Nickname + "</td>");
					sbUserSearchInfo.Append(@"<td >" + item.Name + "</td>");
					sbUserSearchInfo.Append(@"<td>" + item.Tel + "</td>");
					sbUserSearchInfo.Append(@"<td>" + item.PassWord + "</td>");
					var strSex = item.Sex == 'N' ? "女" : "男";
					sbUserSearchInfo.Append(@"<td>" + strSex + "</td>");
					sbUserSearchInfo.Append(@"<td>" + item.Areacity + "</td>");
					//var imgUrl = item.HeadImg == null ? "assets/images/nophoto.gif" : item.HeadImg;
					//sbUserSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:60px;height:60px;'/></td>");

					sbUserSearchInfo.Append(@"<td>" + item.Address + "</td>");
					sbUserSearchInfo.Append(@"<td>" + item.InvitedCode + "</td>");
					//var strUserType = item.UserType == '0' ? "甲方" : "乙方";
					//sbUserSearchInfo.Append(@"<td>" + strUserType + "</td>");
					//sbUserSearchInfo.Append(@"<td>" + item.Point + "</td>");
					sbUserSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbUserSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 检索广告信息数据增删
		/// </summary>
		/// <param name="strCommunityC">社区名称</param>
		/// <param name="chValidate">是否有效</param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetAdInfo(string strWhereAll, string currPage)
		{
			var returnResult = string.Empty;
			var getAdData = SearchDataClass.GetSearchAdData(strWhereAll);
			if (getAdData.ToList().Count > 0)
			{
				pageInfo = getAdData.ToList().Count + "," + pageSize + "," + currPage + "," + (getAdData.ToList().Count % pageSize == 0 ?
(getAdData.ToList().Count / pageSize) : (getAdData.ToList().Count / pageSize + 1));

				var lstGetAdItemData = (getAdData.ToList().Count > pageSize ? getAdData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getAdData.ToList()).ToList();
				foreach (var item in lstGetAdItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbAdSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbAdSearchInfo.Append(@"<td><a class='btn btn-info' href='NewAdAdd.aspx?mid=10,11&upid=" + item.ID + "'>编辑</a> ");
					sbAdSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteAdInfo('" + item.ID + "," + item.AdTitle + "') style='width: 60px; height: 28px;'  value='删除' /></td>");

					sbAdSearchInfo.Append(@"<td >" + item.AdTitle + "</td>");
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					var splitContent = Subfilter(item.AdContent).Length > 30 ? Subfilter(item.AdContent).Substring(0, 30) + "...." : item.AdContent;

					sbAdSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");
					if (!string.IsNullOrEmpty(item.AdImg))
						sbAdSearchInfo.Append(@"<td><img src='" + item.AdImg + "' style='width:90px;height:90px;'/></td>");
					else
						sbAdSearchInfo.Append(@"<td><img src='assets/images/nophoto.gif' style='width:90px;height:90px;'/></td>");
					var strIndex = item.SetIndex == 'N' ? "否" : "是";
					sbAdSearchInfo.Append(@"<td>" + strIndex + "</td>");
					sbAdSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbAdSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");
					rowCount++;
				}
				returnResult = sbAdSearchInfo + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}



		/// <summary>
		/// 新闻/公告信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetNoticesInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getNoticesData = SearchDataClass.GetSearchNoticesInfoData();
			if (getNoticesData.ToList().Count > 0)
			{
				pageInfo = getNoticesData.ToList().Count + "," + pageSize + "," + currPage + "," + (getNoticesData.ToList().Count % pageSize == 0 ?
(getNoticesData.ToList().Count / pageSize) : (getNoticesData.ToList().Count / pageSize + 1));

				var lstGetNoticesItemData = (getNoticesData.ToList().Count > pageSize ? getNoticesData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getNoticesData.ToList()).ToList();
				foreach (var item in lstGetNoticesItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbNewsSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbNewsSearchInfo.Append(@"<td><a class='btn btn-info' href='NewsNoticesAdd.aspx?mid=12,14&&upid=" + item.ID + "'>编辑</a> ");
					sbNewsSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteNoticesInfo('" + item.ID + "," + HttpContext.Current.Server.UrlEncode(item.Title) + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbNewsSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbNewsSearchInfo.Append(@"<td>" + item.NewsSort + "</td>");
					sbNewsSearchInfo.Append(@"<td>" + item.NewsSource + "</td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbNewsSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");
					var imgUrl = item.Img == "" ? "assets/images/nophoto.gif" : item.Img;
					sbNewsSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:90px;height:90px;'/></td>");

					//var strVal = item.Validate == 'N' ? "否" : "是";
					//sbNewsSearchInfo.Append(@"<td>" + strVal + "</td>");
					//var strIndex = item.SetIndex == 'N' ? "否" : "是";
					//sbNewsSearchInfo.Append(@"<td>" + strIndex + "</td>");
					sbNewsSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbNewsSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}
		/// <summary>
		///新闻分类信息数据增删改
		/// </summary>
		/// <param name="strCommunityC">社区名称</param>
		/// <param name="chValidate">是否有效</param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetNewsSortInfo(string strNewsSort, string currPage)
		{
			var returnResult = string.Empty;
			var getNewsSortData = SearchDataClass.GetSearchNewsSortData(strNewsSort);
			if (getNewsSortData.ToList().Count > 0)
			{
				pageInfo = getNewsSortData.ToList().Count + "," + pageSize + "," + currPage + "," + (getNewsSortData.ToList().Count % pageSize == 0 ?
(getNewsSortData.ToList().Count / pageSize) : (getNewsSortData.ToList().Count / pageSize + 1));

				var lstGetNewsSortItemData = (getNewsSortData.ToList().Count > pageSize ? getNewsSortData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getNewsSortData.ToList()).ToList();
				foreach (var item in lstGetNewsSortItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbNewsSortSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbNewsSortSearchInfo.Append(@"<td>" + item.NewsSortName + "</td>");
					sbNewsSortSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ID + "," + item.NewsSortName;
					sbNewsSortSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditNewsSortInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbNewsSortSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteNewsSortInfo('" + item.ID + "," + item.NewsSortName + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbNewsSortSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 新闻/公告评论信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetNewsCommentInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getNewsCommentData = SearchDataClass.GetSearchNewsCommentInfoData();
			if (getNewsCommentData.ToList().Count > 0)
			{
				pageInfo = getNewsCommentData.ToList().Count + "," + pageSize + "," + currPage + "," + (getNewsCommentData.ToList().Count % pageSize == 0 ?
(getNewsCommentData.ToList().Count / pageSize) : (getNewsCommentData.ToList().Count / pageSize + 1));

				var lstGetNoticesItemData = (getNewsCommentData.ToList().Count > pageSize ? getNewsCommentData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getNewsCommentData.ToList()).ToList();
				foreach (var item in lstGetNoticesItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbNewsCommentSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbNewsCommentSearchInfo.Append(@"<td><input type='button' class='btn btn-info'  onclick=DeleteNewsCommentInfo('" + item.ID + "," + HttpContext.Current.Server.UrlEncode(item.Title) + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbNewsCommentSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbNewsCommentSearchInfo.Append(@"<td>" + item.NewsSort + "</td>");
					sbNewsCommentSearchInfo.Append(@"<td>" + item.DiscussPeople + "</td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbNewsCommentSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");

					sbNewsCommentSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbNewsCommentSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetHuoDongInfo(string strTitle, string strActive, string currPage)
		{
			var returnResult = string.Empty;
			var getHuoDongData = SearchDataClass.GetSearchHuoDongData(strTitle, strActive);
			if (getHuoDongData.ToList().Count > 0)
			{
				pageInfo = getHuoDongData.ToList().Count + "," + pageSize + "," + currPage + "," + (getHuoDongData.ToList().Count % pageSize == 0 ?
(getHuoDongData.ToList().Count / pageSize) : (getHuoDongData.ToList().Count / pageSize + 1));

				var lstGetHuoDongItemData = (getHuoDongData.ToList().Count > pageSize ? getHuoDongData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getHuoDongData.ToList()).ToList();
				foreach (var item in lstGetHuoDongItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbHuoDongSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbHuoDongSearchInfo.Append(@"<td><div style='padding-bottom:1px; '><a class='btn btn-info' href='NewHuoDongAdd.aspx?mid=16,18&&upid=" + item.ID + "'>编辑</a></div><br/> ");
					//var display = item.HuodongActive == 'N' ? "" : "disabled='disabled'";
					//sbHuoDongSearchInfo.Append(@"<input type='button' class='btn btn-info' " + display + "  onclick=ActiveHuoDongInfo('" + item.ID + "," + item.HuodongTitle + "') style='width: 60px; height: 28px;'  value='审核' />  ");

					sbHuoDongSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteHuoDongInfo('" + item.ID + "," + item.HuodongTitle + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbHuoDongSearchInfo.Append(@"<td>" + item.HuodongTitle + "</td>");
					sbHuoDongSearchInfo.Append(@"<td>" + item.HuodongSort + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongPeople + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongKeyWord + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongDate + "</td>");
					if (!string.IsNullOrEmpty(item.HuodongImg))
						sbHuoDongSearchInfo.Append(@"<td><img src='" + item.HuodongImg + "' style='width:90px;height:90px;'/></td>");
					else
						sbHuoDongSearchInfo.Append(@"<td><img src='assets/images/nophoto.gif' style='width:90px;height:90px;'/></td>");

					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					var splitContent = Subfilter(item.HuodongContent).Length > 30 ? Subfilter(item.HuodongContent).Substring(0, 30) + "...." : item.HuodongContent;

					sbHuoDongSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");


					//var varActive = item.HuodongActive == 'N' ? "未审核" : "已审核";
					//sbHuoDongSearchInfo.Append(@"<td>" + varActive + "</td>");
					//var varSetIndex = item.SetIndex == 'N' ? "否" : "是";
					//sbHuoDongSearchInfo.Append(@"<td>" + varSetIndex + "</td>");
					sbHuoDongSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbHuoDongSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		///活动分类信息数据增删改
		/// </summary>
		/// <param name="strNewsSort">新闻分类</param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetHuoDongSortInfo(string strHuoDongSort, string currPage)
		{
			var returnResult = string.Empty;
			var getHuoDongData = SearchDataClass.GetSearchHuoDongSortData(strHuoDongSort);
			if (getHuoDongData.ToList().Count > 0)
			{
				pageInfo = getHuoDongData.ToList().Count + "," + pageSize + "," + currPage + "," + (getHuoDongData.ToList().Count % pageSize == 0 ?
(getHuoDongData.ToList().Count / pageSize) : (getHuoDongData.ToList().Count / pageSize + 1));

				var lstGetHuoDongSortItemData = (getHuoDongData.ToList().Count > pageSize ? getHuoDongData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getHuoDongData.ToList()).ToList();
				foreach (var item in lstGetHuoDongSortItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbHuoDongSortSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbHuoDongSortSearchInfo.Append(@"<td>" + item.NewsSortName + "</td>");
					sbHuoDongSortSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ID + "," + item.NewsSortName;
					sbHuoDongSortSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditHuoDongSortInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbHuoDongSortSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteHuoDongSortInfo('" + item.ID + "," + item.NewsSortName + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbHuoDongSortSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}
		/// <summary>
		/// 参与活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetJoinHuoDong(string strWhereAll, string currPage)
		{
			var returnResult = string.Empty;
			var getJoinHuoDongData = SearchDataClass.GetSearchJoinHuoDongData(strWhereAll);
			if (getJoinHuoDongData.ToList().Count > 0)
			{
				pageInfo = getJoinHuoDongData.ToList().Count + "," + pageSize + "," + currPage + "," + (getJoinHuoDongData.ToList().Count % pageSize == 0 ?
(getJoinHuoDongData.ToList().Count / pageSize) : (getJoinHuoDongData.ToList().Count / pageSize + 1));

				var lstGetJoinHuoDongItemData = (getJoinHuoDongData.ToList().Count > pageSize ? getJoinHuoDongData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getJoinHuoDongData.ToList()).ToList();
				foreach (var item in lstGetJoinHuoDongItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbJoinHuoDongSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbJoinHuoDongSearchInfo.Append(@"<td>" + item.HuodongTitle + "</td>");
					sbJoinHuoDongSearchInfo.Append(@"<td >" + item.HuodongDate + "</td>");
					sbJoinHuoDongSearchInfo.Append(@"<td >" + item.UserName + "</td>");
					sbJoinHuoDongSearchInfo.Append(@"<td >" + item.Tel + "</td>");
					var getContent = Subfilter(item.HuodongContent);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.HuodongContent;

					sbJoinHuoDongSearchInfo.Append(@"<td >" + splitContent + "</td>");
					sbJoinHuoDongSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					sbJoinHuoDongSearchInfo.Append(@"<td><input type='button' class='btn btn-info'  onclick=DeleteJoinHuoDong('" + item.ID + "," + item.HuodongTitle + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbJoinHuoDongSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 参与活动报名信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetJoinHuoDongInfo(string strWhereAll, string currPage)
		{
			var returnResult = string.Empty;
			var getJoinHuoDongInfoData = SearchDataClass.GetSearchJoinHuoDongInfoData(strWhereAll);
			if (getJoinHuoDongInfoData.ToList().Count > 0)
			{
				pageInfo = getJoinHuoDongInfoData.ToList().Count + "," + pageSize + "," + currPage + "," + (getJoinHuoDongInfoData.ToList().Count % pageSize == 0 ?
(getJoinHuoDongInfoData.ToList().Count / pageSize) : (getJoinHuoDongInfoData.ToList().Count / pageSize + 1));

				var lstGetJoinHuoDongItemData = (getJoinHuoDongInfoData.ToList().Count > pageSize ? getJoinHuoDongInfoData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getJoinHuoDongInfoData.ToList()).ToList();
				foreach (var item in lstGetJoinHuoDongItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbJoinHuoDongInfoSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td><input type='button' class='btn btn-info'  onclick=DeleteJoinHuoDong('" + item.ID + "," + item.HuodongTitle + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbJoinHuoDongInfoSearchInfo.Append(@"<td>" + item.HuodongTitle + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td>" + item.HuodongSort + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.HuodongDate + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.UserName + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.UserTel + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.Tel + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.Joinnumber + "</td>");
					var sex = item.JoinSex == 'Y' ? "男" : "女";
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + sex + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.JoinAge + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.Job + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + item.Iinterest + "</td>");
					var getContent = Subfilter(item.Remarks);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Remarks;

					sbJoinHuoDongInfoSearchInfo.Append(@"<td >" + splitContent + "</td>");
					sbJoinHuoDongInfoSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbJoinHuoDongInfoSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 参与团购/预售活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetJoinGoodsHuoDongInfo(string strTitle, string strSaleName, string currPage)
		{
			var returnResult = string.Empty;
			var getHuoDongData = SearchDataClass.GetSearchJoinGoodsHuoDongData(strTitle, strSaleName);
			if (getHuoDongData.ToList().Count > 0)
			{
				pageInfo = getHuoDongData.ToList().Count + "," + pageSize + "," + currPage + "," + (getHuoDongData.ToList().Count % pageSize == 0 ?
(getHuoDongData.ToList().Count / pageSize) : (getHuoDongData.ToList().Count / pageSize + 1));

				var lstGetHuoDongItemData = (getHuoDongData.ToList().Count > pageSize ? getHuoDongData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getHuoDongData.ToList()).ToList();
				foreach (var item in lstGetHuoDongItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbHuoDongSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbHuoDongSearchInfo.Append(@"<td><input type='button' class='btn btn-info'  onclick=DeleteJoinGoodsHuoDongInfo('" + item.ID + "," + item.HuodongTitle + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbHuoDongSearchInfo.Append(@"<td>" + item.HuodongTitle + "</td>");
					sbHuoDongSearchInfo.Append(@"<td>" + item.HuodongSort + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongStartTime + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongEndTime + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongPrice + "</td>");

					sbHuoDongSearchInfo.Append(@"<td >" + item.HuodongGoodsCode + "</td>");

					var strtuangouyushu = item.SelectytSort == 'Y' ? "<span class='badge badge-info'>预售活动</span>" : "<span class='badge badge-important'>团购活动</span>";
					sbHuoDongSearchInfo.Append(@"<td>" + strtuangouyushu + "</td>");

					sbHuoDongSearchInfo.Append(@"<td >" + item.UserName + "</td>");
					sbHuoDongSearchInfo.Append(@"<td >" + item.Tel + "</td>");
					sbHuoDongSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbHuoDongSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}
		/// <summary>
		/// 商品信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetGoodsInfo(string mid,string type,string goodtitle, string goodsort, string goodststus, string goodsku, string currPage)
		{
		 
			var returnResult = string.Empty;
            var getGoodsData = SearchDataClass.GetSearchGoodsInfoData(type,goodtitle, goodsort, goodststus, goodsku);
			if (getGoodsData.ToList().Count > 0)
			{
				pageInfo = getGoodsData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsData.ToList().Count % pageSize == 0 ?
(getGoodsData.ToList().Count / pageSize) : (getGoodsData.ToList().Count / pageSize + 1));

				var lstGetGoodsItemData = (getGoodsData.ToList().Count > pageSize ? getGoodsData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsData.ToList()).ToList();
				foreach (var item in lstGetGoodsItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodsSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodsSearchInfo.Append(@"<td><div style='padding-bottom:10px; '><a class='btn btn-info' href='GoodsAdd.aspx?mid="+ mid + "&type="+type+"&upid=" + item.ID + "'>编辑</a></div>  ");
					//sbGoodsSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteGoodsInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='删除' /> ");

					var display = item.Validate == 'Y' ? "" : "disabled='disabled'";
					sbGoodsSearchInfo.Append(@"<input type='button' class='btn btn-info' " + display + "  onclick=ActiveGoodsInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='下架' />  </td> ");
                    var strPuTongTuiJian = item.PuTongTuiJian == 'T' ? "<span class='badge badge-info'>推荐商品</span>" : "<span class='badge badge-important'>普通商品</span>";
                    sbGoodsSearchInfo.Append(@"<td>" + strPuTongTuiJian + "</td>");
                    var strVal = item.Validate == 'N' ? "下架商品" : "上架商品";
                    sbGoodsSearchInfo.Append(@"<td>" + strVal + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.GoodsCost + "</td>");
					sbGoodsSearchInfo.Append(@"<td>" + item.GoodsStock + "</td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					//Regex regex1 = new Regex(@"<img alt=""", RegexOptions.IgnoreCase);
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbGoodsSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");

					//	var splitContent = Subfilter(item.Content).Length > 30 ? Subfilter(item.Content).Substring(0, 30) + "...." : item.Content;
					//	sbGoodsSearchInfo.Append(@"<td title='" + Subfilter(item.Content) + "' style='text-align:left'>" + splitContent + "</td>");
					var imgUrl = item.Img == "" ? "assets/images/nophoto.gif" : item.Img;
					sbGoodsSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:90px;height:90px;'/></td>");

					var strIndex = item.SetIndex == 'N' ? "否" : "是";
					sbGoodsSearchInfo.Append(@"<td>" + strIndex + "</td>");
					var strSales = item.GoodSales == 'N' ? "否" : "是";
					sbGoodsSearchInfo.Append(@"<td>" + strSales + "</td>");
					//var strExchange = item.ExchangeGood == 'N' ? "否" : "是";
					//sbGoodsSearchInfo.Append(@"<td>" + strExchange + "</td>");
                    if (!string.IsNullOrEmpty(item.ExpireDate.ToString()))
                    {
                        sbGoodsSearchInfo.Append(@"<td>" + Convert.ToDateTime(item.ExpireDate).ToString("yyyy/MM/dd") + "</td>");

                    }
                    else
                    {
                        sbGoodsSearchInfo.Append(@"<td>无限制</td>");
                    }
                        sbGoodsSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbGoodsSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 套餐商品信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetGoodsPackageInfo(string mid,  string goodtitle, string goodsort, string goodststus, string goodsku, string currPage)
		{

			var returnResult = string.Empty;
			var getGoodsPackageData = SearchDataClass.GetSearchGoodsPackageInfoData(goodtitle, goodsort, goodststus, goodsku);
			if (getGoodsPackageData.ToList().Count > 0)
			{
				pageInfo = getGoodsPackageData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsPackageData.ToList().Count % pageSize == 0 ?
(getGoodsPackageData.ToList().Count / pageSize) : (getGoodsPackageData.ToList().Count / pageSize + 1));

				var lstGetGoodsItemData = (getGoodsPackageData.ToList().Count > pageSize ? getGoodsPackageData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsPackageData.ToList()).ToList();
				foreach (var item in lstGetGoodsItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodsPackageSearchInfo.Append(@"<tr class='" + cssName + "'>");
                    sbGoodsPackageSearchInfo.Append(@"<td><div style='padding-bottom:10px; '><a class='btn btn-info' href='GoodsPackageAdd.aspx?mid=" + mid + "&upid=" + item.ID + "'>编辑</a></div>  ");
					//sbGoodsSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteGoodsInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='删除' /> ");

					var display = item.Validate == 'Y' ? "" : "disabled='disabled'";
					sbGoodsPackageSearchInfo.Append(@"<input type='button' class='btn btn-info' " + display + "  onclick=ActiveGoodsInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='下架' />  </td> ");
 
					var strVal = item.Validate == 'N' ? "下架商品" : "上架商品";
					sbGoodsPackageSearchInfo.Append(@"<td>" + strVal + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.GoodsCost + "</td>");
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.GoodsStock + "</td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					//Regex regex1 = new Regex(@"<img alt=""", RegexOptions.IgnoreCase);
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbGoodsPackageSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");

					//	var splitContent = Subfilter(item.Content).Length > 30 ? Subfilter(item.Content).Substring(0, 30) + "...." : item.Content;
					//	sbGoodsSearchInfo.Append(@"<td title='" + Subfilter(item.Content) + "' style='text-align:left'>" + splitContent + "</td>");
					var imgUrl = item.Img == "" ? "assets/images/nophoto.gif" : item.Img;
					sbGoodsPackageSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:90px;height:90px;'/></td>");

					var strIndex = item.SetIndex == 'N' ? "否" : "是";
					sbGoodsPackageSearchInfo.Append(@"<td>" + strIndex + "</td>");
					var strSales = item.GoodSales == 'N' ? "否" : "是";
					sbGoodsPackageSearchInfo.Append(@"<td>" + strSales + "</td>");
					//var strExchange = item.ExchangeGood == 'N' ? "否" : "是";
					//sbGoodsSearchInfo.Append(@"<td>" + strExchange + "</td>");
					 
					sbGoodsPackageSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbGoodsPackageSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		//商品分类信息数据增删改
		/// </summary>
		/// <returns></returns>
		public string GetGoodsSortInfo(string strGoodsSort, string currPage)
		{
			var returnResult = string.Empty;
			var getGoodsSortData = SearchDataClass.GetSearchGoodsSortData(strGoodsSort);
			if (getGoodsSortData.ToList().Count > 0)
			{
				pageInfo = getGoodsSortData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsSortData.ToList().Count % pageSize == 0 ?
(getGoodsSortData.ToList().Count / pageSize) : (getGoodsSortData.ToList().Count / pageSize + 1));

				var lstGetGoodsSortItemData = (getGoodsSortData.ToList().Count > pageSize ? getGoodsSortData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsSortData.ToList()).ToList();
				foreach (var item in lstGetGoodsSortItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodsSortSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodsSortSearchInfo.Append(@"<td>" + item.FatherSortName + "</td>");
					sbGoodsSortSearchInfo.Append(@"<td>" + item.GoodsSortName + "</td>");
					sbGoodsSortSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ID + "," + item.GoodsSortName + "," + item.SortFatherID;
					sbGoodsSortSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditGoodsSortInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbGoodsSortSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteGoodsSortInfo('" + item.ID + "," + item.GoodsSortName + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbGoodsSortSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 团购列表信息查询及删除
		/// </summary>
		/// <param name="chValidate"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGroupBuyInfo(string mid, string type,string goodtitle, string goodsort, string goodststus, string goodsku, string currPage)
		{
			var returnResult = string.Empty;
			var getGroupBuyData = SearchDataClass.GetSearchGroupBuyInfoData(type,goodtitle, goodsort, goodststus, goodsku);
			if (getGroupBuyData.ToList().Count > 0)
			{
				pageInfo = getGroupBuyData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGroupBuyData.ToList().Count % pageSize == 0 ?
(getGroupBuyData.ToList().Count / pageSize) : (getGroupBuyData.ToList().Count / pageSize + 1));

				var lstGetGroupBuyItemData = (getGroupBuyData.ToList().Count > pageSize ? getGroupBuyData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGroupBuyData.ToList()).ToList();
				foreach (var item in lstGetGroupBuyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGroupBuySearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGroupBuySearchInfo.Append(@"<td><div style='padding-bottom:1px; '><a class='btn btn-info' href='GroupBuyAdd.aspx?mid="+mid+"&type="+type+"&upid=" + item.ID + "'>编辑</a></div><br/> ");
                    var display = item.Validate == 'Y' ? "" : "disabled='disabled'";
                    sbGroupBuySearchInfo.Append(@"<input type='button' class='btn btn-info' " + display + "  onclick=ActiveGroupGoodsInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='下架' />  </td> ");
	
					//sbGroupBuySearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteGroupBuyInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");
                    var strtuangouyushu = item.TuanGouYuShou == 'Y' ? "<span class='badge badge-info'>预售活动</span>" : "<span class='badge badge-important'>团购活动</span>";
                    sbGroupBuySearchInfo.Append(@"<td>" + strtuangouyushu + "</td>");
                    var strVal = item.Validate == 'N' ?"<span class='badge badge-info'>下架商品</span>" : "<span class='badge badge-important'>上架商品</span>";
                    sbGroupBuySearchInfo.Append(@"<td>" + strVal + "</td>");

					sbGroupBuySearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbGroupBuySearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
					var imgUrl = item.ImgUrl == "" ? "assets/images/nophoto.gif" : item.ImgUrl;
					sbGroupBuySearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:90px;height:90px;'/></td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					//Regex regex1 = new Regex(@"<img alt=""", RegexOptions.IgnoreCase);
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbGroupBuySearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");

					sbGroupBuySearchInfo.Append(@"<td>" + item.Starttime + "</td>");
					sbGroupBuySearchInfo.Append(@"<td>" + item.Endtime + "</td>");
                    sbGroupBuySearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbGroupBuySearchInfo.Append(@"<td>" + item.quantity + "</td>");
					sbGroupBuySearchInfo.Append(@"<td>" + item.price + "</td>");
					sbGroupBuySearchInfo.Append(@"<td>" + item.CostPrice + "</td>");
				
					var strSetIndex = item.SetIndex == 'N' ? "否" : "是";
					sbGroupBuySearchInfo.Append(@"<td>" + strSetIndex + "</td>");

					sbGroupBuySearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbGroupBuySearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 订单列表
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetOrderListInfo(string strOrderno, string strOrderStatus, string strTime, string currPage)
		{
			var returnResult = string.Empty;
			var getOrderListData = SearchDataClass.GetSearchOrderListInfoData(strOrderno, strOrderStatus, strTime);
			if (getOrderListData.ToList().Count > 0)
			{
				pageInfo = getOrderListData.ToList().Count + "," + pageSize + "," + currPage + "," + (getOrderListData.ToList().Count % pageSize == 0 ?
(getOrderListData.ToList().Count / pageSize) : (getOrderListData.ToList().Count / pageSize + 1));

				var lstGetOrderListItemData = (getOrderListData.ToList().Count > pageSize ? getOrderListData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getOrderListData.ToList()).ToList();
				sbOrderListSearchInfo.Append(@"<tr>  <th ><input type='checkbox' name='option1' id='chkAll' class='uniform' onclick='selectAll();' value='全选' /></th>
                                                		<th>操作</th>
																										<th>订单编号</th>
																										<th>买家姓名</th>
																										<th>电话</th>
																										<th>地址</th>
																										<th>订单金额</th>
																												<th>支付时间 </th>
																										<th>订单状态</th>
																										<th>区市</th>
																											<th>下单时间 </th>
																										<th>订单来源</th>
																										<th>支付方式 </th>
							                        
																				</tr>");

				foreach (var item in lstGetOrderListItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbOrderListSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbOrderListSearchInfo.Append(@"<td><input type='checkbox' name='option1' value='" + item.OrderNo + "' /></td>");
					sbOrderListSearchInfo.Append(@"<script>var orderno ='';function selectAll(){if ($('#chkAll').attr('checked')) 
                                                       {  
                                                     $(':checkbox').attr('checked', true);  for (var i = 1; i < $(':checkbox').length; i++) {
                                                                orderno += $(':checkbox')[i].value + ';';};$('#hfSeleckSo').val(orderno);
                                                        } else {  
                                                            $(':checkbox').attr('checked', false);  orderno='';
                                                        }  }</script>");
					sbOrderListSearchInfo.Append(@"<td><a class='btn btn-info' href='OrdersDetails.aspx?upid=" + item.OrderNo + "'>详情</a> ");
					var display = item.OrderStatus == '0' ? "" : "disabled='disabled'";
					sbOrderListSearchInfo.Append(@"<input type='button' class='btn btn-info' " + display + "  onclick=DeleteOneOrderInfo('" + item.OrderNo + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					//.Append(@"<input type='button' class='btn btn-info'  onclick=OkOrderInfo('" + item.OrderNo + "') style='width: 60px; height: 28px;'  value='发货' />  ");
					// sbOrderListSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=CancelOrderInfo('" + item.OrderNo + "') style='width: 70px; height: 28px;'  value='取消订单' />  </td></tr>");

					sbOrderListSearchInfo.Append(@"<td>" + item.OrderNo + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.Name + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.Tel + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.Address + "</td>");

					sbOrderListSearchInfo.Append(@"<td>" + item.OrderPrice + "</td>");

					sbOrderListSearchInfo.Append(@"<td>" + item.paymenttime + "</td>");
					var orderStatus = string.Empty;
					switch (item.OrderStatus)
					{
						case '0':
							orderStatus = "待付款";
							break;
						case '1':
							orderStatus = "待发货";
							break;
						case '2':
							orderStatus = "已发货";
							break;
						case '3':
							orderStatus = "退货/退款中";
							break;
						case '4':
							orderStatus = "已完成";
							break;
						case '5':
							orderStatus = "已取消";
							break;
						default: break;
					}
					sbOrderListSearchInfo.Append(@"<td style='color:red;font-weight:bold'>" + orderStatus + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.Areacity + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.ordertime + "</td>");

					sbOrderListSearchInfo.Append(@"<td>" + item.Ordersource + "</td>");
					sbOrderListSearchInfo.Append(@"<td>" + item.Paymode + "</td></tr>");
					rowCount++;
					//超过24小时未付款的订单删除
					TimeSpan ifoneday = DateTime.Now - item.paymenttime;
					//20161011 del
					//if (ifoneday.Days >= 1 && item.OrderStatus == '0')
					//{
					//	using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					//	{
					//		orders delorder = db.orders.Single(x => x.orderno == item.OrderNo);
					//		db.orders.DeleteOnSubmit(delorder);
					//		var getDetails = db.ordersdetails.Where(x => x.orderno == item.OrderNo);
					//		foreach (var itemInfo in getDetails)
					//		{
					//			ordersdetails delDetails = db.ordersdetails.Single(x => x.ordersid == itemInfo.ordersid);
					//			db.ordersdetails.DeleteOnSubmit(delDetails);
					//		}
					//		db.SubmitChanges();
					//	}
					//}
					//7天后付款订单默认收货变已完成状态
					if (ifoneday.Days >= 7 && item.OrderStatus == '2')
					{
						using (EnshineUnionDataContext db = new EnshineUnionDataContext())
						{
							orders uporder = db.orders.Single(x => x.orderno == item.OrderNo);
							uporder.orderstatus = '4';
							db.SubmitChanges();
						}
					}

				}
				returnResult = sbOrderListSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		///植保分类信息数据增删改
		/// </summary>
		/// <param name="strCommunityC">社区名称</param>
		/// <param name="chValidate">是否有效</param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetPlantDoctorSortInfo(string strPlantDoctorSort, string currPage)
		{
			var returnResult = string.Empty;
			var getPlantDoctorSortData = SearchDataClass.GetSearchPlantDoctorSortData(strPlantDoctorSort);
			if (getPlantDoctorSortData.ToList().Count > 0)
			{
				pageInfo = getPlantDoctorSortData.ToList().Count + "," + pageSize + "," + currPage + "," + (getPlantDoctorSortData.ToList().Count % pageSize == 0 ?
(getPlantDoctorSortData.ToList().Count / pageSize) : (getPlantDoctorSortData.ToList().Count / pageSize + 1));

				var lstGetPlantDoctorSortItemData = (getPlantDoctorSortData.ToList().Count > pageSize ? getPlantDoctorSortData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getPlantDoctorSortData.ToList()).ToList();
				foreach (var item in lstGetPlantDoctorSortItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbPlantDoctorSortSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbPlantDoctorSortSearchInfo.Append(@"<td>" + item.FatherSortName + "</td>");
					sbPlantDoctorSortSearchInfo.Append(@"<td>" + item.NewsSortName + "</td>");
					var imgurl = string.Empty;
					if (!string.IsNullOrEmpty(item.SortImage))
						imgurl = item.SortImage;
					else
						imgurl = "assets/images/nophoto.gif";
					sbPlantDoctorSortSearchInfo.Append(@"<td><img src='" + imgurl + "' style='width:90px;height:90px;'/></td>");

					sbPlantDoctorSortSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ID + "," + item.NewsSortName + "," + item.SortImage + "," + item.SortFatherID;
					sbPlantDoctorSortSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditPlantDoctorSortInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbPlantDoctorSortSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeletePlantDoctorSortInfo('" + item.ID + "," + item.NewsSortName + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbPlantDoctorSortSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 植保信息查询及删除后查询
		/// </summary>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetPlantDoctorInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getPlantDoctorData = SearchDataClass.GetSearchPlantDoctorInfoData();
			if (getPlantDoctorData.ToList().Count > 0)
			{
				pageInfo = getPlantDoctorData.ToList().Count + "," + pageSize + "," + currPage + "," + (getPlantDoctorData.ToList().Count % pageSize == 0 ?
(getPlantDoctorData.ToList().Count / pageSize) : (getPlantDoctorData.ToList().Count / pageSize + 1));

				var lstGetPlantDoctorItemData = (getPlantDoctorData.ToList().Count > pageSize ? getPlantDoctorData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getPlantDoctorData.ToList()).ToList();
				foreach (var item in lstGetPlantDoctorItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbPlantDoctoSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbPlantDoctoSearchInfo.Append(@"<td><a class='btn btn-info' href='PlantDoctorAdd.aspx?upid=" + item.ID + "'>编辑</a> ");
					sbPlantDoctoSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeletePlantDoctorInfo('" + item.ID + "," + item.Title + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbPlantDoctoSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
					sbPlantDoctoSearchInfo.Append(@"<td>" + item.NewsSort + "</td>");
					//	var splitContent = Subfilter(item.Content).Length > 30 ? Subfilter(item.Content).Substring(0, 30) + "...." : item.Content;
					//	sbNewsSearchInfo.Append(@"<td title='" + Subfilter(item.Content) + "' style='text-align:left'>" + splitContent + "</td>");
					var getContent = Subfilter(item.Content);
					var splitContent = getContent.Length > 30 ? getContent.Substring(0, 30) + "...." : item.Content;
					//Regex regex1 = new Regex(@"<img alt=""", RegexOptions.IgnoreCase);
					Regex regex1 = new Regex(@"<img ", RegexOptions.IgnoreCase);
					sbPlantDoctoSearchInfo.Append(@"<td  style='text-align:left'>" + regex1.Replace(splitContent, "<img style=\"width:90px;height:90px; \"") + "</td>");
					var imgUrl = item.Img == "" ? "assets/images/nophoto.gif" : item.Img;
					sbPlantDoctoSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:90px;height:90px;'/></td>");
					var strVal = item.Validate == 'N' ? "否" : "是";
					sbPlantDoctoSearchInfo.Append(@"<td>" + strVal + "</td>");
					var strIndex = item.SetIndex == 'N' ? "否" : "是";
					sbPlantDoctoSearchInfo.Append(@"<td>" + strIndex + "</td>");
					sbPlantDoctoSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbPlantDoctoSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 充值列表
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserRechargeListInfo(string strtel, string strRecStatus, string strTime, string currPage)
		{
			var returnResult = string.Empty;
			var getUserRechargeListData = SearchDataClass.GetSearchUserRechargeInfoData(strtel, strRecStatus, strTime);
			if (getUserRechargeListData.ToList().Count > 0)
			{
				pageInfo = getUserRechargeListData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserRechargeListData.ToList().Count % pageSize == 0 ?
(getUserRechargeListData.ToList().Count / pageSize) : (getUserRechargeListData.ToList().Count / pageSize + 1));

				var lstGetUserRechargeListItemData = (getUserRechargeListData.ToList().Count > pageSize ? getUserRechargeListData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserRechargeListData.ToList()).ToList();

				foreach (var item in lstGetUserRechargeListItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserRechargeSearchInfo.Append(@"<tr class='" + cssName + "'>");
					var calInfo = item.RecNo + "," + item.Tel + "," + item.UserID;
					var display = item.RecStatus == 'Y' ? "" : "disabled='disabled'";
					var display1 = item.RecStatus == 'Y' ? "disabled='disabled'" : "";

					sbUserRechargeSearchInfo.Append(@"<td><input type='button' class='btn btn-info' " + display + " onclick=CancelRechargeInfo('" + calInfo + "'); style='width: 80px; height: 28px;'  value='撤回充值' /> ");
					sbUserRechargeSearchInfo.Append(@"<td><input type='button' class='btn btn-info' " + display1 + " onclick=DeleteRechargeInfo('" + calInfo + "'); style='width: 80px; height: 28px;'  value='删除' /> </td>");


					sbUserRechargeSearchInfo.Append(@"<td>" + item.RecNo + "</td>");
					sbUserRechargeSearchInfo.Append(@"<td>" + item.RecTime + "</td>");
					sbUserRechargeSearchInfo.Append(@"<td>" + item.Tel + "</td>");
					sbUserRechargeSearchInfo.Append(@"<td>" + item.UserName + "</td>");
					sbUserRechargeSearchInfo.Append(@"<td>" + item.RecMoeny + "</td>");
					var recstatus = string.Empty;
					var successinfo = string.Empty;
					switch (item.RecStatus)
					{
						case 'N':
							recstatus = "交易失败"; successinfo = "充值失败";
							break;
						case 'Y':
							recstatus = "交易成功";
							successinfo = "充值成功";
							break;
						//case 'S':
						//	recstatus = "分红成功";
						//	successinfo = "充值成功";
						//	break;
						case 'C':
							recstatus = "撤销充值";
							successinfo = "撤销充值成功";
							break;
					}

					var content = item.RecContent == "" ? "东方柏农-" + item.RecTime.ToString("yyyy.MM.dd") + "-" + successinfo : item.RecContent;
					sbUserRechargeSearchInfo.Append(@"<td>" + content + "</td>");



					sbUserRechargeSearchInfo.Append(@"<td style='color: red; font-weight: bold'>" + recstatus + "</td></tr>");


					rowCount++;
				}
				returnResult = sbUserRechargeSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 角色权限信息显示
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserRoleInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getRoleData = SearchDataClass.GetSearchRoleInfoData();
			if (getRoleData.ToList().Count > 0)
			{
				pageInfo = getRoleData.ToList().Count + "," + pageSize + "," + currPage + "," + (getRoleData.ToList().Count % pageSize == 0 ?
(getRoleData.ToList().Count / pageSize) : (getRoleData.ToList().Count / pageSize + 1));

				var lstGetRoleItemData = (getRoleData.ToList().Count > pageSize ? getRoleData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getRoleData.ToList()).ToList();
				foreach (var item in lstGetRoleItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserRoleSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbUserRoleSearchInfo.Append(@"<td><a class='btn btn-info' href='UserRoleSet.aspx?mid=3,6&&upid=" + item.ID + "'>编辑</a>&nbsp;&nbsp;");
					sbUserRoleSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteRoleInfo('" + item.ID + "," + item.RoleName + "') style='width: 60px; height: 28px;'  value='删除' />  </td>");

					sbUserRoleSearchInfo.Append(@"<td>" + item.RoleName + "</td>");
					sbUserRoleSearchInfo.Append(@"<td>" + item.RoleContent + "</td>");

					var powerid = item.PowerName.Split(',');
					var menuName = string.Empty;
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						foreach (var id in powerid)
						{
							menuName += db.menulist.Single(x => x.menuid == int.Parse(id)).menuname + ",";
						}
					}
					sbUserRoleSearchInfo.Append(@"<td>" + menuName.TrimEnd(',') + "</td>");

					sbUserRoleSearchInfo.Append(@"<td>" + item.AddTime + "</td></tr>");

					rowCount++;
				}
				returnResult = sbUserRoleSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}



		/// <summary>
		//会员信息数据增删改
		/// </summary>
		/// <returns></returns>
		public string GetMemberInfo(string strWhereAll, string currPage)
		{
			var returnResult = string.Empty;
			var getMemberData = SearchDataClass.GetSearchMemberData(strWhereAll);
			if (getMemberData.ToList().Count > 0)
			{
				pageInfo = getMemberData.ToList().Count + "," + pageSize + "," + currPage + "," + (getMemberData.ToList().Count % pageSize == 0 ?
(getMemberData.ToList().Count / pageSize) : (getMemberData.ToList().Count / pageSize + 1));

				var lstGetMemberItemData = (getMemberData.ToList().Count > pageSize ? getMemberData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getMemberData.ToList()).ToList();
				foreach (var item in lstGetMemberItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMemberSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbMemberSearchInfo.Append(@"<td>" + item.MemberName + "</td>");
					sbMemberSearchInfo.Append(@"<td>" + item.Discount + "</td>");
					sbMemberSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ID + "," + item.MemberName + "," + item.Discount;
					sbMemberSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditMemberInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbMemberSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteMemberInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbMemberSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 我的收入明细一览
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetMyPayMoneyInfo(string currPage, string userid)
		{
			var returnResult = string.Empty;
			var getPayMoneyData = SearchDataClass.GetSearchMyPayMoneyInfoData(userid);
			if (getPayMoneyData.ToList().Count > 0)
			{
				pageInfo = getPayMoneyData.ToList().Count + "," + pageSize + "," + currPage + "," + (getPayMoneyData.ToList().Count % pageSize == 0 ?
(getPayMoneyData.ToList().Count / pageSize) : (getPayMoneyData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getPayMoneyData.ToList().Count > pageSize ? getPayMoneyData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getPayMoneyData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMyPayMoneySearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbMyPayMoneySearchInfo.Append(@"<td>" + item.MoneyCode + "</td>");
					sbMyPayMoneySearchInfo.Append(@"<td>" + item.Moneytime + "</td>");
					sbMyPayMoneySearchInfo.Append(@"<td >" + item.OrderNo + "</td>");
					sbMyPayMoneySearchInfo.Append(@"<td >" + item.GetMoney + "</td>");
					sbMyPayMoneySearchInfo.Append(@"<td >" + item.SumMoney + "</td>");
					var splitContent = Subfilter(item.GetContent).Length > 30 ? Subfilter(item.GetContent).Substring(0, 30) + "...." : item.GetContent;
					sbMyPayMoneySearchInfo.Append(@"<td style='text-align:left'>" + splitContent + "</td></tr>");

					rowCount++;
				}
				returnResult = sbMyPayMoneySearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 我的积分明细一览
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetMyPointInfo(string currPage, string userid)
		{
			var returnResult = string.Empty;
			var getMyPointData = SearchDataClass.GetSearchMyPointInfoData(userid);
			if (getMyPointData.ToList().Count > 0)
			{
				pageInfo = getMyPointData.ToList().Count + "," + pageSize + "," + currPage + "," + (getMyPointData.ToList().Count % pageSize == 0 ?
(getMyPointData.ToList().Count / pageSize) : (getMyPointData.ToList().Count / pageSize + 1));

				var lstGetMyPointItemData = (getMyPointData.ToList().Count > pageSize ? getMyPointData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getMyPointData.ToList()).ToList();
				foreach (var item in lstGetMyPointItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMyPointSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbMyPointSearchInfo.Append(@"<td>" + item.Pointime + "</td>");
					sbMyPointSearchInfo.Append(@"<td>" + item.Point + "</td>");

					sbMyPointSearchInfo.Append(@"<td>" + item.SumPoint + "</td>");

					var splitContent = Subfilter(item.GetContent).Length > 30 ? Subfilter(item.GetContent).Substring(0, 30) + "...." : item.GetContent;

					sbMyPointSearchInfo.Append(@"<td  style='text-align:left'>" + splitContent + "</td></tr>");

					rowCount++;
				}
				returnResult = sbMyPointSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 月销售金额报表统计信息
		/// </summary>
		/// <param name="strTime">时间筛选</param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetFinanceMonthListInfo(string strTime, string currPage)
		{
			var returnResult = string.Empty;
			var getFinanceListData = SearchDataClass.GetSearchFinanceMonthListInfoData(strTime);
			if (getFinanceListData.ToList().Count > 0)
			{
				var OrderPrice = (from p in getFinanceListData select p.OrderPrice).Sum();
				var SumQty = (from p in getFinanceListData select p.SumQty).Sum();
				var OrderCount = (from p in getFinanceListData select p.OrderCount).Sum();
				pageInfo = getFinanceListData.ToList().Count + "," + pageSize + "," + currPage + "," + (getFinanceListData.ToList().Count % pageSize == 0 ?
(getFinanceListData.ToList().Count / pageSize) : (getFinanceListData.ToList().Count / pageSize + 1));

				var lstGetFinanceItemData = (getFinanceListData.ToList().Count > pageSize ? getFinanceListData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getFinanceListData.ToList()).ToList();
				foreach (var item in lstGetFinanceItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbFinanceMonthSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbFinanceMonthSearchInfo.Append(@"<td>" + item.OrderTime + "</td>");
					sbFinanceMonthSearchInfo.Append(@"<td>" + item.OrderPrice + "</td>");

					sbFinanceMonthSearchInfo.Append(@"<td>" + item.SumQty + "</td>");
					sbFinanceMonthSearchInfo.Append(@"<td>" + item.OrderCount + "</td>");
					sbFinanceMonthSearchInfo.Append(@"<td >" + item.OrderPrice + "</td></tr>");

					rowCount++;
				}         //合计
				sbFinanceMonthSearchInfo.Append(@"<tr><td colspan='1' style='text-align:right'>小计：</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + lstGetFinanceItemData.Sum(x => x.OrderPrice) + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + lstGetFinanceItemData.Sum(x => x.SumQty) + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + lstGetFinanceItemData.Sum(x => x.OrderCount) + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td class='rightborder'>" + lstGetFinanceItemData.Sum(x => x.OrderPrice) + "</td></tr>");
				sbFinanceMonthSearchInfo.Append(@"<tr><td colspan='1' style='text-align:right;color:red;font-weight:bold'>总合计：</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + OrderPrice + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + SumQty + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td >" + OrderCount + "</td>");
				sbFinanceMonthSearchInfo.Append(@"<td class='rightborder' >" + OrderPrice + "</td></tr>");
				returnResult = sbFinanceMonthSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 当月分销提成金额报表
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetFenXiaoExtractInfo(string strTime, string currPage)
		{
			var returnResult = string.Empty;
			var getFenXiaoExtractData = SearchDataClass.GetSearchFenXiaoExtractInfoData(strTime);
			if (getFenXiaoExtractData.ToList().Count > 0)
			{
				var Recmoney = (from p in getFenXiaoExtractData select p.Recmoney).Sum();
				var RechargeCount = (from p in getFenXiaoExtractData select p.RechargeCount).Sum();
				pageInfo = getFenXiaoExtractData.ToList().Count + "," + pageSize + "," + currPage + "," + (getFenXiaoExtractData.ToList().Count % pageSize == 0 ?
(getFenXiaoExtractData.ToList().Count / pageSize) : (getFenXiaoExtractData.ToList().Count / pageSize + 1));

				var lstGetFenXiaoExtractItemData = (getFenXiaoExtractData.ToList().Count > pageSize ? getFenXiaoExtractData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getFenXiaoExtractData.ToList()).ToList();
				foreach (var item in lstGetFenXiaoExtractItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbFenXiaoExtractSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbFenXiaoExtractSearchInfo.Append(@"<td>" + item.RechargeTime.ToShortDateString() + "</td>");
					sbFenXiaoExtractSearchInfo.Append(@"<td>" + item.Recmoney + "</td>");


					sbFenXiaoExtractSearchInfo.Append(@"<td>" + item.RechargeCount + "</td></tr>");

					rowCount++;
				}
				//合计
				sbFenXiaoExtractSearchInfo.Append(@"<tr><td colspan='1' style='text-align:right'>小计：</td>");
				sbFenXiaoExtractSearchInfo.Append(@"<td >" + lstGetFenXiaoExtractItemData.Sum(x => x.Recmoney) + "</td>");

				sbFenXiaoExtractSearchInfo.Append(@"<td class='rightborder'>" + lstGetFenXiaoExtractItemData.Sum(x => x.RechargeCount) + "</td></tr>");
				sbFenXiaoExtractSearchInfo.Append(@"<tr><td colspan='1' style='text-align:right;color:red;font-weight:bold'>总合计：</td>");
				sbFenXiaoExtractSearchInfo.Append(@"<td >" + Recmoney + "</td>");

				sbFenXiaoExtractSearchInfo.Append(@"<td class='rightborder' >" + RechargeCount + "</td></tr>");
				returnResult = sbFenXiaoExtractSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// 当月c)	会员提现报表
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetMemberExtractInfo(string strTime, string currPage)
		{
			var returnResult = string.Empty;
			var getMemberExtractData = SearchDataClass.GetSearchMemberExtractInfoData(strTime);
			if (getMemberExtractData.ToList().Count > 0)
			{

				pageInfo = getMemberExtractData.ToList().Count + "," + pageSize + "," + currPage + "," + (getMemberExtractData.ToList().Count % pageSize == 0 ?
(getMemberExtractData.ToList().Count / pageSize) : (getMemberExtractData.ToList().Count / pageSize + 1));

				var lstGetMemberExtractItemData = (getMemberExtractData.ToList().Count > pageSize ? getMemberExtractData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getMemberExtractData.ToList()).ToList();
				foreach (var item in lstGetMemberExtractItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMemberExtractSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbMemberExtractSearchInfo.Append(@"<td>" + item.ExtractNo + "</td>");
					sbMemberExtractSearchInfo.Append(@"<td>" + item.ExtractListInfo + "</td>");


					sbMemberExtractSearchInfo.Append(@"<td>" + item.ExtractCreateTime + "</td></tr>");

					rowCount++;
				}

				returnResult = sbMemberExtractSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}
		/// <summary>
		/// 会员提现管理及生成的提现清单
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserExtractListInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getUserExtractListData = SearchDataClass.GetSearchUserExtractListInfoData();
			if (getUserExtractListData.ToList().Count > 0)
			{
				pageInfo = getUserExtractListData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserExtractListData.ToList().Count % pageSize == 0 ?
(getUserExtractListData.ToList().Count / pageSize) : (getUserExtractListData.ToList().Count / pageSize + 1));

				var lstGetUserExtractListItemData = (getUserExtractListData.ToList().Count > pageSize ? getUserExtractListData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserExtractListData.ToList()).ToList();
				sbMemberExtractManagerSearchInfo.Append(@"<tr><th ><input type='checkbox' name='option1' id='chkAll' class='uniform' onclick='selectAll();' value='全选' /></th>
                                                                                    <th>提现编号</th>
																					 <th>会员姓名</th>																	 
																						<th>电话</th>
																					     <th>提现金额</th> 
  <th>转账账号</th>
																						<th>原因</th>
																				        <th>提现时间</th>
																				 
																				</tr>");
				foreach (var item in lstGetUserExtractListItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMemberExtractManagerSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbMemberExtractManagerSearchInfo.Append(@"<td><input type='checkbox' name='option1' value='" + item.Extno + "' /></td>");
					sbMemberExtractManagerSearchInfo.Append(@"<script>var orderno ='';function selectAll(){if ($('#chkAll').attr('checked')) 
                                                       {  
                                                     $(':checkbox').attr('checked', true);  for (var i = 1; i < $(':checkbox').length; i++) {
                                                                orderno += $(':checkbox')[i].value + ';';};$('#hfSeleckExtNo').val(orderno);
                                                        } else {  
                                                            $(':checkbox').attr('checked', false);  orderno='';
                                                        }  }</script>");
					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.Extno + "</td>");
					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.ExtName + "</td>");
					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.ExtTel + "</td>");

					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.Extmoney + "</td>");
					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.ExtCardNo + "</td>");
					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.Extcontent + "</td>");

					sbMemberExtractManagerSearchInfo.Append(@"<td>" + item.Exttime + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbMemberExtractManagerSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		///  门店店铺信息
		/// </summary>
		/// <param name="strshopName"></param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetShopInfo(string strshopName, string currPage)
		{
			var returnResult = string.Empty;
			var getShopData = SearchDataClass.GetSearchShopData(strshopName);
			if (getShopData.ToList().Count > 0)
			{
				pageInfo = getShopData.ToList().Count + "," + pageSize + "," + currPage + "," + (getShopData.ToList().Count % pageSize == 0 ?
(getShopData.ToList().Count / pageSize) : (getShopData.ToList().Count / pageSize + 1));

				var lstGetShopItemData = (getShopData.ToList().Count > pageSize ? getShopData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getShopData.ToList()).ToList();
				foreach (var item in lstGetShopItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbShopManagerSearchInfo.Append(@"<tr class='" + cssName + "'>");

					sbShopManagerSearchInfo.Append(@"<td>" + item.ShopName + "</td>");
					sbShopManagerSearchInfo.Append(@"<td>" + item.ShopTel + "</td>");
					sbShopManagerSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
					var editinfo = item.ShopID + "," + item.ShopName + "," + item.ShopTel;
					sbShopManagerSearchInfo.Append(@"<td><input type='button' class='btn btn-info'   data-target='#anim-modal' data-toggle='modal' onclick=EditShopInfo('" + editinfo + "') style='width: 60px; height: 28px;'  value='编辑' /> ");
					sbShopManagerSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteShopInfo('" + item.ShopID + "," + item.ShopName + "') style='width: 60px; height: 28px;'  value='删除' />  </td></tr>");

					rowCount++;
				}
				returnResult = sbShopManagerSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		///  厂家核对报表信息
		/// </summary>
		/// <param name="strshopName"></param>
		/// <param name="currPage">页数</param>
		/// <returns></returns>
		public string GetSaleCheckInfo(string strshopName, string strtime, string currPage)
		{
			var returnResult = string.Empty;
			var getSaleCheckData = SearchDataClass.GetSearchSaleCheckData(strshopName, strtime);
			if (getSaleCheckData.ToList().Count > 0)
			{
				pageInfo = getSaleCheckData.ToList().Count + "," + pageSize + "," + currPage + "," + (getSaleCheckData.ToList().Count % pageSize == 0 ?
(getSaleCheckData.ToList().Count / pageSize) : (getSaleCheckData.ToList().Count / pageSize + 1));

				var lstGetSaleCheckItemData = (getSaleCheckData.ToList().Count > pageSize ? getSaleCheckData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getSaleCheckData.ToList()).ToList();
				foreach (var item in lstGetSaleCheckItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbSaleCheckSearchInfo.Append(@"<tr class='" + cssName + "'>");
					//	var status = item.OrderStatus == '1' ? "付款未发货" : "其他状态";
					sbSaleCheckSearchInfo.Append(@"<td style='color:red;font-weight:bold'>" + item.OrderStatus + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.OrderNo + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.PaymentTime + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.OrderPrice + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td style='text-align:left'>" + item.GoodsTitle + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td style='text-align:left'>" + item.GoodsCompany + "</td>");

					sbSaleCheckSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.GoodsCost + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.GoodsSpec + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.BuySumQty + "</td>");
					sbSaleCheckSearchInfo.Append(@"<td>" + item.GoodsSumWeight + "</td></tr>");

					rowCount++;
				}
				returnResult = sbSaleCheckSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// 商品发货表
		/// </summary>
		/// <param name="strshopName"></param>
		/// <param name="strtime"></param>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGoodsShipInfo(string strorderno, string strgoodsname, string strbuyname, string strcompany,
																		string strgudong, string strzhanzhang, string strtime, string strshopName, string goodshipstatus, string currPage)
		{
			var returnResult = string.Empty;
			var getGoodShipData = SearchDataClass.GetSearchGoodsShipData(strorderno, strgoodsname, strbuyname, strcompany,
				strgudong, strzhanzhang, strtime, strshopName, goodshipstatus);
			if (getGoodShipData.ToList().Count > 0)
			{
				pageInfo = getGoodShipData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodShipData.ToList().Count % pageSize == 0 ?
(getGoodShipData.ToList().Count / pageSize) : (getGoodShipData.ToList().Count / pageSize + 1));

				var lstGetGoodShipItemData = (getGoodShipData.ToList().Count > pageSize ? getGoodShipData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodShipData.ToList()).ToList();
				sbGoodsShipSearchInfo.Append(@"<tr>  <th ><input type='checkbox' name='option1' id='chkAll' class='uniform' onclick='selectAll();' value='全选' /></th>
                                                			<th>订单状态</th>
	<th>发货单号</th>
																							<th>上级股东</th>
	<th>上级站长</th>
																						
																							<th>订单编号</th>
																						
																							<th>订单金额</th>
																							<th>买家姓名</th>
																							<th>电话</th>
																							<th>商品名称</th>
	                                                                                        <th>商品编码</th>
	                                                            <th>购买数量</th>
																							<th>账户余额</th>
																							<th>区市</th>
																							<th>详细地址</th>

																							<th>厂家名称</th>
																						
																							<th>商品价格</th>
																							<th>成本价格</th>
																							<th>商品规格</th>
										  													
	<th>支付时间</th><th>购买者角色</th>	
																				</tr>");

				foreach (var item in lstGetGoodShipItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";

					sbGoodsShipSearchInfo.Append(@"<tr class='" + cssName + "'>");
					var display = item.GoodShipStatus == '0' ? "" : "disabled='disabled'";
					sbGoodsShipSearchInfo.Append(@"<td><input type='checkbox' name='option1' " + display + " value='" + item.BuyGoodsid + "' /></td>");
					sbGoodsShipSearchInfo.Append(@"<script>var orderno ='';function selectAll(){if ($('#chkAll').attr('checked')) 
                                                       {  
                                                     $(':checkbox').attr('checked', true);  for (var i = 1; i < $(':checkbox').length; i++) {
                                                                orderno += $(':checkbox')[i].value + ';';};$('#hfSeleckSo').val(orderno);
                                                        } else {  
                                                            $(':checkbox').attr('checked', false);  orderno='';
                                                        }  }</script>");
					var status = string.Empty;
					if (item.GoodShipStatus == '0' || item.GoodShipStatus == null)
					{
						status = " 未发货";
					}
					else { status = " 已发货"; }
					sbGoodsShipSearchInfo.Append(@"<td style='color:red;font-weight:bold'>" + status + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td >" + item.GoodShipNo + "</td>");

					var gudong = item.SjGuDongInfo == null ? "无上级股东信息" : item.SjGuDongInfo;
					var zhanzhang = item.SjZhanZhangInfo == null ? "无上级站长信息" : item.SjZhanZhangInfo;
					sbGoodsShipSearchInfo.Append(@"<td>" + gudong + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + zhanzhang + "</td>");

					sbGoodsShipSearchInfo.Append(@"<td>" + item.OrderNo + "</td>");

					sbGoodsShipSearchInfo.Append(@"<td>" + item.OrderPrice + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.name + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.tel + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td style='text-align:left'>" + item.GoodsTitle + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.BuySumQty + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.housemoney + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.areacity + "</td>");

					sbGoodsShipSearchInfo.Append(@"<td style='text-align:left'>" + item.address + "</td>");


					sbGoodsShipSearchInfo.Append(@"<td style='text-align:left'>" + item.GoodsCompany + "</td>");


					sbGoodsShipSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.GoodsCost + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.GoodsSpec + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.PaymentTime + "</td>");
					sbGoodsShipSearchInfo.Append(@"<td>" + item.juese + "</td></tr>");
					rowCount++;


				}
				returnResult = sbGoodsShipSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}



		/// <summary>
		/// i.	地区分析
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserAreaInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getUserAreaData = SearchDataClass.GetSearchUserAreaInfoData();
			if (getUserAreaData.ToList().Count > 0)
			{
				pageInfo = getUserAreaData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserAreaData.ToList().Count % pageSize == 0 ?
(getUserAreaData.ToList().Count / pageSize) : (getUserAreaData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getUserAreaData.ToList().Count > pageSize ? getUserAreaData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserAreaData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserAreaSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbUserAreaSearchInfo.Append(@"<td>" + item.Name + "</td>");
					sbUserAreaSearchInfo.Append(@"<td>" + item.Tel + "</td>");
					sbUserAreaSearchInfo.Append(@"<td >" + item.Areacity + "</td>");

					sbUserAreaSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbUserAreaSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// ii.	年龄、性别等信息分析
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserAgeSexInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getUserAgeSexData = SearchDataClass.GetSearchUserAgeSexInfoData();
			if (getUserAgeSexData.ToList().Count > 0)
			{
				pageInfo = getUserAgeSexData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserAgeSexData.ToList().Count % pageSize == 0 ?
(getUserAgeSexData.ToList().Count / pageSize) : (getUserAgeSexData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getUserAgeSexData.ToList().Count > pageSize ? getUserAgeSexData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserAgeSexData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserAgeSexSearchInfo.Append(@"<tr class='" + cssName + "'>");
					var sex = item.Sex == 'Y' ? "男" : "女";
			  	sbUserAgeSexSearchInfo.Append(@"<td>" + sex + "</td>");
					//DateTime dtNow = DateTime.Now;
					//DateTime dtBirdth = DateTime.Parse(item.Age);
					//var tsAge = dtNow.Year - dtBirdth.Year;
					sbUserAgeSexSearchInfo.Append(@"<td>" + item.Age + "</td>");
					sbUserAgeSexSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbUserAgeSexSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// ii.	i.	销售排行榜
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGoodSalesInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getGoodSalesData = SearchDataClass.GetSearchGoodSalesTotalData();
			if (getGoodSalesData.ToList().Count > 0)
			{
				pageInfo = getGoodSalesData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodSalesData.ToList().Count % pageSize == 0 ?
(getGoodSalesData.ToList().Count / pageSize) : (getGoodSalesData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getGoodSalesData.ToList().Count > pageSize ? getGoodSalesData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodSalesData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodSalesSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodSalesSearchInfo.Append(@"<td>" + item.GoodsTitle + "</td>");

					sbGoodSalesSearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
					 var imgUrl = item.GoodsImg == null ? "assets/images/nophoto.gif" : item.GoodsImg;
					sbGoodSalesSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:60px;height:60px;'/></td>");
				  
					sbGoodSalesSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbGoodSalesSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// ii.iii.	地区消费排行榜分析
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGoodAreaSalesInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getGoodAreaSalesData = SearchDataClass.GetSearchGoodAreaSalesTotalData();
			if (getGoodAreaSalesData.ToList().Count > 0)
			{
				pageInfo = getGoodAreaSalesData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodAreaSalesData.ToList().Count % pageSize == 0 ?
(getGoodAreaSalesData.ToList().Count / pageSize) : (getGoodAreaSalesData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getGoodAreaSalesData.ToList().Count > pageSize ? getGoodAreaSalesData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodAreaSalesData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodAreaSalesSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodAreaSalesSearchInfo.Append(@"<td>" + item.AreaCity + "</td>");
					sbGoodAreaSalesSearchInfo.Append(@"<td>" + item.SumPrice + "</td>");
 
					sbGoodAreaSalesSearchInfo.Append(@"<td>" + item.BuySumQty + "</td>");
					sbGoodAreaSalesSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbGoodAreaSalesSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// ii.	商品销售分类排行榜
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGoodSortSalesInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getGoodSortSalesData = SearchDataClass.GetSearchGoodSortSalesTotalData();
			if (getGoodSortSalesData.ToList().Count > 0)
			{
				pageInfo = getGoodSortSalesData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodSortSalesData.ToList().Count % pageSize == 0 ?
(getGoodSortSalesData.ToList().Count / pageSize) : (getGoodSortSalesData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getGoodSortSalesData.ToList().Count > pageSize ? getGoodSortSalesData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodSortSalesData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodSortSalesSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodSortSalesSearchInfo.Append(@"<td>" + item.SortName + "</td>");
					sbGoodSortSalesSearchInfo.Append(@"<td>" + item.BuySumQty + "</td>");

					sbGoodSortSalesSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbGoodSortSalesSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// iv.	商品销售数据检索
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetGoodSalesSearchInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getGoodSalesSearchData = SearchDataClass.GetSearchGoodSalesSearchTotalData();
			if (getGoodSalesSearchData.ToList().Count > 0)
			{
				pageInfo = getGoodSalesSearchData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodSalesSearchData.ToList().Count % pageSize == 0 ?
(getGoodSalesSearchData.ToList().Count / pageSize) : (getGoodSalesSearchData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getGoodSalesSearchData.ToList().Count > pageSize ? getGoodSalesSearchData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodSalesSearchData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbGoodSalesSearchSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbGoodSalesSearchSearchInfo.Append(@"<td>" + item.GoodsTitle + "</td>");
					sbGoodSalesSearchSearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
					var imgUrl = item.GoodsImg == null ? "assets/images/nophoto.gif" : item.GoodsImg;
					sbGoodSalesSearchSearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:60px;height:60px;'/></td>");

					sbGoodSalesSearchSearchInfo.Append(@"<td>" + item.SelectytSort + "</td>");
					sbGoodSalesSearchSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbGoodSalesSearchSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// iv.		用户订单分类分析索
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserOrderTypeFenXiSearchInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getUserOrderTypeFenXiSearchData = SearchDataClass.GetSearchUserOrderTypeFenXiData();
			if (getUserOrderTypeFenXiSearchData.ToList().Count > 0)
			{
				pageInfo = getUserOrderTypeFenXiSearchData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserOrderTypeFenXiSearchData.ToList().Count % pageSize == 0 ?
(getUserOrderTypeFenXiSearchData.ToList().Count / pageSize) : (getUserOrderTypeFenXiSearchData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getUserOrderTypeFenXiSearchData.ToList().Count > pageSize ? getUserOrderTypeFenXiSearchData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserOrderTypeFenXiSearchData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserOrderTypeFenXiSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbUserOrderTypeFenXiSearchInfo.Append(@"<td>" + item.SumPrice + "</td>");
					sbUserOrderTypeFenXiSearchInfo.Append(@"<td>" + item.SumCount + "</td>");
					sbUserOrderTypeFenXiSearchInfo.Append(@"<td>" + item.BuySumQty + "</td>");
					var orderStatus = string.Empty;
					switch (item.OrderStatus)
					{
						case '0':
							orderStatus = "创建订单待付款";
							break;
						case '1':
							orderStatus = "已付款未发货";
							break;
						case '2':
							orderStatus = "已发货未确认";
							break;
						case '3':
							orderStatus = "订单退货/退款中";
							break;
						case '4':
							orderStatus = "确认收货已完成";
							break;
						case '5':
							orderStatus = "订单已取消";
							break;
						default: break;
					}
					sbUserOrderTypeFenXiSearchInfo.Append(@"<td>" + orderStatus + "</td>");
					sbUserOrderTypeFenXiSearchInfo.Append(@"<td >" + item.PayMode + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbUserOrderTypeFenXiSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

		/// <summary>
		/// iv.	ii.	用户消费分析
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetUserOrderSalesFenXiSearchInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getUserOrderSalesFenXiSearchData = SearchDataClass.GetSearchUserOrderSalesFenXiData();
			if (getUserOrderSalesFenXiSearchData.ToList().Count > 0)
			{
				pageInfo = getUserOrderSalesFenXiSearchData.ToList().Count + "," + pageSize + "," + currPage + "," + (getUserOrderSalesFenXiSearchData.ToList().Count % pageSize == 0 ?
(getUserOrderSalesFenXiSearchData.ToList().Count / pageSize) : (getUserOrderSalesFenXiSearchData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getUserOrderSalesFenXiSearchData.ToList().Count > pageSize ? getUserOrderSalesFenXiSearchData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getUserOrderSalesFenXiSearchData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbUserOrderSalesFenXiSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbUserOrderSalesFenXiSearchInfo.Append(@"<td>" + item.BuyName + "</td>");
					sbUserOrderSalesFenXiSearchInfo.Append(@"<td>" + item.BuyTel + "</td>");
					sbUserOrderSalesFenXiSearchInfo.Append(@"<td>" + item.Areacity + "</td>");

					sbUserOrderSalesFenXiSearchInfo.Append(@"<td>" + item.SumPrice + "</td>");
					sbUserOrderSalesFenXiSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbUserOrderSalesFenXiSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}


		/// <summary>
		/// ii.	意见收集整理
		/// </summary>
		/// <param name="currPage"></param>
		/// <returns></returns>
		public string GetMessageViewSearchInfo(string currPage)
		{
			var returnResult = string.Empty;
			var getMessageViewSearchData = SearchDataClass.GetSearchMessageViewData();
			if (getMessageViewSearchData.ToList().Count > 0)
			{
				pageInfo = getMessageViewSearchData.ToList().Count + "," + pageSize + "," + currPage + "," + (getMessageViewSearchData.ToList().Count % pageSize == 0 ?
(getMessageViewSearchData.ToList().Count / pageSize) : (getMessageViewSearchData.ToList().Count / pageSize + 1));

				var lstGetMoneyItemData = (getMessageViewSearchData.ToList().Count > pageSize ? getMessageViewSearchData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
				 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getMessageViewSearchData.ToList()).ToList();
				foreach (var item in lstGetMoneyItemData)
				{
					var cssName = rowCount % 2 == 0 ? "warning" : "success";
					sbMessageViewSearchInfo.Append(@"<tr class='" + cssName + "'>");
					sbMessageViewSearchInfo.Append(@"<td>" + item.Messagename + "</td>");
					sbMessageViewSearchInfo.Append(@"<td>" + item.Messagetel + "</td>");
					sbMessageViewSearchInfo.Append(@"<td>" + item.Messagecontent + "</td>");

					sbMessageViewSearchInfo.Append(@"<td >" + item.Messagetime + "</td> </tr>");

					rowCount++;
				}
				returnResult = sbMessageViewSearchInfo.ToString() + "_" + pageInfo;
			}
			else
			{
				returnResult = "NoData";
			}
			return returnResult;

		}

        /// <summary>
        /// ii.	分销层级分析
        /// </summary>
        /// <param name="currPage"></param>
        /// <returns></returns>
        public string GetMemberJiBieFenXiSearchInfo(string currPage)
        {
            var returnResult = string.Empty;
            var MemberJiBieFenXi = SearchDataClass.GetSearchMemberJiBieFenXiData().OrderByDescending(x=>x.SumCount);
            if (MemberJiBieFenXi.ToList().Count > 0)
            {
                pageInfo = MemberJiBieFenXi.ToList().Count + "," + pageSize + "," + currPage + "," + (MemberJiBieFenXi.ToList().Count % pageSize == 0 ?
(MemberJiBieFenXi.ToList().Count / pageSize) : (MemberJiBieFenXi.ToList().Count / pageSize + 1));

                var lstGetMoneyItemData = (MemberJiBieFenXi.ToList().Count > pageSize ? MemberJiBieFenXi.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
                 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : MemberJiBieFenXi.ToList()).ToList();
                foreach (var item in lstGetMoneyItemData)
                {
                    var cssName = rowCount % 2 == 0 ? "warning" : "success";
                    sbMemberJiBieFenXiSearchInfo.Append(@"<tr class='" + cssName + "'>");
                    sbMemberJiBieFenXiSearchInfo.Append(@"<td>" + item.FenXiaoName + "</td>");
                    sbMemberJiBieFenXiSearchInfo.Append(@"<td>" + item.FenXiaoID + "</td>");
                    sbMemberJiBieFenXiSearchInfo.Append(@"<td>" + item.FenXiaoTel + "</td>");
                    sbMemberJiBieFenXiSearchInfo.Append(@"<td>" + item.Membertype + "</td>");
                    sbMemberJiBieFenXiSearchInfo.Append(@"<td >" + item.SumCount + "</td> </tr>");

                    rowCount++;
                }
                returnResult = sbMemberJiBieFenXiSearchInfo.ToString() + "_" + pageInfo;
            }
            else
            {
                returnResult = "NoData";
            }
            return returnResult;

        }

        /// <summary>
        /// 商品库龄分析
        /// </summary>
        /// <param name="currPage"></param>
        /// <returns></returns>
        public string GetGoodsStockAgeFenXiSearchInfo(string currPage)
        {
            var returnResult = string.Empty;
            var getStockAgeFenXi = SearchDataClass.GetSearchGoodsStockAgeData();
            if (getStockAgeFenXi.ToList().Count > 0)
            {
                pageInfo = getStockAgeFenXi.ToList().Count + "," + pageSize + "," + currPage + "," + (getStockAgeFenXi.ToList().Count % pageSize == 0 ?
(getStockAgeFenXi.ToList().Count / pageSize) : (getStockAgeFenXi.ToList().Count / pageSize + 1));

                var lstGetMoneyItemData = (getStockAgeFenXi.ToList().Count > pageSize ? getStockAgeFenXi.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
                 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getStockAgeFenXi.ToList()).ToList();
                foreach (var item in lstGetMoneyItemData)
                {
                    var cssName = rowCount % 2 == 0 ? "warning" : "success";
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<tr class='" + cssName + "'>");
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + item.GoodsTitle + "</td>");
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + item.GoodsStock + "</td>");
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + item.SortName + "</td>");

                    TimeSpan tsStockage = Convert.ToDateTime(DateTime.Now.ToShortDateString())- Convert.ToDateTime(item.Addtime);
                   // var stockage = DateTime.Now.Day - Convert.ToDateTime(item.Addtime).Day;
                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td>" + tsStockage.Days + "</td>");
                    TimeSpan tsSyDate = Convert.ToDateTime(item.ExpireDate) - Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    sbGoodsStockAgeFenXiSearchInfo.Append(@"<td >" + tsSyDate.Days + "</td> </tr>");

                    rowCount++;
                }
                returnResult = sbGoodsStockAgeFenXiSearchInfo.ToString() + "_" + pageInfo;
            }
            else
            {
                returnResult = "NoData";
            }
            return returnResult;

        }
        /// <summary>
        /// 发货商品管理
        /// </summary>
        /// <param name="currPage"></param>
        /// <returns></returns>
        public string GetGoodsDeliverySearchInfo(string currPage)
        {
            var returnResult = string.Empty;
            var getGoodsDelivery = SearchDataClass.GetSearchGoodsDeliveryData();
            if (getGoodsDelivery.ToList().Count > 0)
            {
                pageInfo = getGoodsDelivery.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsDelivery.ToList().Count % pageSize == 0 ?
(getGoodsDelivery.ToList().Count / pageSize) : (getGoodsDelivery.ToList().Count / pageSize + 1));

                var lstGetMoneyItemData = (getGoodsDelivery.ToList().Count > pageSize ? getGoodsDelivery.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
                 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsDelivery.ToList()).ToList();
                foreach (var item in lstGetMoneyItemData)
                {
                    var cssName = rowCount % 2 == 0 ? "warning" : "success";
                    sbGoodsDeliverySearchInfo.Append(@"<tr class='" + cssName + "'>");
                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.OrderNo + "</td>");
                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.GoodsTitle + "</td>");
                    var imgUrl = item.GoodsImg == null ? "assets/images/nophoto.gif" : item.GoodsImg;
                    sbGoodsDeliverySearchInfo.Append(@"<td><img src=" + imgUrl + " style='width:60px;height:60px;'/></td>");

                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.GoodsStock + "</td>");
                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
                    sbGoodsDeliverySearchInfo.Append(@"<td>" + item.BuysumQty + "</td>");

                    sbGoodsDeliverySearchInfo.Append(@"<td >" + item.SelectytSort + "</td> </tr>");

                    rowCount++;
                }
                returnResult = sbGoodsDeliverySearchInfo.ToString() + "_" + pageInfo;
            }
            else
            {
                returnResult = "NoData";
            }
            return returnResult;

        }
        /// <summary>
        /// 剩余商品库存
        /// </summary>
        /// <param name="currPage"></param>
        /// <returns></returns>
        public string GetGoodsSyStockSearchInfo(string currPage)
        {
            var returnResult = string.Empty;
            var getGoodsSyStock = SearchDataClass.GetSearchGoodsSyStockData();
            if (getGoodsSyStock.ToList().Count > 0)
            {
                pageInfo = getGoodsSyStock.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsSyStock.ToList().Count % pageSize == 0 ?
(getGoodsSyStock.ToList().Count / pageSize) : (getGoodsSyStock.ToList().Count / pageSize + 1));

                var lstGetMoneyItemData = (getGoodsSyStock.ToList().Count > pageSize ? getGoodsSyStock.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
                 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsSyStock.ToList()).ToList();
                foreach (var item in lstGetMoneyItemData)
                {
                    var cssName = rowCount % 2 == 0 ? "warning" : "success";
                    sbGoodsSyStockSearchInfo.Append(@"<tr class='" + cssName + "'>");
                    sbGoodsSyStockSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
                    sbGoodsSyStockSearchInfo.Append(@"<td>" + item.GoodsTitle + "</td>");
                    sbGoodsSyStockSearchInfo.Append(@"<td>" + item.SortName + "</td>");
                    sbGoodsSyStockSearchInfo.Append(@"<td>" + item.GoodsStock + "</td>");
                    sbGoodsSyStockSearchInfo.Append(@"<td>" + item.GoodsPrice + "</td>");
                 
                    sbGoodsSyStockSearchInfo.Append(@"<td >" + item.GoodsCost + "</td> </tr>");

                    rowCount++;
                }
                returnResult = sbGoodsSyStockSearchInfo.ToString() + "_" + pageInfo;
            }
            else
            {
                returnResult = "NoData";
            }
            return returnResult;

        }

        /// <summary>
        /// 入库管理信息查询及删除后查询
        /// </summary>
        /// <param name="currPage">页数</param>
        /// <returns></returns>
        public string GetGoodsInWareHouseInfo(string goodtitle, string goodsort, string goodsaddtime, string goodsku, string currPage)
        {

            var returnResult = string.Empty;
            var getGoodsInWHData = SearchDataClass.GetSearchGoodsInWareHouseData(goodtitle, goodsort, goodsaddtime, goodsku);
            if (getGoodsInWHData.ToList().Count > 0)
            {
                pageInfo = getGoodsInWHData.ToList().Count + "," + pageSize + "," + currPage + "," + (getGoodsInWHData.ToList().Count % pageSize == 0 ?
(getGoodsInWHData.ToList().Count / pageSize) : (getGoodsInWHData.ToList().Count / pageSize + 1));

                var lstGetGoodsItemData = (getGoodsInWHData.ToList().Count > pageSize ? getGoodsInWHData.ToList().Skip(int.Parse(currPage) == 1 ? 0 :
                 pageSize * (int.Parse(currPage) - 1)).Take(pageSize) : getGoodsInWHData.ToList()).ToList();
                foreach (var item in lstGetGoodsItemData)
                {
                    var cssName = rowCount % 2 == 0 ? "warning" : "success";
                    sbGoodsInWareHouseSearchInfo.Append(@"<tr class='" + cssName + "'>");
                    if (item.SelectytSort == 'P' || item.SelectytSort == 'T')
                    {
                        var mid = item.SelectytSort == 'P' ? "16,18" : "16,19";
                        sbGoodsInWareHouseSearchInfo.Append(@"<td><div style='padding-bottom:10px; '><a class='btn btn-info' href='GoodsAdd.aspx?mid=" + mid + "&type=" + item.SelectytSort + "&upid=" + item.ID + "'>编辑</a></div>  ");
                    }
                    else
                    {
                        var mid = item.SelectytSort == 'P' ? "16,20" : "16,21";
                        sbGoodsInWareHouseSearchInfo.Append(@"<td><div style='padding-bottom:10px; '><a class='btn btn-info' href='GroupBuyAdd.aspx?mid=" + mid + "&type=" + item.SelectytSort + "&upid=" + item.ID + "'>编辑</a></div>  ");
                    }
                    sbGoodsInWareHouseSearchInfo.Append(@"<input type='button' class='btn btn-info'  onclick=DeleteGoodsInWareHouseInfo('" + item.ID + "," + item.Title + "') style='width: 55px; height: 28px;'  value='删除' />  </td> ");
       
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.Title.Trim() + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.GoodsSort + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.GoodsCode + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.GoodsSpec + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.GoodsPrice +"/"+ item.GoodsCost + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.GoodsStock + "/" + item.Buysumqty + "</td>");
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + item.AddTime + "</td>");
                    var SelectytSort = string.Empty;
                    switch (Convert.ToChar( item.SelectytSort.ToString().ToUpper()))
                    {
                        case 'P':
                            SelectytSort = "普通商品";
                            break;
                        case 'T':
                            SelectytSort = "推荐商品";
                            break;
                        case 'G':
                            SelectytSort = "团购商品";
                            break;
                        case 'Y':
                            SelectytSort = "预售商品";
                            break;
                    }
                    sbGoodsInWareHouseSearchInfo.Append(@"<td>" + SelectytSort + "</td></tr>");

                    rowCount++;
                }
                returnResult = sbGoodsInWareHouseSearchInfo.ToString() + "_" + pageInfo;
            }
            else
            {
                returnResult = "NoData";
            }
            return returnResult;

        }

		/// <summary>
		/// 取得邀请码上级信息
		/// </summary>
		/// <param name="invcode"></param>
		/// <returns></returns>
		public List<ShangJiGDZZInfo> ReturnSJGDZZ(string invcode)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<ShangJiGDZZInfo> lstSJ = (
													from a in db.UserInfo
													where a.tel == invcode
													select new ShangJiGDZZInfo
													{
														ID = a.Id,
														Tel = a.tel,
														InvitedCode = a.invitedcode,
														JueSe = a.juese,
														GuDongJiBie = a.gudongjibie
													}).ToList<ShangJiGDZZInfo>();

				return lstSJ;
			}
		}
		/// <summary>
		/// 上级股东 站长
		/// </summary>
		public class ShangJiGDZZInfo
		{
			public int ID { get; set; }
			public string Tel { get; set; }
			public string JueSe { get; set; }
			public string InvitedCode { get; set; }
			public string GuDongJiBie { get; set; }
			public string SjGuDong { get; set; }
			public string SjZhanZhang { get; set; }
		}
		#region//过滤简单的HTML代码去
		/// <summary>
		/// 过滤简单的HTML代码去 图片空格。之类
		/// </summary>
		public static string Subfilter(string html)
		{
			Regex regex1 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(@"</p>", RegexOptions.IgnoreCase);
			Regex regex3 = new Regex(@"<p>", RegexOptions.IgnoreCase);
			Regex regex4 = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
			Regex regex5 = new Regex(@"&nbsp;", RegexOptions.IgnoreCase);
			Regex regex6 = new Regex(@"&gt;", RegexOptions.IgnoreCase);

			html = regex1.Replace(html, ""); //过滤frameset 
			html = regex2.Replace(html, ""); //过滤frameset 
			html = regex3.Replace(html, ""); //过滤frameset 
			html = regex4.Replace(html, "");
			html = regex5.Replace(html, "");//过滤空格
			html = regex6.Replace(html, "");
			html = html.Replace(" ", "");
			html = html.Replace("</strong>", "");
			html = html.Replace("<strong>", "");
			return html;
		}
		#endregion

		public class DaysGoodsSum
		{
			public DateTime? Day { get; set; }
			public string GoodsTitle { get; set; }
			public int GoodsSum { get; set; }

		}
		public class MonethOrdersNo
		{
			public DateTime? Day { get; set; }
			public int DayOrderNo { get; set; }
		}

		/// <summary>
		/// 定义一个Series类 设置其每一组sereis的一些基本属性
		/// </summary>
		/// 
		public class Series
		{
			/// <summary>
			/// series序列组名称
			/// </summary>
			public string name { get; set; }
			/// <summary>
			/// series序列组呈现图表类型(line、column、bar等)
			/// </summary>
			public string type { get; set; }
			/// <summary>
			/// series序列组的数据为数据类型数组
			/// </summary>
			public List<int> data { get; set; }
		}
		/// <summary>
		/// 获取登陆IP
		/// </summary>
		/// <returns></returns>
		public static string GetIPAddress()
		{
			string ipv4 = String.Empty;
			foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
			{
				if (IPA.AddressFamily.ToString() == "InterNetwork")
				{
					ipv4 = IPA.ToString();
					break;
				}
			}
			if (ipv4 != String.Empty)
			{
				return ipv4;
			}
			foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				if (IPA.AddressFamily.ToString() == "InterNetwork")
				{
					ipv4 = IPA.ToString();
					break;
				}
			}
			return ipv4;
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	}

}