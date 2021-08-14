using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpMessageHandler
{
    public class MyHttpClientHandler : HttpClientHandler
    {
        public MyHttpClientHandler(CookieContainer cookie_container)
        {

            CookieContainer = cookie_container;     // Thay thế CookieContainer mặc định
            AllowAutoRedirect = false;                // không cho tự động Redirect
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            UseCookies = true;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            Console.WriteLine("Bất đầu kết nối " + request.RequestUri.ToString());
            // Thực hiện truy vấn đến Server
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("Hoàn thành tải dữ liệu");
            return response;
        }
    }

    public class ChangeUri : DelegatingHandler
    {
        public ChangeUri(System.Net.Http.HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            var host = request.RequestUri.Host.ToLower();
            Console.WriteLine($"Check in  ChangeUri - {host}");
            if (host.Contains("google.com"))
            {
                // Đổi địa chỉ truy cập từ google.com sang github
                request.RequestUri = new Uri("https://github.com/");
            }
            // Chuyển truy vấn cho base (thi hành InnerHandler)
            return base.SendAsync(request, cancellationToken);
        }
    }


    public class DenyAccessFacebook : DelegatingHandler
    {
        public DenyAccessFacebook(System.Net.Http.HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {

            var host = request.RequestUri.Host.ToLower();
            Console.WriteLine($"Check in DenyAccessFacebook - {host}");
            if (host.Contains("facebook.com"))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(Encoding.UTF8.GetBytes("Không được truy cập"));
                return await Task.FromResult<HttpResponseMessage>(response);
            }
            // Chuyển truy vấn cho base (thi hành InnerHandler)
            return await base.SendAsync(request, cancellationToken);
        }
    }
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // var url = "https://postman-echo.com/post";
            var url = "https://google.com";

            var cookies = new CookieContainer();

            // tao chuoi handler
            var bottomHander = new MyHttpClientHandler(cookies);
            var changeHandler = new ChangeUri(bottomHander);
            var denyAccessFB = new DenyAccessFacebook(changeHandler);


            // using var handler = new SocketsHttpHandler();
            // handler.AllowAutoRedirect = true;
            // handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip; //giai nen
            // handler.UseCookies = true;
            // handler.CookieContainer = cookies;

            var httpClient = new HttpClient(denyAccessFB);

            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri(url);
            request.Headers.Add("User-Agent", "Mozilla/6.0");

            var lstParams = new List<KeyValuePair<string, string>>();
            lstParams.Add(new KeyValuePair<string, string>("key1", "value1"));
            lstParams.Add(new KeyValuePair<string, string>("key2", "value2"));
            request.Content = new FormUrlEncodedContent(lstParams);

            var response = await httpClient.SendAsync(request);
            // sau khi nhan duoc response thi co the xem cookies
            Console.WriteLine("Danh sach Cookies:");
            cookies.GetCookies(new Uri(url)).ToList().ForEach(c => Console.WriteLine($"{c.Name} - {c.Value}"));
            Console.WriteLine("=====================================");
            var html = await response.Content.ReadAsStringAsync();
            Console.WriteLine(html);



        }
    }
}
