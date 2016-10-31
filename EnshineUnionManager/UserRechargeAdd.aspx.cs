using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
	public partial class UserRechargeAdd : System.Web.UI.Page
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
					this.btnAddUserRecharge.Attributes["onclick"] = "return UserRechargeValidate()";
                    this.btnAddUserRPoint.Attributes["onclick"] = "return UserPointValidate()";
					this.btnReset.Attributes["onclick"] = "return ClearUserRecharge()";
					using (EnshineUnionDataContext db = new EnshineUnionDataContext())
					{

						var getMember = (from p in db.memberset select new { p.memberid, p.membername }).ToList();
						drpHuiYuanJIbie.DataTextField = "membername";
						drpHuiYuanJIbie.DataValueField = "memberid";
						drpHuiYuanJIbie.DataSource = getMember;
						drpHuiYuanJIbie.DataBind();
						drpHuiYuanJIbie.Items.Insert(0, new ListItem("-请选择会员级别-"));
					}
				}
				else
				{
					ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请登录在进行查询。');window.location.href='Login.aspx';</script>");
				}
			}
		}

        protected void btnAddUserRecharge_Click(object sender, EventArgs e)
        {
            //充值成功状态
            var result = false;
            var chongzhiprice = Convert.ToDecimal(txtRecMoney.Value.Trim());
            var chongzhipoint = int.Parse(txtPoint.Value);
            if (chongzhiprice != 0.00m)
            {
                //存放账号剩余金额
                decimal shengxiamoney = 0.0m;
                //积分
                int addpoint = 0;
                var recno = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    try
                    {
                        userrecharge addrec = new userrecharge();
                        addrec.recno = recno; //txtTitle.Value.Trim();
                        addrec.content = txtFckContent.Value;
                        addrec.tel = txtRecTel.Value.Trim();
                        if (hfuserid.Value == "NoData")
                            addrec.validate = 'N';
                        else
                        {
                            addrec.validate = 'Y';
                            addrec.userid = int.Parse(hfuserid.Value);
                        }
                        addrec.recmoney = Convert.ToDecimal(txtRecMoney.Value.Trim());

                        addrec.addtime = DateTime.Now;
                        db.userrecharge.InsertOnSubmit(addrec);
                        db.SubmitChanges();
                        if (chongzhipoint > 0)
                        {
                            //积分
                            pointdetails pointadd = new pointdetails();
                            pointadd.userid = int.Parse(hfuserid.Value);
                            pointadd.getpoint = int.Parse(txtPoint.Value);
                            pointadd.getpointreason = txtFckContent.Value;
                            pointadd.recno = recno;
                            pointadd.getpointtime = DateTime.Now;
                            db.pointdetails.InsertOnSubmit(pointadd);
                            db.SubmitChanges();
                        }
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                    finally
                    {
                        if (result)       //成功后更新余额
                        {
                            //检索剩余金额+刚充值金额				+积分
                            using (EnshineUnionDataContext dbup = new EnshineUnionDataContext())
                            {
                                //记录充值信息到收入明细中
                                ////moenydetails md = new moenydetails();
                                ////md.moneycode = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                                ////md.money = Convert.ToDecimal(txtRecMoney.Value.Trim());
                                ////md.moneyreason = txtFckContent.Value;
                                ////md.userid = int.Parse(hfuserid.Value);
                                ////md.moneytime = DateTime.Now;
                                ////md.recno = recno;
                                ////db.moenydetails.InsertOnSubmit(md);
                                ////db.SubmitChanges();

                                var usermoney = dbup.UserInfo.SingleOrDefault(x => x.Id == int.Parse(hfuserid.Value)).housemoney;
                                var userpoint = dbup.UserInfo.SingleOrDefault(x => x.Id == int.Parse(hfuserid.Value)).point;

                                UserInfo upinfo = dbup.UserInfo.Single(x => x.Id == int.Parse(hfuserid.Value));
                                shengxiamoney = Convert.ToDecimal(usermoney == null ? 0.0m : usermoney) + Convert.ToDecimal(txtRecMoney.Value.Trim());
                                upinfo.housemoney = shengxiamoney;
                                upinfo.usertype = drpHuiYuanJIbie.SelectedIndex;
															if (userpoint == null)
															{
																addpoint = 0;
															}
															else
															{
																addpoint = int.Parse(txtPoint.Value);
															}
															upinfo.point = addpoint;

                                dbup.SubmitChanges();
                            }
                        }
                    }
                }
                spMessage.InnerHtml = "手机号为：" + txtRecTel.Value + "充值成功，充值金额为" + txtRecMoney.Value + "元，账户余额为：" + shengxiamoney + "元，账户积分为：" + addpoint + "分,请等待5秒系统自动会自动跳转。<script>setTimeout(function(){window.location.href='UserRecharge.aspx';},5000);</script>";

                txtFckContent.Value = ""; txtRecTel.Value = ""; txtRecMoney.Value = "";
                hfuserid.Value = ""; txtPoint.Value = "";

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('充值金额不能为0');</script>");

            }
        }

        protected void btnAddUserRPoint_Click(object sender, EventArgs e)
        {
            //充值成功状态
            var result = false;
            var chongzhipoint = int.Parse(txtPoint.Value);
            if (chongzhipoint > 0)
            {
                //积分
                int addpoint = 0;
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    try
                    {
                        pointdetails pointadd = new pointdetails();
                        pointadd.userid = int.Parse(hfuserid.Value);
                        pointadd.getpoint = chongzhipoint;
                        pointadd.getpointreason = txtFckContent.Value;

                        pointadd.getpointtime = DateTime.Now;
                        db.pointdetails.InsertOnSubmit(pointadd);
                        db.SubmitChanges();

                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                    finally
                    {
                        if (result)       //成功后更新余额
                        {
                            //检索剩余金额+刚充值金额				+积分
                            using (EnshineUnionDataContext dbup = new EnshineUnionDataContext())
                            {
                                var userpoint = dbup.UserInfo.SingleOrDefault(x => x.Id == int.Parse(hfuserid.Value)).point;

                                UserInfo upinfo = dbup.UserInfo.Single(x => x.Id == int.Parse(hfuserid.Value));
                             
                                addpoint = int.Parse(userpoint.ToString()) + int.Parse(txtPoint.Value);
                                upinfo.point = addpoint;

                                dbup.SubmitChanges();
                            }
                        }
                    }
                }
                spMessage.InnerHtml = "手机号为：" + txtRecTel.Value + "充值积分成功，账户积分为：" + addpoint + "分,请等待5秒系统自动会自动跳转。<script>setTimeout(function(){window.location.href='UserRecharge.aspx';},5000);</script>";

                txtFckContent.Value = ""; txtRecTel.Value = ""; txtRecMoney.Value = "";
                hfuserid.Value = ""; txtPoint.Value = "";

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('充值积分不能为0');</script>");

            }
        }

	}
}