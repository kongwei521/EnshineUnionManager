using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EnshineUnionManager
{
	public partial class UserInfoUpdate : System.Web.UI.Page
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
						if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
							this.btnUpdateUserInfo.Attributes["onclick"] = "return UserInfoValidate()";
				 			this.btnReset.Attributes["onclick"] = "return ClearUserInfo()";
						BindCityArea("0", seachprov);
						this.BindShowUpInfo(Request["upid"]);
						btnAddUserInfo.Visible = false; btnUpdateUserInfo.Visible = true;

					}
					else
					{
						txtInvitedCode.Value =EnshineUnionManager.model.SearchDataClass. GenerateRandomNumber(8, 10);
						btnAddUserInfo.Visible = true; btnUpdateUserInfo.Visible = false;
						this.btnAddUserInfo.Attributes["onclick"] = "return UserInfoValidate()";
						this.btnReset.Attributes["onclick"] = "return ClearUserInfo()";
						BindCityArea("0", seachprov);
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
						UserInfo getNot = db.UserInfo.Single(x => x.Id == int.Parse(strUpid));
						txtNickName.Value = getNot.nickname;
						txtName.Value = getNot.name;
						txtPass.Value = getNot.pass;
						txtTel.Value = getNot.tel;
						txtEmail.Value = getNot.email;
						drpSex.SelectedValue = getNot.sex.ToString();
						// txtAreacity.Value = getNot.areacity;
						var area = getNot.areacity.Split(' ');
						if (area.Length > 1)
						{
							seachprov.Items.FindByText(area[0]).Selected = true;
 
							//var getCity = (from p in db.areas where p.parentid == db.areas.Single(x => x.areaname == area[1]).parentid select new { p.id, p.areaname }).ToList();
							//seachcity.DataTextField = "areaname";
							//seachcity.DataValueField = "id";
							//seachcity.DataSource = getCity;
							//seachcity.DataBind(); seachcity.Items.Insert(0, new ListItem("请选择"));
							BindCityArea(db.areas.Single(x => x.areaname == area[1]).parentid, seachcity);
							seachcity.Items.FindByText(area[1]).Selected = true;

							//var getDis = (from p in db.areas where p.parentid==db.areas.Single(x => x.areaname == area[1]).id	 select new { p.id, p.areaname }).ToList();
							//seachdistrict.DataTextField = "areaname";
							//seachdistrict.DataValueField = "id";
							//seachdistrict.DataSource = getDis;
							//seachdistrict.DataBind(); seachdistrict.Items.Insert(0, new ListItem("请选择"));
							BindCityArea(db.areas.Single(x => x.areaname == area[1]).id, seachdistrict);
							seachdistrict.Items.FindByText(area[2]).Selected = true;
						}
					else
						{
							var parentid = db.areas.Single(x => x.areaname.Contains(area[0])).parentid;
							var getProv = db.areas.Single(x =>x.id== parentid).areaname;
							seachprov.Items.FindByText(getProv).Selected = true;

							var getCity = db.areas.Single(x => x.areaname.Contains(area[0])).areaname;
							BindCityArea(db.areas.Single(x => x.areaname.Contains(area[0])).parentid, seachcity);
							seachcity.Items.FindByText(getCity).Selected = true;

							BindCityArea(db.areas.Single(x => x.areaname.Contains(area[0])).id, seachdistrict);
							//seachdistrict.Items.FindByText(area[2]).Selected = true;
						}
						txtAddress.Value = getNot.address;
						//20160131 东方汇农需要手机号为验证码
						//txtInvitedCode.Value = getNot.invitedcode;
						txtInvitedCode.Value = getNot.tel;
						txtCardNo.Value = getNot.cardno;
						txtPlantNum.Value = getNot.plantnum;
						txtHomeNum.Value = getNot.homenum;
						//db.SubmitChanges();
					}
			}
		}
		protected void btnAddUserInfo_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				UserInfo adNot = new UserInfo();
				adNot.nickname = txtNickName.Value.Trim();
				adNot.name = txtName.Value.Trim(); ;
				adNot.tel = txtTel.Value.Trim(); ;
				adNot.email = txtEmail.Value.Trim(); ;
				adNot.sex = Convert.ToChar(drpSex.SelectedValue);
				adNot.address = txtAddress.Value.Trim(); ;
				adNot.areacity = seachprov.SelectedItem.Text + " " + seachcity.SelectedItem.Text + " " +
