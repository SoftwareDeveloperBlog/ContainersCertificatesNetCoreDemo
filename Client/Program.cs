using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    static class Program
    {
        static void Main()
        {
            // foreach (var file in Directory.GetFiles("/certs/", "*.pfx"))
            // {
            //     try
            //     {
            //         using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser, OpenFlags.ReadWrite))
            //         {
            //             store.Add(new X509Certificate2(file, "password", X509KeyStorageFlags.PersistKeySet));
            //             Console.WriteLine($"Cert: '{file}' added to store");
            //         }
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine($"Could not add cert: '{file}' to store: {e}");
            //     }
            // }

            var httpClient = new HttpClient();
            
            // var connectToServer1 = Task.Run(() =>
            // {
            //     while (true)
            //     {
            //         try
            //         {
            //             Thread.Sleep(5000);
            //             var result = httpClient.GetAsync("https://server1:5001/api/values").Result;
            //             Console.WriteLine("Connected to server1. Result:");
            //             Console.WriteLine(result);
            //             Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            //         }
            //         catch (Exception e)
            //         {
            //             Console.WriteLine("Could not connect to server1. Openssl diagnosis:");
            //             Console.WriteLine("openssl s_client -connect server1:5001 -debug -state".Bash());
            //             Console.WriteLine(e);
            //             break;
            //         }
            //     }
            // });
            
            var connectToServer2 = Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(5000);
                        var result = httpClient.GetAsync("https://server2:6001/api/values").Result;
                        Console.WriteLine("Connected to server2. Result:");
                        Console.WriteLine(result);
                        Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Could not connect to server2. Openssl diagnosis:");
                        Console.WriteLine("openssl s_client -connect server2:6001 -debug -state".Bash());
                        Console.WriteLine(e);
                        break;
                    }
                }
            });

            Task.WaitAll(/*connectToServer1,*/ connectToServer2);
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