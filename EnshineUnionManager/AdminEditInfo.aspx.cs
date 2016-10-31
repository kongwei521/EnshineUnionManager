using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class AdminEditInfo : System.Web.UI.Page
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
						var getSoStatus = (from p in db.userrole select new { p.roleid, p.rolename }).ToList();
						ddlrole.DataTextField = "rolename";
						ddlrole.DataValueField = "roleid";
						ddlrole.DataSource = getSoStatus;
						ddlrole.DataBind();
						ddlrole.Items.Insert(0, new ListItem("-所属角色-"));
						if (!string.IsNullOrEmpty(Request["upid"]))
						{
							if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
								this.btnUpdateAdmin.Attributes["onclick"] = "return AdminValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearAdmin()";
							this.BindShowUpInfo(Request["upid"]);
							this.btnAddAdmin.Visible = false; this.btnUpdateAdmin.Visible = true;
						}
						else
						{
							this.btnAddAdmin.Attributes["onclick"] = "return AdminValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearAdmin()";
							this.btnAddAdmin.Visible = true; this.btnUpdateAdmin.Visible = false;
						}
					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}
			}
		}
		public void BindShowUpInfo(string strUpid)
		{
			if (!string.IsNullOrEmpty(strUpid))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(strUpid) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						superadmin getNot = db.superadmin.Single(x => x.Id == int.Parse(strUpid));

						txtlogname.Value = getNot.name;
						txtPass.Value = getNot.pass;
						txttruename.Value = getNot.truename;
						txtTel.Value = getNot.contacttel;
						ddlrole.SelectedValue = getNot.roleid.ToString();
						if (getNot.ifdisable == 'Y')
						{
							cbifdisable.Checked = true;
						}
					}
			}
		}

		protected void btnAddNotices_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				superadmin addNot = new superadmin();
				addNot.name = txtlogname.Value.Trim();
				addNot.pass = txtPass.Value;
				addNot.truename = txttruename.Value;
				addNot.contacttel = txtTel.Value;
				addNot.roleid = int.Parse(ddlrole.SelectedValue);
				var ifdisable = cbifdisable.Checked == true ? 'Y' : 'N';
				addNot.ifdisable = ifdisable;
				addNot.addtime = DateTime.Now;
				db.superadmin.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("AdminManager.aspx?mid="+Request["mid"]);
		}

		protected void btnUpdateNotices_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						superadmin upNot = db.superadmin.Single(x => x.Id == int.Parse(Request["upid"]));
						upNot.name = txtlogname.Value.Trim();
						upNot.pass = txtPass.Value;
						upNot.truename = txttruename.Value;
						upNot.contacttel = txtTel.Value;
						upNot.roleid = int.Parse(ddlrole.SelectedValue);
						var ifdisable = cbifdisable.Checked == true ? 'Y' : 'N';
						upNot.ifdisable = ifdisable;
						db.SubmitChanges();
					}
				Response.Redirect("AdminManager.aspx?mid=" + Request["mid"]);

			}
		}


	}
}