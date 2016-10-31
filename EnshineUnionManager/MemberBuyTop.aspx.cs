using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class MemberBuyTop : System.Web.UI.Page
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

					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						var buyTop = (from a in db.orders
													join b in db.UserInfo on new { buyername = a.buyername } equals new { buyername = b.Id }
													where
														Convert.ToString(a.orderstatus) == "1"
													group new { b, a } by new
													{
														b.name,
														b.tel
													} into g
													orderby
														(System.Decimal?)g.Sum(p => p.a.orderprice) descending
													select new
													{
														SumPrice = (System.Decimal?)g.Sum(p => p.a.orderprice),
														OrderCount = (Int64?)g.Count(p => p.a.orderno != null),
														g.Key.name,
														g.Key.tel
													}).Take(50);
						if (buyTop.Count() > 0)
						{
							rpMemberBuyTopTotal.DataSource = buyTop;
							rpMemberBuyTopTotal.DataBind();
						}
						else
							liMessage2.Visible = true;

					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}

			}
		}
	}
}