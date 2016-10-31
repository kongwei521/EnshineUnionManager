using EnshineUnionManager.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace EnshineUnionManager
{
	public partial class GroupBuyAdd : System.Web.UI.Page
	{
		public string PhotoUrl;//图片路径(预览用)
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HttpCookie getCookies = Request.Cookies["UserLogin"];
				if (getCookies != null)
				{
					spClientName.InnerHtml = "【" + Server.UrlDecode(getCookies["ClientName"].ToString()) + "】 Welcome To Food Order Manager";

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
						var getSoStatus = (from p in db.goodssort select new { p.sortId, p.sortName }).ToList();
						drpGoodsSort.DataTextField = "sortName";
						drpGoodsSort.DataValueField = "sortId";
						drpGoodsSort.DataSource = getSoStatus;
						drpGoodsSort.DataBind();
						drpGoodsSort.Items.Insert(0, new ListItem("-请选择商品类型-"));
					 
					}
					if (!string.IsNullOrEmpty(Request["upid"]))
					{
						if (SearchDataClass.IsNumber(Request["upid"]) == true)
							this.btnUpdateGroupBuy.Attributes["onclick"] = "return GroupBuyValidate()";
						this.btnReset.Attributes["onclick"] = "return ClearGroupBuy()";
						this.BindShowUpInfo(Request["upid"]);
						this.btnAddGroupBuy.Visible = false; this.btnUpdateGroupBuy.Visible = true;
					}
					else
					{
						this.btnAddGroupBuy.Attributes["onclick"] = "return GroupBuyValidate()";
						this.btnReset.Attributes["onclick"] = "return ClearGroupBuy()";
						this.btnAddGroupBuy.Visible = true; this.btnUpdateGroupBuy.Visible = false;
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
				if (SearchDataClass.IsNumber(strUpid) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						goodstuan getOrderBy = db.goodstuan.Single(x => x.Id == int.Parse(strUpid));
						txtTitle.Value = getOrderBy.title;
						txtFckContent.Value = SearchDataClass.CheckStr(getOrderBy.content);
						txtTimeSelect.Value = Convert.ToDateTime(getOrderBy.starttime).ToString("yyyy/MM/dd") + "-" + Convert.ToDateTime(getOrderBy.endtime).ToString("yyyy/MM/dd");
						drpValidate.SelectedValue = getOrderBy.validate.ToString();
						drpSetIndex.SelectedValue = getOrderBy.setindex.ToString();
						txtQuantily.Value = getOrderBy.quantity.ToString();
                        txtGoodsPrice.Value = Convert.ToString(getOrderBy.price);
						txtGoodsCost.Value = Convert.ToString(getOrderBy.costprice.ToString());
						txtWeight.Value = getOrderBy.weight.ToString();
					//	drpCommunity.SelectedValue = getOrderBy.community.ToString();
						//drpShop.SelectedValue = getOrderBy.shopid.ToString();
						txtGroupNum.Value = getOrderBy.batch.ToString();
						txtSummary.Value = getOrderBy.tuansummary;
						txtSummary1.Value = getOrderBy.tuansummary1;
						txtSummary2.Value = getOrderBy.tuansummary2;
						this.hfDel.Value = getOrderBy.img;
						this.HFurl.Value = getOrderBy.img;
						this.iShowPhoto.Src = getOrderBy.img == "" ? "assets/images/nophoto.gif" : getOrderBy.img; ;
						this.hfDel1.Value = getOrderBy.imgtwo;
						this.HFurl1.Value = getOrderBy.imgtwo;
						this.iShowPhoto1.Src = getOrderBy.imgtwo == "" ? "assets/images/nophoto.gif" : getOrderBy.imgtwo;

						this.hfDel2.Value = getOrderBy.groupsortimg;
						this.HFurl2.Value = getOrderBy.groupsortimg;
						if (!string.IsNullOrEmpty(getOrderBy.groupsortimg))
							this.iShowPhoto2.Src = getOrderBy.groupsortimg;
						else
							this.iShowPhoto2.Src = "assets/images/nophoto.gif";

					drpGoodsSort.SelectedIndex= getOrderBy.groupsort;
                    drpSaleGoodsSort.SelectedValue = getOrderBy.selectytsort.ToString();
                    txtGoodsCode.Value = getOrderBy.goodscode;
						txtGetGoodPoint.Value = getOrderBy.getgoodpoint.ToString();
            db.SubmitChanges();
					}
			}
		}
		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/GroupBuyImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (!string.IsNullOrEmpty(fUpLoad.PostedFile.FileName))
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张团购图片不能超过4096K(4M)，请重新选择团购图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('团购图片修改成功。点击预览查看团购图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('团购图片上传成功。点击预览查看团购图片。');</script>");
					}
				}
			}
			else
			{
				Page.RegisterStartupScript("starup", "<script>alert('请选择团购商品图片上传');</script>");
			}

		}

		protected void btnAddGroupBuy_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				goodstuan addTuan = new goodstuan();
				addTuan.title = txtTitle.Value;
				addTuan.content = SearchDataClass.CheckStr(txtFckContent.Value);
				addTuan.starttime = Convert.ToDateTime(txtTimeSelect.Value.Split('-')[0]);
				var endtime = txtTimeSelect.Value.Split('-')[1] + " 23:59:59";
				addTuan.endtime = Convert.ToDateTime(endtime);
				addTuan.validate = Convert.ToChar(drpValidate.SelectedValue);
				addTuan.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
				addTuan.quantity = int.Parse(txtQuantily.Value);
                addTuan.price = Convert.ToDecimal(txtGoodsPrice.Value);
				addTuan.costprice = Convert.ToDecimal(txtGoodsCost.Value);
				addTuan.weight = txtWeight.Value.ToString();
				//addTuan.community = int.Parse(drpCommunity.SelectedValue);
				//addTuan.shopid = int.Parse(drpShop.SelectedValue);
				addTuan.batch = txtGroupNum.Value;
				addTuan.img = HFurl.Value;
				addTuan.imgtwo = HFurl1.Value;
				addTuan.groupsort = int.Parse(drpGoodsSort.SelectedValue);
				addTuan.groupsortimg = HFurl2.Value.Trim();
				addTuan.tuansummary = txtSummary.Value;
				addTuan.tuansummary1 = txtSummary1.Value;
				addTuan.tuansummary2 = txtSummary2.Value;
				addTuan.addtime = DateTime.Now;
                addTuan.selectytsort = Convert.ToChar(drpSaleGoodsSort.SelectedValue);
                addTuan.goodscode = txtGoodsCode.Value;
				addTuan.getgoodpoint = int.Parse(txtGetGoodPoint.Value);
        db.goodstuan.InsertOnSubmit(addTuan);
				db.SubmitChanges();
			}
            Response.Redirect("GroupBuyManager.aspx?mid="+ Request["mid"] + "&type="+Request["type"]);
		}

		protected void btnUpdateGroupBuy_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						goodstuan upTuan = db.goodstuan.Single(x => x.Id == int.Parse(Request["upid"]));
						upTuan.title = txtTitle.Value;
						upTuan.content = SearchDataClass.CheckStr(txtFckContent.Value);
						upTuan.starttime = Convert.ToDateTime(txtTimeSelect.Value.Split('-')[0]);
						var endtime = txtTimeSelect.Value.Split('-')[1] + " 23:59:59";
						upTuan.endtime = Convert.ToDateTime(endtime);
						upTuan.validate = Convert.ToChar(drpValidate.SelectedValue);
						upTuan.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
						upTuan.quantity = int.Parse(txtQuantily.Value);
                        upTuan.price = Convert.ToDecimal(txtGoodsPrice.Value);
						upTuan.costprice = Convert.ToDecimal(txtGoodsCost.Value);
						upTuan.weight = txtWeight.Value.ToString();
						//upTuan.community = int.Parse(drpCommunity.SelectedValue);
						//upTuan.shopid = int.Parse(drpShop.SelectedValue);
						upTuan.batch = txtGroupNum.Value;
						if (!string.IsNullOrEmpty(HFurl.Value))
						{
							upTuan.img = HFurl.Value.Trim();
						}
						if (!string.IsNullOrEmpty(HFurl1.Value))
						{ upTuan.imgtwo = HFurl1.Value.Trim(); }
						upTuan.groupsort = int.Parse( drpGoodsSort.SelectedValue);
						if (!string.IsNullOrEmpty(HFurl2.Value))
						{ upTuan.groupsortimg = HFurl2.Value.Trim(); }
						upTuan.tuansummary = txtSummary.Value;
						upTuan.tuansummary1 = txtSummary1.Value;
						upTuan.tuansummary2 = txtSummary2.Value;
                        upTuan.selectytsort = Convert.ToChar(drpSaleGoodsSort.SelectedValue);
                        upTuan.goodscode = txtGoodsCode.Value;
						upTuan.getgoodpoint = int.Parse(txtGetGoodPoint.Value);
						db.SubmitChanges();
					}
				Response.Redirect("GroupBuyManager.aspx?mid=" + Request["mid"] + "&type=" + Request["type"]);
			}
		}


	 
		protected void iUpLoad1_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/GroupBuyImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad1.PostedFile.FileName != "")
			{
				if (fUpLoad1.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张团购图片不能超过4096K(4M)，请重新选择团购图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel1.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
					SearchDataClass.	DeleteDir(Server.MapPath(hfDel1.Value));
						string imgname = fUpLoad1.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto1.Src = imgurl;
						fUpLoad1.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl1.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('团购图片修改成功。点击预览查看团购图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad1.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto1.Src = imgurl;
						fUpLoad1.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl1.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('团购图片上传成功。点击预览查看团购图片。');</script>");
					}
				}
			}

		}

		protected void iUpLoad2_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/GroupBuyImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad2.PostedFile.FileName != "")
			{
				if (fUpLoad2.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张团购图片不能超过4096K(4M)，请重新选择团购图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel2.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
					SearchDataClass.	DeleteDir(Server.MapPath(hfDel2.Value));
						string imgname = fUpLoad2.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto2.Src = imgurl;
						fUpLoad2.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl2.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('分类图片修改成功。点击预览查看分类图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad2.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GroupBuyImage/" + quanname;
						this.iShowPhoto2.Src = imgurl;
						fUpLoad2.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl2.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('分类图片上传成功。点击预览查看分类图片。');</script>");
					}
				}
			}
		}
	}
}