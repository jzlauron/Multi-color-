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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using mdtypes;
using System.Drawing.Drawing2D;
namespace juancarlosl_2800_Multidraw
{
    public partial class Multidraw : Form
    {
        private ConnectionDialog _connector = new ConnectionDialog();                               //object for connectiondialog
        private Socket _socket;                                                                     //socket for receiving and sending
        private Receiver _receiver;                                                                 //receiver class object
        private Sender _sender;                                                                     //sender class object
        private Thread _DrawerTh;                                                                   //a thread to draw lines

        //initialized drawing fields
        private int _thickness = 1;
        private int _alpha = 255;
        private Color _color = Color.Black;
        private PointF _lastPos = new PointF(-1, -1);
        private bool _bLeftClick = false;
        private bool _bAlpha = false;

        public Multidraw()
        {
            InitializeComponent();
            _connector.ShowDialog();                                                                //show the connection dialog
            Start();                                                                                //start sending and receiving of sockets
            updatetmr.Enabled = true;
            MouseWheel += MultiDraw_MouseWheel;                                                     //mousewheel event
            UI_lblThickness.Text = $"Thickness: {_thickness} [Alpha: {_alpha}]";                    //initialize text value
        }
        //for startign sending and receiving of sockets and drawing thread
        private void Start()
        {
            _socket = _connector.Socket;
            _sender = new Sender(_socket);
            _receiver = new Receiver(_socket);
            _DrawerTh = new Thread(Drawer);
            _DrawerTh.IsBackground = true;
            _DrawerTh.Start();
        }
        //draw threading for drawing the lines received 
        private void Drawer()
        {
            while (true)
            {
                int lines;
                lock (_receiver.ReceivedLines)
                    lines = _receiver.ReceivedLines.Count;
                if (lines > 0)
                {
                    lock (_receiver.ReceivedLines)
                    {
                        LineSegment lineseg = _receiver.ReceivedLines.Dequeue();
                        GraphicsPath line = new GraphicsPath();
                        Color col = Color.FromArgb(lineseg.Alpha, lineseg.Colour.R, lineseg.Colour.G, lineseg.Colour.B);
                        Pen pen = new Pen(col, lineseg.Thickness);
                        pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                        line.AddLine(lineseg.Start, lineseg.End);
                        Graphics gr = CreateGraphics();
                        gr.DrawPath(pen, line);
                    }
                }
                else
                    Thread.Sleep(1);
            }
        }
        //for initializing and modifying the line to be drawn
        private void AddSeg(MouseEventArgs e)
        {
            lock (_sender._LinesToSend)
            {
                LineSegment LineSeg = new LineSegment();
                LineSeg.Colour = _color;
                LineSeg.Alpha = (byte)_alpha;
                LineSeg.Thickness = (ushort)_thickness;
                LineSeg.Start = e.Location;
                LineSeg.End = _lastPos;
                lock (_sender._LinesToSend)
                {
                    _sender._LinesToSend.Enqueue(LineSeg);
                }
                _lastPos = e.Location;
            }
        }
        //a color dialog to open for UI to choose color
        private void UI_btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = false;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                UI_btnColor.ForeColor = cd.Color;
                _color = cd.Color;
            }

        }
        //connect button for UI to connect socket to the address
        private void UI_btnConnect_Click(object sender, EventArgs e)
        {
            if (!_socket.Connected)
            {
                _connector.ShowDialog();
                _socket = _connector.Socket;
                UI_btnConnect.Text = "Connected";
                Start();
            }
        }
        //mouse wheel for modifying alpha value and color thickness
        private void MultiDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            if(!_bAlpha)
            {
                if (e.Delta > 0)
                {
                    _thickness++;
                    _thickness = Math.Min(_thickness, ushort.MaxValue);
                }
                else if (e.Delta < 0)
                {
                    _thickness--;
                    _thickness = Math.Max(_thickness, 1);
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    _alpha++;
                    _alpha = Math.Min(_alpha, byte.MaxValue);
                }
                else if (e.Delta < 0)
                {
                    _alpha--;
                    _alpha = Math.Max(_alpha, 1);
                }
            }
            UI_lblThickness.Text = $"Thickness: {_thickness} [Alpha: {_alpha}]";
        }
        //when user left clicks while mouse moving, send line to queue then draws it
        private void Multidraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bLeftClick)
                Invoke(new Action(() => AddSeg(e)));
        }
        
        //when users left click, dictate the position then pass the position value to the starting pos of line
        private void Multidraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left) && ClientRectangle.Contains(e.Location))
            {
                _bLeftClick = true;
                _lastPos = e.Location;
            }
        }

        private void Multidraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
                _bLeftClick = false;
        }
        //Timer tick for updating texts and display 
        private void updatetmr_Tick(object sender, EventArgs e)
        {
            UI_lblFragments.Text = $"Fragments: {_receiver.Frags.ToString()}";
            UI_lblBytesRx.Text = $"Bytes RXed: {_receiver.RxBytes.Bit()}";
            UI_Destack.Text = $"Destack Avg: {_receiver.Destack.ToString("F")}";
            UI_lblRxedFrames.Text = $"Frames RX'ed: {_receiver.Frames.ToString()}";
            UI_btnConnect.Text = _socket.Connected ? "Connected" : "Disconnected";
        }

        private void Multidraw_FormClosing(object sender, FormClosingEventArgs e)
        {
            _socket.Close();
        }
        //when user pressed shift, modfying alpha value set to true
        private void Multidraw_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ShiftKey)
                _bAlpha = true;
        }

        private void Multidraw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                _bAlpha = false;
        }
    }
    public static class Extensions
    {
        public static string Bit(this int num)
        {
            if (num / Math.Pow(2, 30) > 1)
                return (num / Math.Pow(2, 30)).ToString("F") + "GB";
            else if (num / Math.Pow(2, 20) > 1)
                return (num / Math.Pow(2, 20)).ToString("F") + "MB";
            else if (num / Math.Pow(2, 10) > 1)
                return (num / Math.Pow(2, 10)).ToString("F") + "kB";
            else
                return num + "bytes";
        }
    }


}

