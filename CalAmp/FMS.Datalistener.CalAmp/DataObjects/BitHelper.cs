using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    public class BitHelper
    {
        /// <summary>
        /// takes a subset of bytes from a byte array, then reverses the order.
        /// </summary>
        public static byte[] RevByteOrder(byte[] byteArr, int startIndex, int count)
        {

            byte[] retArr = new byte[count];
            Array.Copy(byteArr, startIndex, retArr, 0, count);
            Array.Reverse(retArr);

            return retArr;
        }

        public static void GetBitMapString(ref string message, byte b, int bitID, string msg)
        {

            int bitInt = (int)Math.Pow(2, bitID);
            string retStr =  (bitInt & b) == bitInt ? msg : string.Empty;
            if (!string.IsNullOrEmpty(retStr) && !string.IsNullOrEmpty(message)) retStr = ", " + retStr;

            message +=  retStr;
        }

        public static int Convert(byte[] byteArr, ref int startIndex, int length)
        {

            if (length == 1)//if we only want to convert one byte of data into an int
            {
                startIndex++;
                return (int)byteArr[startIndex - 1];
            }

            byte[] intArr = RevByteOrder(byteArr, startIndex, length);
            startIndex += length;

            int retInt;

            switch (length)
            {
                case 2: retInt = BitConverter.ToInt16(intArr, 0); break;
                case 4: retInt = BitConverter.ToInt32(intArr, 0); break;
                default: throw new Exception("unknown length for of byte an integer conversion (will only do 16 and 32 bit conversions).");
                //case 8: retInt = BitConverter.ToInt64(intArr, 0); break;
            }
            return retInt;
        }


        /// <summary>
        /// With a hex value, looks up the bit (locationwithinbyte)
        /// 0 0 0 0 X 0 0 0 
        /// (x would be position 5)
        /// 0 0 0 0 0 0 0 y
        /// (y would be position 8)
        /// z 0 0 0 0 0 p q
        /// (z =1, p = 7, q = 8)
        /// </summary>        
        public static bool bitwiseANDFromHex(byte b, int locationWithinByte)
        {

            int byteVal = System.Convert.ToInt32(b);

            if (locationWithinByte > 4)
            {
                locationWithinByte -= 4;
            }
            else
            {
                byteVal /= 16;
            }

            int compareVal = System.Convert.ToInt32(Math.Pow(2, 4 - locationWithinByte));

            return (byteVal & compareVal) == compareVal;
        }

    }
}