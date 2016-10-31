using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class Index : System.Web.UI.Page
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
					MonthSkuTotal("", 1);
					CustomerTotal("");

				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}

			}

		}

		DateTime dt = DateTime.Now;     //得到当前时间

		protected void lbPageGetData_Click(object sender, EventArgs e)
		{
			MonthSkuTotal("", int.Parse(((LinkButton)sender).CommandArgument));
		}

		/// <summary>
		/// 本月热销产品排行榜
		/// </summary>
		/// <param name="customerid"></param>
		/// <param name="pagecount"></param>
		public void MonthSkuTotal(string customerid, int pagecount)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
				var getData =
						 (
							//(from a in psdc.ordersdetails
							//           join b in psdc.goods on new
							//{ a.buygoodsid, selectytsort = Convert.ToChar(a.selectytsort.ToString()) }
							//equals new { buygoodsid = b.goodsId, selectytsort = b.selectytsort } into b_join
							//           from b in b_join.DefaultIfEmpty()
							//           where
							//             a.ordertime >= startMonth.Date.Date && a.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59")) 
							//             //& a.CustomerID == customerid
							//           group new { a, b } by new
							//           {
							//               b.goodstitle,b.goodscode
							//           } into g
							//           select new
							//           {
							//	  GoodsCode=g.Key.goodscode,
							//               SumQty = g.Sum(p => p.a.buysumqty),
							//               GoodsTitle = g.Key.goodstitle
							//           }).Union
							//(
							//		from a in psdc.ordersdetails
							//		join b in psdc.goodstuan   on new { a.buygoodsid, a.selectytsort }
							//		equals new { buygoodsid = b.Id, b.selectytsort }    into b_join
							//               from b in b_join.DefaultIfEmpty()
							//		where
							//			a.ordertime >= startMonth.Date.Date && a.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59"))
							//		//& a.CustomerID == customerid
							//		group new { a, b } by new
							//		{
							//			b.title,
							//			b.goodscode
							//		} into g
							//		select new
							//		{
							//			GoodsCode = g.Key.goodscode,
							//			SumQty = g.Sum(p => p.a.buysumqty),
							//			GoodsTitle = g.Key.title
							//		}
							// )
							(
													from c in db.orders
													join a in db.ordersdetails on c.orderno equals a.orderno
													join b in db.goods
															on new { a.buygoodsid, selectytsort = Convert.ToChar(a.selectytsort.ToString()) }
																								equals new { buygoodsid = b.goodsId, selectytsort = b.selectytsort }
													where
														c.ordertime >= startMonth.Date.Date && c.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59"))

													group new { b, a } by new
													{
														b.goodstitle,
														b.goodscode
													} into g
													select new
													{
														GoodsCode = g.Key.goodscode,
														GoodsTitle = g.Key.goodstitle,
														SumQty = (System.Int32?)g.Sum(p => p.a.buysumqty)
													}
												).Union
												(
													from c in db.orders
													join a in db.ordersdetails on c.orderno equals a.orderno
													join b in db.goodstuan
																on new { a.buygoodsid, a.selectytsort }
														equals new { buygoodsid = b.Id, b.selectytsort }
													where
														c.ordertime >= startMonth.Date.Date && c.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59"))

													group new { b, a } by new
													{
														b.title,
														b.goodscode
													} into g
													select new
													{
														GoodsCode = g.Key.goodscode,
														GoodsTitle = g.Key.title,
														SumQty = (System.Int32?)g.Sum(p => p.a.buysumqty)
													}
												)
							).OrderByDescending(p => p.SumQty).ToList().Skip(pagecount == 1 ? 0 :
									10 * (pagecount - 1)).Take(10);
				if (getData.Count() > 0)
				{
					rpTotalSku.DataSource = getData;
					rpTotalSku.DataBind();
				}
				else
				{
					liMessage.Visible = true;
				}
			}
		}

		/// <summary>
		/// 本月客户购买排行榜
		/// </summary>
		/// <param name="customerid"></param>
		public void CustomerTotal(string customerid)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				DateTime startMonth = dt.AddDays(1 - dt.Day); //本月月初
				DateTime endMonth = startMonth.AddMonths(1).AddDays(-1); //本月月末
				var getCustomerCount = (from t in db.orders
																join pp in db.UserInfo on t.buyername equals pp.Id

																where
																	t.ordertime >= startMonth.Date.Date && t.ordertime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59:59"))
																//&& t.CustomerID == customerid
																group new { t, pp } by new
																{
																	pp.name,
																	pp.tel

																} into g
																orderby
																	(Int64?)g.Count() descending

																select new
																{
																	g.Key.name,
																	g.Key.tel,

																	SumTotal = (Int64?)g.Count()
																}).Take(10);
				if (getCustomerCount.Count() > 0)
				{
					rpCustomerTotal.DataSource = getCustomerCount;
					rpCustomerTotal.DataBind();
				}
				else
				{
					liMessage1.Visible = true;
				}

			}

		}

		/// <summary>
		/// 热销排行榜导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSkuExport_ServerClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtTimeSelect.Value))
			{
				HttpCookie getCookies = Request.Cookies["UserLogin"];
				if (getCookies != null)
				{
					string[] strDate = txtTimeSelect.Value.Split('-');
					var dtBegin = DateTime.Parse(strDate[0]).ToString("yyyy-MM-dd 00:00:00");
					var dtEnd = DateTime.Parse(strDate[1]).ToString("yyyy-MM-dd 23:59:59");
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{

						var getTopList =
								 (
									 //from a in psdc.ordersdetails
									 //               join b in psdc.goods on a.buygoodsid equals b.goodsId into b_join
									 //               from b in b_join.DefaultIfEmpty()
									 //               where
									 //                 a.ordertime >= Convert.ToDateTime(dtBegin) && a.ordertime <= Convert.ToDateTime(dtEnd)
									 //               //& a.CustomerID == customerid
									 //               group new { a, b } by new
									 //               {
									 //                   b.goodstitle,
									 //                   b.goodscode,
									 //                   b.goodsprice,
									 //                   b.goodscost,
									 //                   b.goodscompany,
									 //                   b.goodsspec
									 //               } into g
									 //               select new
									 //               {
									 //                   GoodsCode = g.Key.goodscode,
									 //                   Goodsprice = g.Key.goodsprice,
									 //                   Goodscost = g.Key.goodscost,
									 //                   Goodscompany = g.Key.goodscompany,
									 //                   Goodsspec = g.Key.goodsspec,
									 //                   SumQty = g.Sum(p => p.a.buysumqty),
									 //                   GoodsTitle = g.Key.goodstitle
									 //               }
									 (
										from c in db.orders
										join a in db.ordersdetails on c.orderno equals a.orderno
										join b in db.goods
												on new { a.buygoodsid, selectytsort = Convert.ToChar(a.selectytsort.ToString()) }
																									equals new { buygoodsid = b.goodsId, selectytsort = b.selectytsort }
										where
											c.ordertime >= Convert.ToDateTime(dtBegin) && c.ordertime <= Convert.ToDateTime(dtEnd)

										group new { b, a } by new
										{
											b.goodscode,
											b.goodsprice,
											b.goodscost
										} into g
										select new
										{
											GoodsTitle = g.Max(p => p.b.goodstitle),
											GoodsCode = g.Key.goodscode,
											Goodsprice = (System.Decimal?)g.Key.goodsprice,
											Goodscost = (System.Decimal?)g.Key.goodscost,
											SumQty = (System.Int32?)g.Sum(p => p.a.buysumqty)
										}
									).Union
									(
										from c in db.orders
										join a in db.ordersdetails on c.orderno equals a.orderno
										join b in db.goodstuan
													on new { a.buygoodsid, a.selectytsort }
											equals new { buygoodsid = b.Id, b.selectytsort }
										where
																					c.ordertime >= Convert.ToDateTime(dtBegin) && c.ordertime <= Convert.ToDateTime(dtEnd)
										group new { b, a } by new
										{
											b.goodscode,
											b.price,
											b.costprice
										} into g
										select new
										{
											GoodsTitle = g.Max(p => p.b.title),
											GoodsCode = g.Key.goodscode,
											Goodsprice = (System.Decimal?)g.Key.price,
											Goodscost = (System.Decimal?)g.Key.costprice,
											SumQty = (System.Int32?)g.Sum(p => p.a.buysumqty)
										}
									)
									).ToList();

						var getData = getTopList.OrderByDescending(p => p.SumQty).ToList();
						if (getData.Count() > 0)
						{
							//  创建一个workbookdesigner对象
							WorkbookDesigner designer = new WorkbookDesigner();
							//  制定报表模板
							designer.Open(Server.MapPath(@"model\HotSkuExport.xls"));
							//设置实体类对象
							designer.SetDataSource("Export", getData);
							// 根据数据源处理生成报表内容
							designer.Process();
							// 客户端保存的文件名
							string fileName = DateTime.Parse(strDate[0]).ToString("yyyyMMdd") + "_" + DateTime.Parse(strDate[1]).ToString("yyyyMMdd") +
										 HttpUtility.UrlEncode("热销产品统计数据导出.xls");
							designer.Save(fileName, SaveType.OpenInExcel, FileFormatType.Excel2003, Response);
							Response.Flush();
							Response.Close();
							designer = null;
							Response.End();

						}
					}

				}
			}
			else
			{
				ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择时间后在进行导出。');</script>");
			}
		}
	}
}