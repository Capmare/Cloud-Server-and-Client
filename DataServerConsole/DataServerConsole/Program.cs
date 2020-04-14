using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using System.Collections;


/// <summary>
/// 1 = get current directory
/// 2 = show all files in currrent directory
/// 3 = download selected files
/// 4 = delete selected files
/// </summary>

/// <summary>
/// TODO:
/// send the good file
/// </summary>


namespace DataServerConsolevsiu
{
    class Program
    {
        static IPAddress hostname = IPAddress.Parse("192.168.0.253");
        static Int32 port = 9999;
        static void Main(string[] args)
        {
            string path = "C:\\Users\\capma\\Desktop";
            List<string> filesList = new List<string>();

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

            while (true)
            {
                try
                {
                    dataStream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes);
                    string cleanData = data.Replace("\0", "");
                    Console.WriteLine(cleanData);
                    if (cleanData == "1")
                    {
                        List<string> dir = new List<string>();
                        string message = $"Current path is: {path}";
                        dir.Add(message);
                        bf.Serialize(dataStream, dir);
                        dataStream.Flush();
                    }

                    if (cleanData == "2")
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (var file in files)
                        {
                            filesList.Add(file);

                        }

                        bf.Serialize(dataStream, filesList);
                        filesList.Clear();
                        filesList.ForEach(Console.WriteLine);
                        dataStream.Flush();

                    }
                    if (cleanData == "3")
                    {
                        // create and start server
                        TcpListener listener = new TcpListener(hostname, 6000);
                        listener.Start();
                        TcpClient cl = new TcpClient();
                        cl = listener.AcceptTcpClient();
                        NetworkStream buff = cl.GetStream();
                        Console.WriteLine("65");
                        // do while loop to send download
                        while (cl.Connected)
                            
                        {

                            byte[] buffer = new byte[cl.ReceiveBufferSize];
                            buff.Read(buffer, 0, buffer.Length);
                            string files = System.Text.Encoding.ASCII.GetString(buffer);
                            string cleanFile = files.Replace("\0", "");
                            Console.WriteLine(cleanFile);
                            List<string> fileList = new List<string>();
                            fileList.Add(cleanFile);
                            foreach (string file in fileList)
                            {
                               
                                sendDownload(file); // one error here
                                
                            }
                            
                            dataStream.Flush();
                        }
                    }

                    if (cleanData == "410")
                    {
                        dataStream.Flush();
                        dataStream.Close();
                        client.Close();
                        client.Dispose();
                        Environment.Exit(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        static void sendDownload(string filename)
        {

            try
            {
                TcpClient client; ;
                client = new TcpClient();
                client.Connect("192.168.0.253", 8080);
                client.Client.SendFile(filename);
                Console.WriteLine("file sent");

            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
    }
}
