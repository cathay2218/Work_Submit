using System.Collections.Generic;
using System.Web.Mvc;
using Parser.DataBase;
using Parser.Handler;
using Parser.Model;

namespace UIWeb.Controllers
{
    public class DataOutputController : Controller
    {
        // GET: DataOutput
        public ActionResult Index()
        {
            DataHandler XML = new DataHandler();
            DBInteractive DB = new DBInteractive();
            List<Data_Repository> Receive = new List<Data_Repository>();

            Receive = XML.Parser(@"http://opendata.epa.gov.tw/ws/Data/UV/?format=xml");

            DB.DataBase_Clear();

            foreach (Data_Repository temp in Receive)
            {
                DB.DataBase_Write(temp);
            }

            string message = string.Format(@"已解析{0}筆資料<br/>", Receive.Count);
            message += string.Format(@"站點名稱 / 所在縣市 / 資料發布單位 / 資料發布日期<br/>");
            message += string.Format(@"紫外線數值 / 站點緯度(Lat) / 站點經度(Lon)<br/>");
            message += string.Format(@"==================================================<br/>");

            Receive.ForEach(temp =>
            {
                message += string.Format("{0,-5}/ {1,-5}/ {2,-7}/ {3}<br/>", temp._Parser_SiteName, temp._Parser_County, temp._Parser_PublishAgency, temp._Parser_PublishTime);
                message += string.Format("{0,-12}/ {1}\t/ {2}<br/><br/>", temp._Parser_UVI, temp._Parser_WGS84Lat, temp._Parser_WGS84Lon);
            });

            return Content(message);
        }
    }
}