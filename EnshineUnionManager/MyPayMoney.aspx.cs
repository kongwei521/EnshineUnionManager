using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class MyPayMoney : System.Web.UI.Page
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
					if (getCookies["ClientName"] != null)
					{
						using (EnshineUnionDataContext db = new EnshineUnionDataContext())
						{
							if (getCookies["ClientName"] != "admin")
								spMoney.InnerText = Convert.ToDecimal(db.UserInfo.Single(x => x.tel == getCookies["ClientName"]).housemoney).ToString("0.00");
							else
							{
								spMoney.InnerText = "0.00"; btnMemberExtrac.Disabled = false;
              }
						}
					}
					else
					{
						spMoney.InnerText = "0.00";

					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}
			}
		}

		protected void btnMemberExtrac_ServerClick(object sender, EventArgs e)
		{
			HttpCookie getCookies = Request.Cookies["UserLogin"];
			if (getCookies != null )
			{
				if (Convert.ToDecimal(txtMemberExtrac.Value) > Convert.ToDecimal(spMoney.InnerText))

				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('提现不能超过账户余额。');</script>");
					return;
				}
				else
				{
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						memberextract ext = new memberextract();
						ext.extno = DateTime.Now.ToString("yymmssffff");
						ext.extmoney = Convert.ToDecimal(txtMemberExtrac.Value);
						ext.exttel = getCookies["ClientName"];
						ext.extuserid = int.Parse(getCookies["ClientUserID"]);
						ext.extcontent = "提现";
						ext.extcardno = txtCardNo.Value;
						ext.exttime = DateTime.Now;
						db.memberextract.InsertOnSubmit(ext);
						db.SubmitChanges();
						ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('提现成功等待财务审核后进行手工打款。');window.location.href='MyPayMoney.aspx';</script>");

					}
				}
			}
		}
	}
}