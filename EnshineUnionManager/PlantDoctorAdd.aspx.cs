using EnshineUnionManager.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class PlantDoctorAdd : System.Web.UI.Page
	{
		public string PhotoUrl;//图片路径(预览用)
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
						 
						var getSoStatus = from p in db.plantdoctorsort select new { p.sortId, p.sortName, p.sortFatherId, p.sortPath };
						if (getSoStatus.Count() <= 0)
						{
							plantdoctorsort insert = new plantdoctorsort { sortName = "无上级分类", sortFatherId = -1, sortPath = "-1", addTime = DateTime.Now };
							db.plantdoctorsort.InsertOnSubmit(insert);
							db.SubmitChanges();
						}
						DataTable dtSort = SearchDataClass.ToDataTable(getSoStatus);
						//添加根目录
						DataRow[] drs = dtSort.Select("sortFatherId=-1");
						for (var i = 0; i < drs.Length; i++)
						{
							string nodeid = drs[i]["sortId"].ToString();
							string text = drs[i]["sortName"].ToString();

							text = "╋" + text;
							this.drpPlantDoctorSort.Items.Add(new ListItem(text, nodeid));
							int sonparentid = int.Parse(nodeid);

					SearchDataClass.addOtherDll("", sonparentid, dtSort, 1, drpPlantDoctorSort);

						}
						drpPlantDoctorSort.DataBind();
						drpPlantDoctorSort.Items.Insert(0, new ListItem("-请选择植保类型-"));
						if (!string.IsNullOrEmpty(Request["upid"]))
						{
							if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
								this.btnUpdatePlantDoctor.Attributes["onclick"] = "return PlantDoctorValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearPlantDoctor()";
							this.BindShowUpInfo(Request["upid"]);
							this.btnAddPlantDoctor.Visible = false; this.btnUpdatePlantDoctor.Visible = true;
						}
						else
						{
							this.btnAddPlantDoctor.Attributes["onclick"] = "return PlantDoctorValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearPlantDoctor()";
							this.btnAddPlantDoctor.Visible = true; this.btnUpdatePlantDoctor.Visible = false;
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
						plantdoctor getNot = db.plantdoctor.Single(x => x.Id == int.Parse(strUpid));
						txtTitle.Value = getNot.title;
						txtFckContent.Value = SearchDataClass.CheckStr(getNot.content);
						drpValidate.SelectedValue = getNot.validate.ToString();
						drpSetIndex.SelectedValue = getNot.setindex.ToString();
						this.drpPlantDoctorSort.SelectedValue = getNot.plantdoctorsort.ToString();

						this.hfDel.Value = getNot.img;
						this.HFurl.Value = getNot.img;
						if (!string.IsNullOrEmpty(getNot.img))
						{ this.iShowPhoto.Src = getNot.img; }
						else
						{
							this.iShowPhoto.Src = "assets/images/nophoto.gif";
						}


						//db.SubmitChanges();
					}
			}
		}
		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/PlantDoctorImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad.PostedFile.FileName != "")
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张植保医院图片不能超过4096K(4M)，请重新选择植保医院图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/PlantDoctorImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('植保医院图片修改成功。点击预览查看植保医院图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/PlantDoctorImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('植保医院图片上传成功。点击预览查看植保医院图片。');</script>");
					}
				}
			}

		}

		protected void btnAddPlantDoctor_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				plantdoctor addNot = new plantdoctor();
				addNot.title = txtTitle.Value.Trim();
				addNot.content = SearchDataClass.CheckStr(txtFckContent.Value);

				addNot.validate = Convert.ToChar(drpValidate.SelectedValue);
				addNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);

				addNot.plantdoctorsort = int.Parse(drpPlantDoctorSort.SelectedValue);
				addNot.img = HFurl.Value.Trim();
				addNot.addtime = DateTime.Now;
				db.plantdoctor.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("PlantDoctorManager.aspx");
		}

		protected void btnUpdatePlantDoctor_Click(object sender, EventArgs e)
		{

			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						plantdoctor upNot = db.plantdoctor.Single(x => x.Id == int.Parse(Request["upid"]));
						upNot.title = txtTitle.Value.Trim();
						upNot.content = SearchDataClass.CheckStr(txtFckContent.Value);

						upNot.validate = Convert.ToChar(drpValidate.SelectedValue);
						upNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);


						upNot.plantdoctorsort = int.Parse(drpPlantDoctorSort.SelectedValue);
						if (!string.IsNullOrEmpty(HFurl.Value))
						{ upNot.img = HFurl.Value.Trim(); }
						upNot.addtime = DateTime.Now;
						db.SubmitChanges();
					}
				Response.Redirect("PlantDoctorManager.aspx");
			}
		}



 
	

	
	}
}