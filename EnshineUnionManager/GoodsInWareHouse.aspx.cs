using EnshineUnionManager.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
    public partial class GoodsInWareHouse : System.Web.UI.Page
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
                        drpGoodsSort.Items.Insert(0, new ListItem("商品分类"));
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
                }

            }

        }
    }
}