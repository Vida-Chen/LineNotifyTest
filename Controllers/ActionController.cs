using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiHelper;

namespace LineNotifyTest.Controllers
{
	[RoutePrefix("api/action")]
	public class ActionController : ApiController
	{
		private string lineNotifyUrl = "https://notify-api.line.me/api/notify";
		private string lineToken = "xxx";//dev_triggerlinetest
		private string openWeatherDataAuth = "xxx";//取得中央氣象局open data必先申請的授權碼

		//// GET: api/Action
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		//// GET: api/Action/5
		//public string Get(int id)
		//{
		//	return "value";
		//}

		//// POST: api/Action
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT: api/Action/5
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE: api/Action/5
		//public void Delete(int id)
		//{
		//}

		/// <summary>
		/// 取得台中市36小時內天氣資料, 並以Line notify發送訊息至Line群組
		/// </summary>
		/// <param name="msgTitle">輸入Json：{"msgTitle":"XXX"}, 可加入顯示Line通知訊息</param>
		/// <returns>ExecuteResult - 是否成功/回傳自訂物件/執行訊息</returns>
		[HttpPost]
		[Route("triggerLine")]
		public ExecuteResult triggerLineNotifyTest(string msgTitle)
		{
			ExecuteResult executeResult = new ExecuteResult();
			//string imgurl = "";//圖片網址(選擇性)

			try
			{
				string content = "message=\r\n";
				//content += sendMsg + "\r\n";//發送的文字訊息內容
				content += getWeatherData(msgTitle);

				#region sample of request a https data
				//var url = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-051DF5E5-3338-434B-8794-738FBB6324E2&locationName=臺中市";//"https://www.moi.gov.tw/";
				//string results;

				//// 強制認為憑證都是通過的，特殊情況再使用
				////ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

				////error:要求已經中止: 無法建立 SSL/TLS 的安全通道。
				//ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

				//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				//request.AutomaticDecompression = DecompressionMethods.GZip;

				//// 加入憑證驗證
				//request.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate());

				//HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
				//using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
				//{
				//	results = sr.ReadToEnd();
				//	sr.Close();
				//}
				#endregion

				#region line notify
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(lineNotifyUrl);
				request.Method = "POST";
				request.KeepAlive = true; //是否保持連線
				request.ContentType = "application/x-www-form-urlencoded";
				//request.ContentType = "multipart/form-data";
				request.Headers.Set("Authorization", "Bearer " + lineToken);

				#region 傳送圖片(選擇性)
				//imgurl = tbxURL.Text.ToString();
				//content += "&imageThumbnail=" + imgurl;
				//content += "&imageFullsize=" + imgurl;
				#endregion

				#region 貼圖
				//content += "&stickerPackageId=2";
				//content += "&stickerId=512";
				#endregion

				//Parameters - Use stream to write content to webrequest
				byte[] bytes = Encoding.UTF8.GetBytes(content);
				using (var stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}

				var response = (HttpWebResponse)request.GetResponse();
				#endregion

				executeResult.IsSuccess = true;
			}
			catch (Exception e)
			{
				executeResult.Msg = e.Message;
				executeResult.IsSuccess = false;
			}
			return executeResult;
		}

		/// <summary>
		/// 取得台中市36小時內天氣資料, 並以Line notify發送訊息至Line群組(No message title)
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route("triggerLineWithNoTitle")]
		public ExecuteResult triggerLineNotifyTest()
		{
			ExecuteResult executeResult = new ExecuteResult();
			//string imgurl = "";//圖片網址(選擇性)

			try
			{
				string content = "message=\r\n";
				content += getWeatherData("");

				#region sample of request a https data
				//var url = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-051DF5E5-3338-434B-8794-738FBB6324E2&locationName=臺中市";//"https://www.moi.gov.tw/";
				//string results;

				//// 強制認為憑證都是通過的，特殊情況再使用
				////ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

				////error:要求已經中止: 無法建立 SSL/TLS 的安全通道。
				//ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

				//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				//request.AutomaticDecompression = DecompressionMethods.GZip;

				//// 加入憑證驗證
				//request.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate());

				//HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
				//using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
				//{
				//	results = sr.ReadToEnd();
				//	sr.Close();
				//}
				#endregion

				#region line notify
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(lineNotifyUrl);
				request.Method = "POST";
				request.KeepAlive = true; //是否保持連線
				request.ContentType = "application/x-www-form-urlencoded";
				//request.ContentType = "multipart/form-data";
				request.Headers.Set("Authorization", "Bearer " + lineToken);

				#region 傳送圖片(選擇性)
				//imgurl = tbxURL.Text.ToString();
				//content += "&imageThumbnail=" + imgurl;
				//content += "&imageFullsize=" + imgurl;
				#endregion

				#region 貼圖
				//content += "&stickerPackageId=2";
				//content += "&stickerId=512";
				#endregion

				//Parameters - Use stream to write content to webrequest
				byte[] bytes = Encoding.UTF8.GetBytes(content);
				using (var stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}

				var response = (HttpWebResponse)request.GetResponse();
				#endregion

				executeResult.IsSuccess = true;
			}
			catch (Exception e)
			{
				executeResult.Msg = e.Message;
				executeResult.IsSuccess = false;
			}
			return executeResult;
		}

		private string getWeatherData(string msgTitle)
		{
			StringBuilder msg = new StringBuilder();
			msg.Append(msgTitle).AppendLine();

			string uri = "https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=" + openWeatherDataAuth + "&locationName=臺中市";

			//error:要求已經中止: 無法建立 SSL/TLS 的安全通道, 這兩行要寫在建立連線之前。
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };// 強制認為憑證都是通過的，特殊情況再使用
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri); //request請求
			req.Timeout = 10000; //request逾時時間
			req.Method = "GET"; //request方式(default:GET)

			string result = string.Empty;
			HttpWebResponse respone = (HttpWebResponse)req.GetResponse(); //接收respone
			using (StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8))
			{ //讀取respone資料
				result = streamReader.ReadToEnd(); //讀取到最後一行
												   //respone.Close();
												   //streamReader.Close();
			}
			JObject jsondata = JsonConvert.DeserializeObject<JObject>(result); //將資料轉為json物件
																			   //return (JArray)jsondata["records"]["location"]; //回傳json陣列
			foreach (JObject data in (JArray)jsondata["records"]["location"])
			{
				string loactionname = (string)data["locationName"]; //地名
				string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //天氣狀況
				string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //降雨機率
				string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //最低溫度
				string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //最高溫度
																													//msg.Append("%25").AppendLine();//%、#、@、&...etc在URI中為特殊保留字元; %25才是代表%, 或者以Uri.EscapeDataString(c#寫法)、encodeURIComponent(javascript寫法) 進行另外處理
				msg.Append(loactionname).Append(" 天氣:").Append(weathdescrible).Append(" 溫度:").Append(mintemperature).Append("°c-").Append(maxtemperature).Append("°c 降雨機率:").Append(pop).Append(Uri.EscapeDataString("%"));
				msg.AppendLine();
			}

			return msg.ToString();
		}
	}
}
