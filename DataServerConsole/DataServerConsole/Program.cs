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
/// cannot connect to socket bug
/// </summary>


namespace DataServerConsole
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
                        string[] files = Directory.GetFiles(path);
                        foreach (var file in files)
                        {
                            download(file);
                        }
                        dataStream.Flush();

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
        static void download(string filename)
        {

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(hostname, 5050);
            EndPoint endPoint = ipEndPoint;
            client.Bind(endPoint);
            client.Listen(10);
            AsyncCallback callback = new AsyncCallback(ProcessDnsInformation);
            client.BeginSendFile(filename, callback, client);
            client.SendFile(filename);
            IAsyncResult ar = null;
            IAsyncResult clientStatus = (IAsyncResult)ar.AsyncState;
            client.EndSendFile(clientStatus);
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();

        }
        static int requestCounter;
        static ArrayList hostData = new ArrayList();
        static StringCollection hostNames = new StringCollection();

        static void ProcessDnsInformation(IAsyncResult result)
        {
            string hostName = (string)result.AsyncState;
            hostNames.Add(hostName);
            try
            {
                // Get the results.
                IPHostEntry host = Dns.EndGetHostEntry(result);
                hostData.Add(host);
            }
            // Store the exception message.
            catch (SocketException e)
            {
                hostData.Add(e.Message);
            }
            finally
            {
                // Decrement the request counter in a thread-safe manner.
                Interlocked.Decrement(ref requestCounter);
            }
        }
        
    }
}
