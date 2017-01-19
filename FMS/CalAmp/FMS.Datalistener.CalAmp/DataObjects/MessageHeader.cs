using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class MessageHeader
    {

        public MessageTypeEnum MessageType { get; set; }

        public ServiceTypeEnum ServiceType { get; set; }

        public int SequenceNumber { get; set; }

        public MessageHeader()
        {

        }
    }

    public enum MessageTypeEnum
    {
        NullMessage = 0,
        ACK_NAK_Message = 1,
        Event_Report_message = 2,
        ID_Report_message = 3,
        User_Data_message = 4,
        Application_Data_message = 5,
        Configuration_Parameter_message = 6,
        Unit_Request_message = 7,
        Locate_Report_message = 8,
        User_Data_with_Accumulators_message = 9,
        Mini_Event_Report_message = 10,
        Mini_User_Data_message = 11,
        Mini_Application_message = 12,
        Device_Version_message = 13,
        Application_message_with_accumulators = 14
    }

    public enum ServiceTypeEnum
    {
        UnacknowlegedRequest = 0,
        AcknowlegedRequest = 1,
        ResponseToAcklowlegedRequest = 2
    }

}
