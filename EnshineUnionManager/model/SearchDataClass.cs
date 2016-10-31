using EnshineUnionManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace EnshineUnionManager.model
{
	public class SearchDataClass
	{
		/// <summary>
		/// 管理员信息
		/// </summary>
		/// <returns></returns>
		public static List<SearchAdminInfoData> GetSearchAdminInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchAdminInfoData> getAdminData = (from p in db.superadmin
																									join pp in db.userrole on p.roleid equals pp.roleid into g
																									from c in g.DefaultIfEmpty()
																									orderby p.addtime descending
																									select new SearchAdminInfoData
																									{
																										AdminID = p.Id,
																										AdminName = p.name,
																										AdminPassWord = p.pass,
																										Lastloginip = p.lastloginip,
																										Lastlogintime = p.lastlogintime,
																										RoleName = c.rolename,
																										IfDisable = p.ifdisable,
																										TrueName = p.truename,
																										ContactTel = p.contacttel,
																										AddTime = p.addtime
																									}).ToList<SearchAdminInfoData>();
				return getAdminData;
			}
		}
		public static List<SearchUserInfoData> GetSearchUserInfoData(string strNickName, string strName, string strInvitedcode)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchUserInfoData> getUserInfoData = (from p in db.UserInfo

																										select new SearchUserInfoData
																										{
																											ID = p.Id,
																											Nickname = p.nickname,
																											Sex = p.sex,
																											Name = p.name,
																											PassWord = p.pass,
																											Tel = p.tel,
																											Areacity = p.areacity,
																											HeadImg = p.headimg,
																											Address = p.address,
																											InvitedCode = p.invitedcode,
																											UserType = p.usertype,
																											AddTime = p.addtime
																										}).ToList<SearchUserInfoData>();
				//昵称
				if (!string.IsNullOrEmpty(strNickName))
				{
					getUserInfoData = (from p in getUserInfoData where p.Nickname.Contains(strNickName) select p).ToList<SearchUserInfoData>();
				}
				//真实姓名
				if (!string.IsNullOrEmpty(strName))
				{
					getUserInfoData = (from p in getUserInfoData where p.Name.Contains(strName) select p).ToList<SearchUserInfoData>();
				}

				if (!string.IsNullOrEmpty(strInvitedcode))
				{
					getUserInfoData = (from p in getUserInfoData where p.InvitedCode.Contains(strInvitedcode) select p).ToList<SearchUserInfoData>();
				}
				return getUserInfoData;
			}
		}


		/// <summary>
		/// 广告信息列表
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchAdData> GetSearchAdData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchAdData> getAdData = (from p in db.Advertisement
																				select new SearchAdData
																				{
																					ID = p.AdID,
																					AdTitle = p.AdTitle,
																					AdContent = p.AdContent,
																					AdImg = p.AdImg,
																					AdCompany = p.Company,
																					SetIndex = p.setindex,
																					GoodsCode = p.goodscode,
																					AddTime = p.Addtime
																				}).ToList<SearchAdData>();
				return getAdData;
			}
		}


		/// <summary>
		/// 新闻/公告信息
		/// </summary>
		/// <returns></returns>
		public static List<SearchNoticesInfoData> GetSearchNoticesInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchNoticesInfoData> getNoticesData = (from p in db.notices
																											join pp in db.noticessort on p.newssort equals pp.sortId into g
																											from c in g.DefaultIfEmpty()
																											orderby p.addtime descending
																											select new SearchNoticesInfoData
																											{
																												ID = p.Id,
																												Title = p.title,
																												Content = p.content,
																												SetIndex = p.setindex,
																												Validate = p.validate,
																												NewsSort = c.sortName,
																												NewsSource = p.newssource,
																												Img = p.img,
																												AddTime = p.addtime
																											}).ToList<SearchNoticesInfoData>();
				return getNoticesData;
			}
		}

		/// <summary>
		///新闻/公告分类
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchNewsSortData> GetSearchNewsSortData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchNewsSortData> getNewsSortData = (from p in db.noticessort
																										select new SearchNewsSortData
																										{
																											ID = p.sortId,
																											NewsSortName = p.sortName,

																											AddTime = p.addTime
																										}).ToList<SearchNewsSortData>();
				return getNewsSortData;
			}
		}

		/// <summary>
		/// 新闻/公告评论信息
		/// </summary>
		/// <returns></returns>
		public static List<SearchNewsCommentInfoData> GetSearchNewsCommentInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchNewsCommentInfoData> getNoticesData = (from a in db.noticesdiscuss
																													join b in db.notices on new { noticeid = a.noticeid } equals new { noticeid = b.Id }
																													join c in db.noticessort on new { sortId = b.newssort } equals new { sortId = c.sortId }
																													join d in db.UserInfo on new { discussuserid = a.discussuserid } equals new { discussuserid = d.Id }
																													orderby a.discusstime descending
																													select new SearchNewsCommentInfoData
																													{
																														ID = a.discussid,
																														Title = b.title,
																														NewsSort = c.sortName,
																														DiscussPeople = d.name,
																														DiscussUserID = a.discussuserid,
																														Content = a.discusscontent,
																														AddTime = a.discusstime
																													}).ToList<SearchNewsCommentInfoData>();
				return getNoticesData;
			}
		}

		/// 活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchHuoDongData> GetSearchHuoDongData(string strTitle, string strActive)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchHuoDongData> getHuoDongData = (from p in db.HuoDong
																									join pp in db.HuoDongSort on p.huodongsort equals pp.sortId into g
																									from c in g.DefaultIfEmpty()
																									orderby p.addTime descending
																									select new SearchHuoDongData
																									{
																										ID = p.huodongId,
																										HuodongTitle = p.huodongTitle,
																										HuodongSort = c.sortName,
																										HuodongPeople = p.huodongPeople,
																										HuodongKeyWord = p.huodongKeyWord,
																										HuodongDate = p.huodongDate,
																										HuodongImg = p.huodongImg,
																										HuodongActive = p.huodongActive,
																										HuodongContent = p.huodongContent,
																										SetIndex = p.setindex,
																										AddTime = p.addTime
																									}).ToList<SearchHuoDongData>();
				//方案标题
				if (!string.IsNullOrEmpty(strTitle))
				{
					getHuoDongData = (from p in getHuoDongData where p.HuodongTitle.Contains(strTitle) select p).ToList<SearchHuoDongData>();
				}
				//审核状态
				if (!string.IsNullOrEmpty(strActive) && strActive != "-请选择审核状态-")
				{
					getHuoDongData = (from p in getHuoDongData where p.HuodongActive.ToString().Contains(strActive) select p).ToList<SearchHuoDongData>();
				}

				return getHuoDongData;
			}
		}

		/// <summary>
		///活动分类
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchHuoDongSortData> GetSearchHuoDongSortData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchHuoDongSortData> getHuoDongSortData = (from p in db.HuoDongSort
																													orderby p.addTime descending
																													select new SearchHuoDongSortData
																													{
																														ID = p.sortId,
																														NewsSortName = p.sortName,

																														AddTime = p.addTime
																													}).ToList<SearchHuoDongSortData>();
				return getHuoDongSortData;
			}
		}

		/// <summary>
		/// 参与活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchJoinHuoDongData> GetSearchJoinHuoDongData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchJoinHuoDongData> getJoinHuoDongData = (from p in db.JoinHuoDong
																													join us in db.HuoDong on p.huodongid equals us.huodongId into g
																													from x in g.DefaultIfEmpty()
																														//	join ux in db.Ticket on p.ticketid equals ux.tickedId into gg
																														//from xx in gg.DefaultIfEmpty()
																													join ug in db.UserInfo on p.userid equals ug.Id

																													orderby p.addTime descending
																													select new SearchJoinHuoDongData
																													{
																														ID = p.Id,
																														HuodongTitle = x.huodongTitle,
																														HuodongContent = x.huodongContent,
																														Tel = ug.tel,
																														HuodongDate = x.huodongDate,
																														UserName = ug.name,
																														UserNickName = ug.nickname,
																														AddTime = p.addTime
																													}).ToList<SearchJoinHuoDongData>();

				return getJoinHuoDongData;
			}
		}


		/// <summary>
		/// 参与活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchJoinHuoDongInfoData> GetSearchJoinHuoDongInfoData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchJoinHuoDongInfoData> getJoinHuoDongData = (from a in db.JoinHuoDongInfo
																															join b in db.HuoDong on new { huodongid = a.huodongid } equals new { huodongid = b.huodongId }
																															join c in db.HuoDongSort on new { huodongsort = a.huodongsort } equals new { huodongsort = c.sortId }
																															join d in db.UserInfo on new { userid = a.userid } equals new { userid = d.Id }
																															orderby a.addTime descending
																															select new SearchJoinHuoDongInfoData
																															{
																																ID = a.id,
																																HuodongTitle = b.huodongTitle,
																																HuodongSort = c.sortName,
																																HuodongDate = b.huodongDate,
																																UserName = d.name,
																																UserTel = d.tel,
																																Joinnumber = a.joinnumber,
																																JoinSex = a.joinsex,
																																Tel = a.jointel,
																																JoinAge = a.age,
																																Job = a.job,
																																Iinterest = a.interest,
																																Remarks = a.remarks,
																																AddTime = a.addTime

																															}).ToList<SearchJoinHuoDongInfoData>();

				return getJoinHuoDongData;
			}
		}


		/// 参与团购.预售活动信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchJoinGoodsHuoDongData> GetSearchJoinGoodsHuoDongData(string strTitle, string strSaleName)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchJoinGoodsHuoDongData> getHuoDongData = (
					from a in db.JoinHuoDong
					join b in db.goodstuan on new { huodongid = a.huodongid } equals new { huodongid = b.Id }
					join c in db.UserInfo on new { Id = a.userid } equals new { Id = c.Id }
					join d in db.goodssort on new { groupsort = b.groupsort } equals new { groupsort = d.sortId }
					select new SearchJoinGoodsHuoDongData
					{
						ID = a.Id,
						UserName = c.name,
						Tel = c.tel,
						AddTime = a.addTime,
						HuodongTitle = b.title,
						HuodongSort = d.sortName,
						HuodongStartTime = b.starttime,
						HuodongEndTime = b.endtime,
						HuodongPrice = b.price,
						HuodongGoodsCode = b.goodscode,
						SelectytSort = b.selectytsort
					}
					).ToList<SearchJoinGoodsHuoDongData>();
				//方案标题
				if (!string.IsNullOrEmpty(strTitle))
				{
					getHuoDongData = (from p in getHuoDongData where p.HuodongTitle.Contains(strTitle) select p).ToList<SearchJoinGoodsHuoDongData>();
				}
				//审核状态
				if (!string.IsNullOrEmpty(strSaleName) )
				{
					getHuoDongData = (from p in getHuoDongData where p.UserName.ToString().Contains(strSaleName) select p).ToList<SearchJoinGoodsHuoDongData>();
				}

				return getHuoDongData;
			}
		}
		/// <summary>
		/// 商品信息查看
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodsInfoData> GetSearchGoodsInfoData(string type, string goodtitle, string goodsort, string goodststus, string goodsku)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodsInfoData> getGoodssData = (from p in db.goods
																									 join pp in db.goodssort on new { goodssort = p.goodssort } equals new { goodssort = pp.sortId }
															where p.selectytsort==Convert.ToChar(type)										 
                                                           orderby p.addtime descending
																									 select new SearchGoodsInfoData
																									 {
																										 ID = p.goodsId,
																										 Title = p.goodstitle,
																										 GoodsCode = p.goodscode,
																										 GoodsPrice = p.goodsprice,
																										 GoodsCost = p.goodscost,
																										 GoodsStock = p.goodstock,
																										 Content = p.goodscontent,
																										 SetIndex = p.setindex,
																										 Validate = p.goodsvalidate,
																										 GoodSales = p.goodsSales,
																										 GoodsSortID =pp.sortId,
																										 GoodsSort = pp.sortName,
																										 Img = p.goodsimg,
																										 Iftuangou=p.iftuangou,
                                                     ExchangeGood = p.ifexchange,PuTongTuiJian=p.selectytsort,
																										 ExpireDate=p.expiredate,
                                                     AddTime = p.addtime
																									 }).ToList<SearchGoodsInfoData>();
				//商品名称
				if (!string.IsNullOrEmpty(goodtitle))
				{
					getGoodssData = (from p in getGoodssData where p.Title.Contains(goodtitle) select p).ToList<SearchGoodsInfoData>();
				}
				//商品分类
				if (!string.IsNullOrEmpty(goodsort) && goodsort != "商品分类")
				{
					getGoodssData = (from p in getGoodssData where p.GoodsSortID == int.Parse(goodsort) select p).ToList<SearchGoodsInfoData>();
				}
				//商品状态
				if (!string.IsNullOrEmpty(goodststus) && goodststus != "商品状态")
				{
					getGoodssData = (from p in getGoodssData where p.Validate == Convert.ToChar(goodststus) select p).ToList<SearchGoodsInfoData>();
				}
				//商品条码
				if (!string.IsNullOrEmpty(goodsku))
				{
					getGoodssData = (from p in getGoodssData where p.GoodsCode.Contains(goodsku) select p).ToList<SearchGoodsInfoData>();
				}
				return getGoodssData;
			}
		}

		/// <summary>
		/// 套餐商品信息查看
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodsInfoData> GetSearchGoodsPackageInfoData(  string goodtitle, string goodsort, string goodststus, string goodsku)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodsInfoData> getGoodssData = (from p in db.goodspackage
																									 join pp in db.goodssort on new { goodssort = p.goodssort } equals new { goodssort = pp.sortId }
																								 
																									 orderby p.addtime descending
																									 select new SearchGoodsInfoData
																									 {
																										 ID = p.goodsId,
																										 Title = p.goodstitle,
																										 GoodsCode = p.goodscode,
																										 GoodsPrice = p.goodsprice,
																										 GoodsCost = p.goodscost,
																										 GoodsStock = p.goodstock,
																										 Content = p.goodscontent,
																										 SetIndex = p.setindex,
																										 Validate = p.goodsvalidate,
																										 GoodSales = p.goodsSales,
																										 GoodsSortID = pp.sortId,
																										 GoodsSort = pp.sortName,
																										 Img = p.goodsimg,
																										 Iftuangou = p.iftuangou,
																										 ExchangeGood = p.ifexchange,
																										 
																										 AddTime = p.addtime
																									 }).ToList<SearchGoodsInfoData>();
				//商品名称
				if (!string.IsNullOrEmpty(goodtitle))
				{
					getGoodssData = (from p in getGoodssData where p.Title.Contains(goodtitle) select p).ToList<SearchGoodsInfoData>();
				}
				//商品分类
				if (!string.IsNullOrEmpty(goodsort) && goodsort != "商品分类")
				{
					getGoodssData = (from p in getGoodssData where p.GoodsSortID == int.Parse(goodsort) select p).ToList<SearchGoodsInfoData>();
				}
				//商品状态
				if (!string.IsNullOrEmpty(goodststus) && goodststus != "商品状态")
				{
					getGoodssData = (from p in getGoodssData where p.Validate == Convert.ToChar(goodststus) select p).ToList<SearchGoodsInfoData>();
				}
				//商品条码
				if (!string.IsNullOrEmpty(goodsku))
				{
					getGoodssData = (from p in getGoodssData where p.GoodsCode.Contains(goodsku) select p).ToList<SearchGoodsInfoData>();
				}
				return getGoodssData;
			}
		}


		/// <summary>
		/// 商品分类
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchGoodsSortData> GetSearchGoodsSortData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchGoodsSortData> getGoodsSortData =
	 (from a in db.goodssort
		join b in
				(
						(from goodssort in db.goodssort
						 where
							 goodssort.sortFatherId != (-1)
						 select new
						 {
							 goodssort
						 })) on new { sortId = a.sortId } equals new { sortId = Convert.ToInt32(b.goodssort.sortFatherId) }
		select new SearchGoodsSortData
		{
			FatherSortName = a.sortName,

			ID = b.goodssort.sortId,
			GoodsSortName = b.goodssort.sortName,
			SortFatherID = b.goodssort.sortFatherId,
			SortSonID = b.goodssort.sortSonId,
			SortPath = b.goodssort.sortPath,
			AddTime = b.goodssort.addTime
		}).OrderByDescending(x => x.AddTime).ToList<SearchGoodsSortData>();


				//(from p in db.goodssort
				//                                                          select new SearchGoodsSortData
				//                                                          {
				//                                                              ID = p.sortId,
				//                                                              GoodsSortName = p.sortName,
				//																												FatherNewsSort = p.,
				//                                                              AddTime = p.addTime
				//                                                          }).ToList<SearchGoodsSortData>();
				return getGoodsSortData;
			}
		}

		/// <summary>
		/// 团购商品信息
		/// </summary>
		/// <returns></returns>
        public static List<SearchGroupBuyInfoData> GetSearchGroupBuyInfoData(string type,string goodtitle, string goodsort, string goodststus, string goodsku)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchGroupBuyInfoData> getGroupBuyData = (from p in db.goodstuan
                                                                join pp in db.goodssort on new { groupsort = p.groupsort } equals new { groupsort = pp.sortId }
																												 where p.selectytsort==Convert.ToChar(type)
																												orderby p.addtime descending
																												select new SearchGroupBuyInfoData
																												{
																													ID = p.Id,
																													Title = p.title,
																													Content = p.content,
																													Starttime = p.starttime,
																													Endtime = p.endtime,
																													Validate = p.validate,
																													quantity = p.quantity,
																													price = p.price,
																													CostPrice = p.costprice,
																													SetIndex = p.setindex,
                                                                                                                    GoodsCode=p.goodscode,
																													Batch = p.batch,
																													ImgUrl = p.img,
																													GoodsSort =pp.sortName,
                                                                                                                    GoodsSortID=pp.sortId,
                                                                                                                    TuanGouYuShou=p.selectytsort,
																													AddTime = p.addtime
																												}).ToList<SearchGroupBuyInfoData>();

                //商品名称
                if (!string.IsNullOrEmpty(goodtitle))
                {
                    getGroupBuyData = (from p in getGroupBuyData where p.Title.Contains(goodtitle) select p).ToList<SearchGroupBuyInfoData>();
                }
                //商品分类
                if (!string.IsNullOrEmpty(goodsort) && goodsort != "商品分类")
                {
                    getGroupBuyData = (from p in getGroupBuyData where p.GoodsSortID == int.Parse(goodsort) select p).ToList<SearchGroupBuyInfoData>();
                }
                //商品状态
                if (!string.IsNullOrEmpty(goodststus) && goodststus != "商品状态")
                {
                    getGroupBuyData = (from p in getGroupBuyData where p.Validate == Convert.ToChar(goodststus) select p).ToList<SearchGroupBuyInfoData>();
                }
                //商品条码
                if (!string.IsNullOrEmpty(goodsku))
                {
                    getGroupBuyData = (from p in getGroupBuyData where p.GoodsCode.Contains(goodsku) select p).ToList<SearchGroupBuyInfoData>();
                }
                return getGroupBuyData;
			}
		}
		/// <summary>
		/// 订单信息列表
		/// </summary>
		/// <param name="strOrderno"></param>
		/// <param name="strOrderStatus"></param>
		/// <param name="strTime"></param>
		/// <returns></returns>
		public static List<SearchOrderInfoListData> GetSearchOrderListInfoData(string strOrderno, string strOrderStatus, string strTime)
		{
			//using (var writer = new StreamWriter(@"E:\linq.sql", false, Encoding.UTF8))
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchOrderInfoListData> getOrderListData = (from p in db.orders
																														//	join ui in db.ordersdetails on p.orderno equals ui.orderno
																													join us in db.UserInfo on p.buyername equals us.Id
																													// join ug in db.goods on ui.buygoodsid equals ug.goodsId
																													orderby p.paymenttime descending
																													//where p.orderday==DateTime.Parse( "2015/9/15 19:38:23")
																													select new SearchOrderInfoListData
																													{
																														OrderNo = p.orderno,
																														Name = us.name,
																														Tel = us.tel,
																														Address = us.address,
																														Areacity = us.areacity,
																														OrderPrice = p.orderprice,
																														Ordersource = p.ordersource,
																														Paymode = p.paymode,
																														OrderStatus = p.orderstatus,
																														ordertime = p.ordertime,
																														paymenttime = p.paymenttime,


																													}).ToList<SearchOrderInfoListData>();
				//昵称
				if (!string.IsNullOrEmpty(strOrderno))
				{
					getOrderListData = (from p in getOrderListData where p.OrderNo.Contains(strOrderno) select p).ToList<SearchOrderInfoListData>();
				}
				//真实姓名
				if (!string.IsNullOrEmpty(strOrderStatus) && strOrderStatus != "订单状态")
				{
					getOrderListData = (from p in getOrderListData where p.OrderStatus == Convert.ToChar(strOrderStatus) select p).ToList<SearchOrderInfoListData>();
				}


				if (!string.IsNullOrEmpty(strTime))
				{
					string[] strDate = strTime.Split('-');
					var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
					var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
					getOrderListData = (from p in getOrderListData
															where p.paymenttime >= Convert.ToDateTime(dtBegin)
									&& p.paymenttime <= Convert.ToDateTime(dtEnd)
															select p).ToList<SearchOrderInfoListData>();

				}

				return getOrderListData;
			}

		}


		/// <summary>
		///植物保护分类
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchPlantDoctorSortData> GetSearchPlantDoctorSortData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchPlantDoctorSortData> getNewsSortData = (from a in db.plantdoctorsort
																													 join b in
																															 (
																																	 (from plantdoctorsort in db.plantdoctorsort
																																		where
																																			plantdoctorsort.sortFatherId != (-1)
																																		select new
																																		{
																																			plantdoctorsort
																																		})) on new { sortId = a.sortId } equals new { sortId = Convert.ToInt32(b.plantdoctorsort.sortFatherId) }
																													 select new SearchPlantDoctorSortData
																													 {
																														 FatherSortName = a.sortName,

																														 ID = b.plantdoctorsort.sortId,
																														 NewsSortName = b.plantdoctorsort.sortName,
																														 SortImage = b.plantdoctorsort.sortImg,
																														 SortFatherID = b.plantdoctorsort.sortFatherId,
																														 SortSonID = b.plantdoctorsort.sortSonId,
																														 SortPath = b.plantdoctorsort.sortPath,
																														 AddTime = b.plantdoctorsort.addTime
																													 }).OrderByDescending(x => x.AddTime).ToList<SearchPlantDoctorSortData>(); ;
				return getNewsSortData;
			}
		}

		/// <summary>
		/// 植保医院信息
		/// </summary>
		/// <returns></returns>
		public static List<SearchPlantDoctorInfoData> GetSearchPlantDoctorInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchPlantDoctorInfoData> getPlantDoctorData = (from p in db.plantdoctor
																															join pp in db.plantdoctorsort on p.plantdoctorsort equals pp.sortId into g
																															from c in g.DefaultIfEmpty()
																															orderby p.addtime descending
																															select new SearchPlantDoctorInfoData
																															{
																																ID = p.Id,
																																Title = p.title,
																																Content = p.content,
																																SetIndex = p.setindex,
																																Validate = p.validate,
																																NewsSort = c.sortName,
																																Img = p.img,
																																AddTime = p.addtime
																															}).ToList<SearchPlantDoctorInfoData>();
				return getPlantDoctorData;
			}
		}


		/// <summary>
		/// 	 用户充值
		/// </summary>

		public static List<SearchUserRechargeInfoData> GetSearchUserRechargeInfoData(string strTel, string strRecStatus, string strTime)
		{
			//using (var writer = new StreamWriter(@"E:\linq.sql", false, Encoding.UTF8))
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserRechargeInfoData> getUserRechargeListData = (from p in db.userrecharge
																																			//	join ui in db.ordersdetails on p.orderno equals ui.orderno
																																		join us in db.UserInfo on p.userid equals us.Id
																																		// join ug in db.goods on ui.buygoodsid equals ug.goodsId
																																		orderby p.addtime descending
																																		//where p.orderday==DateTime.Parse( "2015/9/15 19:38:23")
																																		select new SearchUserRechargeInfoData
																																		{
																																			RecNo = p.recno,
																																			RecTime = p.addtime,
																																			Tel = p.tel,
																																			UserName = us.name,
																																			RecMoeny = p.recmoney,

																																			RecContent = p.content,
																																			UserID = p.userid,

																																			RecStatus = p.validate,


																																		}).ToList<SearchUserRechargeInfoData>();
				//昵称
				if (!string.IsNullOrEmpty(strTel))
				{
					getUserRechargeListData = (from p in getUserRechargeListData where p.Tel.Contains(strTel) select p).ToList<SearchUserRechargeInfoData>();
				}
				//真实姓名
				if (!string.IsNullOrEmpty(strRecStatus) && strRecStatus != "交易状态")
				{
					getUserRechargeListData = (from p in getUserRechargeListData where p.RecStatus == Convert.ToChar(strRecStatus) select p).ToList<SearchUserRechargeInfoData>();
				}


				if (!string.IsNullOrEmpty(strTime))
				{
					string[] strDate = strTime.Split('-');
					var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
					var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
					getUserRechargeListData = (from p in getUserRechargeListData
																		 where p.RecTime >= Convert.ToDateTime(dtBegin)
												 && p.RecTime <= Convert.ToDateTime(dtEnd)
																		 select p).ToList<SearchUserRechargeInfoData>();

				}

				return getUserRechargeListData;
			}

		}

		/// <summary>
		/// 角色设置
		/// </summary>
		/// <returns></returns>
		public static List<SearchRoleInfoData> GetSearchRoleInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchRoleInfoData> getRoleData = (from p in db.userrole
																								orderby p.addTime descending
																								select new SearchRoleInfoData
																								{
																									ID = p.roleid,
																									RoleName = p.rolename,
																									RoleContent = p.roledescribe,


																									PowerName = p.powerid,

																									AddTime = p.addTime
																								}).ToList<SearchRoleInfoData>();

				return getRoleData;
			}
		}
		/// <summary>
		/// 会员信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchMemberData> GetSearchMemberData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchMemberData> geMemberData = (from p in db.memberset
																							 select new SearchMemberData
																							 {
																								 ID = p.memberid,
																								 MemberName = p.membername,
																								 Discount = p.discount,
																								 AddTime = p.addTime
																							 }).ToList<SearchMemberData>();
				return geMemberData;
			}
		}

		/// <summary>
		/// 我的收入明细
		/// </summary>
		/// <returns></returns>
		public static List<SearchMyPayMoneyInfoData> GetSearchMyPayMoneyInfoData(string userid)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchMyPayMoneyInfoData> getPayMoneyData = (from p in db.moenydetails
																													join pp in db.UserInfo on p.userid equals pp.Id into g
																													from c in g.DefaultIfEmpty()
																													where c.tel == userid
																													orderby p.moneytime descending
																													select new SearchMyPayMoneyInfoData
																													{
																														MoneyCode = p.moneycode,
																														Moneytime = p.moneytime,
																														OrderNo = p.orderno,
																														GetMoney = p.money,
																														SumMoney = c.housemoney,
																														GetContent = p.moneyreason
																													}).ToList<SearchMyPayMoneyInfoData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// 我的积分明细
		/// </summary>
		/// <returns></returns>
		public static List<SearchMyPointInfoData> GetSearchMyPointInfoData(string userid)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchMyPointInfoData> getPlantDoctorData = (from p in db.pointdetails
																													join pp in db.UserInfo on p.userid equals pp.Id into g
																													from c in g.DefaultIfEmpty()
																													where c.tel == userid
																													orderby p.getpointtime descending
																													select new SearchMyPointInfoData
																													{
																														PointID = p.Id,
																														Pointime = p.getpointtime,
																														Point = p.getpoint,
																														SumPoint = c.point,
																														GetContent = p.getpointreason

																													}).ToList<SearchMyPointInfoData>();
				return getPlantDoctorData;
			}
		}

		/// <summary>
		/// 当月消费金额报表，有汇总统计。
		/// </summary>
		/// <param name="strTime"></param>
		/// <returns></returns>
		public static List<SearchFinanceMonthInfoListData> GetSearchFinanceMonthListInfoData(string strTime)
		{
			//using (var writer = new StreamWriter(@"E:\linq.sql", false, Encoding.UTF8))
			//			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			//			{
			//				var ss = from a in
			//(
			//(from a0 in db.orders
			//join b in
			//(
			//		(from ordersdetails in db.ordersdetails
			//		 group ordersdetails by new
			//		 {
			//			 ordersdetails.orderno
			//		 } into g
			//		 select new
			//		 {
			//			 SumQty = (System.Int32?)g.Sum(p => p.buysumqty),
			//			 g.Key.orderno
			//		 })) on a0.orderno equals b.orderno
			//group new { a0, b } by new
			//{
			//Column1 = Convert.ToString(a0.ordertime),
			//a0.orderprice,
			//b.SumQty
			//} into g
			//select new
			//{
			//orderprice = (System.Decimal?)g.Key.orderprice,
			//SumQty = (System.Int32?)g.Key.SumQty,
			//OrderTime = g.Key.Column1,
			//OrderCount = (Int64?)g.Count(p => p.b.orderno != null)
			//}))
			//								 group a by new
			//								 {
			//									 a.OrderTime,
			//									 a.SumQty,
			//									 a.OrderCount
			//								 } into g
			//								 orderby
			//								 g.Key.OrderTime
			//								 select new SearchFinanceMonthInfoListData
			//								 {
			//									 OrderTime = DateTime.Parse(g.Key.OrderTime),
			//									 OrderPrice = (System.Decimal?)g.Sum(p => p.orderprice),
			//									 SumQty = g.Sum(p => p.SumQty),
			//									 OrderCount = Convert.ToInt32(g.Sum(p => p.OrderCount))
			//								 };
			//        List<SearchFinanceMonthInfoListData> getFinanceMonthInfo =
			//				(
			//from a in
			//(
			//(from a0 in db.orders
			// join b in
			//		 (
			//				 (from ordersdetails in db.ordersdetails
			//					group ordersdetails by new
			//					{
			//						ordersdetails.orderno
			//					} into g
			//					select new
			//					{
			//						SumQty = (System.Int32?)g.Sum(p => p.buysumqty),
			//						g.Key.orderno
			//					})) on a0.orderno equals b.orderno
			// group new { a0, b } by new
			// {
			//	 Column1 = Convert.ToString(a0.ordertime),
			//	 a0.orderprice,
			//	 b.SumQty
			// } into g
			// select new
			// {
			//	 orderprice = (System.Decimal?)g.Key.orderprice,
			//	 SumQty = (System.Int32?)g.Key.SumQty,
			//	 OrderTime = g.Key.Column1,
			//	 OrderCount = (Int64?)g.Count(p => p.b.orderno != null)
			// }))
			//group a by new
			//{
			//	a.OrderTime,
			//	a.SumQty,
			//	a.OrderCount
			//} into g
			//orderby
			//g.Key.OrderTime
			//select new SearchFinanceMonthInfoListData
			//{
			//	OrderTime = DateTime.Parse(g.Key.OrderTime),
			//	OrderPrice = (System.Decimal?)g.Sum(p => p.orderprice),
			//	SumQty = g.Sum(p => p.SumQty),
			//	OrderCount = Convert.ToInt32(g.Sum(p => p.OrderCount))
			//}
			//).ToList<SearchFinanceMonthInfoListData>();
			List<SearchFinanceMonthInfoListData> getFinanceMonthInfo = new List<SearchFinanceMonthInfoListData>();
        if (!string.IsNullOrEmpty(strTime))
				{
					if (strTime == "NowMonth")
					{
						DateTime dt = DateTime.Now;
						DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
						DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
					//getFinanceMonthInfo = (from p in getFinanceMonthInfo
					//											 where p.OrderTime >= DateTime.Parse(startMonth.ToString("yyyy-MM-dd 00:00:00")) &&
					//							p.OrderTime <= DateTime.Parse(endMonth.ToString("yyyy-MM-dd 23:59:59"))
					//											 select p).ToList<SearchFinanceMonthInfoListData>();
						getFinanceMonthInfo = ReturnSearchResult(startMonth, endMonth);

					}
					else
					{
						string[] strDate = strTime.Split('-');
						//var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						//var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getFinanceMonthInfo = ReturnSearchResult(DateTime.Parse(strDate[0]), DateTime.Parse(strDate[1]));
						//			getFinanceMonthInfo = (from p in getFinanceMonthInfo
						//														 where p.OrderTime >= Convert.ToDateTime(dtBegin)
						//&& p.OrderTime <= Convert.ToDateTime(dtEnd)
						//														 select p).ToList<SearchFinanceMonthInfoListData>();
					}
				}

				return getFinanceMonthInfo;
			//}

		}

		public static List<SearchFinanceMonthInfoListData>	 ReturnSearchResult(DateTime startMonth, DateTime endMonth)
		{
			List<SearchFinanceMonthInfoListData> getFinanceMonthInfo = new List<SearchFinanceMonthInfoListData>();
      using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				getFinanceMonthInfo = (from a in db.orders
															 join b in (
																 (from ordersdetails in db.ordersdetails
																	group ordersdetails by new
																	{
																		ordersdetails.orderno
																	} into g
																	select new
																	{
																		g.Key.orderno,
																		buysumqty = (System.Int32?)g.Sum(p => p.buysumqty)
																	})) on a.orderno equals b.orderno
															 where a.ordertime >= DateTime.Parse(startMonth.ToString("yyyy-MM-dd 00:00:00")) &&
													a.ordertime <= DateTime.Parse(endMonth.ToString("yyyy-MM-dd 23:59:59"))
															 group new { a, b } by new
															 {
																 Column1 = Convert.ToString(a.ordertime),
																 b.buysumqty
															 } into g
															 select new SearchFinanceMonthInfoListData
															 {
																 OrderTime =DateTime.Parse( g.Key.Column1),
																 OrderPrice = (System.Decimal?)g.Sum(p => p.a.orderprice),
																 OrderCount = g.Count(p => p.a.orderno != null),
																 SumQty = (System.Int32?)g.Sum(p => p.b.buysumqty)
															 }).ToList<SearchFinanceMonthInfoListData>();
      }
			return getFinanceMonthInfo;
      }

		///20160716
		/// <summary>
		/// 当月消费金额报表，有汇总统计。导出
		/// </summary>
		/// <param name="startMonth"></param>
		/// <param name="endMonth"></param>
		/// <returns></returns>
		public static List<ExportFinanceMonthInfoListData> ExportFinanceMonthInfoListData(DateTime startMonth, DateTime endMonth)
		{
			List<ExportFinanceMonthInfoListData> getFinanceMonthInfo = new List<ExportFinanceMonthInfoListData>();
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				getFinanceMonthInfo = ((
	from a in db.orders
	join b in db.ordersdetails on a.orderno equals b.orderno
	join c in db.UserInfo on new { buyername = a.buyername } equals new { buyername = c.Id }
	join d in db.goods on new { b.buygoodsid, selectytsort=Convert.ToChar( b.selectytsort.ToString()) } 
	equals new { buygoodsid = d.goodsId, selectytsort=d.selectytsort }
	select new ExportFinanceMonthInfoListData
	{
		OrderNo = a.orderno,
		OrderTime =a.ordertime,
		OrderPrice =a.orderprice,
		expressno = a.expressno,
		shippindtime =a.shippindtime,
		goodstitle = d.goodstitle,
		buysumqty =b.buysumqty,
		goodsprice = b.goodsprice,
		Name = c.name,
		Tel = c.tel
	}
).Union
(
	from a in db.orders
	join b in db.ordersdetails on a.orderno equals b.orderno
	join c in db.UserInfo on new { buyername = a.buyername } equals new { buyername = c.Id }
	join d in db.goodstuan
				on new { b.buygoodsid, b.selectytsort }
		equals new { buygoodsid = d.Id, d.selectytsort }
	select new ExportFinanceMonthInfoListData
	{
		OrderNo = a.orderno,
		OrderTime = a.ordertime,
		OrderPrice = (System.Decimal?)a.orderprice,
		expressno = a.expressno,
		shippindtime = (System.DateTime?)a.shippindtime,
		goodstitle = d.title,
		buysumqty = b.buysumqty,
		goodsprice = (System.Decimal?)b.goodsprice,
		Name = c.name,
		Tel = c.tel
	}
)).ToList<ExportFinanceMonthInfoListData>();
			}
			return getFinanceMonthInfo;
		}

		/// <summary>
		/// 当月分销提成金额报表
		/// </summary>
		/// <returns></returns>
		public static List<SearchFenXiaoExtractListData> GetSearchFenXiaoExtractInfoData(string strTime)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				DateTime dt = DateTime.Now;
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
																																 //List<SearchFenXiaoExtractListData> getFenXiaoExtractData = (from p in db.userrecharge
																																 //																														where
																																 //																																p.getrecuserid != null
																																 //																														group p by
																																 //																														new DateTime(p.addtime.Year, p.addtime.Month, p.addtime.Day)
																																 //																										 //SqlMethods.DateDiffDay(e.Month, new DateTime(1900, 1, 1))
																																 //																										 into g
																																 //																														select new SearchFenXiaoExtractListData
																																 //																														{
																																 //																															RechargeTime = g.Key,
																																 //																															Recmoney = (System.Decimal?)g.Sum(p => p.recmoney),
																																 //																															RechargeCount = Convert.ToInt32(g.Count(p => p.recno != null))
																																 //																														}
																																 //																						).ToList<SearchFenXiaoExtractListData>();
				List<SearchFenXiaoExtractListData> getFenXiaoExtractData = (from p in db.moenydetails
																																		where
																																				p.userid != null
																																		group p by
																																		new {p.moneytime }
																										 //SqlMethods.DateDiffDay(e.Month, new DateTime(1900, 1, 1))
																										 into g
																																		select new SearchFenXiaoExtractListData
																																		{
																																			RechargeTime =DateTime.Parse( g.Key.moneytime.ToString()),
																																			Recmoney = (System.Decimal?)g.Sum(p => p.money),
																																			RechargeCount = g.Count()
																																		}
																						).ToList<SearchFenXiaoExtractListData>();
				if (!string.IsNullOrEmpty(strTime))
				{
					if (strTime == "NowMonth")
					{

						getFenXiaoExtractData = (from p in getFenXiaoExtractData
																		 where p.RechargeTime >= DateTime.Parse(startMonth.ToString("yyyy-MM-dd 00:00:00")) &&
													p.RechargeTime <= DateTime.Parse(endMonth.ToString("yyyy-MM-dd 23:59:59"))
																		 select p).ToList<SearchFenXiaoExtractListData>();
					}
					else
					{
						string[] strDate = strTime.Split('-');
						var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getFenXiaoExtractData = (from p in getFenXiaoExtractData
																		 where p.RechargeTime >= Convert.ToDateTime(dtBegin)
			&& p.RechargeTime <= Convert.ToDateTime(dtEnd)
																		 select p).ToList<SearchFenXiaoExtractListData>();
					}
				}
				return getFenXiaoExtractData;
			}
		}


		//20160716
		/// <summary>
		///  当月分销提成金额报表导出
		/// </summary>
		/// <param name="strTime"></param>
		/// <returns></returns>
		public static List<ExportFenXiaoExtractListData> ExportFenXiaoExtractInfoData(string strTime)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				DateTime dt = DateTime.Now;
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
				List<ExportFenXiaoExtractListData> getFenXiaoExtractData = (from a in db.moenydetails
																																		join b in db.UserInfo on new { userid = a.userid } equals new { userid = b.Id }
																																		select new ExportFenXiaoExtractListData
																																		{
																																			MoneyCode = a.moneycode,
																																			Moneytime = a.moneytime,
																																			GetMoney = a.money,
																																			OrderNo = a.orderno,
																																			MoneyReason = a.moneyreason,
																																			GetUserTel =
																																				((from userinfo in db.UserInfo
																																					where
																																						userinfo.Id == a.getrecuserid
																																					select new
																																					{
																																						userinfo.tel
																																					}).First().tel),
																																			Name = b.name,
																																			Tel = b.tel
																																		}
																						).ToList<ExportFenXiaoExtractListData>();
				if (!string.IsNullOrEmpty(strTime))
				{
					if (strTime == "NowMonth")
					{

						getFenXiaoExtractData = (from p in getFenXiaoExtractData
																		 where p.Moneytime >= DateTime.Parse(startMonth.ToString("yyyy-MM-dd 00:00:00")) &&
													p.Moneytime <= DateTime.Parse(endMonth.ToString("yyyy-MM-dd 23:59:59"))
																		 select p).ToList<ExportFenXiaoExtractListData>();
					}
					else
					{
						string[] strDate = strTime.Split('-');
						var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getFenXiaoExtractData = (from p in getFenXiaoExtractData
																		 where p.Moneytime >= Convert.ToDateTime(dtBegin)
			&& p.Moneytime <= Convert.ToDateTime(dtEnd)
																		 select p).ToList<ExportFenXiaoExtractListData>();
					}
				}
				return getFenXiaoExtractData;
			}
		}

		/// <summary>
		///  当月会员提现报表
		/// </summary>
		/// <param name="strTime"></param>
		/// <returns></returns>
		public static List<SearchMemberExtractListData> GetSearchMemberExtractInfoData(string strTime)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				DateTime dt = DateTime.Now;
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
				List<SearchMemberExtractListData> getMemberExtractData = (from p in db.extractlist
																																	orderby p.createtime descending
																																	select new SearchMemberExtractListData
																																	{
																																		ExtractCreateTime = p.createtime,
																																		ExtractListInfo = p.extlistinfo,
																																		ExtractNo = p.extlistid
																																	}
																										).ToList<SearchMemberExtractListData>();
				if (!string.IsNullOrEmpty(strTime))
				{
					if (strTime == "NowMonth")
					{

						getMemberExtractData = (from p in getMemberExtractData
																		where p.ExtractCreateTime >= DateTime.Parse(startMonth.ToString("yyyy-MM-dd 00:00:00")) &&
													p.ExtractCreateTime <= DateTime.Parse(endMonth.ToString("yyyy-MM-dd 23:59:59"))
																		select p).ToList<SearchMemberExtractListData>();
					}
					else
					{
						string[] strDate = strTime.Split('-');
						var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getMemberExtractData = (from p in getMemberExtractData
																		where p.ExtractCreateTime >= Convert.ToDateTime(dtBegin)
			&& p.ExtractCreateTime <= Convert.ToDateTime(dtEnd)
																		select p).ToList<SearchMemberExtractListData>();
					}
				}
				return getMemberExtractData;
			}
		}

		/// <summary>
		/// a)	会员提现管理

		public static List<SearchUserExtractInfoListData> GetSearchUserExtractListInfoData()
		{
			//using (var writer = new StreamWriter(@"E:\linq.sql", false, Encoding.UTF8))
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserExtractInfoListData> getExtractListData = (from p in db.memberextract

																																	join us in db.UserInfo on p.extuserid equals us.Id

																																	orderby p.exttime descending

																																	select new SearchUserExtractInfoListData
																																	{
																																		Extno = p.extno,
																																		ExtName = us.name,
																																		ExtTel = p.exttel,
																																		Extmoney = p.extmoney,
																																		ExtCardNo = p.extcardno,
																																		Extcontent = p.extcontent,
																																		Exttime = p.exttime,
																																	}).ToList<SearchUserExtractInfoListData>();


				return getExtractListData;
			}

		}

		/// <summary>
		///门店店铺信息
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <returns></returns>
		public static List<SearchShopData> GetSearchShopData(string strWhereAll)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchShopData> getShopData = (from p in db.shopset
																						select new SearchShopData
																						{
																							ShopID = p.shopCode,
																							ShopName = p.shopname,
																							ShopTel = p.shoptel,
																							AddTime = p.addtime
																						}).ToList<SearchShopData>();
				return getShopData;
			}
		}


		/// <summary>
		/// 厂家核对报表
		/// </summary>
		/// <param name="strWhereAll"></param>
		/// <param name="strtime"></param>
		/// <returns></returns>
		public static List<SearchSaleCheckIData> GetSearchSaleCheckData(string shopid, string strtime)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				List<SearchSaleCheckIData> getSaleCheckData = (

					from a in db.orders
					join b in db.ordersdetails on a.orderno equals b.orderno
					join c in db.goods on new { buygoodsid = b.buygoodsid } equals new { buygoodsid = c.goodsId }
					join d in db.UserInfo on new { buyername = a.buyername } equals new { buyername = d.Id }
                    where a.orderstatus == '1'
                    orderby a.paymenttime descending
					select new SearchSaleCheckIData
					{
						OrderNo = a.orderno,
						PaymentTime = a.paymenttime,
						OrderPrice = a.orderprice,
						GoodsTitle = c.goodstitle,
						GoodsCompany = c.goodscompany,
						GoodsCode = c.goodscode,

						GoodsPrice = c.goodsprice,
						GoodsCost = c.goodscost,
						GoodsSpec = c.goodsspec,
						BuyGoodsid = b.buygoodsid,
						BuySumQty = b.buysumqty,
						GoodsSumWeight = (System.Decimal?)(c.Goodsweight * b.buygoodsid),
						OrderStatus = "付款未发货", 
                        OKOrderStatus = a.orderstatus,
						ShopSet = d.shopset
					}).ToList<SearchSaleCheckIData>();
				if (!string.IsNullOrEmpty(shopid) && shopid != "县域门店")
				{
					getSaleCheckData = getSaleCheckData.Where(x => x.ShopSet == int.Parse(shopid)).ToList<SearchSaleCheckIData>();
				}
				if (!string.IsNullOrEmpty(strtime))
				{
					//if (strtime == "NowMonth")
					//{
					//	DateTime dt = DateTime.Now;
					//	getSaleCheckData = (from p in getSaleCheckData
					//											where p.PaymentTime >= DateTime.Parse(dt.ToString("yyyy-MM-dd 00:00:00")) &&
					//								p.PaymentTime <= DateTime.Parse(dt.ToString("yyyy-MM-dd 23:59:59"))
					//											select p).ToList<SearchSaleCheckIData>();
					//}
					//else
					//{
						string[] strDate = strtime.Split('-');
						var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getSaleCheckData = (from p in getSaleCheckData
																where p.PaymentTime >= Convert.ToDateTime(dtBegin)
			&& p.PaymentTime <= Convert.ToDateTime(dtEnd)
																select p).ToList<SearchSaleCheckIData>();
					}
				//}
				return getSaleCheckData;
			}
		}



		/// <summary>
		/// 商品发货表
		/// </summary>
		/// <param name="strorderno">订单编号</param>
		/// <param name="strgoodsname">商品名称</param>
		/// <param name="strbuyname">买家姓名</param>
		/// <param name="strcompany">厂家名称</param>
		/// <param name="strgudong">股东</param>
		/// <param name="strzhanzhang">站长</param>
		/// <param name="strshopName">时间</param>
		/// <param name="strtime">县域</param>
		/// <returns></returns>
		public static List<SearchGoodsShipData> GetSearchGoodsShipData(string strorderno, string strgoodsname,
			string strbuyname, string strcompany, string strgudong, string strzhanzhang, string strtime, string strshopName,string goodshipstatus)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
