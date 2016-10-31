using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class MenuList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HttpCookie getCookies = Request.Cookies["UserLogin"];
				if (getCookies != null)
				{
					string[] role = getCookies["UserRole"].ToString().Split(',');
					//select * from menulist where menuid in(1,2,3,4,5) and fathermenuid='0' and isnull(isshow,'Y')='Y' order by sortid
					GetFaherMenuList(role, 0);
				}
				else
				{
					return;
				}
			}
		}
		public void GetFaherMenuList(string[] mid, int parentid)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				var getFaMenuList = //from p in db.menulist where p.fathermenuid==0 select p;
														from menulist in db.menulist
														where mid.Contains(menulist.menuid.ToString()) &&
														menulist.fathermenuid == parentid &&
														Convert.ToString((Convert.ToString(menulist.isshow) ?? "Y")) == "Y"
														orderby menulist.sortid
														select menulist;
				if (getFaMenuList.Count() > 0)
				{
					rpFatherMenu.DataSource = getFaMenuList;
					rpFatherMenu.DataBind();
				}
			}
		}

		protected void rpFatherMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Repeater rptProduct = (Repeater)e.Item.FindControl("rpSonMenu");

				//找到分类Repeater关联的数据项
				menulist rowv = (menulist)e.Item.DataItem;
				//提取分类ID
				int CategorieId = Convert.ToInt32(rowv.menuid);
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					HttpCookie getCookies = Request.Cookies["UserLogin"];
					if (getCookies != null)
					{
						string[] sonRole = getCookies["UserRole"].ToString().Split(',');
						var getSonMenuList = //from p in db.menulist where p.fathermenuid == CategorieId select p;
											from menulist in db.menulist
											where sonRole.Contains(menulist.menuid.ToString()) &&
											menulist.fathermenuid == CategorieId &&
											Convert.ToString((Convert.ToString(menulist.isshow) ?? "Y")) == "Y"
											orderby menulist.sortid
											select menulist;
						if (getSonMenuList.Count() > 0)
						{
							//根据分类ID查询该分类下的产品，并绑定产品Repeater
							rptProduct.DataSource = getSonMenuList;
							rptProduct.DataBind();
						}
						//else
						//    {
						//var getFaMenuList =	from menulist in db.menulist
						//                        where sonRole.Contains(menulist.menuid.ToString()) &&
						//                        Convert.ToString((Convert.ToString(menulist.isshow) ?? "Y")) == "Y"
						//                        select menulist.fathermenuid;
						//    }
					}
					else
					{
						return;
					}
				}
			}
		}
	}
}