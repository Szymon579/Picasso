using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;

namespace WordsGame
{
    static class DataTypeHandler
    {

        public static byte[] MakeDataFromBitmap(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                byte[] bitmapData = ms.ToArray();
                byte[] dataLength = BitConverter.GetBytes(bitmapData.Length);
                byte[] readyBytes = new byte[1 + dataLength.Length + bitmapData.Length];

                readyBytes[0] = LogicController.sendBitmap;

                Array.Copy(dataLength, 0, readyBytes, 1, dataLength.Length);
                Array.Copy(bitmapData, 0, readyBytes, 5, bitmapData.Length);

                return readyBytes;
            }
        }

        public static Bitmap MakeBitmapFromData(byte[] data)
        {
            byte[] dataLengthBytes = new byte[4];
            Array.Copy(data, 1, dataLengthBytes, 0, 4);

            int dataLength = BitConverter.ToInt32(dataLengthBytes, 0);
            byte[] bmpData = new byte[dataLength];

            Array.Copy(data, 5, bmpData, 0, dataLength);

            using (var ms = new MemoryStream(bmpData))
            {
                return new Bitmap(ms);
            }
        }

        public static byte[] MakeDataFromMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] readyBytes = new byte[messageBytes.Length + 1];

            readyBytes[0] = LogicController.sendMessage;
            Array.Copy(messageBytes, 0, readyBytes, 1, messageBytes.Length);
            messageBytes.CopyTo(readyBytes, 1);

            return readyBytes;
        }

        public static string MakeMessageFromData(byte[] data)
        {
            string message = Encoding.UTF8.GetString(data, 1, data.Length - 1);

            return message;
        }

        public static byte[] MakeDataFromLogic(byte logicCode)
        {
            byte[] readyBytes = new byte[1];
            readyBytes[0] = logicCode;

            return readyBytes;
        }
        
        public static byte[] MakeLogicFromData(byte[] data)
        {
            return data;
        }

        public static byte[] MakeDataFromLogic(byte logicCode, string str)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(str);      
            byte[] readyBytes = new byte[stringBytes.Length + 1];

            readyBytes[0] = logicCode;
            Array.Copy(stringBytes, 0, readyBytes, 1, stringBytes.Length);

            return readyBytes;
        }


        public static byte[] MakeDataFromWords(List<string> words)
        {
            string str = string.Join(" ", words);
            byte[] stringBytes = Encoding.UTF8.GetBytes(str);
            byte[] readyBytes = new byte[stringBytes.Length + 1];

            readyBytes[0] = LogicController.sendWordsToChoose;
            Array.Copy(stringBytes, 0, readyBytes, 1, stringBytes.Length);

            return readyBytes;
        }

        public static (string, string, string) MakeWordsFromData(byte[] data)
        {
            byte[] readyWords = new byte[data.Length - 1];
            Array.Copy(data, 1, readyWords, 0, data.Length - 1);

            string allInOneWords = Encoding.UTF8.GetString(readyWords);
            string[] wordList = allInOneWords.Split(' ');

            return (wordList[0], wordList[1], wordList[2]);
        }

    }
}
