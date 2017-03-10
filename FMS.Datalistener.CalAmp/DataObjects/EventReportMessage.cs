using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class EventReportMessage : MessageBody
    {

        public DateTime Updatetime { get; set; }
        public DateTime TimeOfFix { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longtiude { get; set; }
        public decimal Altitude { get; set; }
        public decimal speed { get; set; }
        public int Heading { get; set; }
        public int Satellites { get; set; }
        public int Carrier { get; set; }

        public int RSSI { get; set; }

        public string CommState { get; set; }

        public int inputs { get; set; }

        public int HDOP { get; set; }

        public bool Ignition { get; set; }
        public bool INPUT_1 { get; set; }
        public bool INPUT_2 { get; set; }
        public bool INPUT_3 { get; set; }
        public bool INPUT_4 { get; set; }
        public bool INPUT_5 { get; set; }
        public bool INPUT_6 { get; set; }
        public bool INPUT_7 { get; set; }

        public string Unitstatus { get; set; }


        public int EventCode { get; set; }
        public int Accums { get; set; }
        public int Append { get; set; }
        public int EventIndex { get; set; }



        public GPS_Status FixStatus { get; set; }

        public EventReportMessage(byte[] b, int byteIndex)
        {
            this.bytes = b;
            this.currentByteIndex = byteIndex;

            this.FixStatus = new DataObjects.GPS_Status();

            //Update time
            int utcTimeSecs = BitHelper.Convert(b, ref byteIndex, 4);
            this.Updatetime = DateTime.Parse("01 jan 1970").AddSeconds(utcTimeSecs);


            //time of fix
            utcTimeSecs = BitHelper.Convert(b, ref byteIndex, 4);
            this.TimeOfFix = DateTime.Parse("01 jan 1970").AddSeconds(utcTimeSecs);

            //Lattitude
            decimal LatInDeg = BitHelper.Convert(b, ref byteIndex, 4);
            this.Lattitude = LatInDeg * ((decimal)Math.Pow(10, -7));

            //longitude
            decimal LongInDeg = BitHelper.Convert(b, ref byteIndex, 4);
            this.Longtiude = LongInDeg * ((decimal)Math.Pow(10, -7));

            //altitude 
            this.Altitude = ((decimal)BitHelper.Convert(b, ref byteIndex, 4)) / 100;

            //speed convert to km/h
            this.speed = (decimal)BitHelper.Convert(b, ref byteIndex, 4);
            this.speed = speed * 1000 * 100;
            this.speed = speed / (60 * 60);

            this.Heading = BitHelper.Convert(b, ref byteIndex, 2);
            this.Satellites = BitHelper.Convert(b, ref byteIndex, 1);


            //FIX Status for the GPS
            byte fixstatusByte = b[byteIndex];

            GPS_Status tempGPSStat = new GPS_Status();

            tempGPSStat.DifferentiallyCalcd = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.LastKnown = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.InvalidFix = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.TwoDFix = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.Historic = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.Invalidtime = BitHelper.bitwiseANDFromHex(fixstatusByte, 8);
            tempGPSStat.Everything_OK = (int)b[byteIndex] == 0;

            this.FixStatus = tempGPSStat;
            byteIndex++;


            //carrier
            this.Carrier = BitHelper.Convert(b, ref byteIndex, 2);

            //RSSI 
            this.RSSI = BitHelper.Convert(b, ref byteIndex, 2);

            string cState = string.Empty;
            //int commStateInt = BitHelper.Convert(b, ref byteIndex, 1);
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 1, "Available");
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 2, "Network Service");
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 3, "Data Service");
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 4, "Connected (PPP Session Up)");
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 5, "Voice call is active");
            BitHelper.GetBitMapString(ref cState, b[byteIndex], 6, "Roaming");

            byteIndex++;
            this.CommState = cState;

            this.HDOP = BitHelper.Convert(b, ref byteIndex, 1);

            //inputs
            this.Ignition = BitHelper.bitwiseANDFromHex(b[byteIndex], 8);
            this.INPUT_1 = BitHelper.bitwiseANDFromHex(b[byteIndex], 7);
            this.INPUT_2 = BitHelper.bitwiseANDFromHex(b[byteIndex], 6);
            this.INPUT_3 = BitHelper.bitwiseANDFromHex(b[byteIndex], 5);
            this.INPUT_4 = BitHelper.bitwiseANDFromHex(b[byteIndex], 4);
            this.INPUT_5 = BitHelper.bitwiseANDFromHex(b[byteIndex], 3);
            this.INPUT_6 = BitHelper.bitwiseANDFromHex(b[byteIndex], 2);
            this.INPUT_7 = BitHelper.bitwiseANDFromHex(b[byteIndex], 1);
            byteIndex++;

            //unit status
            string uStatus = string.Empty;

            BitHelper.GetBitMapString(ref uStatus, b[byteIndex], 1, "LMU32: HTTP OTA Update Status ERROR, LMU8: Unused");
            BitHelper.GetBitMapString(ref uStatus, b[byteIndex], 2, "GPS Antenna Status ERROR");
            BitHelper.GetBitMapString(ref uStatus, b[byteIndex], 3, "GPS Receiver Self-Test ERROR (LMU32 only)");
            BitHelper.GetBitMapString(ref uStatus, b[byteIndex], 4, "GPS Receiver Tracking FAIL");

            if (string.IsNullOrEmpty(uStatus)) uStatus = "OK";
            this.Unitstatus = uStatus;
            byteIndex++;

            //final few, ignore the end of the message if it includes anything else, not worth the time parsing as we 
            //are not going to use the "Accumlist" properites at time of writing.            
            this.EventIndex = BitHelper.Convert(b, ref byteIndex, 1);
            this.EventCode = BitHelper.Convert(b, ref byteIndex, 1);
            this.Accums = BitHelper.Convert(b, ref byteIndex, 1);
            this.Append = BitHelper.Convert(b, ref byteIndex, 1);

        }

        /// <summary>
        /// for serialization
        /// </summary>
        public EventReportMessage()
        {

        }

    }


    public struct GPS_Status
    {

        public bool Everything_OK { get; set; }
        public bool DifferentiallyCalcd { get; set; }

        public bool LastKnown { get; set; }

        public bool InvalidFix { get; set; }

        public bool TwoDFix { get; set; }

        public bool Historic { get; set; }

        public bool Invalidtime { get; set; }


    }

}
