using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class CalAMP_Telegram
    {

        public OptionsHeader OptionsHeader { get; set; }

        public MessageHeader MessageHeader { get; set; }

        public EventReportMessage MessageBody { get; set; }


        /// <summary>
        /// for serializatoin purposes only
        /// </summary>
        public CalAMP_Telegram()
        {
        }

        public CalAMP_Telegram(byte[] bytes)
        {
            this.OptionsHeader = new OptionsHeader();
            this.MessageHeader = new MessageHeader();

            int currentBitNumber = 0;

            //find the header options from the first byte
            this.OptionsHeader.HeaderContentOptions.MobileID = BitHelper.bitwiseANDFromHex(bytes[0], 8);
            this.OptionsHeader.HeaderContentOptions.MobileIDType = BitHelper.bitwiseANDFromHex(bytes[0], 7);
            this.OptionsHeader.HeaderContentOptions.AuthenticationWord = BitHelper.bitwiseANDFromHex(bytes[0], 6);
            this.OptionsHeader.HeaderContentOptions.Routing = BitHelper.bitwiseANDFromHex(bytes[0], 5);
            this.OptionsHeader.HeaderContentOptions.Forwarding = BitHelper.bitwiseANDFromHex(bytes[0], 4);
            this.OptionsHeader.HeaderContentOptions.ResponseRedirection = BitHelper.bitwiseANDFromHex(bytes[0], 3);
            this.OptionsHeader.HeaderContentOptions.OptionsExtension = BitHelper.bitwiseANDFromHex(bytes[0], 2);
            this.OptionsHeader.HeaderContentOptions.AlwaysSet = BitHelper.bitwiseANDFromHex(bytes[0], 1);
            currentBitNumber++;


            if (OptionsHeader.HeaderContentOptions.MobileID)
            {

                this.OptionsHeader.MobileIDLength = BitHelper.Convert(bytes, ref currentBitNumber, 1);

                //find the mobileIDLength then read that data             
                //now find the mobileID;
                byte[] mobileIDBytes = new byte[this.OptionsHeader.MobileIDLength];
                Array.Copy(bytes, currentBitNumber, mobileIDBytes, 0, this.OptionsHeader.MobileIDLength);
                this.OptionsHeader.MobileID = BitConverter.ToString(mobileIDBytes).Replace("-", "");
                int test = BitConverter.ToInt16(mobileIDBytes, 0);
                currentBitNumber += OptionsHeader.MobileIDLength;
            }

            if (OptionsHeader.HeaderContentOptions.MobileIDType)
            {
                //mobileID type length (should always be 1)
                this.OptionsHeader.MobileIDTypeLength = BitHelper.Convert(bytes, ref currentBitNumber, 1);
                //mobileIDType
                this.OptionsHeader.MobileIDType = (OptionsHeader.MobileIDTypeEnum)BitHelper.Convert(bytes, ref currentBitNumber, OptionsHeader.MobileIDTypeLength);
            }


            if (OptionsHeader.HeaderContentOptions.AuthenticationWord)
            {
                //authentication length and message
                this.OptionsHeader.AuthenticationLength = bytes[currentBitNumber];
                currentBitNumber++;
                byte[] authenticationBytes = new byte[OptionsHeader.AuthenticationLength];
                Array.Copy(bytes, currentBitNumber, authenticationBytes, 0, OptionsHeader.AuthenticationLength);
                this.OptionsHeader.Authentication = BitConverter.ToString(authenticationBytes).Replace("-", string.Empty);
                currentBitNumber += OptionsHeader.AuthenticationLength;
            }


            if (OptionsHeader.HeaderContentOptions.Routing) throw new Exception("not implemented Routing");
            if (OptionsHeader.HeaderContentOptions.Forwarding) throw new Exception("not implemented Forwarding");
            if (OptionsHeader.HeaderContentOptions.ResponseRedirection) throw new Exception("not implemented ResponseRedirection");
            if (OptionsHeader.HeaderContentOptions.OptionsExtension) throw new Exception("not implemented OptionsExtension");

            //===============================   move on to the message header   ===============================   
            this.MessageHeader.ServiceType = (ServiceTypeEnum)bytes[currentBitNumber];
            currentBitNumber++;

            //Messgae Type
            this.MessageHeader.MessageType = (MessageTypeEnum)bytes[currentBitNumber];
            currentBitNumber++;

            //seq #    
            this.MessageHeader.SequenceNumber = BitHelper.Convert(bytes, ref currentBitNumber, 2);

            //=====================================        MESSAGE BODY    ======================================

            switch (MessageHeader.MessageType)
            {
                case MessageTypeEnum.Event_Report_message: this.MessageBody = new EventReportMessage(bytes, currentBitNumber); break;

                default: throw new Exception("not implemented this type of Message type");
            }
        }

        public string GetXML()
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(this.GetType());
            serializer.Serialize(stringwriter, this);
            return stringwriter.ToString();
        }

    }
}
