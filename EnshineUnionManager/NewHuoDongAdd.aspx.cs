using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class NewHuoDongAdd : System.Web.UI.Page
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
						var getSoStatus = (from p in db.HuoDongSort select new { p.sortId, p.sortName }).ToList();
						drpHuoDongSort.DataTextField = "sortName";
						drpHuoDongSort.DataValueField = "sortId";
						drpHuoDongSort.DataSource = getSoStatus;
						drpHuoDongSort.DataBind();
						drpHuoDongSort.Items.Insert(0, new ListItem("-请选择活动类型-"));
					}
					if (!string.IsNullOrEmpty(Request["upid"]))
					{
						if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
							this.btnUpdateHuoDong.Attributes["onclick"] = "return AddHuoDongInfo()";
						this.btnReset.Attributes["onclick"] = "return ClearHuoDongInfo()";
						this.BindShowUpInfo(Request["upid"]);
						this.btnAddHuoDong.Visible = false; this.btnUpdateHuoDong.Visible = true;
					}
					else
					{
						this.btnAddHuoDong.Attributes["onclick"] = "return AddHuoDongInfo()";
						this.btnReset.Attributes["onclick"] = "return ClearHuoDongInfo()";
						this.btnAddHuoDong.Visible = true; this.btnUpdateHuoDong.Visible = false;
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
						HuoDong getNot = db.HuoDong.Single(x => x.huodongId == int.Parse(strUpid));
						txtTitle.Value = getNot.huodongTitle;
						txtPeople.Value = getNot.huodongPeople;
						txtKeyWord.Value = getNot.huodongKeyWord;
						txtTimeSelect.Value = getNot.huodongDate.ToString();
						drpSetIndex.SelectedValue = getNot.setindex.ToString();
						txtFckContent.Value = getNot.huodongContent;
						this.hfDel.Value = getNot.huodongImg;
						this.HFurl.Value = getNot.huodongImg;
						if (!string.IsNullOrEmpty(getNot.huodongImg))
						{ this.iShowPhoto.Src = getNot.huodongImg; }
						else
						{
							this.iShowPhoto.Src = "assets/images/nophoto.gif";
						}
						drpHuoDongSort.SelectedValue = getNot.huodongsort.ToString();

						//		db.SubmitChanges();
					}
			}
		}
		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/HuodongImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad.PostedFile.FileName != "")
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张活动图片不能超过4096K(4M)，请重新选择活动图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/HuodongImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('活动图片修改成功。点击预览查看活动图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/HuodongImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('活动图片上传成功。点击预览查看活动图片。');</script>");
					}
				}
			}

		}
		protected void btnAddHuoDong_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				HuoDong addNot = new HuoDong();
				addNot.huodongTitle = txtTitle.Value.Trim();
				addNot.huodongContent = txtFckContent.Value.Trim();
				addNot.huodongPeople = txtPeople.Value;
				addNot.huodongKeyWord = txtKeyWord.Value;
				addNot.huodongDate = txtTimeSelect.Value;
				if (!string.IsNullOrEmpty(HFurl.Value))
				{ addNot.huodongImg = HFurl.Value.Trim(); }
				addNot.huodongActive = 'N';
				addNot.addTime = DateTime.Now;
				addNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
				addNot.huodongsort = int.Parse(drpHuoDongSort.SelectedValue);
				db.HuoDong.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("HuoDongManager.aspx?mid=" + Request["mid"]);
		}
		protected void btnUpdateHuoDong_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						HuoDong upNot = db.HuoDong.Single(x => x.huodongId == int.Parse(Request["upid"]));
						upNot.huodongTitle = txtTitle.Value.Trim();
						upNot.huodongPeople = txtPeople.Value;
						upNot.huodongKeyWord = txtKeyWord.Value;
						upNot.huodongDate = txtTimeSelect.Value;
						upNot.huodongContent = txtFckContent.Value.Trim();
						upNot.huodongImg = HFurl.Value.Trim();
						upNot.huodongsort = int.Parse(drpHuoDongSort.SelectedValue);

						//upNot.addTime = DateTime.Now;
						upNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
						db.SubmitChanges();
					}
				Response.Redirect("HuoDongManager.aspx?mid="+Request["mid"]);
			}
		}



	}
}