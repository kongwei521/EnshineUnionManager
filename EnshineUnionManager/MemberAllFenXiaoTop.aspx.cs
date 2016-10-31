using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class MemberAllFenXiaoTop : System.Web.UI.Page
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
						//SELECT * from  (SELECT count(fenxiaoid) fenxiaocount, fenxiaoid from UserInfo where fenxiaoid is not NULL
						//GROUP BY fenxiaoid)a INNER JOIN UserInfo b  on a.fenxiaoid=b.fenxiaoid
						var huiyuanfenxiaoTop = (from a in (
																(from userinfo in db.UserInfo
																 where
																	 userinfo.fenxiaoid != null
																 group userinfo by new
																 {
																	 userinfo.fenxiaoid
																 } into g
																 select new
																 {
																	 fenxiaocount = (Int64?)g.Count(p => p.fenxiaoid != null),
																	 fenxiaoid = (System.Int32?)g.Key.fenxiaoid
																 }))
																		 join b in db.UserInfo on new { fenxiaoid = Convert.ToInt32(a.fenxiaoid) } equals new { fenxiaoid = b.Id }
																		 join c in db.memberset on new { usertype = Convert.ToInt32(b.usertype) } equals new { usertype = c.memberid } into c_join
																		 from c in c_join.DefaultIfEmpty()
																		 select new
																		 {
																			 b.name,
																			 b.tel,
																			 MemberName = (c.membername ?? "普通会员"),
																			 a.fenxiaocount
																		 }).Take(50);
						if (huiyuanfenxiaoTop.Count() > 0)
						{
							rpMemberFenXiaoAllTopTotal.DataSource = huiyuanfenxiaoTop.OrderByDescending(x => x.fenxiaocount);
							rpMemberFenXiaoAllTopTotal.DataBind();
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