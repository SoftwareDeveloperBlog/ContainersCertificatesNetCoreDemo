using System;
using System.Diagnostics;
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
                try
                {
                    Thread.Sleep(5000);
                    var result = httpClient.GetAsync("https://server1:5001/api/values").Result;
                    Console.WriteLine(result);
                    Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not connect. Openssl diagnosis:");
                    Console.WriteLine("openssl s_client -connect server1:5001 -debug -state".Bash());
                    throw;
                }
            }
        }
    }

    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}