using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common
{
    public static class Json
    {
        /// <summary>
        /// DataTable转json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string DateTableToJson(DataTable dt,string[] arr) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("{");
                for (int j = 0; j < arr.Length; j++)
                {
                    sb.Append("\"" + arr[j] + "\":\"" + dt.Rows[i][arr[j]].ToString() + "\"");
                    if (j != arr.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");
                if (i != dt.Rows.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("],\"maxsize\":0,\"uptime\":\""+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"\"");
            sb.Append("}");
            return sb.ToString();
            //return "{\"list\":[{\"id\":\"9878\",\"title\":\"是的哇的是11111111111\",\"source\":\"sdsd\",\"imgmode\":\"1\",\"images\":\"news/201405/121725308994.jpg,\",\"uptime\":\"aaa\"},{\"id\":\"9877\",\"title\":\"ddddddddd\",\"source\":\"ss\",\"imgmode\":\"3\",\"images\":\"news/201405/121629216426.jpg,\",\"uptime\":\"sa\"}],\"maxsize\":0,\"uptime\":\"2014-05-12 21:50:35\"}";
        }
        public static string DateTableToJson(DataTable dt, string[] arr, string other)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("{");
                for (int j = 0; j < arr.Length; j++)
                {
                    sb.Append("\"" + arr[j] + "\":\"" + dt.Rows[i][arr[j]].ToString().Trim() + "\"");
                    if (j != arr.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");
                if (i != dt.Rows.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("],\"maxsize\":0,\"uptime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\"");
            sb.Append("," + other);
            sb.Append("}");
            return sb.ToString();
            //return "{\"list\":[{\"id\":\"9878\",\"title\":\"是的哇的是11111111111\",\"source\":\"sdsd\",\"imgmode\":\"1\",\"images\":\"news/201405/121725308994.jpg,\",\"uptime\":\"aaa\"},{\"id\":\"9877\",\"title\":\"ddddddddd\",\"source\":\"ss\",\"imgmode\":\"3\",\"images\":\"news/201405/121629216426.jpg,\",\"uptime\":\"sa\"}],\"maxsize\":0,\"uptime\":\"2014-05-12 21:50:35\"}";
        }

        public static string DateTableToJson(DataTable dt, string[] arr, string other,string top)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"list\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("{");
                for (int j = 0; j < arr.Length; j++)
                {
                    sb.Append("\"" + arr[j] + "\":\"" + dt.Rows[i][arr[j]].ToString().Trim() + "\"");
                    if (j != arr.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");
                if (i != dt.Rows.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append("],\"maxsize\":0,\"uptime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\"");
            sb.Append("," + other);
            if (dt.Rows.Count >= int.Parse(top))
            {
                sb.Append(",\"pager\":1");
            }
            else
            {
                sb.Append(",\"pager\":0");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public static string DateTableToJson(List<DataTable> dts, string[] arr, int count,string top)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int z = 1; z <= count; z++)
            {
                sb.Append("\"list" + z + "\":[");
                for (int i = 0; i < dts[z-1].Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int j = 0; j < arr.Length; j++)
                    {
                        sb.Append("\"" + arr[j] + "\":\"" + dts[z-1].Rows[i][arr[j]].ToString().Replace("\"","'") + "\"");
                        if (j != arr.Length - 1)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append("}");
                    if (i != dts[z-1].Rows.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");
                if (dts[z - 1].Rows.Count > 0)
                {
                    if (dts[z - 1].Rows.Count >= int.Parse(top))
                    {
                        sb.Append(",\"pager" + z + "\":1");
                    }
                    else
                    {
                        sb.Append(",\"pager" + z + "\":0");
                    }
                }
                
                if (z != count)
                {
                    sb.Append(",");
                }
            }
            sb.Append(",\"maxsize\":0,\"uptime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\"");
            sb.Append("}");
            return sb.ToString();
            //return "{\"list\":[{\"id\":\"9878\",\"title\":\"是的哇的是11111111111\",\"source\":\"sdsd\",\"imgmode\":\"1\",\"images\":\"news/201405/121725308994.jpg,\",\"uptime\":\"aaa\"},{\"id\":\"9877\",\"title\":\"ddddddddd\",\"source\":\"ss\",\"imgmode\":\"3\",\"images\":\"news/201405/121629216426.jpg,\",\"uptime\":\"sa\"}],\"maxsize\":0,\"uptime\":\"2014-05-12 21:50:35\"}";
        }
    }
}
