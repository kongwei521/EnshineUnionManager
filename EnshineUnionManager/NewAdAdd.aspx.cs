using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace EnshineUnionManager
{
	public partial class NewAdAdd : System.Web.UI.Page
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
					if (!string.IsNullOrEmpty(Request["upid"]))
					{
						if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
							this.btnUpdateAd.Attributes["onclick"] = "return AddAdInfo()";
						this.btnReset.Attributes["onclick"] = "return ClearAdInfo()";
						this.BindShowUpInfo(Request["upid"]);
						this.btnAddAd.Visible = false; this.btnUpdateAd.Visible = true;
					}
					else
					{
						this.btnAddAd.Attributes["onclick"] = "return AddAdInfo()";
						this.btnReset.Attributes["onclick"] = "return ClearAdInfo()";
						this.btnAddAd.Visible = true; this.btnUpdateAd.Visible = false;
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
					using (EnshineUnionDataContext  db = new EnshineUnionDataContext())
					{
						Advertisement getNot = db.Advertisement.Single(x => x.AdID == int.Parse(strUpid));
						txtTitle.Value = getNot.AdTitle;
						txtFckContent.Value = getNot.AdContent;
						this.hfDel.Value = getNot.AdImg;
						this.HFurl.Value = getNot.AdImg;
						if (!string.IsNullOrEmpty(getNot.AdImg))
						{ this.iShowPhoto.Src = getNot.AdImg; }
						else
						{
							this.iShowPhoto.Src = "assets/images/nophoto.gif";
						}
						txtCompany.Value = getNot.Company;
						drpSetIndex.SelectedValue = getNot.setindex.ToString();
						txtGoodsCode.Value = getNot.goodscode;
						//	db.SubmitChanges();
					}
			}
		}
		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/AdImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad.PostedFile.FileName != "")
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张广告图片不能超过4096K(4M)，请重新选择广告图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/AdImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('广告图片修改成功。点击预览查看广告图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/AdImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('广告图片上传成功。点击预览查看广告图片。');</script>");
					}
				}
			}

		}
		protected void btnAddAd_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				Advertisement addNot = new Advertisement();
				addNot.AdTitle = txtTitle.Value.Trim();
				addNot.AdContent = txtFckContent.Value.Trim();
				addNot.Company = txtCompany.Value;
				addNot.AdImg = HFurl.Value.Trim();
				addNot.Addtime = DateTime.Now; 
addNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
				addNot.goodscode = txtGoodsCode.Value;
				db.Advertisement.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("AdManager.aspx?mid="+Request["mid"]);
		}

		protected void btnUpdateAd_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						Advertisement upNot = db.Advertisement.Single(x => x.AdID == int.Parse(Request["upid"]));
						upNot.AdTitle = txtTitle.Value.Trim();
						upNot.Company = txtCompany.Value;
						upNot.AdContent = txtFckContent.Value.Trim();
						if (!string.IsNullOrEmpty(HFurl.Value))
						{
							upNot.AdImg = HFurl.Value.Trim();
						}
						//upNot.Addtime = DateTime.Now;
						upNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
						upNot.goodscode = txtGoodsCode.Value;

						db.SubmitChanges();
					}
				Response.Redirect("AdManager.aspx?mid=" + Request["mid"]);
			}
		}
	 
		 
	}
}