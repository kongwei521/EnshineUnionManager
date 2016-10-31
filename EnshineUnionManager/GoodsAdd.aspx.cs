using EnshineUnionManager.model;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class GoodsAdd : System.Web.UI.Page
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

						var getSoStatus = from p in db.goodssort select new { p.sortId, p.sortName, p.sortFatherId, p.sortPath };
						if (getSoStatus.Count() <= 0)
						{
							goodssort insert = new goodssort { sortName = "无上级分类", sortFatherId = -1, sortPath = "-1", addTime = DateTime.Now };
							db.goodssort.InsertOnSubmit(insert);
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
							this.drpGoodsSort.Items.Add(new ListItem(text, nodeid));
							int sonparentid = int.Parse(nodeid);

							SearchDataClass.addOtherDll("", sonparentid, dtSort, 1, drpGoodsSort);
						}
						drpGoodsSort.DataBind();
						drpGoodsSort.Items.Insert(0, new ListItem("-请选择商品类型-"));
						if (!string.IsNullOrEmpty(Request["upid"]))
						{
							if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
								this.btnUpdateGoods.Attributes["onclick"] = "return GoodsValidate();";
							this.btnReset.Attributes["onclick"] = "return ClearGoods();";
							this.BindShowUpInfo(Request["upid"]);
							this.btnAddGoods.Visible = false; this.btnUpdateGoods.Visible = true;
						}
						else
						{
							this.btnAddGoods.Attributes["onclick"] = "return GoodsValidate();";
							this.btnReset.Attributes["onclick"] = "return ClearGoods();";
							this.btnAddGoods.Visible = true; this.btnUpdateGoods.Visible = false;
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
						goods getNot = db.goods.Single(x => x.goodsId == int.Parse(strUpid));
						txtTitle.Value = getNot.goodstitle;
						txtFckContent.Value = SearchDataClass.CheckStr(getNot.goodscontent);
						drpValidate.SelectedValue = getNot.goodsvalidate.ToString();
						drpSetIndex.SelectedValue = getNot.setindex.ToString();
						this.drpGoodsSort.SelectedValue = getNot.goodssort.ToString();
						txtGoodsCode.Value = getNot.goodscode;
						txtGoodsPrice.Value = getNot.goodsprice.ToString(); ;
						this.hfDel.Value = getNot.goodsimg;
						this.HFurl.Value = getNot.goodsimg;
						if (!string.IsNullOrEmpty(getNot.goodsimg))
						{ this.iShowPhoto.Src = getNot.goodsimg; }
						else
						{
							this.iShowPhoto.Src = "assets/images/nophoto.gif";
						}
						txtStockNum.Value = getNot.goodstock.ToString();
						drpSales.SelectedValue = getNot.goodsSales.ToString();
						txtGoodsCost.Value = getNot.goodscost.ToString(); ;
						txtExchangePoint.Value = getNot.exchangepoint.ToString(); ;
						drpExchangeGood.SelectedValue = getNot.ifexchange.ToString();
						txtGetGoodPoint.Value = getNot.getgoodpoint.ToString();
                        //txtGoldPrice.Value = getNot.goodgoldprice.ToString();
                        //txtSilverprice.Value = getNot.goodsilverprice.ToString();
						//判断分成比例 0/20/20/10 
						var tichengsplit = getNot.tichengpoint.Split('/');
						decimal fencheng1 = (Convert.ToDecimal(getNot.goodsprice) - Convert.ToDecimal(getNot.goodscost)) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengsplit[1]) / Convert.ToDecimal(100);
						decimal fencheng2 = (Convert.ToDecimal(getNot.goodsprice) - Convert.ToDecimal(getNot.goodscost)) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengsplit[2]) / Convert.ToDecimal(100);
						decimal fencheng3 = (Convert.ToDecimal(getNot.goodsprice) - Convert.ToDecimal(getNot.goodscost)) * Convert.ToDecimal(75 / 100) * Convert.ToDecimal(tichengsplit[3]) / Convert.ToDecimal(100);

						if (tichengsplit.Length > 0)
						{
							if (int.Parse(tichengsplit[0]) == 0)
							{
								//sp1.InnerText = "分成比例20%=" + Convert.ToDecimal((getNot.goodsprice * (int.Parse(tichengsplit[1]) / 100))).ToString("N2") + "元";
								//sp2.InnerText = "分成比例20%=" + Convert.ToDecimal((getNot.goodsprice * (int.Parse(tichengsplit[2]) / 100))).ToString("N2") + "元";
								//sp3.InnerText = "分成比例10%=" + Convert.ToDecimal((getNot.goodsprice * (int.Parse(tichengsplit[3]) / 100))).ToString("N2") + "元";
								sp1.InnerText = "分成比例20%=" + Convert.ToDecimal(fencheng1).ToString("N2") + "元";
								sp2.InnerText = "分成比例20%=" + Convert.ToDecimal(fencheng2).ToString("N2") + "元";
								sp3.InnerText = "分成比例10%=" + Convert.ToDecimal(fencheng3).ToString("N2") + "元";

							}
							else
							{
								Text1.Value = tichengsplit[1];
								Text2.Value = tichengsplit[2]; Text3.Value = tichengsplit[3];
								//spz1.InnerText = "%=" + Convert.ToDecimal((getNot.goodsprice * (Convert.ToDecimal(tichengsplit[1]) / 100))).ToString("N2") + "元";
								//spz2.InnerText = "%=" + Convert.ToDecimal((getNot.goodsprice * (Convert.ToDecimal(tichengsplit[2]) / 100))).ToString("N2") + "元";
								//spz3.InnerText = "%=" + Convert.ToDecimal((getNot.goodsprice * (Convert.ToDecimal(tichengsplit[3]) / 100))).ToString("N2") + "元";
								spz1.InnerText = "%=" + Convert.ToDecimal(fencheng1).ToString("N2") + "元";
								spz2.InnerText = "%=" + Convert.ToDecimal(fencheng2).ToString("N2") + "元";
								spz3.InnerText = "%=" + Convert.ToDecimal(fencheng3).ToString("N2") + "元";

							}
						}
						drpXianGou.SelectedValue = getNot.ifxiangou.ToString();
						txtXianGouNumber.Value = getNot.xiangounumber.ToString();
						txtGoodsSpec.Value = getNot.goodsspec;
						txtGoodsCompany.Value = getNot.goodscompany;
						//drpGroupBy.SelectedValue = getNot.iftuangou.ToString();
						txtExpireDate.Value =Convert.ToDateTime( getNot.expiredate).ToString("yyyy/MM/dd");
                        drpSaleGoodsSort.SelectedValue = getNot.selectytsort.ToString();
					}
			}
		}
		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/GoodsImage");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (!string.IsNullOrEmpty(fUpLoad.PostedFile.FileName))
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张商品图片不能超过4096K(4M)，请重新选择商品图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GoodsImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('商品图片修改成功。点击预览查看商品图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/GoodsImage/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('商品图片上传成功。点击预览查看商品图片。');</script>");
					}
				}
			}
			else
			{
				Page.RegisterStartupScript("starup", "<script>alert('请选择商品图片上传');</script>");
			}

		}

		protected void btnAddGoods_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				goods addNot = new goods();
				addNot.goodstitle = txtTitle.Value.Trim();
				addNot.goodscontent = SearchDataClass.CheckStr(txtFckContent.Value);

				addNot.goodsvalidate = Convert.ToChar(drpValidate.SelectedValue);
				addNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
				addNot.goodscode = txtGoodsCode.Value;
				addNot.goodsprice = Convert.ToDecimal(txtGoodsPrice.Value);
				addNot.goodssort = int.Parse(drpGoodsSort.SelectedValue);
				addNot.goodsimg = HFurl.Value.Trim();
				addNot.addtime = DateTime.Now;

				addNot.goodstock = int.Parse(txtStockNum.Value);
				addNot.goodsSales = Convert.ToChar(drpSales.SelectedValue);
				addNot.getgoodpoint = txtGetGoodPoint.Value == "" ? 0 : int.Parse(txtGetGoodPoint.Value);

				addNot.goodscost = txtGoodsCost.Value == "" ? 0.00m : Convert.ToDecimal(txtGoodsCost.Value);
				addNot.ifexchange = Convert.ToChar(drpExchangeGood.SelectedValue);
				addNot.exchangepoint = txtExchangePoint.Value == "" ? 0 : int.Parse(txtExchangePoint.Value);
				addNot.tichengpoint = ticheng.Value == "" ? "0/20/20/10" : ticheng.Value;
				//addNot.goodgoldprice = txtGoldPrice.Value == "" ? 0.00m : Convert.ToDecimal(txtGoldPrice.Value);
				//addNot.goodsilverprice = txtSilverprice.Value == "" ? 0.00m : Convert.ToDecimal(txtSilverprice.Value);
				addNot.ifxiangou = Convert.ToChar(drpXianGou.SelectedValue);
				addNot.xiangounumber = txtXianGouNumber.Value == "" ? "0/0/0" : txtXianGouNumber.Value;
				addNot.goodsspec = txtGoodsSpec.Value;
				addNot.goodscompany = txtGoodsCompany.Value;
				//addNot.iftuangou = Convert.ToChar(drpGroupBy.SelectedValue);
				addNot.expiredate =Convert.ToDateTime( txtExpireDate.Value);
                addNot.selectytsort = Convert.ToChar(drpSaleGoodsSort.SelectedValue);
				db.goods.InsertOnSubmit(addNot);
				db.SubmitChanges();
			}
			Response.Redirect("GoodsManager.aspx?mid=" + Request["mid"] + "&type=" + Request["type"]);
 		}

		protected void btnUpdateGoods_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request["upid"]))
			{
				if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{
						goods upNot = db.goods.Single(x => x.goodsId == int.Parse(Request["upid"]));
						upNot.goodstitle = txtTitle.Value.Trim();
						upNot.goodscontent = SearchDataClass.CheckStr(txtFckContent.Value);

						upNot.goodsvalidate = Convert.ToChar(drpValidate.SelectedValue);
						upNot.setindex = Convert.ToChar(drpSetIndex.SelectedValue);
						upNot.goodscode = txtGoodsCode.Value;
						upNot.goodsprice = Convert.ToDecimal(txtGoodsPrice.Value);

						upNot.goodssort = int.Parse(drpGoodsSort.SelectedValue);
						if (!string.IsNullOrEmpty(HFurl.Value))
						{
							upNot.goodsimg = HFurl.Value.Trim();
						}
						upNot.goodstock = int.Parse(txtStockNum.Value);
						upNot.goodsSales = Convert.ToChar(drpSales.SelectedValue);
						upNot.goodscost = Convert.ToDecimal(txtGoodsCost.Value);
						upNot.ifexchange = Convert.ToChar(drpExchangeGood.SelectedValue);
						if (!string.IsNullOrEmpty(txtExchangePoint.Value))
						{ upNot.exchangepoint = int.Parse(txtExchangePoint.Value); }
						if (!string.IsNullOrEmpty(txtGetGoodPoint.Value))
						{ upNot.getgoodpoint = int.Parse(txtGetGoodPoint.Value); }
						upNot.tichengpoint = ticheng.Value == "" ? "0/20/20/10" : ticheng.Value;
						//upNot.goodgoldprice = Convert.ToDecimal(txtGoldPrice.Value);
						//upNot.goodsilverprice = Convert.ToDecimal(txtSilverprice.Value);
						upNot.ifxiangou = Convert.ToChar(drpXianGou.SelectedValue);
						upNot.xiangounumber = txtXianGouNumber.Value == "" ? "0/0/0" : txtXianGouNumber.Value; ;
						upNot.goodsspec = txtGoodsSpec.Value;
						upNot.goodscompany = txtGoodsCompany.Value;
						//upNot.iftuangou = Convert.ToChar(drpGroupBy.SelectedValue);
						upNot.expiredate = Convert.ToDateTime(txtExpireDate.Value);
                        upNot.selectytsort = Convert.ToChar(drpSaleGoodsSort.SelectedValue);
						db.SubmitChanges();
					}
				Response.Redirect("GoodsManager.aspx?mid=" + Request["mid"] + "&type=" + Request["type"]);
			}
		}

		protected void drpSales_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}