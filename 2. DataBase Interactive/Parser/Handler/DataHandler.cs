using Parser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Parser.Handler
{
    public class DataHandler
    {
        public List<Data_Repository> Parser(string Path)
        {
            //http://data.gov.tw/node/6076  ->  紫外線即時監測資料
            List<Data_Repository> ParserResult = new List<Data_Repository>();

            Console.WriteLine(@"Loading XML File...");
            XElement xml = XElement.Load(Path);

            Console.WriteLine("Analyze XML File...\n");
            IEnumerable<XElement> StationNode = xml.Descendants("Data");

            StationNode.ToList().ForEach(stationNode =>
            {
                string StationIdentifier = stationNode.Element("SiteName").Value.Trim();
                string UV_Value = stationNode.Element("UVI").Value.Trim();
                string PublishAgency = stationNode.Element("PublishAgency").Value.Trim();
                string County = stationNode.Element("County").Value.Trim();
                string WGS84Lon = stationNode.Element("WGS84Lon").Value.Trim();
                string WGS84Lat = stationNode.Element("WGS84Lat").Value.Trim();
                string RecordTime = stationNode.Element("PublishTime").Value.Trim();

                Data_Repository parser_repository = new Data_Repository();
                parser_repository._Parser_SiteName = StationIdentifier;
                parser_repository._Parser_UVI = UV_Value;
                parser_repository._Parser_PublishAgency = PublishAgency;
                parser_repository._Parser_County = County;
                parser_repository._Parser_WGS84Lon = WGS84Lon;
                parser_repository._Parser_WGS84Lat = WGS84Lat;
                parser_repository._Parser_PublishTime = RecordTime;

                ParserResult.Add(parser_repository);
            });

            return ParserResult;
        }
    }
}
