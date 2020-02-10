using System;
using System.Net.Http;
using System.Threading;

namespace Client
{
    static class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            while (true)
            {
                Thread.Sleep(5000);
                var result = httpClient.GetAsync("http://server1:5000/api/values").Result;
                Console.WriteLine(result);
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