seachdistrict.SelectedItem.Text;
				//txtAreacity.Value.Trim();
				adNot.pass = txtPass.Value.Trim(); ;
				adNot.invitedcode = txtTel.Value.Trim();  // 20160220 del txtInvitedCode.Value.Trim();
				adNot.cardno=txtCardNo.Value  ;
				adNot.plantnum=txtPlantNum.Value   ;
				adNot.homenum=txtHomeNum.Value  ;
				adNot.addtime = DateTime.Now;
				db.UserInfo.InsertOnSubmit(adNot);
				db.SubmitChanges();
			}
			Response.Redirect("UserinfoManager.aspx");
		}
		protected void btnUpdateUserInfo_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						UserInfo upNot = db.UserInfo.Single(x => x.Id == int.Parse(Request["upid"]));
						upNot.nickname = txtNickName.Value.Trim();
						upNot.name = txtName.Value.Trim();
						upNot.tel = txtTel.Value.Trim();
						upNot.email = txtEmail.Value.Trim();
						upNot.sex = Convert.ToChar(drpSex.SelectedValue);
						upNot.address = txtAddress.Value.Trim();
						upNot.areacity = seachprov.SelectedItem.Text + " " + seachcity.SelectedItem.Text + " " +
seachdistrict.SelectedItem.Text;//txtAreacity.Value.Trim();
						upNot.pass = txtPass.Value.Trim();
						upNot.cardno = txtCardNo.Value;
						upNot.plantnum = txtPlantNum.Value;
						upNot.homenum = txtHomeNum.Value;
						db.SubmitChanges();
					}
				Response.Redirect("UserinfoManager.aspx");
			}
		}

		protected void seachprov_SelectedIndexChanged(object sender, EventArgs e)
		{
			string pid = seachprov.SelectedItem.Value.Trim();
			if (pid != "0")
			{
				//初始化 市和县级列表
				//初始化市列表
				seachcity.Items.Clear(); /*seachdistrict.Items.Clear(); seachdistrict.Items.Insert(0, new ListItem("请选择"));*/
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					var getCity = (from p in db.areas where p.parentid == pid select new { p.id, p.areaname }).ToList();
					if (getCity.Count == 1)
					{
						seachcity.DataTextField = "areaname";
						seachcity.DataValueField = "id";
						seachcity.DataSource = getCity;
						seachcity.DataBind(); seachcity.Items.Insert(0, new ListItem("请选择"));
						//var getDis = (from p in db.areas where p.parentid == getCity[0].id select new { p.id, p.areaname }).ToList();
						//seachdistrict.DataTextField = "areaname";
						//seachdistrict.DataValueField = "id";
						//seachdistrict.DataSource = getDis;
						//seachdistrict.DataBind(); seachdistrict.Items.Insert(0, new ListItem("请选择"));
						BindCityArea(getCity[0].id, seachdistrict);	
					}
					else
					{
						seachcity.DataTextField = "areaname";
						seachcity.DataValueField = "id";
						seachcity.DataSource = getCity;
						seachcity.DataBind(); seachcity.Items.Insert(0, new ListItem("请选择"));
					}
				}
			}
		}

		protected void seachcity_SelectedIndexChanged(object sender, EventArgs e)
		{
			string pid = seachcity.SelectedItem.Value.Trim();
			if (pid != "0")
			{
				BindCityArea(pid, seachdistrict);
      }
		}
		/// <summary>
		/// 共通引用部分 绑定省市区
		/// 	//初始化 市和县级列表
		///初始化市列表
		/// </summary>
		/// <param name="codeid"></param>
		/// <param name="ddlArea"></param>
		public void BindCityArea(string codeid, DropDownList ddlArea)
		{
			ddlArea.Items.Clear();
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				var getDis = (from p in db.areas where p.parentid == codeid select new { p.id, p.areaname }).ToList();
				ddlArea.DataTextField = "areaname";
				ddlArea.DataValueField = "id";
				ddlArea.DataSource = getDis;
				ddlArea.DataBind(); ddlArea.Items.Insert(0, new ListItem("请选择"));
			}
		}
	}
}