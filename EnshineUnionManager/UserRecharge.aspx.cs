using Aspose.Cells;
using EnshineUnionManager.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class UserRecharge : System.Web.UI.Page
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
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
                }
            }
        }

		protected void btnExportExcel_ServerClick(object sender, EventArgs e)
		{
			HttpCookie getCookies = Request.Cookies["UserLogin"];
			if (getCookies != null)
			{
				var timeSearch = string.Empty;
				if (string.IsNullOrEmpty(txtTimeSelect.Value.Trim()))
				{
					var dtBegin = DateTime.Now.ToString("yyyy/MM/dd");
					var dtEnd = DateTime.Now.ToString("yyyy/MM/dd");
					timeSearch = dtBegin + " - " + dtEnd;
				}
				else
				{
					timeSearch = txtTimeSelect.Value.Trim();
				}
				var getBadProData = SearchDataClass.GetSearchUserRechargeInfoData(txtRechargeTel.Value.Trim(),drpRechargeStatus.SelectedValue,timeSearch);


				ArrayList ColTitle = new ArrayList()
						 { "充值流水号", "充值日期", "充值手机", "充值人", "充值金额",
							"充值名称","充值状态"  };

				//string[] strTitle = new string[] { "ASNNo", "SKU", "SKUDescrC", "ExpectedQty", "ReceivedQty", "UOM", "ReceivingLocation", "ReceivedTime", "CustomerID", "CodeName_C" };
				if (getBadProData.ToList().Count > 0)
				{
					Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
					//创建一个sheet
					Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
					//为单元格添加样式      
					Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
					style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
					style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 左边界线
					style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 右边界线
					style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 上边界线
					style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 下边界线 

					//给各列的标题行PutValue赋值
					int currow = 0;
					byte curcol = 0;																														 
					sheet.Cells.ImportCustomObjects((System.Collections.ICollection)getBadProData,
					null, true, 0, 0, getBadProData.Count, true, "yyyy/MM/dd HH:mm:ss", false);
					// 设置内容样式
					for (int i = 0; i < getBadProData.ToList().Count; i++)
					{
						sheet.Cells[i + 1, 0].PutValue(getBadProData[i].RecNo);
						sheet.Cells[i + 1, 1].PutValue(getBadProData[i].RecTime);
						sheet.Cells[i + 1, 2].PutValue(getBadProData[i].Tel);
						sheet.Cells[i + 1, 3].PutValue(getBadProData[i].UserName);
						sheet.Cells[i + 1, 4].PutValue(getBadProData[i].RecMoeny);
						var recstatus = string.Empty;
						var successinfo = string.Empty;
						switch (getBadProData[i].RecStatus)
						{
							case 'N':
								recstatus = "交易失败"; successinfo = "充值失败";
								break;
							case 'Y':
								recstatus = "交易成功";
								successinfo = "充值成功";
								break;
							//case 'S':
							//	recstatus = "分红成功";
							//	successinfo = "充值成功";
							//	break;
							case 'C':
								recstatus = "撤销充值";
								successinfo = "撤销充值成功";
								break;
						}
						//if (getBadProData[i].RecStatus == 'N')
						//{
						//	recstatus = "交易失败";
						//	successinfo = "充值失败";
						//}
						//else
						//{
						//	recstatus = "交易成功";
						//	successinfo = "充值成功";
						//}
						sheet.Cells[i + 1, 5].PutValue(getBadProData[i].RecContent == "" ? "东方柏农-" + getBadProData[i].RecTime.ToString("yyyy.MM.dd") + "-" + successinfo : getBadProData[i].RecContent);

						sheet.Cells[i + 1, 6].PutValue(recstatus);
						for (int j = 0; j < 7; j++)
						{
							sheet.Cells[i + 1, j].Style = style;
							sheet.Cells[i + 1, 1].Style.Custom = "yyyy/MM/dd HH:mm:ss";
							//sheet.Cells[i + 1, 5].Style.Custom = "yyyy/MM/dd HH:mm:ss";
						}
					}
					// 设置标题样式及背景色
					foreach (string s in ColTitle)
					{
						sheet.Cells[currow, curcol].PutValue(s);
						style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
						style.Pattern = Aspose.Cells.BackgroundType.Solid;
						style.Font.IsBold = true;
						sheet.Cells[currow, curcol].Style = style;
						curcol++;
					}

					Aspose.Cells.Cells cells = sheet.Cells;
					//设置标题行高
					cells.SetRowHeight(0, 30);
					//让各列自适应宽度
					sheet.AutoFitColumns();
					//生成数据流
					System.IO.MemoryStream ms = workbook.SaveToStream();
					byte[] bt = ms.ToArray();
					//客户端保存的文件名
					string fileName= "用户充值列表导出_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
					//以字符流的形式下载文件  
					//     Response.ContentType = "application/vnd.ms-excel";
					//通知浏览器下载文件而不是打开
					Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
					Response.BinaryWrite(bt);
					Response.Flush();
					Response.End();
				}
			}
		}

	}
}