using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class MessageBody
    {

        public byte[] bytes { get; set; }

        public int currentByteIndex { get; set; }


        public MessageBody() { }

        public MessageBody(byte[] b, int byteIndex)
        {
            this.bytes = b;
            this.currentByteIndex = byteIndex;
        }

    }
}
