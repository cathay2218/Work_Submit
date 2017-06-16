using System.Collections.Generic;
using System.Web.Mvc;
using Parser.DataBase;
using Parser.Handler;
using Parser.Model;
using System.Linq;

namespace UIWeb.Controllers
{
    public class DataOutputController : Controller
    {
        // GET: DataOutput
        public ActionResult Index()
        {
            List<Data_Repository> Receive = new List<Data_Repository>();
            DataHandler XML = new DataHandler();
            DBInteractive DB = new DBInteractive();
            
            Receive = XML.Parser(@"http://opendata.epa.gov.tw/ws/Data/UV/?format=xml");

            DB.DataBase_Clear();

            foreach (Data_Repository temp in Receive)
            {
                DB.DataBase_Write(temp);
            }

            ViewBag.Title = "顯示UV監控站資料";

            return View(Receive);
        }

        // GET: DataSearch
        public ActionResult DataSearch(string search = "")
        {
            List<Data_Repository> Receive = new List<Data_Repository>();
            DataHandler XML = new DataHandler();

            Receive = XML.Parser(@"http://opendata.epa.gov.tw/ws/Data/UV/?format=xml");

            if (!string.IsNullOrEmpty(search))
                Receive = Receive.Where(temp =>
                    temp._Parser_County.Contains(search) ||
                    temp._Parser_SiteName.Contains(search))
                    .ToList();

            ViewBag.Search = search;
            ViewBag.Title = "顯示UV監控站資料 - 搜尋結果";

            return View(Receive);
        }

        [HttpGet]
        public ActionResult DataDisplay(string id)
        {
            List<Data_Repository> Display = new List<Data_Repository>();
            DBInteractive DB = new DBInteractive();

            if (string.IsNullOrEmpty(id))
                Display = new List<Data_Repository>();
            else
                Display = DB.FindByID(id);

            ViewBag.Title = id + "站詳細資料";

            return View(Display);
        }
    }
}