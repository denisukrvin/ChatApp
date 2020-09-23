using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace ChatApp.Service.Tools
{
    public class ImgBBExtensions
    {
        private static readonly string _apiUrl = "https://api.imgbb.com/1/upload";

        public static string UploadImage(string apiKey, string base64Image)
        {
            using (WebClient client = new WebClient())
            {
                NameValueCollection param = new NameValueCollection();
                param.Add("key", apiKey);
                param.Add("image", base64Image);

                var response = client.UploadValues(_apiUrl, "POST", param);
                var result = Encoding.UTF8.GetString(response);

                var json = JObject.Parse(result);
                var imageUrl = (string)json["data"]["url"];

                return imageUrl;
            }
        }
    }
}