using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace CopaFilmesBackEnd.IntegrationTest
{
    public class BaseIntegration
    {
        public HttpClient client { get; set; }

       
        public string hostBaseApi { get; set; }
        public BaseIntegration()
        {
            hostBaseApi = "http://localhost:5001/"; 
             IWebHostBuilder builder = getWebHostBuilder();
            var server = new TestServer(builder);
        
            client = server.CreateClient();
            

        }
       
        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }

       


        private static IWebHostBuilder getWebHostBuilder()
        {
            var builder = new WebHostBuilder()
                //.UseEnvironment("Development") //TODO Create a  Test env 
                .UseStartup<Startup>();
            return builder;
        }
    }
}
