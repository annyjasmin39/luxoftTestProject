using System;
using System.IO;
using System.Text;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string path; string datatype;
            Console.WriteLine("Enter path of file");
            path= Console.ReadLine();
            Console.WriteLine("Enter data type of file");
            datatype = Console.ReadLine();
            string readData = "";
            switch(datatype)
            {
                case "binary":
                    BinaryClass bn = new BinaryClass();
                    readData=bn.ProcessData(path);
                    break;
                case "text":
                    TextClass bn1 = new TextClass();
                    readData = bn1.ProcessData(path);
                    break;
                case "text reverse":
                    TextReverseClass bn2 = new TextReverseClass();
                    readData = bn2.ProcessData(path);
                    break;
                default:
                    readData = "Error reading file";
                    break;
            }
            Console.WriteLine(readData);
        }

        
    }

    public class BinaryClass : IDataProcessor
    {
        public string ProcessData(string path)
        {
            byte[] buffer = new byte[5];
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var bytesToread = fileStream.Read(buffer, 0, buffer.Length);
                    fileStream.Close();

                    if (bytesToread != buffer.Length)
                    {
                        Console.WriteLine("could not read binary data");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Convert.ToBase64String(buffer, 0, buffer.Length) ;
        }
    }
    public class TextClass : IDataProcessor
    {
        public string ProcessData(string path)
        {
            byte[] bytes = null;
            try
            {
                string fileToRead = File.ReadAllText(path);
                string sevenChars = fileToRead.Substring(0, 7);
                bytes = Encoding.UTF8.GetBytes(sevenChars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return Encoding.UTF8.GetString(bytes);
        }
    }

    public class TextReverseClass : IDataProcessor
    {
        public string ProcessData(string path)
        {
            byte[] bytes = null;
            try
            {
                string fileToRead = File.ReadAllText(path);
                char[] chars = fileToRead.ToCharArray();
                Array.Reverse(chars);
                string sevenChars = new string(chars).Substring(0, 6);
                bytes = Encoding.UTF8.GetBytes(sevenChars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
