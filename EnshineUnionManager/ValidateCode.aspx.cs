using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace EnshineUnionManager
{
	public partial class ValidateCode : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string tmp = RndNum(5).ToUpper();
			this.Session["ImageV"] = tmp;
			HttpCookie a = new HttpCookie("ImageV", tmp);
			Response.Cookies.Add(a);
			this.CreateCheckCodeImage(tmp);
		}
		#region//创建验证码
		private void ValidateCodeCreate(string VNum)
		{
			Bitmap Img = null;
			Graphics g = null;
			MemoryStream ms = null;

			int gheight = VNum.Length * 10;
			Img = new Bitmap(gheight, 25);
			g = Graphics.FromImage(Img);
			//背景颜色 
			g.Clear(Color.White);
			//文字字体 
			Font f = new Font("Arial Black", 10);
			//文字颜色 
			SolidBrush s = new SolidBrush(Color.Black);
			g.DrawString(VNum, f, s, 3, 3);
			ms = new MemoryStream();
			Img.Save(ms, ImageFormat.Jpeg);
			Response.ClearContent();
			Response.ContentType = "image/Jpeg";
			Response.BinaryWrite(ms.ToArray());

			g.Dispose();
			Img.Dispose();
			Response.End();
		}
		#endregion
		#region//生验证码随即数字+字母组合
		private string RndNum(int VcodeNum)
		{
			string Vchar = "a,b,c,d,e,0,1,2,3,4,5,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,0,1,2,3,4,5,6,7,8,9,w,x,y,z,a,b,c,d,e,f,g,h,i,j,k,l,m,1,2,3,4,5,6,7,8,9,n,o,p,q,r,s,t,u,v,w,x,y,z";
			string[] VcArray = Vchar.Split(new Char[] { ',' });
			string VNum = "";
			int temp = -1;

			Random rand = new Random();

			for (int i = 1; i < VcodeNum + 1; i++)
			{
				if (temp != -1)
				{
					rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
				}

				int t = rand.Next(35);
				if (temp != -1 && temp == t)
				{
					return RndNum(VcodeNum);
				}
				temp = t;
				VNum += VcArray[t];
			}
			return VNum;
		}
		#endregion
		#region //创建验证码背景图片
		private void CreateCheckCodeImage(string checkCode)
		{
			if (checkCode == null || checkCode.Trim() == String.Empty)
				return;

			System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 13.0)), 21);
			Graphics g = Graphics.FromImage(image);

			try
			{
				//生成随机生成器

				Random random = new Random();

				//清空图片背景色

				g.Clear(Color.White);

				//画图片的背景噪音线

				for (int i = 0; i < 25; i++)
				{
					int x1 = random.Next(image.Width);
					int x2 = random.Next(image.Width);
					int y1 = random.Next(image.Height);
					int y2 = random.Next(image.Height);

					g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
				}

				Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
				System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
				g.DrawString(checkCode, font, brush, 2, 1);

				//画图片的前景噪音点

				for (int i = 0; i < 100; i++)
				{
					int x = random.Next(image.Width);
					int y = random.Next(image.Height);

					image.SetPixel(x, y, Color.FromArgb(random.Next()));
				}

				//画图片的边框线

				g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
				Response.ClearContent();
				Response.ContentType = "image/Gif";
				Response.BinaryWrite(ms.ToArray());
			}
			finally
			{
				g.Dispose();
				image.Dispose();
			}
		}
		#endregion
	}
}