//				SELECT(SELECT name from UserInfo where tel = d.shangjigudong ) as sjgudongname,d.shangjigudong,
// (SELECT name from UserInfo where tel = d.shangjizhanzhang ) as sjzhanzhangname,d.shangjizhanzhang, 
// a.orderno, a.buyername,d.name,d.tel,d.juese,d.housemoney,d.areacity,d.address,
//c.goodstitle,c.goodscode,c.goodsprice,c.goodscost
//,c.goodsspec,c.ifxiangou,c.xiangounumber,b.buysumqty,c.goodscompany,a.orderprice,a.paymenttime,a.orderstatus
//,d.shopset
//from orders a INNER join ordersdetails b on a.orderno = b.orderno
//INNER JOIN  goods c on b.buygoodsid = c.goodsid
//INNER JOIN (SELECT id as buyuserid, name, tel, juese, housemoney, areacity, address,
// fenxiaoid, shangjigudong, shangjizhanzhang, shopset from UserInfo) d on d.buyuserid = a.buyername
// where a.orderno = '16030415463947' and a.orderstatus = '1'
				#region //读取数据
				List<SearchGoodsShipData> getgoodShipData = (
				 		from a in db.orders
						 join b in db.ordersdetails on a.orderno equals b.orderno
						 join c in db.goods on new { buygoodsid = b.buygoodsid } equals new { buygoodsid = c.goodsId }
						 join d in (
							 (from userinfo in db.UserInfo
								select new
								{
									buyuserid = userinfo.Id,
									userinfo.name,
									userinfo.tel,
									userinfo.housemoney,
									userinfo.areacity,
									userinfo.address,
									userinfo.fenxiaoid,
									userinfo.shangjigudong,
									userinfo.shangjizhanzhang,
									userinfo.shopset ,userinfo.juese
								})) on new { buyuserid = a.buyername } equals new { buyuserid = Convert.ToInt32(d.buyuserid) }
						 where a.orderstatus == '1' orderby a.paymenttime descending
						 select new SearchGoodsShipData
						 {
							 GoodShipStatus = b.goodshipstatus,
                             GoodShipNo = b.goodshipno,
							 SjGuDongInfo =
							 ((from userinfo in db.UserInfo
								 where userinfo.tel == Convert.ToString(d.shangjigudong)
								 select new { userinfo.name }).First().name).Trim() + "/" + d.shangjigudong,
							 SjZhanZhangInfo =
							 ((from userinfo in db.UserInfo
								 where userinfo.tel == Convert.ToString(d.shangjizhanzhang)
                               select new { userinfo.name }).First().name).Trim() + "/" + d.shangjizhanzhang,

							 OrderNo = a.orderno,
							 OrderPrice = b.orderprice,
							 PaymentTime = a.paymenttime,
							 BuyerID = a.buyername,
							 name = d.name,
							 tel = d.tel,
							 juese=d.juese,
							 housemoney = d.housemoney,
							 areacity = d.areacity,
							 address = d.address,
                             BuyGoodsid=b.ordersid,
							 GoodsTitle = c.goodstitle,
							 GoodsCompany = c.goodscompany,
							 GoodsCode = c.goodscode, 
							 GoodsPrice = c.goodsprice,
							 GoodsCost = c.goodscost,
							 GoodsSpec = c.goodsspec,
							 ifxiangou = c.ifxiangou,
							 xiangounumber = c.xiangounumber,
							 BuySumQty = b.buysumqty,
							  
							 ShopSet = d.shopset
						 }
					 ).ToList<SearchGoodsShipData>();
				#endregion
				if (!string.IsNullOrEmpty(strorderno))
				{
					getgoodShipData = getgoodShipData.Where(x => x.OrderNo.Contains(strorderno)).ToList<SearchGoodsShipData>();
				}
				if (!string.IsNullOrEmpty(strgoodsname))
				{
					getgoodShipData = getgoodShipData.Where(x => x.GoodsTitle.Contains(strgoodsname)).ToList<SearchGoodsShipData>();
				}
				if (!string.IsNullOrEmpty(strbuyname))
				{
					getgoodShipData = getgoodShipData.Where(x => x.name.Contains(strbuyname)).ToList<SearchGoodsShipData>();
				}
				if (!string.IsNullOrEmpty(strcompany))
				{
					getgoodShipData = getgoodShipData.Where(x => x.GoodsCompany.Contains(strcompany)).ToList<SearchGoodsShipData>();
				}
				if (!string.IsNullOrEmpty(strgudong))
				{
					getgoodShipData = getgoodShipData.Where(x => x.SjGuDongInfo.Contains(strgudong)).ToList<SearchGoodsShipData>();
				}
				if (!string.IsNullOrEmpty(strzhanzhang))
				{
					getgoodShipData = getgoodShipData.Where(x => x.SjZhanZhangInfo.Contains(strzhanzhang)).ToList<SearchGoodsShipData>();
				}
				
				if (!string.IsNullOrEmpty(strtime))
				{
					//if (strtime == "NowMonth")
					//{
					//	DateTime dt = DateTime.Now;
					//	getgoodShipData = (from p in getgoodShipData
					//											where p.PaymentTime >= DateTime.Parse(dt.ToString("yyyy-MM-dd 00:00:00")) &&
					//								p.PaymentTime <= DateTime.Parse(dt.ToString("yyyy-MM-dd 23:59:59"))
					//											select p).ToList<SearchGoodsShipData>();
					//}
					//else
					//{
						string[] strDate = strtime.Split('-');
						var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
						var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
						getgoodShipData = (from p in getgoodShipData
																where p.PaymentTime >= Convert.ToDateTime(dtBegin)
			&& p.PaymentTime <= Convert.ToDateTime(dtEnd)
																select p).ToList<SearchGoodsShipData>();
					}
			//	}
                if (!string.IsNullOrEmpty(strshopName) && strshopName != "县域门店")
                {
                    getgoodShipData = getgoodShipData.Where(x => x.ShopSet == int.Parse(strshopName)).ToList<SearchGoodsShipData>();
                }
                if (!string.IsNullOrEmpty(goodshipstatus) && goodshipstatus != "发货状态")
                {
                    getgoodShipData = getgoodShipData.Where(x => x.GoodShipStatus == Convert.ToChar(goodshipstatus)).ToList<SearchGoodsShipData>();
                }
				return getgoodShipData;
			}
		}

		/// <summary>
		/// 用户i.	地区分析
		/// </summary>
		/// <returns></returns>
		public static List<SearchUserAreaData> GetSearchUserAreaInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserAreaData> getPayMoneyData = (from userinfo in db.UserInfo
																										group userinfo by new
																										{
																											userinfo.areacity
																										} into g
																										select new SearchUserAreaData
																										{
																											Name = g.Max(p => p.name),
																											Tel = g.Max(p => p.tel),
																										Areacity=	g.Key.areacity,
																											SumCount = g.Count()
																										}).ToList<SearchUserAreaData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// 用户ii.	年龄、性别等信息分析
		/// </summary>
		/// <returns></returns>
		public static List<SearchUserAgeSexData> GetSearchUserAgeSexInfoData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserAgeSexData> getPayMoneyData = (from userinfo in db.UserInfo
																											group userinfo by new
																											{
																												userinfo.sex,
																												Age=DateTime.Now.Year- Convert.ToDateTime(userinfo.birthday).Year
																											} into g
																											select new SearchUserAgeSexData
																											{
																												Sex =g.Key.sex,
																												Age = g.Key.Age,
																												SumCount = g.Count()
																											}).OrderByDescending(x=>x.SumCount).ToList<SearchUserAgeSexData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// f)	商品销售分析i.	销售排行榜
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodSalesTotalData> GetSearchGoodSalesTotalData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodSalesTotalData> getPayMoneyData = (
					(from a in
(from a in db.orders
 join b in db.ordersdetails on a.orderno equals b.orderno
 join c in db.goods on new { buygoodsid = b.buygoodsid } equals new { buygoodsid = c.goodsId }
 select new
 {
	 c.goodstitle,
	 Column1 = c.selectytsort.ToString() == "T" ? "推荐商品" : "普通商品",
	 c.goodsimg,
	 b.buysumqty,
	 Dummy = "x"
 })
					 group a by new { a.Dummy } into g
					 select new SearchGoodSalesTotalData
					 {
						 GoodsTitle = g.Max(p => p.goodstitle),
						 GoodsSort = g.Max(p => p.Column1),
						 GoodsImg = g.Max(p => p.goodsimg),
						 SumCount = g.Sum(p => p.buysumqty)
					 }
).Union
(from a in
(from a in db.orders
 join b in db.ordersdetails on a.orderno equals b.orderno
 join c in db.goodstuan on new { buygoodsid = b.buygoodsid } equals new { buygoodsid = c.Id }
 select new
 {
	 c.title,
	 Column1 = c.selectytsort.ToString() == "G" ? "团购商品" : "预售商品",
	 c.img,
	 b.buysumqty,
	 Dummy = "x"
 })
 group a by new { a.Dummy } into g
 select new SearchGoodSalesTotalData
 {
	 GoodsTitle = g.Max(p => p.title),
	 GoodsSort = g.Max(p => p.Column1),
	 GoodsImg = g.Max(p => p.img),
	 SumCount = g.Sum(p => p.buysumqty)
 }
)
					).ToList<SearchGoodSalesTotalData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// 商品销售分析i.	iii.	地区消费排行榜
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodAreaSalesTotalData> GetSearchGoodAreaSalesTotalData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodAreaSalesTotalData> getPayMoneyData = (
					from a in db.UserInfo
					join b in db.orders on new { Id = a.Id } equals new { Id = b.buyername }
					join c in db.ordersdetails on b.orderno equals c.orderno
					group new { a, b, c } by new
					{
						a.areacity
					} into g
					select new SearchGoodAreaSalesTotalData
					{
						AreaCity =		g.Key.areacity,
						SumPrice = (System.Decimal?)g.Sum(p => p.b.orderprice),
						BuySumQty =  g.Sum(p => p.c.buysumqty),
						SumCount = g.Count()
					}
					).ToList<SearchGoodAreaSalesTotalData>();
				return getPayMoneyData;
			}
		}


		/// <summary>
		/// 商品销售分析i.ii.	分类排行榜
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodSortSalesTotalData> GetSearchGoodSortSalesTotalData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodSortSalesTotalData> getPayMoneyData = (
					 (
							from a in db.ordersdetails
							join b in db.goods on new { buygoodsid = a.buygoodsid } equals new { buygoodsid = b.goodsId }
							join c in db.goodssort on new { goodssort = b.goodssort } equals new { goodssort = c.sortId }
							group new { c, a } by new
							{
								c.sortName
							} into g
							select new SearchGoodSortSalesTotalData
							{
							SortName=	g.Key.sortName,
								BuySumQty =  g.Sum(p => p.a.buysumqty),
								SumCount = g.Count()
							}
						).Union
						(
							from a in db.ordersdetails
							join b in db.goodstuan on new { buygoodsid = a.buygoodsid } equals new { buygoodsid = b.Id }
							join c in db.goodssort on new { groupsort = b.groupsort } equals new { groupsort = c.sortId }
							group new { c, a } by new
							{
								c.sortName
							} into g
							select new SearchGoodSortSalesTotalData
							{
								SortName = g.Key.sortName,
								BuySumQty = g.Sum(p => p.a.buysumqty),
								SumCount = g.Count()
							}
						)
					).ToList<SearchGoodSortSalesTotalData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// 商品销售分析i.	iii.	iv.	商品销售数据检索
		/// </summary>
		/// <returns></returns>
		public static List<SearchGoodSalesSearchTotalData> GetSearchGoodSalesSearchTotalData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchGoodSalesSearchTotalData> getPayMoneyData = (
					 (
								from a in db.goodtypelikes
								join b in db.goodssort on new { goodstype = a.goodstype } equals new { goodstype = b.sortName }
								join c in db.goods on new { sortId = b.sortId } equals new { sortId = c.goodssort }
								group new { c, b } by new
								{
									c.selectytsort
								} into g
								select new SearchGoodSalesSearchTotalData
								{
									GoodsTitle = g.Max(p => p.c.goodstitle),
									GoodsSort = g.Max(p => p.b.sortName),
									GoodsImg = g.Max(p => p.c.goodsimg),
									SelectytSort = g.Key.selectytsort.ToString() == "T" ? "推荐商品" : "普通商品",
									SumCount =  g.Count()
								}
							).Union
							(
								from a in db.goodtypelikes
								join b in db.goodssort on new { goodstype = a.goodstype } equals new { goodstype = b.sortName }
								join c in db.goodstuan on new { sortId = b.sortId } equals new { sortId = c.groupsort }
								group new { c, b } by new
								{
									c.selectytsort
								} into g
								select new SearchGoodSalesSearchTotalData
								{
									GoodsTitle = g.Max(p => p.c.title),
									GoodsSort = g.Max(p => p.b.sortName),
									GoodsImg = g.Max(p => p.c.img),
									SelectytSort = g.Key.selectytsort.ToString() == "T" ? "推荐商品" : "普通商品",
									SumCount = g.Count()
								}
							)
					).ToList<SearchGoodSalesSearchTotalData>();
				return getPayMoneyData;
			}
		}


		/// <summary>
		/// g)	用户订单分类分析
		/// </summary>
		/// <returns></returns>
		public static List<SearchUserOrderTypeFenXiData> GetSearchUserOrderTypeFenXiData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserOrderTypeFenXiData> getPayMoneyData = (
					 (
							from a in db.orders
							join b in db.ordersdetails on a.orderno equals b.orderno
							group new { a, b } by new
							{
								a.orderstatus
							} into g
							select new SearchUserOrderTypeFenXiData
							{
								SumPrice =  g.Sum(p => p.a.orderprice),
								SumCount =  g.Count(p => p.a.orderno != null),
								BuySumQty =  g.Sum(p => p.b.buysumqty),
								OrderStatus=	g.Key.orderstatus,
								PayMode = g.Max(p => p.a.paymode)
							}
							)
					).ToList<SearchUserOrderTypeFenXiData>();
				return getPayMoneyData;
			}
		}

		/// <summary>
		/// ii.	用户订单消费分析
		/// </summary>
		/// <returns></returns>
		public static List<SearchUserOrderSalesFenXiData> GetSearchUserOrderSalesFenXiData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchUserOrderSalesFenXiData> getPayMoneyData = 
					(
								from a in db.orders
								join b in db.UserInfo on new { buyername = a.buyername } equals new { buyername = b.Id }
								group new { b, a } by new
								{
									b.tel
								} into g
								select new SearchUserOrderSalesFenXiData
								{
									BuyName = g.Max(p => p.b.name),
									BuyTel= g.Key.tel,
									Areacity = g.Max(p => p.b.areacity),
									SumPrice =  g.Sum(p => p.a.orderprice),
									SumCount =  g.Count(p => p.a.orderno != null)
								}
					).ToList<SearchUserOrderSalesFenXiData>();
				return getPayMoneyData;
			}
		}


		/// <summary>
		/// ii.	意见收集整理
		/// </summary>
		/// <returns></returns>
		public static List<SearchMessageViewData> GetSearchMessageViewData()
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{

				List<SearchMessageViewData> getPayMoneyData =
					(
							from a in db.messageview
							join b in db.UserInfo on new { messageuserid = Convert.ToInt32(a.messageuserid) } equals new { messageuserid = b.Id } into b_join
							from b in b_join.DefaultIfEmpty()
							select new SearchMessageViewData
							{
								Messageid = a.messageid,
								Messagename = (a.messagename ?? b.name),
								Messagetel = a.messagetel,
								Messagecontent  =a.messagecontent,
								Messagetime = a.messagetime
							}
					).ToList<SearchMessageViewData>();
				return getPayMoneyData;
			}
		}

        /// <summary>
        /// ii.	分销层级分析
        /// </summary>
        /// <returns></returns>
        public static List<SearchMemberJiBieFenXiData> GetSearchMemberJiBieFenXiData()
        {
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {

                List<SearchMemberJiBieFenXiData> getPayMoneyData =
                    (
                            from a in
                                (
                                    ((
                                    from a in
                                        (
                                            (from userinfo in db.UserInfo
                                             where
                                               userinfo.tel == userinfo.invitedcode
                                             select new
                                             {
                                                 userinfo.Id,
                                                 userinfo.fenxiaoid,
                                                 userinfo.nickname,
                                                 userinfo.name,
                                                 userinfo.tel
                                             }))
                                    join b in db.UserInfo on new { Id = a.Id } equals new { Id =Convert.ToInt32(b.fenxiaoid) }
                                    where
                                      a.Id != b.Id
                                    select new
                                    {
                                        a.name,
                                        b.tel,
                                        fenxiaoid = b.fenxiaoid,
                                        b.invitedcode,
                                        membertype = "一级会员"
                                    }
                                    ).Union
                                    (
                                    from a in
                                        (
                                            (from a in
                                                 (
                                                     (from userinfo in db.UserInfo
                                                      where
                                                        userinfo.tel == userinfo.invitedcode
                                                      select new
                                                      {
                                                          userinfo.Id,
                                                          userinfo.fenxiaoid,
                                                          userinfo.nickname,
                                                          FName = userinfo.name,
                                                          FTel = userinfo.tel
                                                      }))
                                             join b0 in db.UserInfo on new { Id = a.Id } equals new { Id =Convert.ToInt32( b0.fenxiaoid) }
                                             where
                                               a.Id != b0.Id
                                             select new
                                             {
                                                 a,
                                                 levelid = b0.Id,
                                                 b0.name,
                                                 b0.tel,
                                                 b0.invitedcode,
                                                 membertype = "一级会员"
                                             }))
                                    join b in db.UserInfo on new { levelid = a.levelid } equals new { levelid =Convert.ToInt32( b.fenxiaoid) }
                                    select new
                                    {
                                        name = a.name,
                                        tel = b.tel,
                                        fenxiaoid =  b.fenxiaoid,
                                        invitedcode = b.invitedcode,
                                        membertype = "二级会员"
                                    }
                                    ).Union
                                    (
                                    from a in
                                        (
                                            (from a in
                                                 (
                                                     (from a in
                                                          (
                                                              (from userinfo in db.UserInfo
                                                               where
                                                                 userinfo.tel == userinfo.invitedcode
                                                               select new
                                                               {
                                                                   userinfo.Id,
                                                                   userinfo.fenxiaoid,
                                                                   userinfo.nickname,
                                                                   FName = userinfo.name,
                                                                   FTel = userinfo.tel
                                                               }))
                                                      join b01 in db.UserInfo on new { Id = a.Id } equals new { Id =Convert.ToInt32( b01.fenxiaoid) }
                                                      where
                                                        a.Id != b01.Id
                                                      select new
                                                      {
                                                          a,
                                                          levelid = b01.Id,
                                                          b01.name,
                                                          b01.tel,
                                                          b01.invitedcode,
                                                          membertype = "一级会员"
                                                      }))
                                             join b0 in db.UserInfo on new { levelid = a.levelid } equals new { levelid = Convert.ToInt32(b0.fenxiaoid) }
                                             select new
                                             {
                                                 a,
                                                 TwoID = b0.Id,
                                                 TwoName = b0.name,
                                                 TwoTel = b0.tel,
                                                 TwoInvitedcode = b0.invitedcode,
                                                 Twomembertype = "二级会员"
                                             }))
                                    join b in db.UserInfo on new { TwoID = a.TwoID } equals new { TwoID = Convert.ToInt32(b.fenxiaoid) }
                                    select new
                                    {
                                        name = a.a.name,
                                        tel = b.tel,
                                        fenxiaoid = (System.Int32?)b.fenxiaoid,
                                        invitedcode = b.invitedcode,
                                        membertype = "三级会员"
                                    }
                                    )))
                            group a by new
                            {
                                a.name,
                                a.membertype,
                                a.fenxiaoid,
                                a.invitedcode
                            } into g
                            select new SearchMemberJiBieFenXiData
                            {
                               FenXiaoName=  g.Key.name,
                              FenXiaoID=  Convert.ToInt32(g.Key.fenxiaoid),
                               FenXiaoTel= g.Key.invitedcode,
                               Membertype= g.Key.membertype,
                                SumCount = g.Count()
                            }
                    ).ToList<SearchMemberJiBieFenXiData>();
                return getPayMoneyData;
            }
        }


        /// <summary>
        /// ii.	商品库龄分析
        /// </summary>
        /// <returns></returns>
        public static List<SearchGoodsStockAgeData> GetSearchGoodsStockAgeData()
        {
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {

                List<SearchGoodsStockAgeData> getPayMoneyData =
                    (
                           from a in db.goods
                           join b in db.goodssort on new { goodssort = a.goodssort } equals new { goodssort = b.sortId }
                           select new SearchGoodsStockAgeData
                           {
                               GoodsCode = a.goodscode,
                               GoodsTitle = a.goodstitle,
                               GoodsStock = a.goodstock,
                               GoodsPrice = a.goodsprice,
                               GoodsCost = a.goodscost,
                               SortName = b.sortName,
                               Addtime = a.addtime,
                               ExpireDate = a.expiredate
                           }

                    ).ToList<SearchGoodsStockAgeData>();
                return getPayMoneyData;
            }
        }
        /// <summary>
        /// ii.	发货商品管理
        /// </summary>
        /// <returns></returns>
        public static List<SearchGoodsDeliveryData> GetSearchGoodsDeliveryData()
        {
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {

                List<SearchGoodsDeliveryData> getPayMoneyData =
                    (
                     (
                        from a in db.ordersdetails
                        join b in db.goods
                                on new { a.buygoodsid,selectytsort=Convert.ToChar(a.selectytsort) }
                            equals new { buygoodsid = b.goodsId, b.selectytsort }
                        join c in db.orders on a.orderno equals c.orderno
                        where
                            Convert.ToString(c.orderstatus) == "2"
                        select new SearchGoodsDeliveryData
                        {
                            OrderNo=  a.orderno,
                            GoodsTitle= b.goodstitle,
                            GoodsCode= b.goodscode,
                                GoodsImg=  b.goodsimg,
                            GoodsPrice=  (System.Decimal?)a.goodsprice,
                            GoodsStock = b.goodstock,
                            BuysumQty = a.buysumqty,
                            SelectytSort = b.selectytsort.ToString() == "P" ? "普通商品" : "推荐商品"
                        }
                    ).Union
                    (
                        from a in db.ordersdetails
                        join b in db.goodstuan
                                on new { a.buygoodsid, a.selectytsort }
                            equals new { buygoodsid = b.Id, b.selectytsort }
                        join c in db.orders on a.orderno equals c.orderno
                        where
                            Convert.ToString(c.orderstatus) == "2"
                        select new SearchGoodsDeliveryData
                        {
                            OrderNo = a.orderno,
                            GoodsTitle = b.title,
                            GoodsCode = b.goodscode,
                            GoodsImg = b.img,
                            GoodsPrice = (System.Decimal?)b.price,
                            GoodsStock = b.quantity,
                            BuysumQty = a.buysumqty,
                            SelectytSort = b.selectytsort.ToString() == "G" ? "团购商品" : "预售商品"
                        }
                    )
                    ).ToList<SearchGoodsDeliveryData>();
                return getPayMoneyData;
            }
        }
        /// <summary>
        /// ii.	剩余商品库存
        /// </summary>
        /// <returns></returns>
        public static List<SearchGoodsSyStockData> GetSearchGoodsSyStockData()
        {
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {

                List<SearchGoodsSyStockData> getPayMoneyData =
                    (
                         (
                            from a in db.goods
                            join b in db.goodssort on new { goodssort = a.goodssort } equals new { goodssort = b.sortId }
                            select new SearchGoodsSyStockData
                            {
                               GoodsCode =a.goodscode,
                               GoodsTitle= a.goodstitle,
                                GoodsStock = a.goodstock,
                                GoodsPrice = (System.Decimal?)a.goodsprice,
                                GoodsCost = (System.Decimal?)a.goodscost,
                               SortName= b.sortName
                            }
                        ).Union
                        (
                            from a in db.goodstuan
                            join b in db.goodssort on new { groupsort = a.groupsort } equals new { groupsort = b.sortId }
                            select new SearchGoodsSyStockData
                            {
                                GoodsCode = a.goodscode,
                                GoodsTitle = a.title,
                                GoodsStock = a.quantity,
                                GoodsPrice = (System.Decimal?)a.price,
                                GoodsCost = (System.Decimal?)a.costprice,
                                SortName = b.sortName
                            }
                        )
                    ).ToList<SearchGoodsSyStockData>();
                return getPayMoneyData;
            }
        }

        /// <summary>
        /// 入库商品管理信息查看
        /// </summary>
        /// <returns></returns>
        public static List<SearchGoodsInWareHouseData> GetSearchGoodsInWareHouseData(string goodtitle, string goodsort, string goodsaddtime, string goodsku)
        {
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {

                List<SearchGoodsInWareHouseData> getGoodssData = (
                    (
                            from a in db.goods
                            join b in db.goodssort on new { goodssort = a.goodssort } equals new { goodssort = b.sortId }
                            join c in
                                (
                                    (from ordersdetails in db.ordersdetails
                                     group ordersdetails by new
                                     {
                                         ordersdetails.buygoodsid,
                                         ordersdetails.selectytsort
                                     } into g
                                     select new
                                     {
                                         buygoodsid = (System.Int32?)g.Key.buygoodsid,
                                         buysumqty = (System.Int32?)g.Sum(p => p.buysumqty),
                                         selectytsort = g.Max(p => p.selectytsort)
                                     }))
                                  on new { a.goodsId, a.selectytsort }
                              equals new { goodsId = Convert.ToInt32(c.buygoodsid), selectytsort=Convert.ToChar(c.selectytsort) }
                            select new SearchGoodsInWareHouseData
                            {
                                ID =  a.goodsId,
                               Title= a.goodstitle,
                               GoodsSortID=b.sortId,
                               GoodsSort= b.sortName,
                               GoodsCode= a.goodscode,
                              GoodsSpec = a.goodsspec,
                                GoodsPrice = (System.Decimal?)a.goodsprice,
                                GoodsCost = (System.Decimal?)a.goodscost,
                                GoodsStock = (System.Int32?)a.goodstock,
                                Buysumqty = (System.Int32?)c.buysumqty,
                                SelectytSort =  c.selectytsort,//.ToString() == "P" ? "普通商品" : "推荐商品",
                                AddTime = a.addtime
                            }
                        ).Union
                        (
                            from a in db.goodstuan
                            join b in db.goodssort on new { groupsort = a.groupsort } equals new { groupsort = b.sortId }
                            join c in
                                (
                                    (from ordersdetails in db.ordersdetails
                                     group ordersdetails by new
                                     {
                                         ordersdetails.buygoodsid,
                                         ordersdetails.selectytsort
                                     } into g
                                     select new
                                     {
                                         buygoodsid = (System.Int32?)g.Key.buygoodsid,
                                         buysumqty = (System.Int32?)g.Sum(p => p.buysumqty),
                                         selectytsort = g.Max(p => p.selectytsort)
                                     }))
                                  on new { a.Id, a.selectytsort }
                              equals new { Id = Convert.ToInt32(c.buygoodsid), c.selectytsort }
                            select new SearchGoodsInWareHouseData
                            {
                                ID = a.Id,
                                Title = a.title,
                                GoodsSortID=b.sortId,
                                GoodsSort = b.sortName,
                                GoodsCode = a.goodscode,
                                GoodsSpec = "",
                                GoodsPrice = (System.Decimal?)a.price,
                                GoodsCost = (System.Decimal?)a.costprice,
                                GoodsStock = (System.Int32?)a.quantity,
                                Buysumqty = c.buysumqty,
                                SelectytSort = c.selectytsort,//.ToString() == "G" ? "团购商品" : "预售商品",
                                AddTime = a.addtime
                            }
                        )
                    
                    ).ToList<SearchGoodsInWareHouseData>();
                //商品名称
                if (!string.IsNullOrEmpty(goodtitle))
                {
                    getGoodssData = (from p in getGoodssData where p.Title.Contains(goodtitle) select p).ToList<SearchGoodsInWareHouseData>();
                }
                //商品分类
                if (!string.IsNullOrEmpty(goodsort) && goodsort != "商品分类")
                {
                    getGoodssData = (from p in getGoodssData where p.GoodsSortID == int.Parse(goodsort) select p).ToList<SearchGoodsInWareHouseData>();
                }
               //入库时间
                if (!string.IsNullOrEmpty(goodsaddtime) )
                {
                    var dtBegin = DateTime.Parse(goodsaddtime).ToString("yyyy-MM-dd 00:00:00");
                    var dtEnd = DateTime.Parse(goodsaddtime).ToString("yyyy-MM-dd 23:59:59");
                    getGoodssData = (from p in getGoodssData
                                        where p.AddTime >= Convert.ToDateTime(dtBegin)
                && p.AddTime <= Convert.ToDateTime(dtEnd)
                                     select p).ToList<SearchGoodsInWareHouseData>();
                 }
                //商品条码
                if (!string.IsNullOrEmpty(goodsku))
                {
                    getGoodssData = (from p in getGoodssData where p.GoodsCode.Contains(goodsku) select p).ToList<SearchGoodsInWareHouseData>();
                }
                return getGoodssData;
            }
        }


		#region 过滤html,js,css代码
		/// <summary>
		/// 过滤html,js,css代码
		/// </summary>
		/// <param name="html">参数传入</param>
		/// <returns></returns>
		public static string CheckStr(string html)
		{
			Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
			Regex regex3 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
			Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
			Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
			//	Regex regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
			Regex regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
			Regex regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);
			//	Regex regex9 = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
			Regex regex10 = new Regex(@"&nbsp;", RegexOptions.IgnoreCase);
			Regex regex11 = new Regex(@"&gt;", RegexOptions.IgnoreCase);
			Regex regex12 = new Regex(@"&lt;", RegexOptions.IgnoreCase);
			html = regex1.Replace(html, ""); //过滤<script></script>标记 
			html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
			html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
			html = regex4.Replace(html, ""); //过滤iframe 
			html = regex5.Replace(html, ""); //过滤frameset 
																			 //	html = regex6.Replace(html, ""); //过滤frameset 
			html = regex7.Replace(html, ""); //过滤frameset 
			html = regex8.Replace(html, ""); //过滤frameset 
																			 //	html = regex9.Replace(html, "");
			html = regex10.Replace(html, "");//过滤空格
			html = regex11.Replace(html, "");
			html = regex12.Replace(html, "");
			//html = html.Replace(" ", "");
			//html = html.Replace("</strong>", "");
			//html = html.Replace("<strong>", "");
			return html;
		}
		#endregion

		#region 删除指定目录以及该目录下所有文件
		/// </summary><param name="dir">欲删除文件或者目录的路径</param>
		public static void DeleteDir(string dir)
		{
			CleanFiles(dir);//第一次删除文件
			CleanFiles(dir);//第二次删除目录
		}
		/// <summary>
		/// 删除文件和目录
		/// </summary>
		///使用方法Directory.Delete( path, true)
		private static void CleanFiles(string dir)
		{
			if (!Directory.Exists(dir))
			{
				File.Delete(dir); return;
			}
			else
			{
				string[] dirs = Directory.GetDirectories(dir);
				string[] files = Directory.GetFiles(dir);
				if (0 != dirs.Length)
				{
					foreach (string subDir in dirs)
					{
						if (null == Directory.GetFiles(subDir))
						{ Directory.Delete(subDir); return; }
						else CleanFiles(subDir);
					}
				}
				if (0 != files.Length)
				{
					foreach (string file in files)
					{ File.Delete(file); }
				}
				else Directory.Delete(dir);
			}
		}
		#endregion

		#region 判断给定的字符串(strNumber)是否是数值型
		/// <param name="strNumber">要确认的字符串</param>
		/// <returns>是则返加true 不是则返回false</returns>
		public static bool IsNumber(string strNumber)
		{
			return new System.Text.RegularExpressions.Regex(@"^[0-9]*$").IsMatch(strNumber);
		}
		public static int CutIntFromStr(string str)
		{
			string sr = "";
			for (int i = 0; i < str.Length; i++)
			{
				if (IsNumber(str[i].ToString()))
				{
					sr += str[i].ToString();
				}
			}
			return Convert.ToInt32(sr);
		}
		#endregion

		#region //生成8位分销码
		private static char[] constant =
	{
				'0','1','2','3','4','5','6','7','8','9',
			//	'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
				'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
			};
		/// <summary>
		/// 生成8位编码
		/// </summary>
		/// <param name="Length">长度</param>
		/// <param name="sleep">等待线程几秒</param>
		/// <returns></returns>
		public static string GenerateRandomNumber(int Length, int sleep)
		{
			System.Text.StringBuilder newRandom = new System.Text.StringBuilder(36);// new System.Text.StringBuilder(62);
			Random rd = new Random();
			System.Threading.Thread.Sleep(sleep);
			for (int i = 0; i < Length; i++)
			{
				newRandom.Append(constant[rd.Next(36)]);
				//newRandom.Append(constant[rd.Next(62)]);
			}
			return newRandom.ToString();
		}
		#endregion

		/// <param name="Pading">空格</param>
		/// <param name="DirId">父路径ID</param>
		/// <param name="datatable">返回的datatable</param>
		/// <param name="deep">树形的深度</param>
		public static void addOtherDll(string Pading, int DirId, DataTable datatable, int deep, DropDownList ddl)
		{
			DataRow[] rowlist = datatable.Select("sortFatherId='" + DirId + "'");
			foreach (DataRow row in rowlist)
			{
				string strPading = "";
				for (int j = 0; j < deep; j++)
				{
					strPading += "　"; //用全角的空格
				}
				//添加节点
				ListItem li = new ListItem(strPading + "|--" + row["sortName"].ToString(), row["sortId"].ToString());
				ddl.Items.Add(li);
				//递归调用addOtherDll函数，在函数中把deep加1
				addOtherDll(strPading, Convert.ToInt32(row["sortId"]), datatable, deep + 1, ddl);
			}
		}


		/// <summary>
		///  下面通过一个方法来实现返回DataTable类型 
		/// LINQ返回DataTable类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="varlist"></param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
		{
			DataTable dtReturn = new DataTable();
			// column names 
			PropertyInfo[] oProps = null;
			if (varlist == null)
				return dtReturn;
			foreach (T rec in varlist)
			{
				if (oProps == null)
				{
					oProps = ((Type)rec.GetType()).GetProperties();
					foreach (PropertyInfo pi in oProps)
					{
						Type colType = pi.PropertyType;
						if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
								 == typeof(Nullable<>)))
						{
							colType = colType.GetGenericArguments()[0];
						}
						dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
					}
				}
				DataRow dr = dtReturn.NewRow();
				foreach (PropertyInfo pi in oProps)
				{
					dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
					(rec, null);
				}
				dtReturn.Rows.Add(dr);
			}
			return dtReturn;
		}
	}
	public class SearchAdminInfoData
	{
		public int AdminID { get; set; }
		public string AdminName { get; set; }
		public string AdminPassWord { get; set; }
		public DateTime? Lastlogintime { get; set; }

		public string Lastloginip { get; set; }
		public string RoleName { get; set; }
		public string PartName { get; set; }

		public Char? IfDisable { get; set; }
		public string TrueName { get; set; }
		public string ContactTel { get; set; }
		public DateTime? AddTime { get; set; }
	}
	public class SearchUserInfoData
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Nickname { get; set; }
		public string PassWord { get; set; }
		public char? Sex { get; set; }
		public string Email { get; set; }
		public string Tel { get; set; }
		public string HeadImg { get; set; }
		public string Areacity { get; set; }
		public string Address { get; set; }
		public string InvitedCode { get; set; }
		public int? UserType { get; set; }
		public int Point { get; set; }
		public DateTime AddTime { get; set; }
	}
	public class SearchAdData
	{
		public int ID { get; set; }
		public string AdTitle { get; set; }
		public string AdContent { get; set; }
		public string AdImg { get; set; }
		public string AdCompany { get; set; }
		public Char? SetIndex { get; set; }
		public string GoodsCode { get; set; }
		public DateTime AddTime { get; set; }
	}
	public class SearchNoticesInfoData
	{
		public int ID { get; set; }
		public string NewsSort { get; set; }

		public string Title { get; set; }
		public string NewsSource { get; set; }
		public string Img { get; set; }
		public string Content { get; set; }

		public Char? SetIndex { get; set; }
		public Char? Validate { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchNewsSortData
	{
		public int ID { get; set; }
		public string NewsSortName { get; set; }

		public DateTime? AddTime { get; set; }
	}

	public class SearchNewsCommentInfoData
	{
		public int ID { get; set; }
		public string NewsSort { get; set; }

		public string Title { get; set; }
		public string DiscussPeople { get; set; }
		public int DiscussUserID { get; set; }

		public string Content { get; set; }

		public DateTime? AddTime { get; set; }
	}
	public class SearchHuoDongData
	{
		public int ID { get; set; }
		public string HuodongTitle { get; set; }
		public string HuodongSort { get; set; }
		public string HuodongPeople { get; set; }
		public string HuodongKeyWord { get; set; }
		public string HuodongDate { get; set; }
		public string HuodongImg { get; set; }
		public string HuodongContent { get; set; }
		public char? HuodongActive { get; set; }
		public char? SetIndex { get; set; }

		public string UserName { get; set; }
		public string UserNickName { get; set; }
		public DateTime? AddTime { get; set; }
	}

	///20160716
	/// <summary>
	/// 参与团购/预售活动信息
	/// </summary>
	public class SearchJoinGoodsHuoDongData
	{
		public int ID { get; set; }
		public string UserName { get; set; }
		public string Tel { get; set; }
		public DateTime? AddTime { get; set; }
		public string HuodongTitle { get; set; }
		public string HuodongSort { get; set; }
		public DateTime? HuodongStartTime { get; set; }
		public DateTime? HuodongEndTime { get; set; }
		public decimal? HuodongPrice { get; set; }
		public string HuodongGoodsCode { get; set; }
		public char? SelectytSort { get; set; }

	
	}
	public class SearchHuoDongSortData
	{
		public int ID { get; set; }
		public string NewsSortName { get; set; }

		public DateTime? AddTime { get; set; }
	}
	public class SearchJoinHuoDongData
	{
		public int ID { get; set; }
		public string HuodongTitle { get; set; }
		public string Tel { get; set; }
		public string HuodongDate { get; set; }
		public string HuodongContent { get; set; }
		public string UserName { get; set; }
		public string UserNickName { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchJoinHuoDongInfoData
	{
		public int ID { get; set; }
		public string HuodongTitle { get; set; }
		public string HuodongSort { get; set; }
		public string Tel { get; set; }
		public string HuodongDate { get; set; }
		public string UserName { get; set; }
		public string UserTel { get; set; }
		public char? JoinSex { get; set; }
		public string JoinAge { get; set; }
		public string Joinnumber { get; set; }
		public string Job { get; set; }
		public string Iinterest { get; set; }
		public string Remarks { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchGoodsInfoData
	{
		public int ID { get; set; }

		public int GoodsSortID { get; set; }
		public string GoodsSort { get; set; }

		public string Title { get; set; }
		public string Img { get; set; }
		public string Content { get; set; }
		public string GoodsCode { get; set; }
		public decimal? GoodsPrice { get; set; }
		public decimal? GoodsCost { get; set; }
		public int? GoodsStock { get; set; }
		public Char? SetIndex { get; set; }
		public Char? GoodSales { get; set; }
		public Char? ExchangeGood { get; set; }
		public Char Validate { get; set; }
		public Char? Iftuangou { get; set; }
    public DateTime AddTime { get; set; }
		public DateTime? ExpireDate { get; set; }
        public Char? PuTongTuiJian { get; set; }
	}


	public class SearchGoodsSortData
	{
		public int ID { get; set; }
		public string FatherSortName { get; set; }
		public string GoodsSortName { get; set; }

		public int? SortFatherID { get; set; }
		public int? SortSonID { get; set; }
		public string SortPath { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchGroupBuyInfoData
	{
		public int ID { get; set; }
		public string ImgUrl { get; set; }

		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? Starttime { get; set; }
		public DateTime? Endtime { get; set; }
		public int? quantity { get; set; }
		public Decimal? price { get; set; }
		public Decimal? CostPrice { get; set; }
		/// <summary>
		/// 团购期数
		/// </summary>
		public string Batch { get; set; }
		public Char? Validate { get; set; }
		public Char? SetIndex { get; set; }
		public string GoodsSort { get; set; }
        public int? GoodsSortID { get; set; }
        
		public DateTime? AddTime { get; set; }
        public Char? TuanGouYuShou { get; set; }
        public string GoodsCode { get; set; }

	}

	public class SearchOrderInfoListData
	{
		public string OrderNo { get; set; }

		public string Name { get; set; }
		public string Tel { get; set; }
		public string Areacity { get; set; }
		public string Address { get; set; }
		public string Ordersource { get; set; }
		public string Paymode { get; set; }

		public Decimal? OrderPrice { get; set; }

		public char OrderStatus { get; set; }
		public DateTime ordertime { get; set; }
		public DateTime paymenttime { get; set; }

		//public string goodsTitle { get; set; }
		//public string goodsSort { get; set; }
		//public string goodsImg { get; set; }
		//public string goodscode { get; set; }
	}

	public class SearchPlantDoctorSortData
	{
		public int ID { get; set; }
		public string FatherSortName { get; set; }
		public string NewsSortName { get; set; }
		public string SortImage { get; set; }
		public int? SortFatherID { get; set; }
		public int? SortSonID { get; set; }
		public string SortPath { get; set; }
		public DateTime? AddTime { get; set; }
	}
	public class SearchPlantDoctorInfoData
	{
		public int ID { get; set; }
		public string NewsSort { get; set; }

		public string Title { get; set; }
		public string Img { get; set; }
		public string Content { get; set; }

		public Char? SetIndex { get; set; }
		public Char Validate { get; set; }
		public DateTime AddTime { get; set; }
	}

	public class SearchUserRechargeInfoData
	{

		public string Tel { get; set; }
		public int UserID { get; set; }
		public string UserName { get; set; }
		public DateTime RecTime { get; set; }
		public string RecContent { get; set; }

		public string RecNo { get; set; }
		public Decimal? RecMoeny { get; set; }

		public Char RecStatus { get; set; }

	}


	public class SearchRoleInfoData
	{
		public int ID { get; set; }

		public string RoleName { get; set; }
		public string RoleContent { get; set; }
		public string PowerName { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchMemberData
	{
		public int ID { get; set; }
		public string MemberName { get; set; }
		public string Discount { get; set; }
		public DateTime? AddTime { get; set; }
	}

	public class SearchMyPayMoneyInfoData
	{
		public string MoneyCode { get; set; }
		public DateTime? Moneytime { get; set; }
		public string OrderNo { get; set; }
		public decimal? GetMoney { get; set; }
		public decimal? SumMoney { get; set; }
		public string GetContent { get; set; }

	}

	public class SearchMyPointInfoData
	{
		public int PointID { get; set; }
		public DateTime? Pointime { get; set; }
		public int Point { get; set; }
		public int? SumPoint { get; set; }

		public string GetContent { get; set; }

	}
	public class SearchFinanceMonthInfoListData
	{
		public DateTime OrderTime { get; set; }
		public Decimal? OrderPrice { get; set; }

		public int? SumQty { get; set; }

		public int? OrderCount { get; set; }

	}

	/// <summary>
	/// 20160716
	/// </summary>
	public class ExportFinanceMonthInfoListData
	{
		public string  OrderNo { get; set; }
		public DateTime OrderTime { get; set; }
		public Decimal? OrderPrice { get; set; }

		public string expressno { get; set; }
		public DateTime? shippindtime { get; set; }
		public string goodstitle { get; set; }
		public int buysumqty { get; set; }
		public Decimal? goodsprice { get; set; }
		public string Name { get; set; }
		public string Tel { get; set; }

	}
	public class SearchFenXiaoExtractListData
	{
		public DateTime RechargeTime { get; set; }
		public Decimal? Recmoney { get; set; }

		public int? RechargeCount { get; set; }

	}
	/// <summary>
	/// 20160716
	/// </summary>
	public class ExportFenXiaoExtractListData
	{
		public string MoneyCode { get; set; }
		public string MoneyReason { get; set; }
		public DateTime? Moneytime { get; set; }
		public string OrderNo { get; set; }
		public decimal? GetMoney { get; set; }
		public string GetUserTel { get; set; }
		public string Name { get; set; }
		public string Tel { get; set; }

	}
	public class SearchMemberExtractListData
	{
		public string ExtractNo { get; set; }
		public string ExtractListInfo { get; set; }

		public DateTime ExtractCreateTime { get; set; }

	}
	public class SearchUserExtractInfoListData
	{
		public string Extno { get; set; }
		public Decimal? Extmoney { get; set; }
		public string ExtTel { get; set; }
		public string ExtName { get; set; }
		public string ExtCardNo { get; set; }
		public string Extcontent { get; set; }
		public DateTime Exttime { get; set; }

	}
	public class SearchShopData
	{
		public int ShopID { get; set; }
		public string ShopName { get; set; }
		public string ShopTel { get; set; }
		public DateTime? AddTime { get; set; }
	}


	public class SearchSaleCheckIData
	{
		public string OrderNo { get; set; }
		public DateTime PaymentTime { get; set; }

		public decimal OrderPrice { get; set; }

		public string GoodsCompany { get; set; }

		public string GoodsCode { get; set; }
		public string GoodsTitle { get; set; }
		public decimal? GoodsPrice { get; set; }
		public decimal? GoodsCost { get; set; }
		public string GoodsSpec { get; set; }
		public int BuyGoodsid { get; set; }
		public int BuySumQty { get; set; }
		public decimal? GoodsSumWeight { get; set; }
        public char? OKOrderStatus { get; set; }
		public string OrderStatus { get; set; }
		public int? ShopSet { get; set; }
	}


	public class SearchGoodsShipData
	{
        public char? GoodShipStatus { get; set; }
        public string ShipStatus { get; set; }
        public string GoodShipNo { get; set; }
        public string SjGuDongInfo { get; set; }
		public string SjZhanZhangInfo { get; set; }

		public string OrderNo { get; set; }
		public DateTime PaymentTime { get; set; }

		public int BuyerID { get; set; }

		public string name { get; set; }
		public string tel { get; set; }
		public decimal? housemoney { get; set; }
		public string areacity { get; set; }
		public string address { get; set; }
		public string juese { get; set; }
		public string gudongjibie { get; set; }

		public decimal OrderPrice { get; set; }

		public string GoodsCompany { get; set; }

		public string GoodsCode { get; set; }
		public string GoodsTitle { get; set; }
		public decimal? GoodsPrice { get; set; }
		public decimal? GoodsCost { get; set; }
		public string GoodsSpec { get; set; }
		public char? ifxiangou { get; set; }
		public string xiangounumber { get; set; }
		public int BuyGoodsid { get; set; }
		public int BuySumQty { get; set; }
		public decimal? GoodsSumWeight { get; set; }
	
		public int? ShopSet { get; set; }
	}


	public class SearchUserAreaData
	{
		public string Name { get; set; }
		public string Tel { get; set; }
		public string Areacity { get; set; }
		public int SumCount { get; set; }
	}

	public class SearchUserAgeSexData
	{
		public char? Sex { get; set; }
		public int Age { get; set; }
		public int SumCount { get; set; }
	}
	public class SearchGoodSalesTotalData
	{
		public string  GoodsTitle { get; set; }
		public string GoodsSort { get; set; }
		public string GoodsImg { get; set; }
		public int SumCount { get; set; }
	}
	public class SearchGoodAreaSalesTotalData
	{
		public string AreaCity { get; set; }
		public decimal? SumPrice { get; set; }
		public int BuySumQty { get; set; }
		public int SumCount { get; set; }
	}
	public class SearchGoodSortSalesTotalData
	{
		public string SortName { get; set; }
		public int BuySumQty { get; set; }
		public int SumCount { get; set; }
	}
	public class SearchGoodSalesSearchTotalData
	{
		public string GoodsTitle { get; set; }
		public string GoodsSort { get; set; }
		public string GoodsImg { get; set; }
		public string SelectytSort { get; set; }
    public int SumCount { get; set; }
	}


	public class SearchUserOrderTypeFenXiData
	{
		public decimal SumPrice { get; set; }
		public char OrderStatus { get; set; }
		public string PayMode { get; set; }
		public int BuySumQty { get; set; }
		public int SumCount { get; set; }
	}
	public class SearchUserOrderSalesFenXiData
	{
		public string BuyName { get; set; }
		public string BuyTel { get; set; }
		public string Areacity { get; set; }
		public decimal SumPrice { get; set; }
		public int SumCount { get; set; }
	}

	public class SearchMessageViewData
	{
		public int Messageid { get; set; }
		public string Messagename { get; set; }
		public string Messagetel { get; set; }
		public string Messagecontent { get; set; }
		public DateTime? Messagetime { get; set; }
	}
    public class SearchMemberJiBieFenXiData
    {
        public int FenXiaoID { get; set; }
        public string FenXiaoName { get; set; }
        public string FenXiaoTel { get; set; }
        public string Membertype { get; set; }
        public int SumCount { get; set; }
    }

    public class SearchGoodsStockAgeData
    {
        public string GoodsCode { get; set; }
        public string GoodsTitle { get; set; }
        public decimal? GoodsPrice { get; set; }
        public decimal? GoodsCost { get; set; }
        public int GoodsStock { get; set; }
        public string SortName { get; set; }

        public DateTime? Addtime { get; set; }
        public DateTime? ExpireDate { get; set; }
        
    }

    public class SearchGoodsDeliveryData
    {
        public string OrderNo { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsTitle { get; set; }
        public decimal? GoodsPrice { get; set; }
        public string GoodsImg { get; set; }
        public int? GoodsStock { get; set; }

        public int BuysumQty { get; set; }
        public string SelectytSort { get; set; }

    }
    public class SearchGoodsSyStockData
    {
        public string GoodsCode { get; set; }
        public string GoodsTitle { get; set; }
        public decimal? GoodsPrice { get; set; }
        public decimal? GoodsCost { get; set; }
        public int? GoodsStock { get; set; }

        public string SortName { get; set; }

    }

    public class SearchGoodsInWareHouseData
    {
        public int ID { get; set; }
        public int GoodsSortID { get; set; }
        public string GoodsSort { get; set; }

        public string Title { get; set; }
        //public string Img { get; set; }
    
        public string GoodsCode { get; set; }
        public decimal? GoodsPrice { get; set; }
        public decimal? GoodsCost { get; set; }
        public int? GoodsStock { get; set; }
        public string GoodsSpec { get; set; }
        public int? Buysumqty { get; set; }
        public char? SelectytSort { get; set; }
        
        public DateTime AddTime { get; set; }
    
    }

}