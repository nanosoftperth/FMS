using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Business;
 

namespace FMS.ReportLogic
{
    public class ReportsGenerator
    { 

        public static FMS.Business.ReportGeneration.VehicleSpeedRetObj GetVehicleSpeedAndDistance_ForGraph(Guid vehicleID, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.timezoneToPerth();
            endDate = endDate.timezoneToPerth(); 
            FMS.Business.ReportGeneration.VehicleSpeedRetObj retobj = new FMS.Business.ReportGeneration.VehicleSpeedRetObj();
            List<DateTime> dateObjects = new List<DateTime>();
            if ((endDate > DateTime.Now))
            {
                endDate = DateTime.Now;
            }

            // get the vehicle
            FMS.Business.DataObjects.ApplicationVehicle vehicle = FMS.Business.DataObjects.ApplicationVehicle.GetForID(vehicleID);
            // get the data for speed
            object tagName = (vehicle.DeviceID + "_speed");

           // DateTime lastGoodValue = FMS.Business.SingletonAccess.HistorianServer.PIPoints(tagName).Data.Snapshot().TimeStamp.LocalDate;

            PITimeServer.PITime pitStart = new PITimeServer.PITime();
  
            //// With...
            //LocalDate = startDate;
            //PITimeServer.PITime pitEnd = new PITimeServer.PITime();;
            //// With...
            //LocalDate = lastGoodValue;
            //PISDK.PIValues pivs = SingletonAccess.HistorianServer.PIPoints(tagName).Data.InterpolatedValues(pitStart, pitEnd, 75);
            //// .Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btOutside)
            //// .Data.RecordedValues(pitStart, pitEnd)
            //retobj.SpeedVals = TimeSeriesFloat.getForSpeedVals(pivs, false, true);
            //// GET THE DATA FOR DISTANCE
            //string tagnameDist = (vehicle.DeviceID + "_DistanceSinceLastVal");
            //bool firstiteration = true;
            //PISDK.PIValue prevVal = null;
            //List<TimeSeriesFloat> distVals = new List<TimeSeriesFloat>();
            //PISDK.PIPoint pit = SingletonAccess.HistorianServer.PIPoints(tagnameDist);
            //// Dim totalizer As Decimal = 0
            //PITimeServer.PITime startpitime = new PITimeServer.PITime();
            //PISDK.PIValues recordedvals = pit.Data.RecordedValues(pitStart, pitEnd);
            //distVals = TimeSeriesFloat.gettValsFromPIVals(recordedvals);
            //List<TimeSeriesFloat> retdistances = new List<TimeSeriesFloat>();
            //foreach (tsv in retobj.SpeedVals) {
            //    TimeSeriesFloat newval = new TimeSeriesFloat();
            //    // With...
            //    DateVal = tsv.DateVal;
            //    double newVal_Val = From;
            //    x;
            //    distVals;
            //    Where;
            //    ((x.DateVal <= tsv.DateVal) 
            //                & (x.Val >= 0.007));
            //    switch (x.Val) {
            //    }
            //    Sum;
            //    newval.Val = newVal_Val;
            //    retdistances.Add(newval);
            //    retobj.DistanceVals = retdistances;
            //    retobj.VehicleName = vehicle.Name;
            //    // i mean, this "should" be moved somewhere else 
            //    foreach (x in retobj.DistanceVals) {
            //        x.DateVal = x.DateVal.timezoneToClient;
            //    }

            //    foreach (x in retobj.Latitudes) {
            //        x.DateVal = x.DateVal.timezoneToClient;
            //    }

            //    foreach (x in retobj.Longitudes) {
            //        x.DateVal = x.DateVal.timezoneToClient;
            //    }

            //    foreach (x in retobj.SpeedVals) {
            //        x.DateVal = x.DateVal.timezoneToClient;
            //    }

            //    return retobj;
            //    ((VehicleSpeedRetObj)(GetVehicleSpeedAndDistance(((Guid)(vehicleID)), ((DateTime)(startDate)), ((DateTime)(endDate)))));
            //    VehicleSpeedRetObj retobj = new VehicleSpeedRetObj();
            //    List<DateTime> dateObjects = new List<DateTime>();
            //    if ((endDate > Now)) {
            //        endDate = Now;
            //    }

            //    // get the vehicle
            //    DataObjects.ApplicationVehicle vehicle = DataObjects.ApplicationVehicle.GetForID(vehicleID);
            //    retobj.VehicleName = vehicle.Name;
            //    // GET THE DATA FOR SPEED & DISTANCE
            //    object speedTagName = (vehicle.DeviceID + "_speed");
            //    string distTagName = (vehicle.DeviceID + "_Distance");
            //    string latTagName = (vehicle.DeviceID + "_lat");
            //    string longTagName = (vehicle.DeviceID + "_long");
            //    DateTime lastGoodValue = SingletonAccess.HistorianServer.PIPoints(speedTagName).Data.Snapshot().TimeStamp.LocalDate;
            //    if ((lastGoodValue > endDate)) {
            //        lastGoodValue = endDate;
            //    }

            //    PITimeServer.PITime pitStart = new PITimeServer.PITime();
            //    // With...
            //    LocalDate = startDate;
            //    PITimeServer.PITime pitEnd = new PITimeServer.PITime();
            //    // With...
            //    LocalDate = lastGoodValue;
            //    PISDK.PIValues speed_pivs = SingletonAccess.HistorianServer.PIPoints(speedTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside);
            //    PISDK.PIValues dist_pivs = SingletonAccess.HistorianServer.PIPoints(distTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside);
            //    PISDK.PIValues lat_pivs = SingletonAccess.HistorianServer.PIPoints(latTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside);
            //    PISDK.PIValues lng_pivs = SingletonAccess.HistorianServer.PIPoints(longTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside);
            //    retobj.SpeedVals = TimeSeriesFloat.getForSpeedVals(speed_pivs, false, true);
            //    // get the other value as dictionaries for speedy lookups
            //    Dictionary<DateTime, Decimal> DistanceValsDict = TimeSeriesFloat.gettValsFromPIValsAsDict(dist_pivs, false, true);
            //    Dictionary<DateTime, Decimal> LatitudesDict = TimeSeriesFloat.gettValsFromPIValsAsDict(lat_pivs, false, false);
            //    Dictionary<DateTime, Decimal> LongitudesDict = TimeSeriesFloat.gettValsFromPIValsAsDict(lng_pivs, false, false);
            //    for (int i = 0; (i 
            //                <= (retobj.SpeedVals.Count - 1)); i++) {
            //        TimeSeriesFloat curr_SpeedVal = retobj.SpeedVals(i);
            //        TimeSpanWithVals x = new TimeSpanWithVals();
            //        x.speed = curr_SpeedVal.Val;
            //        x.EndDate = curr_SpeedVal.DateVal;
            //        // get the lat and long values
            //        DistanceValsDict.ContainsKey(curr_SpeedVal.DateVal);
            //        DistanceValsDict.Item[curr_SpeedVal.DateVal];
            //        0;
            //        x.end_lat = LatitudesDict.Item[curr_SpeedVal.DateVal];
            //        x.end_long = LongitudesDict.Item[curr_SpeedVal.DateVal];
            //        if ((i > 0)) {
            //            TimeSeriesFloat prev_SpeedVal = retobj.SpeedVals((i - 1));
            //            x.StartDate = prev_SpeedVal.DateVal;
            //            x.start_lat = LatitudesDict.Item[prev_SpeedVal.DateVal];
            //            x.start_long = LongitudesDict.Item[prev_SpeedVal.DateVal];
            //        }

            //        retobj.TimeSpansWithVals.Add(x);
            //    }

            return retobj;
        }

    }
}


