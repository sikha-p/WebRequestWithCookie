using System;
using System.IO;
using System.Net;

namespace WebRequestWithCookie
{
    public class Class1
    {
        public static string CallRestApi(string domain ,string apiUrl ,string cookie,string outputFile )
        {
            string[] cookies = cookie.Split(';');
            CookieContainer cookieContainer = new CookieContainer();
            Uri target = new Uri(domain);
            foreach (string cookie_ in cookies)
            {
                if (cookie_.Length != 0)
                {
                cookieContainer.Add(new Cookie(cookie_.Split('=')[0], cookie_.Split('=')[1]) { Domain = target.Host });
                }
            }
            string remoteUrl = string.Format(apiUrl);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(remoteUrl);
            httpRequest.CookieContainer = cookieContainer;
            try
            {
                WebResponse response = httpRequest.GetResponse();


                using (Stream output = File.OpenWrite(outputFile))
                using (Stream input = response.GetResponseStream())
                {
                    input.CopyTo(output);
                }

                return "File saved in  : " + outputFile;
            }catch(Exception ex)
            {
                return ex.Message;
            }
           

        }
    }
}
