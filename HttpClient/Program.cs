using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient
{
    class Program
    {
        public static void ShowHeader(HttpResponseHeaders headers)
        {
            Console.WriteLine("List Headers: ");
            foreach (var h in headers)
            {
                Console.WriteLine($"{h.Key} : {h.Value}");
            }
        }
        public static async Task<string> GetWebContent(string url)
        {
            Console.WriteLine("Strart dowload ...");
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();
                HttpResponseMessage msg = await httpClient.GetAsync(url);
                string html = await msg.Content.ReadAsStringAsync();
                ShowHeader(msg.Headers);
                Console.WriteLine("Dowload done!");
                return html;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Loi";

            }
        }

        public static async Task<byte[]> DowloadFile(string url)
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();
                HttpResponseMessage msg = await httpClient.GetAsync(url);
                var file = await msg.Content.ReadAsByteArrayAsync();
                return file;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new byte[0];

            }
        }

        public static async Task DowloadStream(string url, string filename)
        {

            Console.WriteLine("Strart dowload ...");
            try
            {
                using var fileOutput = File.OpenWrite(filename);

                using var httpClient = new System.Net.Http.HttpClient();
                HttpResponseMessage msg = await httpClient.GetAsync(url);
                var stream = await msg.Content.ReadAsStreamAsync();

                int sizeBuff = 1024;
                var buffer = new byte[sizeBuff];
                int countByte = 0;
                bool isDone = false;
                while (isDone == false)
                {
                    countByte = await stream.ReadAsync(buffer, 0, sizeBuff);
                    if (countByte != 0)
                    {
                        await fileOutput.WriteAsync(buffer, 0, countByte);
                    }
                    else
                    {
                        isDone = true;
                        Console.WriteLine("Dowload done!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
        static async Task Main(string[] args)
        {
            //Đối tượng Uri lưu tất cả thông tin về đường link
            // string url = "https://xuanthulab.net/lap-trinh/csharp/?page=3#acff";
            // var uri = new Uri(url);
            // var uritype = typeof(Uri);
            // uritype.GetProperties().ToList().ForEach(property =>
            // {
            //     Console.WriteLine($"{property.Name,15} {property.GetValue(uri)}");
            // });
            // Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");

            // Dns là đối tượng truy vấn đến máy chủ Dns lấy thông tin host
            // var hostName = Dns.GetHostName();
            // Console.WriteLine(hostName);

            // lấy danh sách ip của host
            // string  url = "https://kdphbc.vnpost.vn";
            // var uri = new Uri(url);
            // Console.WriteLine(uri.Host);

            // var iphostentry = Dns.GetHostEntry(uri.Host);
            // Console.WriteLine(iphostentry.HostName);
            // iphostentry.AddressList.ToList().ForEach(ip =>
            //      Console.WriteLine(ip)
            // );

            // KiỂM TRA XEM LINK CÒN HOẠT ĐỘNG KHÔNG BẰNG pING
            // var ping = new Ping();
            // var pingReply = ping.Send("YOUTUBE.COM");
            // Console.WriteLine(pingReply.Status);
            // if (pingReply.Status == IPStatus.Success)
            // {
            //     Console.WriteLine(pingReply.RoundtripTime);
            //     Console.WriteLine(pingReply.Address);
            // }

            // DowloadFile
            // string url = "https://i.pinimg.com/originals/f2/bd/10/f2bd102f9fc67e029ecd85a6ff8f8c4d.png";
            // var doraemonBytes = await DowloadFile(url);
            // string filename = "Mon1.jpg";
            // using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            // stream.Write(doraemonBytes, 0, doraemonBytes.Length);

            // DowloadFileStream
            // string url = "https://tinnhanhplus.com/wp-content/uploads/2020/11/hinh-anh-doremon-xin-chao.jpg";
            // await DowloadStream(url, "Mon2.png");

            //Get dung Send
            // string url = "https://google.com/";
            // using var httpClient = new System.Net.Http.HttpClient();
            // var request = new HttpRequestMessage();
            // request.Method = HttpMethod.Get;
            // request.RequestUri = new Uri(url);
            // request.Headers.Add("User-Agent", "Mozilla/5.0");

            // var response = await httpClient.SendAsync(request);
            // ShowHeader(response.Headers);
            // var html = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(html);

            //Post

            string url = "https://postman-echo.com/post";
            var content = new MultipartFormDataContent();

            var lstParams = new List<KeyValuePair<string, string>>();
            lstParams.Add(new KeyValuePair<string, string>("key1", "value1"));
            lstParams.Add(new KeyValuePair<string, string>("key2", "value2"));
            var encodedContent = new System.Net.Http.FormUrlEncodedContent(lstParams);

            // post json
            string jsonContent = @"{""key1"": ""gai tri 1""}";
            //  Console.WriteLine(jsonContent);
            var jsonUpload = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //upload file
            var stream = File.OpenRead("fileupload.txt");
            var fileUpload = new StreamContent(stream);

            // Add data vao content
            content.Add(fileUpload, "file", "up.txt");
            content.Add(jsonUpload);
            content.Add(encodedContent);



            using var httpClient = new System.Net.Http.HttpClient();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(url);
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            ShowHeader(response.Headers);
            var html = await response.Content.ReadAsStringAsync();
            Console.WriteLine(html);




        }
    }
}
