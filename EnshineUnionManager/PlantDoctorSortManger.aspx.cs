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
	public partial class PlantDoctorSortManger : System.Web.UI.Page
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
					//this.btnUpPlantDoctorSort.Attributes["onclick"] = "return AddPlantDoctorSortInfo();";

					//  this.btnAddPlantDoctorSort.Attributes["onclick"] = "return AddPlantDoctorSortInfo();";
					this.btnReset.Attributes["onclick"] = "return ClearPlantDoctorSort();";
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{

						var getSoStatus = from p in db.plantdoctorsort select new { p.sortId, p.sortName, p.sortFatherId, p.sortPath };
						if (getSoStatus.Count() <= 0)
						{
							plantdoctorsort insert = new plantdoctorsort { sortName = "无上级分类", sortFatherId = -1, sortPath = "-1", addTime = DateTime.Now };
							db.plantdoctorsort.InsertOnSubmit(insert);
							db.SubmitChanges();
						}
						DataTable dtSort = ToDataTable(getSoStatus);
						//添加根目录
						DataRow[] drs = dtSort.Select("sortFatherId=-1");
						for (var i = 0; i < drs.Length; i++)
						{
							string nodeid = drs[i]["sortId"].ToString();
							string text = drs[i]["sortName"].ToString();

							text = "╋" + text;
							this.drpPlantSort.Items.Add(new ListItem(text, nodeid));
							int sonparentid = int.Parse(nodeid);

							addOtherDll("", sonparentid, dtSort, 1, drpPlantSort);

						}


						drpPlantSort.DataBind();
						drpPlantSort.Items.Insert(0, new ListItem("-请选择植保类型-"));
					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}
			}
		}
		/// <param name="Pading">空格</param>
		/// <param name="DirId">父路径ID</param>
		/// <param name="datatable">返回的datatable</param>
		/// <param name="deep">树形的深度</param>
		private void addOtherDll(string Pading, int DirId, DataTable datatable, int deep, DropDownList ddl)
		{
			DataRow[] rowlist = datatable.Select("sortFatherId='" + DirId + "'");
			foreach (DataRow row in rowlist)
			{
				string strPading = "";
				for (int j = 0; j < deep; j++)
				{
					strPading += "　"; //用全角的空格
				}
				//添加节点
				ListItem li = new ListItem(strPading + "|--" + row["sortName"].ToString(), row["sortId"].ToString());
				ddl.Items.Add(li);
				//递归调用addOtherDll函数，在函数中把deep加1
				addOtherDll(strPading, Convert.ToInt32(row["sortId"]), datatable, deep + 1, ddl);
			}
		}


		/// <summary>
		///  下面通过一个方法来实现返回DataTable类型 
		/// LINQ返回DataTable类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="varlist"></param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
		{
			DataTable dtReturn = new DataTable();
			// column names 
			PropertyInfo[] oProps = null;
			if (varlist == null)
				return dtReturn;
			foreach (T rec in varlist)
			{
				if (oProps == null)
				{
					oProps = ((Type)rec.GetType()).GetProperties();
					foreach (PropertyInfo pi in oProps)
					{
						Type colType = pi.PropertyType;
						if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
								 == typeof(Nullable<>)))
						{
							colType = colType.GetGenericArguments()[0];
						}
						dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
					}
				}
				DataRow dr = dtReturn.NewRow();
				foreach (PropertyInfo pi in oProps)
				{
					dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
					(rec, null);
				}
				dtReturn.Rows.Add(dr);
			}
			return dtReturn;
		}

		protected void btnAddPlantDoctorSort_ServerClick(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				//取得选择父分类的sortpath以便更新新添加分类path
				var sortPath = db.plantdoctorsort.Single(x =>
								x.sortId == Convert.ToInt32(drpPlantSort.SelectedValue)).sortPath;
				//判断三级分类只能三级
				if (sortPath.Split(',').Length <= 3)
				{
					plantdoctorsort addOk = new plantdoctorsort();
					addOk.sortFatherId = int.Parse(drpPlantSort.SelectedValue);
					addOk.sortName = txtPlantDoctorSort.Value;
					//  addOk.sortSonId = int.Parse(drpPlantSort.SelectedValue);
					//获取信息
					addOk.sortImg = HFurl.Value;
					addOk.addTime = DateTime.Now;
					db.plantdoctorsort.InsertOnSubmit(addOk); db.SubmitChanges();
					//取得新添加分类ID
					var sortId = db.plantdoctorsort.Max(x => x.sortId);

					//获取选择父分类的sortSonId（更新时默认本ID）
					//var sortSonId=db.plantdoctorsort.Single(x =>
					//     x.sortId == Convert.ToInt32(drpPlantSort.SelectedValue)).sortSonId;
					plantdoctorsort upSonID = db.plantdoctorsort.Single(x => x.sortId == Convert.ToInt32(drpPlantSort.SelectedValue));
					upSonID.sortSonId = sortId;
					plantdoctorsort updateOK = db.plantdoctorsort.Single(x => x.sortId == Convert.ToInt32(sortId));
					if (!string.IsNullOrEmpty(sortPath) && sortPath != "-1")
						updateOK.sortPath = sortPath + "," + sortId;
					else
					{
						updateOK.sortPath = drpPlantSort.SelectedValue + "," + sortId;
					}
					db.SubmitChanges();
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能创建超过三级的植保分类,当前为第三级。');</script>");

				}
			}

			///	Response.Redirect("PlantDoctorSortManger.aspx");
		}

		protected void btnUpPlantDoctorSort_ServerClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(hfPlantDoctorSortId.Value))
			{
				using (EnshineUnionDataContext db = new EnshineUnionDataContext())
				{
					plantdoctorsort updateOK = db.plantdoctorsort.Single(x => x.sortId == Convert.ToInt32(hfPlantDoctorSortId.Value));
					updateOK.sortFatherId = int.Parse(drpPlantSort.SelectedValue);
					updateOK.sortName = txtPlantDoctorSort.Value;
					if (db.plantdoctorsort.Single(x =>
					 x.sortId == int.Parse(drpPlantSort.SelectedValue)).sortFatherId != int.Parse(drpPlantSort.SelectedValue))
					{
						updateOK.sortPath = db.plantdoctorsort.Single(x =>
						x.sortId == int.Parse(drpPlantSort.SelectedValue)).sortPath + "," + hfPlantDoctorSortId.Value;
					}
					if (!string.IsNullOrEmpty(HFurl.Value))
					{
						updateOK.sortImg = HFurl.Value;
					}
					db.SubmitChanges();
				}
			}
			ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('植保分类修改成功。');window.location.href='PlantDoctorSortManger.aspx';</script>");

		}


		/// <summary>
		/// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
		protected void iUpLoad_Click(object sender, EventArgs e)
		{
			string test = Server.MapPath("UpLoadImg/PlantDoctorSortImg");  //用来生成文件夹
			if (!Directory.Exists(test))
			{
				Directory.CreateDirectory(test);
			}
			int filesize = 4096;
			if (fUpLoad.PostedFile.FileName != "")
			{
				if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
				{
					Page.RegisterStartupScript("Startup", "<script>alert('单张植保分类图片不能超过4096K(4M)，请重新选择植保分类图片上传。');</script>");
				}
				else
				{
					if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
					{
						EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/PlantDoctorSortImg/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('植保分类图片修改成功。点击预览查看植保分类图片。');</script>");
					}
					else
					{
						string imgname = fUpLoad.PostedFile.FileName;
						string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
						string quanname = Guid.NewGuid() + "." + imgType;
						string imgurl = "UpLoadImg/PlantDoctorSortImg/" + quanname;
						this.iShowPhoto.Src = imgurl;
						fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
						this.HFurl.Value = imgurl;
						Page.RegisterStartupScript("starup", "<script>alert('植保分类图片上传成功。点击预览查看植保分类图片。');</script>");
					}
				}
			}

		}


	}

}