//Project : MultiDraw
// Course : CMPE 2800
// Date April 20 2021
// Instructor: Simon Walker
// Author : Juan Carlos Lauron
// Using Socket
//  
// Submission code : 1202_2800_Multidraw
///////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace juancarlosl_2800_Multidraw
{
    public partial class ConnectionDialog : Form
    {
        public Socket Socket { get; private set; }
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void UI_btnConnect_Click(object sender, EventArgs e)
        {
            Socket = new Socket(
                AddressFamily.InterNetwork,             //Socket IPV4 address Scheme
                SocketType.Stream,                      //streaming socket (connection based)
                ProtocolType.Tcp);                      //use TCP as a protocol+
            try
            {
                Socket.BeginConnect(UI_tbAddress.Text, (int)UI_numUDPOrt.Value, CBConnected, Socket);//Asynchronous connection, avoids blocking, thread started in threadpool
                
            }
            catch(SocketException err)
            {
                MessageBox.Show(err.Message,"Connection Failure..",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void CBConnected(IAsyncResult ar)
        {
            try
            {
                Socket.EndConnect(ar);
                Invoke(new Action(() => Connected()));  //prevents cross-thread violations 
                Console.WriteLine("Connected.");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Connection Failure..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void Connected()
        {
            Hide();
        }
    }
}
