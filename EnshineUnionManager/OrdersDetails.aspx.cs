using EnshineUnionManager.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class OrdersDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HttpCookie getCookies = Request.Cookies["UserLogin"];
				if (getCookies != null)
				{
					spClientName.InnerHtml = "【" + Server.UrlDecode(getCookies["ClientName"].ToString()) + "】 Welcome To 益生联盟数据平台";

					string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
					var hour = string.Empty;
					if (DateTime.Now.Hour > 12)
					{
						hour = "下午" + DateTime.Now.Hour + "\n时";
					}
					else { hour = "上午" + DateTime.Now.Hour + "\n时"; }
					spNowTime.InnerText = DateTime.Now.ToString("yyyy年MM月dd日") + "\n" + Day[Convert.ToInt16(DateTime.Now.DayOfWeek)] + "\n" + hour; ;
					if (!string.IsNullOrEmpty(Request["upid"]))
					{
						if (IsNumber(Request["upid"]) == true)
						{
							using (EnshineUnionDataContext db = new EnshineUnionDataContext())
							{
								List<SearchOrderInfoListData> getOrderListData = (from p in db.orders
																																	join us in db.UserInfo on p.buyername equals us.Id into g
																																	from c in g.DefaultIfEmpty()
																																	where p.orderno == Request["upid"]
																																	select new SearchOrderInfoListData
																																	{
																																		OrderNo = p.orderno,
																																		Name = c.name,
																																		Tel = c.tel,
																																		Address = c.address,
																																		Areacity = c.areacity,
																																		OrderPrice = p.orderprice,
																																		Ordersource = p.ordersource,
																																		Paymode = p.paymode,
																																		OrderStatus = p.orderstatus,
																																		ordertime = p.ordertime,
																																		paymenttime = p.paymenttime,
																																	}).ToList<SearchOrderInfoListData>();
								foreach (var item in getOrderListData)
								{
									spOrderno.InnerHtml = "订单编号：" + item.OrderNo + "&nbsp;&nbsp;的详细信息";
									spONO.InnerHtml = item.OrderNo;
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
									spOrderStatus.InnerHtml = orderStatus;
									spOrdersource.InnerHtml = item.Ordersource;
									spordertime.InnerHtml = item.ordertime.ToString();
									spName.InnerHtml = item.Name;
									spTel.InnerHtml = item.Tel;
									spAreacity.InnerHtml = item.Areacity;
									spAddress.InnerHtml = item.Address;
								}
								List<GoogsListInfo> goodslist = (from p in db.orders
																								 join ui in db.ordersdetails on p.orderno equals ui.orderno
																								 join us in db.goods on ui.buygoodsid equals us.goodsId
																								 join ug in db.goodssort on us.goodssort equals ug.sortId

																								 where p.orderno == Request["upid"]

																								 select new GoogsListInfo
																								 {
																									 GoodsTitle = us.goodstitle,
																									 GoodsSort = ug.sortName,
																									 GoodsCode = us.goodscode,
																									 GoodsImg = us.goodsimg,
																									 GoodsPrice = us.goodsprice,
																									 SumQty = ui.buysumqty
																								 }).ToList<GoogsListInfo>();
								rpGoogsList.DataSource = goodslist;
								rpGoogsList.DataBind();

							}
						}

					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}

			}
		}
		public class GoogsListInfo
		{
			public int GoodsID { get; set; }
			public string GoodsCode { get; set; }


			public string GoodsTitle { get; set; }

			public string GoodsSort { get; set; }
			public string GoodsImg { get; set; }

			public decimal? GoodsPrice { get; set; }


			public int SumQty { get; set; }

		}
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
		decimal sum = 0;	 int buyqty = 0;
    protected void rpGoogsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string price = DataBinder.Eval(e.Item.DataItem, "GoodsPrice").ToString().Trim();
				if (!price.Equals(""))
					sum += Convert.ToDecimal(price);
				buyqty = int.Parse(DataBinder.Eval(e.Item.DataItem, "SumQty").ToString());
			}
			spSumPrice.Text = buyqty * sum + "元";

		}
	}
}