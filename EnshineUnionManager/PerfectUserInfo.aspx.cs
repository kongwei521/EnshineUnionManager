using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnshineUnionManager
{
    public partial class PerfectUserInfo : System.Web.UI.Page
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
                    BindCityArea("0", seachprov);

                    using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                    {
                        var getShop = (from p in db.shopset select new { p.shopCode, p.shopname }).ToList();
                        drpSelectShop.DataTextField = "shopname";
                        drpSelectShop.DataValueField = "shopCode";
                        drpSelectShop.DataSource = getShop;
                        drpSelectShop.DataBind();
                        drpSelectShop.Items.Insert(0, new ListItem("-请选择门店-"));
                        var getMember= (from p in db.memberset select new { p.memberid, p.membername }).ToList();
                        drpHuiYuanJIbie.DataTextField = "membername";
                        drpHuiYuanJIbie.DataValueField = "memberid";
                        drpHuiYuanJIbie.DataSource = getMember;
                        drpHuiYuanJIbie.DataBind();
                        drpHuiYuanJIbie.Items.Insert(0, new ListItem("-请选择会员级别-"));
                    }
                    if (!string.IsNullOrEmpty(Request["upid"]))
                    {
                        if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
                            this.btnUpdateUserInfo.Attributes["onclick"] = "return UserInfoValidate()";
                        this.btnReset.Attributes["onclick"] = "return ClearUserInfo()";
                        BindCityArea("0", seachprov);
                        this.BindShowUpInfo(Request["upid"]);
                        btnAddUserInfo.Visible = false; btnUpdateUserInfo.Visible = true;

                    }
                    else
                    {						 //20160220 del
                        //txtInvitedCode.Value = EnshineUnionManager.model.SearchDataClass.GenerateRandomNumber(8, 10);
                        btnAddUserInfo.Visible = true; btnUpdateUserInfo.Visible = false;
                        this.btnAddUserInfo.Attributes["onclick"] = "return UserInfoValidate()";
                        this.btnReset.Attributes["onclick"] = "return ClearUserInfo()";
                        BindCityArea("0", seachprov);
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
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    UserInfo getNot = db.UserInfo.Single(x => x.Id == int.Parse(strUpid));
                    txtNickName.Value = getNot.nickname;
                    txtName.Value = getNot.name;
                    txtPass.Value = getNot.pass;
                    txtTel.Value = getNot.tel;
                    txtEmail.Value = getNot.email;
                    drpSex.SelectedValue = getNot.sex.ToString();
                    // txtAreacity.Value = getNot.areacity;
                    //var area = getNot.areacity.Split(' ');//20161031 原来存放是空格现在存放逗号
                    var area = getNot.areacity.Split(',');
                    if (area.Length > 1)
                    {
                        seachprov.Items.FindByText(area[0]).Selected = true;

                        BindCityArea(db.areas.Single(x => x.areaname == area[1]).parentid, seachcity);
                        seachcity.Items.FindByText(area[1]).Selected = true;
                        BindCityArea(db.areas.Single(x => x.areaname == area[1]).id, seachdistrict);
                        seachdistrict.Items.FindByText(area[2]).Selected = true;
                    }
                    else
                    {
                        var parentid = db.areas.Single(x => x.areaname.Contains(area[0])).parentid;
                        var getProv = db.areas.Single(x => x.id == parentid).areaname;
                        seachprov.Items.FindByText(getProv).Selected = true;

                        var getCity = db.areas.Single(x => x.areaname.Contains(area[0])).areaname;
                        BindCityArea(db.areas.Single(x => x.areaname.Contains(area[0])).parentid, seachcity);
                        seachcity.Items.FindByText(getCity).Selected = true;

                        BindCityArea(db.areas.Single(x => x.areaname.Contains(area[0])).id, seachdistrict);
                        //seachdistrict.Items.FindByText(area[2]).Selected = true;
                    }
                    txtAddress.Value = getNot.address;
                    //20160131 东方柏农需要手机号为验证码
                    //txtInvitedCode.Value = getNot.invitedcode;
                    //txtCardNo.Value = getNot.cardno;

                    //txtHomeNum.Value = getNot.homenum;
                    //txtHomeInfo.Value = getNot.homeinfo;
                    //txtPlantNum.Value = getNot.plantnum;
                    //txtPlantSort.Value = getNot.plantsort;
                    this.hfDel.Value = getNot.cardimg;
                    this.HFurl.Value = getNot.cardimg;
                    if (!string.IsNullOrEmpty(getNot.cardimg))
                    { this.iShowPhoto.Src = getNot.cardimg; }
                    else
                    {
                        this.iShowPhoto.Src = "assets/images/nophoto.gif";
                    }
                    txtInvitedCode.Value = getNot.invitedcode;
                    drpSelectShop.SelectedValue = getNot.shopset.ToString();

                    txtRemarks.Value = getNot.remarks;
                    txtPayPwd.Value = getNot.paypassword;
                   // txtHighstGudong.Value = getNot.highestgudong;
                    //20160301 add
                    drpJueSe.SelectedValue = getNot.juese;
                    txtJueSe.Value = getNot.juese;
                   // drpGuDongJibie.SelectedValue = getNot.gudongjibie;
                  //  txtGuDongJibie.Value = getNot.gudongjibie;
					 
										drpHuiYuanJIbie.SelectedIndex = getNot.usertype==null?0: getNot.usertype.Value;
                //    txtSuoZhanGuFen.Value = getNot.suozhangufen;
                   // txtXiangZhen.Value = getNot.xiangzhen;
                   // txtXiangCun.Value = getNot.xiangcun;
                //    txtShangJiGuDong.Value = getNot.shangjigudong;
                //    txtShangJiZhanZhang.Value = getNot.shangjizhanzhang;
                }
            }
        }
        protected void btnAddUserInfo_Click(object sender, EventArgs e)
		{
			using (EnshineUnionDataContext db = new EnshineUnionDataContext())
			{
				UserInfo adNot = new UserInfo();
				adNot.nickname = txtNickName.Value.Trim();
				adNot.name = txtName.Value.Trim(); ;
				adNot.tel = txtTel.Value.Trim(); ;
				adNot.email = txtEmail.Value.Trim(); ;
				adNot.sex = Convert.ToChar(drpSex.SelectedValue);
				adNot.address = txtAddress.Value.Trim(); ;
                //20161031 更新空格到逗号分割
//                adNot.areacity = seachprov.SelectedItem.Text + " " + seachcity.SelectedItem.Text + " " +
//seachdistrict.SelectedItem.Text;
                adNot.areacity = seachprov.SelectedItem.Text + "," + seachcity.SelectedItem.Text + "," +
seachdistrict.SelectedItem.Text;
				//txtAreacity.Value.Trim();
				adNot.pass = txtPass.Value.Trim(); ;
				adNot.invitedcode =txtInvitedCode.Value.Trim();
				//adNot.cardno = txtCardNo.Value;
				//adNot.plantnum = txtPlantNum.Value;
				//adNot.homenum = txtHomeNum.Value;
				//adNot.homeinfo = txtHomeInfo.Value;
				//adNot.plantsort = txtPlantSort.Value;
				adNot.cardimg = HFurl.Value;
				adNot.shopset =int.Parse( drpSelectShop.SelectedValue);
				adNot.remarks = txtRemarks.Value;
				adNot.paypassword = txtPayPwd.Value;
			//	adNot.highestgudong = txtHighstGudong.Value;
				adNot.housemoney = 0.00m;
				adNot.point = 0;
				var getUserID = from p in db.UserInfo where p.tel == txtInvitedCode.Value select p.Id;
				if (getUserID.Count() > 0)
				{
					foreach (var item in getUserID)
					{ adNot.fenxiaoid = item; }
				}
				//20160301 add

				adNot.juese=   txtJueSe.Value;
                //adNot.gudongjibie=  txtGuDongJibie.Value ;
                //adNot.suozhangufen=txtSuoZhanGuFen.Value ;
             //   adNot.xiangzhen=txtXiangZhen.Value;
              //  adNot.xiangcun= txtXiangCun.Value;
                //adNot.shangjigudong= hfShangJiGuDong.Value;;
               // adNot.shangjizhanzhang = hfShangJiZhanZhang.Value;
			 
				adNot.addtime = DateTime.Now;
				db.UserInfo.InsertOnSubmit(adNot);
				db.SubmitChanges();
			}
			Response.Redirect("UserinfoManager.aspx?mid="+Request["mid"]);
		}
        protected void btnUpdateUserInfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["upid"]))
            {
                if (EnshineUnionManager.model.SearchDataClass.IsNumber(Request["upid"]) == true)
                    using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                    {
                        UserInfo upNot = db.UserInfo.Single(x => x.Id == int.Parse(Request["upid"]));
                        upNot.nickname = txtNickName.Value.Trim();
                        upNot.name = txtName.Value.Trim();
                        upNot.tel = txtTel.Value.Trim();
                        upNot.email = txtEmail.Value.Trim();
                        upNot.sex = Convert.ToChar(drpSex.SelectedValue);
                        upNot.address = txtAddress.Value.Trim();
                        //20161031
                        //upNot.areacity = seachprov.SelectedItem.Text + " " + seachcity.SelectedItem.Text + " " +
                        //seachdistrict.SelectedItem.Text;//txtAreacity.Value.Trim();
                        upNot.areacity = seachprov.SelectedItem.Text + "," + seachcity.SelectedItem.Text + "," +
                        seachdistrict.SelectedItem.Text;
                        upNot.pass = txtPass.Value.Trim();
                        //upNot.cardno = txtCardNo.Value;
                        //upNot.plantnum = txtPlantNum.Value;
                        //upNot.homenum = txtHomeNum.Value;
                        upNot.invitedcode = txtInvitedCode.Value;
                     //   upNot.homeinfo = txtHomeInfo.Value;
                     //   upNot.plantsort = txtPlantSort.Value;
                        if (!string.IsNullOrEmpty(HFurl.Value))
                        { upNot.cardimg = HFurl.Value; }
                        upNot.shopset = int.Parse(drpSelectShop.SelectedValue);
                        upNot.remarks = txtRemarks.Value;
                        upNot.paypassword = txtPayPwd.Value;
                      //  upNot.highestgudong = txtHighstGudong.Value;

						var getUserID = from p in db.UserInfo where p.tel == txtInvitedCode.Value select p.Id;
						if (getUserID.Count() > 0)
						{
							foreach (var item in getUserID)
							{ upNot.fenxiaoid = item; }
						}
						//20160301 add

						upNot.juese = txtJueSe.Value;
                      //  upNot.gudongjibie = txtGuDongJibie.Value;
                        upNot.usertype = drpHuiYuanJIbie.SelectedIndex;
                      //  upNot.suozhangufen = txtSuoZhanGuFen.Value;
                      //  upNot.xiangzhen = txtXiangZhen.Value;
                      //  upNot.xiangcun = txtXiangCun.Value;
					//	upNot.shangjigudong = hfShangJiGuDong.Value; ;
					//	upNot.shangjizhanzhang = hfShangJiZhanZhang.Value;

						db.SubmitChanges();
                    }
				Response.Redirect("UserinfoManager.aspx?mid=" + Request["mid"]);
			}
        }

        protected void seachprov_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = seachprov.SelectedItem.Value.Trim();
            if (pid != "0")
            {
                //初始化 市和县级列表
                //初始化市列表
                seachcity.Items.Clear(); /*seachdistrict.Items.Clear(); seachdistrict.Items.Insert(0, new ListItem("请选择"));*/
                using (EnshineUnionDataContext db = new EnshineUnionDataContext())
                {
                    var getCity = (from p in db.areas where p.parentid == pid select new { p.id, p.areaname }).ToList();
                    if (getCity.Count == 1)
                    {
                        seachcity.DataTextField = "areaname";
                        seachcity.DataValueField = "id";
                        seachcity.DataSource = getCity;
                        seachcity.DataBind(); seachcity.Items.Insert(0, new ListItem("请选择"));
                        //var getDis = (from p in db.areas where p.parentid == getCity[0].id select new { p.id, p.areaname }).ToList();
                        //seachdistrict.DataTextField = "areaname";
                        //seachdistrict.DataValueField = "id";
                        //seachdistrict.DataSource = getDis;
                        //seachdistrict.DataBind(); seachdistrict.Items.Insert(0, new ListItem("请选择"));
                        BindCityArea(getCity[0].id, seachdistrict);
                    }
                    else
                    {
                        seachcity.DataTextField = "areaname";
                        seachcity.DataValueField = "id";
                        seachcity.DataSource = getCity;
                        seachcity.DataBind(); seachcity.Items.Insert(0, new ListItem("请选择"));
                    }
                }
            }
        }

        protected void seachcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = seachcity.SelectedItem.Value.Trim();
            if (pid != "0")
            {
                BindCityArea(pid, seachdistrict);
            }
        }
        /// <summary>
        /// 共通引用部分 绑定省市区
        /// 	//初始化 市和县级列表
        ///初始化市列表
        /// </summary>
        /// <param name="codeid"></param>
        /// <param name="ddlArea"></param>
        public void BindCityArea(string codeid, DropDownList ddlArea)
        {
            ddlArea.Items.Clear();
            using (EnshineUnionDataContext db = new EnshineUnionDataContext())
            {
                var getDis = (from p in db.areas where p.parentid == codeid select new { p.id, p.areaname }).ToList();
                ddlArea.DataTextField = "areaname";
                ddlArea.DataValueField = "id";
                ddlArea.DataSource = getDis;
                ddlArea.DataBind(); ddlArea.Items.Insert(0, new ListItem("请选择"));
            }
        }

        /// <summary>
        /// 上传图片并显示出来/并保存到隐藏域路径.以待点击预览图片查看图片
        protected void iUpLoad_Click(object sender, EventArgs e)
        {
            string test = Server.MapPath("UpLoadImg/CardImage");  //用来生成文件夹
            if (!Directory.Exists(test))
            {
                Directory.CreateDirectory(test);
            }
            int filesize = 4096;
            if (fUpLoad.PostedFile.FileName != "")
            {
                if (fUpLoad.PostedFile.ContentLength / 4096 > filesize)
                {
                    Page.RegisterStartupScript("Startup", "<script>alert('单张头像图片不能超过4096K(4M)，请重新选择身份证图片上传。');</script>");
                }
                else
                {
                    if (!string.IsNullOrEmpty(hfDel.Value))//判断要修改图片路径是否为空，来进行添加还是删除文件夹的图片然后在上传图片
                    {
                        EnshineUnionManager.model.SearchDataClass.DeleteDir(Server.MapPath(hfDel.Value));
                        string imgname = fUpLoad.PostedFile.FileName;
                        string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
                        string quanname = Guid.NewGuid() + "." + imgType;
                        string imgurl = "UpLoadImg/CardImage/" + quanname;
                        this.iShowPhoto.Src = imgurl;
                        fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
                        this.HFurl.Value = imgurl;
                        Page.RegisterStartupScript("starup", "<script>alert(头像图片修改成功。点击预览查看身份证图片。');</script>");
                    }
                    else
                    {
                        string imgname = fUpLoad.PostedFile.FileName;
                        string imgType = imgname.Substring(imgname.LastIndexOf(".") + 1);
                        string quanname = Guid.NewGuid() + "." + imgType;
                        string imgurl = "UpLoadImg/CardImage/" + quanname;
                        this.iShowPhoto.Src = imgurl;
                        fUpLoad.PostedFile.SaveAs(Server.MapPath(imgurl));
                        this.HFurl.Value = imgurl;
                        Page.RegisterStartupScript("starup", "<script>alert('头像图片上传成功。点击预览查看身份证图片。');</script>");
                    }
                }
            }

        }

    }
}