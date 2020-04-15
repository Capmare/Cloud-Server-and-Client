using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.IO;

namespace FileTransfer

    
/// <summary>
/// 1 = get current directory
/// 2 = show all files in currrent directory
/// 3 = download selected files
/// 4 = delete selected files
/// </summary>

/// <summary>
/// TODO:
/// Finish download function
/// send the data to download the good file
/// </summary>



{
    public partial class Form1 : Form
    {
        static string ip = "" ;
        static IPAddress hostname;

        static Int32 port;
        TcpClient client = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);

        public Form1()
        {
            
            try
            {
                
                InitializeComponent();
                client.Connect(hostname, port);
                checkedListBox1.Items.Add("Connected to the main server");
            }
            catch (Exception e)
            {
                 MessageBox.Show(e.ToString());
            }
        }

        
        private void GetList()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            serverStream = client.GetStream();
            int buff = 0;
            byte[] instream = new byte[client.ReceiveBufferSize];
            buff = client.ReceiveBufferSize;
            List<string> data = (List<string>)formatter.Deserialize(serverStream);
            //string data = System.Text.Encoding.ASCII.GetString(instream);
            foreach (var item in data)
            {
                checkedListBox1.Items.Add(item);
            }
            serverStream.Flush();
            return;
        }

        private void getData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            serverStream = client.GetStream();
            int buff = 0;
            byte[] instream = new byte[client.ReceiveBufferSize];
            buff = client.ReceiveBufferSize;
            List<string> data = (List<string>)formatter.Deserialize(serverStream);
            foreach (var item in data)
            {
                checkedListBox1.Items.Add(item);
            }
            serverStream.Flush();
            return;
        }

        private void ShowItems_Click(object sender, EventArgs e)
        {
            string message = "2";
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            NetworkStream dataStream = client.GetStream();
            dataStream.Write(dataToSend, 0, dataToSend.Length);
            dataStream.Flush();
            GetList();
            return;
        }

        private void ReloadItems_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            string message = "2";
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            NetworkStream dataStream = client.GetStream();
            dataStream.Write(dataToSend, 0, dataToSend.Length);
            dataStream.Flush();
            GetList();
            return;
        }

        private void DownloadItems_Click(object sender, EventArgs e)
        {
            string message = "3";
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            NetworkStream dataStream = client.GetStream();
            dataStream.Write(dataToSend, 0, dataToSend.Length);
            dataStream.Flush();
            List<string> itemsToDownload = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                itemsToDownload.Add(item as string);
            }
            foreach (var item in itemsToDownload)
            {
                sendFileRequest(item);
            }
            reciveFiles();
        }

        private void ShowDir_Click(object sender, EventArgs e)
        {
            string message = "1";
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            NetworkStream dataStream = client.GetStream();
            dataStream.Write(dataToSend, 0, dataToSend.Length);
            dataStream.Flush();
            getData();
            return;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            client.Close();
            client.Dispose();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip = IpAddress.Text;
            hostname = IPAddress.Parse(ip);
            port = 9999;
            try
            {
                client.Connect(hostname, port);
                checkedListBox1.Items.Add("Connected to the main server");
            }
            catch (Exception a)
            {

                MessageBox.Show(a.ToString());
            }
        }

        private void CLOSE_SERVER_Click(object sender, EventArgs e)
        {
            string message = "410";
            byte[] dataToSend = Encoding.ASCII.GetBytes(message);
            NetworkStream dataStream = client.GetStream();
            DialogResult dr = MessageBox.Show("Are you sure you want to close the server", "?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                dataStream.Write(dataToSend, 0, dataToSend.Length);
                dataStream.Flush();
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.Add("server closed with code 1");
            }
            if (dr == DialogResult.No)
            {
                dataStream.Flush();
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Checked);
            }
        }
        static void reciveFiles()
        {

            TcpListener listener;
            TcpClient cl;
            
            listener = new TcpListener(hostname, 8080);
            listener.Start();
            cl = listener.AcceptTcpClient();
            byte[] buffer = new byte[cl.ReceiveBufferSize];
            NetworkStream data = cl.GetStream();
            data.Read(buffer, 0, buffer.Length);
            File.WriteAllBytes("file.dat", buffer);
            MessageBox.Show("file accepted");

        }
        static void sendFileRequest(string file)
        {

            TcpClient client = new TcpClient();
            client.Connect(hostname, 6000);
            NetworkStream dataStream =  client.GetStream();
            byte[] FileToDownload = System.Text.Encoding.ASCII.GetBytes(file);
            dataStream.Write(FileToDownload,0,file.Length);
            dataStream.Flush();
        }

        private void IpAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
