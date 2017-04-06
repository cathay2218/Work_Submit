using System;
using System.Collections.Generic;
using Parser.Handler;
using Parser.Model;
using Parser.DataBase;

namespace UIConsole
{
    class Program
    {
        static void Main(string[] args)
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

            //Show Data from Self Variable
            //Output_Data(Receive);     
            
            //Read Data from DB Data Sender
            Output_Data(DB.DataBase_Read());

            Console.Write(@"請按Enter鍵繼續...");
            Console.ReadLine();
        }

        public static void Output_Data(List<Data_Repository> input)
        {
            Console.WriteLine(@"已解析{0}筆資料", input.Count);
            Console.WriteLine(@"站點名稱  所在縣市  資料發布單位  資料發布日期");
            Console.WriteLine(@"紫外線數值  站點緯度(Lat)  站點經度(Lon)");
            Console.WriteLine(@"==================================================");

            input.ForEach(temp =>
            {
                Console.WriteLine("{0,-5}{1,-5}{2,-7}{3}", temp._Parser_SiteName, temp._Parser_County, temp._Parser_PublishAgency, temp._Parser_PublishTime);
                Console.WriteLine("{0,-12}{1}\t{2}\n", temp._Parser_UVI, temp._Parser_WGS84Lat, temp._Parser_WGS84Lon);
            });
        }
    }
}
