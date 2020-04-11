using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


/// <summary>
/// 1 = get current directory
/// 2 = show all files in currrent directory
/// 3 = download selected files
/// </summary>

/// <summary>
/// TODO:
/// resolve infinite loop on default case bug
/// </summary>


namespace DataServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            List<string> filesList = new List<string>();
            IPAddress hostname = IPAddress.Parse("192.168.0.253");
            Int32 port = 9999;
            TcpListener server = null;
            TcpClient client = null;
            string data = null;
            BinaryFormatter bf = new BinaryFormatter();

            server = new TcpListener(hostname, port);
            server.Start();
            Console.WriteLine("server has started ");
            client = server.AcceptTcpClient();
            Console.WriteLine("server accepts clients now");
            NetworkStream dataStream = client.GetStream();
            Console.WriteLine("server started data stream");
            byte[] bytes = new byte[client.ReceiveBufferSize];
            dataStream.Read(bytes, 0, bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(bytes);
            string cleanData = data.Replace("\0", "");
            Console.WriteLine(cleanData);
            do
            {
                try
                {
                    switch (cleanData)
                    {
                        case "1":
                            List<string> dir = new List<string>();
                            string message = $"Current path is: {path}";
                            dir.Add(message);
                            bf.Serialize(dataStream, dir);
                            dataStream.Flush();
                            
                            break;
                        case "2":
                            string[] files = Directory.GetFiles(path);
                            foreach (var file in files)
                            {
                                filesList.Add(file);

                            }

                            bf.Serialize(dataStream, filesList);
                            filesList.Clear();
                            filesList.ForEach(Console.WriteLine);
                            dataStream.Flush();
                            
                            break;
                        default:
                            Console.WriteLine("waiting for input");

                            dataStream.Flush();
                            
                            break;
                    }
                    
                 
                }
                catch (Exception e)
                {
                    throw e;
                }
            } while (true);
            
        }
    }
}
