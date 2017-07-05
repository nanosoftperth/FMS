﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Business;
using System.Net;

namespace FMS.SimulatorService.Classes
{
    public class Worker
    {

        private static Topshelf.Logging.LogWriter logger = Topshelf.Logging.HostLogger.Get<Worker>();

        public static void DoWork()
        {


            logger.Info( "\nFLEET MANAGEMENT \"DEMO\" SIMULATOR \n\n\n");


            List<FMS.Business.DataObjects.SimulatorSetting> settings = FMS.Business.DataObjects.SimulatorSetting.GetAll();

            TimeSpan ts = TimeSpan.FromSeconds(2);

            List<TimeIterator> timeiterators = new List<TimeIterator>();

            

            foreach (FMS.Business.DataObjects.SimulatorSetting Setting in settings)
            {


                logger.Info(Setting);

                TimeIterator ti = new TimeIterator { settingObj = Setting };

                FMS.Business.DataObjects.Device sourceDevice = (from x in FMS.Business.DataObjects.Device.GetAllDevices()
                                                                where x.DeviceID == ti.settingObj.SourceDeviceID
                                                                select x).Single();


                DateTime st = ti.settingObj.StartTime;
                DateTime et = ti.settingObj.EndTime;


                while (st < et)
                {
                    LatLongDate ll = new LatLongDate();

                    ll.datetime = st;

                    var _with1 = sourceDevice.GetLatLongs(ll.datetime);
                    ll.latitude = _with1.Key;
                    ll.longitude = _with1.Value;

                    ti.latlongs.Add(ll);
                    st = st + ts;

                }

                timeiterators.Add(ti);

            }


            string baseURL = @"http://{0}.nanosoft.com.au:9000/api/dataaccess?truckid={1}&lat={2}&lng={3}&time={4}";


            while (true)
            {

                foreach (var x_loopVariable in timeiterators)
                {

                    var x = x_loopVariable;
                    //using(WebClient client = new WebClient()) {
                    //string s = client.DownloadString(url);
                    //}

                    using (WebClient client = new WebClient())
                    {

                        LatLongDate lld = x.GetNext();

                        string url = string.Format(baseURL, Properties.Settings.Default.destination_server
                                                            , x.settingObj.DestinationDeviceID
                                                            , lld.latitude
                                                            , lld.longitude
                                                            , DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss"));

                        logger.Debug(url);

                        string s = client.DownloadString(url);
                    }


                }

                System.Threading.Thread.Sleep(ts);
            }

        }


    }


    /// <summary>
    /// Starts at the beginning of the list and iterates through to the top 
    /// THEN, goes from the top to the bottom again, repeates indefinitley
    /// </summary>
    public class TimeIterator
    {

        public FMS.Business.DataObjects.SimulatorSetting settingObj { get; set; }

        public List<LatLongDate> latlongs { get; set; }

        public TimeIterator()
        {

            latlongs = new List<LatLongDate>();
        }

        /// <summary>
        /// The direction which the list iterates, this should be set to either +1 or -1
        /// </summary>
        public int direction { get; set; }
        public int currentIndex { get; set; }

        public LatLongDate GetNext()
        {

            int maxIndex = latlongs.Count - 1;

            if (currentIndex == 0)
                direction = 1;
            if (currentIndex == maxIndex)
                direction = -1;

            currentIndex = currentIndex + direction;

            //get the  next index
            return latlongs[currentIndex];

        }

    }


    public class LatLongDate
    {

        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public DateTime datetime { get; set; }


        public LatLongDate()
        {
        }

    }
}