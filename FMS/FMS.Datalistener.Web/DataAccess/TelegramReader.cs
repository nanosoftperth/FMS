using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.Datalistener.Web.DataAccess
{
    public class Telegram
    {

        public string filename { get; set; }

        public string contentXML { get; set; }

        public string parsedDate
        {
            get
            {

                try
                {
                    int year = int.Parse(filename.Substring(0, 4));
                    int month = int.Parse(filename.Substring(4, 2));
                    int day = int.Parse(filename.Substring(6, 2));
                    int hour = int.Parse(filename.Substring(8, 2));
                    int minute = int.Parse(filename.Substring(10, 2));
                    int second = int.Parse(filename.Substring(12, 2));

                    DateTime parsedDate = new DateTime(year, month, day, hour, minute, second);

                    return parsedDate.ToString("dd/MMM/yyyy HH:mm:ss");
                }
                catch (Exception e)
                {

                    return e.Message;
                }
               
            }
        }

        public Telegram() { }

    }

    public class TelegramReader
    {

        public static List<Telegram> GetAllTelegramDefs()
        {
            List<string> fileLst = System.IO.Directory.GetFiles(
                                        Properties.Settings.Default.Filelocation, "*.log").ToList();

            List<Telegram> retLst = new List<Telegram>();

            foreach (string s in fileLst)
            {

                string fileName = s.Split((char)@"\"[0]).ToList().Last();

                string content = System.IO.File.ReadAllText(s);

                retLst.Add(new Telegram { filename = fileName, contentXML = content });
            }

            return retLst.OrderByDescending(x=> x.filename).ToList();
        }

        public TelegramReader() { }

    }
}