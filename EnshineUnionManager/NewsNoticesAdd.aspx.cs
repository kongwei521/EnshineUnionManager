using EnshineUnionManager.model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class NewsNoticesAdd : System.Web.UI.Page
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
						var getSoStatus = (from p in db.noticessort select new { p.sortId, p.sortName }).ToList();
						drpNewsSort.DataTextField = "sortName";
						drpNewsSort.DataValueField = "sortId";
						drpNewsSort.DataSource = getSoStatus;
						drpNewsSort.DataBind();
						drpNewsSort.Items.Insert(0, new ListItem("-请选择新闻类型-"));
						if (!string.IsNullOrEmpty(Request["upid"]))
						{
							if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
								this.btnUpdateNotices.Attributes["onclick"] = "return NoticesValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearNotices()";
							this.BindShowUpInfo(Request["upid"]);
							this.btnAddNotices.Visible = false; this.btnUpdateNotices.Visible = true;
						}
						else
						{
							this.btnAddNotices.Attributes["onclick"] = "return NoticesValidate()";
							this.btnReset.Attributes["onclick"] = "return ClearNotices()";
							this.btnAddNotices.Visible = true; this.btnUpdateNotices.Visible = false;
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
						notices getNot = db.notices.Single(x => x.Id == int.Parse(strUpid));
						txtTitle.Value = SearchDataClass.CheckStr(getNot.title); ;
						txtFckContent.Value = getNot.content;
						//SearchDataClass.CheckStr(getNot.content);
						//drpValidate.SelectedValue = getNot.validate.ToString();
						//drpSetIndex.SelectedValue = getNot.setindex.ToString();
						this.drpNewsSort.SelectedValue = getNot.newssort.ToString();

						this.hfDel.Value = getNot.img;
						this.HFurl.Value = getNot.img;
						if (!string.IsNullOrEmpty(getNot.img))
						{ this.iShowPhoto.Src = getNot.img; }
						else
						{
							this.iShowPhoto.Src = "assets/images/nophoto.gif";
						}

						this.hfDel1.Value = getNot.img1;
						this.HFurl1.Value = getNot.img1;
						if (!string.IsNullOrEmpty(getNot.img1))
						{ this.iShowPhoto1.Src = getNot.img1; }
						else
						{
							this.iShowPhoto1.Src = "assets/images/nophoto.gif";
						}

						this.hfDel2.Value = getNot.img2;
						this.HFurl2.Value = getNot.img2;
						if (!string.IsNullOrEmpty(getNot.img2))
						{ this.iShowPhoto2.Src = getNot.img2; }
						else
						{
							this.iShowPhoto2.Src = "assets/images/nophoto.gif";
						}
						txtSource.Value = getNot.newssource;

					}
			}
		}

		protected void btnAddNotices_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				notices addNot = new notices();
				addNot.title = SearchDataClass.CheckStr(txtTitle.Value.Trim()); ;
				addNot.content = txtFckContent.Value;// SearchDataClass.CheckStr(txtFckContent.Value);

				//addNot.validate = Convert.ToChar(drpValidate.SelectedValue);
				//addNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);

				addNot.newssort = int.Parse(drpNewsSort.SelectedValue);
				addNot.img = HFurl.Value.Trim();
				addNot.img1 = HFurl1.Value.Trim();
				addNot.img2 = HFurl2.Value.Trim();
				addNot.newssource = txtSource.Value.Trim();
				addNot.likenum = 0;
				addNot.discussnum = 0;
				addNot.browses = 0;
				addNot.addtime = DateTime.Now;
				db.notices.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("NewsNoticesManager.aspx?mid="+Request["mid"]);
		}

		protected void btnUpdateNotices_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						notices upNot = db.notices.Single(x => x.Id == int.Parse(Request["upid"]));
						upNot.title = SearchDataClass.CheckStr(txtTitle.Value.Trim()); ;
						upNot.content = txtFckContent.Value;//SearchDataClass.CheckStr(txtFckContent.Value);

						//upNot.validate = Convert.ToChar(drpValidate.SelectedValue);
						//upNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);


						upNot.newssort = int.Parse(drpNewsSort.SelectedValue);
						if (!string.IsNullOrEmpty(HFurl.Value))
						{ upNot.img = HFurl.Value.Trim(); }

						if (!string.IsNullOrEmpty(HFurl.Value))
						{ upNot.img1 = HFurl1.Value.Trim(); }

						if (!string.IsNullOrEmpty(HFurl.Value))
						{ upNot.img2 = HFurl2.Value.Trim(); }

						upNot.newssource = txtSource.Value.Trim();
						upNot.addtime = DateTime.Now;
						db.SubmitChanges();
					}
				Response.Redirect("NewsNoticesManager.aspx?mid=" + Request["mid"]);
			}
		}

		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/NoticesImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad.PostedFile.FileName != "")
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张公告图片不能超过4096K(4M)，请重新选择公告图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片1修改成功。点击预览查看公告图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片1上传成功。点击预览查看公告图片。');</script>");
					}
				}
			}

		}

		protected void iUpLoad1_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/NoticesImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad1.PostedFile.FileName != "")
			{
				if (fUpLoad1.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张公告图片不能超过4096K(4M)，请重新选择公告图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel1.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel1.Value));
						string imgname = fUpLoad1.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto1.Src = imgurl;
						fUpLoad1.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl1.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片2修改成功。点击预览查看公告图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad1.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto1.Src = imgurl;
						fUpLoad1.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl1.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片2上传成功。点击预览查看公告图片。');</script>");
					}
				}
			}
		}
		protected void iUpLoad2_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/NoticesImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad2.PostedFile.FileName != "")
			{
				if (fUpLoad2.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张公告图片不能超过4096K(4M)，请重新选择公告图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel2.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel2.Value));
						string imgname = fUpLoad2.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto2.Src = imgurl;
						fUpLoad2.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl2.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片3修改成功。点击预览查看公告图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad2.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/NoticesImage/" + quanname;
						this.iShowPhoto2.Src = imgurl;
						fUpLoad2.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl2.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('公告图片3上传成功。点击预览查看公告图片。');</script>");
					}
				}
			}
		}

	}
}