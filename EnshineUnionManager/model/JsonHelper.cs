using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Reflection;
namespace EnshineUnionManager.model
{
	 /// <summary>
	 /// 注意约束，所转化涉及到的对象必须加上 [DataContractAttribute]  [DataMember(Name = "name")]
	 /// </summary>
	 public static class JsonHelper
	 {
		  public static string XMLSerialize<T>(T t)
		  {

			   using (StringWriter sw = new StringWriter())
			   {
					try
					{
						 XmlSerializer xz = new XmlSerializer(typeof(T));
						 xz.Serialize(sw, t);
						 return sw.ToString();
					}
					catch (Exception e)
					{
						 //LogHelper.Log(e.Message + e.StackTrace);
					}
					return "";
			   }
		  }


		  public static T XMLDeserialize<T>(T t, string s)
		  {


			   using (StringReader sr = new StringReader(s))
			   {
					try
					{
						 XmlSerializer xz = new XmlSerializer(typeof(T));
						 return (T)xz.Deserialize(sr);
					}
					catch (Exception e)
					{
						 //LogHelper.Log(e.Message + e.StackTrace);
					}

			   }
			   return default(T);
		  }



		  public static string ToJson(this object obj)
		  {
			   return NewtonsoftJson(obj);
		  }

		  public static string NewtonsoftJson(object obj)
		  {
			   return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
		  }
		  /// <summary>
		  /// 将对象转化成json
		  /// </summary>
		  /// <param name="obj"></param>
		  /// <returns></returns>
		  public static string ObjectToJson(object obj)
		  {
			   DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
			   using (MemoryStream ms = new MemoryStream())
			   {
					serializer.WriteObject(ms, obj);
					StringBuilder sb = new StringBuilder();
					sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
					string jsonString = sb.ToString();
					if (!jsonString.StartsWith("[") && !jsonString.EndsWith("]"))
					{
						 sb.Insert(0, "[");
						 sb.Append("]");
					}

					return sb.ToString();
			   }
		  }



		  /// <summary>
		  /// 将对象序列为Json数据格式
		  /// </summary>
		  /// <param name="obj"></param>
		  /// <returns></returns>
		  public static string Serialize<T>(T obj)
		  {
			   string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
			   return result;
		  }

		  /// <summary>
		  /// 将Json数据格式的字符串反序列化为一个对象
		  /// </summary>
		  /// <returns></returns>
		  public static T Deserialize<T>(string josnString)
		  {

			   try
			   {
					T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(josnString);
					return obj;

			   }
			   catch (Exception e)
			   {
					string s = e.Message;
					throw e;
			   }

		  }

		  /// <summary>
		  /// 将dataTable转换为json
		  /// </summary>
		  /// <param name="dt"></param>
		  /// <returns></returns>
		  public static string CreateJsonParameters(DataTable dt)
		  {
			   if (dt == null || dt.Rows.Count == 0)
			   {
					return string.Empty;
			   }

			   StringBuilder JsonString = new StringBuilder();
			   JsonString.Append("\"DataTable\":[ ");
			   for (int i = 0; i < dt.Rows.Count; i++)
			   {
					JsonString.Append("{ ");

					for (int j = 0; j < dt.Columns.Count; j++)
					{
						 if (j < dt.Columns.Count - 1)
						 {
							  JsonString.Append("\"" + dt.Columns[j].ColumnName + "\":" + "\"" + dt.Rows[i][j] + "\",");
						 }
						 else if (j == dt.Columns.Count - 1)
						 {
							  JsonString.Append("\"" + dt.Columns[j].ColumnName + "\":" + "\"" + dt.Rows[i][j] + "\"");
						 }
					}

					if (i == dt.Rows.Count - 1)
					{
						 JsonString.Append("} ");
					}
					else
					{
						 JsonString.Append("}, ");
					}
			   }

			   JsonString.Append("]");

			   return JsonString.ToString();
		  }

		  /// <summary>
		  /// 列名和数据都转换为json格式
		  /// </summary>
		  /// <param name="dt"></param>
		  /// <returns></returns>
		  public static string CreateJson(DataTable dt)
		  {
			   if (null == dt)
			   {
					return string.Empty;
			   }

			   StringBuilder JsonString = new StringBuilder();
			   JsonString.Append("\"Col\":[ ");
			   string coLCaption = string.Empty;
			   string colName = string.Empty;
			   string colInfo = string.Empty;
			   for (int i = 0; i < dt.Columns.Count; i++)
			   {
					DataColumn column = dt.Columns[i];
					colName = column.ColumnName;
					coLCaption = column.Caption;
					if (string.IsNullOrEmpty(coLCaption))
					{
						 coLCaption = colName;
					}

					if (i > 0)
					{
						 JsonString.Append(",");
					}

					colInfo = "{\"ColNo\":\"" + i + "\",\"ColName\":\"" + colName + "\",\"ColCaption\":\"" + coLCaption + "\",\"ColDataType\":\"" + column.DataType.Name + "\"}";
					JsonString.Append(colInfo);
			   }

			   JsonString.Append("]");

			   if (dt.Rows.Count > 0)
			   {
					JsonString.Append(",");
					JsonString.Append(CreateJsonParameters(dt));
			   }

			   return JsonString.ToString();
		  }
	 }


}