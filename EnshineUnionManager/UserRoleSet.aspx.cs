using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class UserRoleSet : System.Web.UI.Page
	{
			public StringBuilder seriesData = new StringBuilder();

			protected void Page_Load(object sender, EventArgs e)
			{
				if (!IsPostBack)
				{
					//在服务器端判断request来自Ajax请求(异步)还是传统请求(同步)
					if (Request.Headers["X-Requested-With"] != null && Request.Headers["X-Requested-With"].ToLower() == "XMLHttpRequest".ToLower())
					{
						//清除缓冲区流中的所有内容输出
						Response.Clear();
						//设置输出流的HTTP MIMEl类型
						Response.ContentType = "application/json";
						//将一个字符串写入HTTP相应输出流
						Response.Write(GetJson());
						//将当前所有缓冲的输出发送到客户端，停止该页的执行
						Response.End();
					}
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
							if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
								this.btnUpdateRole.Attributes["onclick"] = "return RoleValidate();";
							this.btnReset.Attributes["onclick"] = "return ClearRole();";
							this.BindShowUpInfo(Request["upid"]);
							this.btnAddRole.Visible = false; this.btnUpdateRole.Visible = true;
						}
						else
						{
							this.btnAddRole.Attributes["onclick"] = "return RoleValidate();";
							this.btnReset.Attributes["onclick"] = "return ClearRole();";
							this.btnAddRole.Visible = true; this.btnUpdateRole.Visible = false;
						}
					}
					else
					{
						ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
					}

				}
			}
			//序列化，将对象转化为JSON字符串
			protected string GetJson()
			{
				//为启用 AFAX 的应用程序提供序列化和反序列化功能
				System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
				List<MenuList> list = new List<MenuList>();
				//获取管理员模块列表
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					list = (from p in db.menulist where p.isshow == 'Y' select new MenuList { menuid = p.menuid, menuname = p.menuname, fathermenuid = p.fathermenuid }).ToList<MenuList>();
				}

				//将对象转换为JSON字符串      
				return json.Serialize(list);
			}
			public void BindShowUpInfo(string strUpid)
			{
				if (!string.IsNullOrEmpty(strUpid))
				{
					if (EnshineUnionManager.model.SearchDataClass.IsNumber(strUpid) == true)
						using (EnshineUnionDataContext db = new EnshineUnionDataContext())
						{
							userrole getNot = db.userrole.Single(x => x.roleid == int.Parse(strUpid));
							txtRoleName.Value = getNot.rolename;
							txtFckContent.Value = getNot.roledescribe;

							hfRole.Value = getNot.powerid;
						}
				}
			}
			protected void btnAddRole_Click(object sender, EventArgs e)
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					userrole url = new userrole();
					url.rolename = txtRoleName.Value.Trim();
					url.roledescribe = txtFckContent.Value.Trim();
					url.powerid = hfRole.Value.Trim().TrimEnd(',');
					url.addTime = DateTime.Now; ;
					db.userrole.InsertOnSubmit(url);
					db.SubmitChanges();
				}
			Response.Redirect("UserRoleManager.aspx?mid=" + Request["mid"]);

		}

		protected void btnUpdateRole_Click(object sender, EventArgs e)
			{
				if (!string.IsNullOrEmpty(Request["upid"]))
				{
					if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
						using (EnshineUnionDataContext db = new EnshineUnionDataContext())
						{
							userrole upurl = db.userrole.Single(x => x.roleid == int.Parse(Request["upid"]));
							upurl.rolename = txtRoleName.Value.Trim();
							upurl.roledescribe = txtFckContent.Value.Trim();
							upurl.powerid = hfRole.Value.Trim().TrimEnd(',');

							db.SubmitChanges();
						}
				Response.Redirect("UserRoleManager.aspx?mid=" + Request["mid"]);

			}
		}
			//http://www.suchso.com/projecteactual/asp-net-ztree-async-demo-down.html
			//http://www.cxyclub.cn/n/53175/
			public class MenuList
			{
				public int? menuid { get; set; }
				public string menuname { get; set; }
				public int fathermenuid { get; set; }
			}
		}
	}