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

namespace FileTransfer

    
/// <summary>
/// 1 = get current directory
/// 2 = show all files in currrent directory
/// </summary>

/// <summary>
/// TODO:
/// Finish download button
/// </summary>



{
    public partial class Form1 : Form
    {
        
        string hostname = "192.168.0.253";
        int port = 9999;
        TcpClient client = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        

        public Form1()
        {
            InitializeComponent();
            client.Connect(hostname, port);
            checkedListBox1.Items.Add("Connected to the main server");
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
            List<string> itemsToDownload = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                itemsToDownload.Add(item as string);
            }
            return;
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
    }
}
