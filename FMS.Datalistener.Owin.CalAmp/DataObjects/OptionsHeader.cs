using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class OptionsHeader
    {


        public HeaderContentOptions HeaderContentOptions = new HeaderContentOptions();

        public int MobileIDLength { get; set; }
        public string MobileID { get; set; }

        public int MobileIDTypeLength { get; set; }

        public MobileIDTypeEnum MobileIDType { get; set; }

        public int AuthenticationLength { get; set; }

        public string Authentication { get; set; }

        public OptionsHeader() { }

        public enum MobileIDTypeEnum: int{
            
            OFF = 0, Electronic_Serial_Number = 1, IMEI = 2, IMSI = 3, User_defined = 4, phone_number = 5, LMU_IPAddress = 6, MEID = 7
        }

    }


        public class HeaderContentOptions
    {

        //Bit 0: Mobile ID (0 = disabled, 1= enabled)
        //Bit 1: Mobile ID Type (0 = disabled, 1 = enabled)
        //Bit 2: Authentication Word (0 = disabled, 1 = enabled)
        //Bit 3: Routing (0 = disabled, 1 = enabled)
        //Bit 4: Forwarding (0 = disabled, 1 = enabled)
        //Bit 5: Response Redirection (0=disabled, 1=enabled)
        //Bit 6: Options Extension (0=disabled, 1=enabled)
        //Bit 7: Always set.

        public bool MobileID { get; set; }
        public bool MobileIDType { get; set; }
        public bool AuthenticationWord { get; set; }
        public bool Routing { get; set; }
        public bool Forwarding { get; set; }
        public bool ResponseRedirection { get; set; }
        public bool OptionsExtension { get; set; }
        public bool AlwaysSet { get; set; }
    }

}
