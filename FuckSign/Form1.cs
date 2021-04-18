using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrotiNet;

namespace FuckSign
{
    public class TransparentProxy : ProxyLogic
    {
        public TransparentProxy(HttpSocket httpSocket)
            : base(httpSocket) { }

        //这个类的构造器
        static new public TransparentProxy CreateProxy(HttpSocket httpSocket)
        {
            return new TransparentProxy(httpSocket);
        }

        protected override void OnReceiveRequest()
        {
            Console.WriteLine("-> " + RequestLine);
        }

        protected override void OnReceiveResponse()
        {
            Console.WriteLine("<- " + ResponseStatusLine);
        }
    }

    public partial class Form1 : Form
    {
        private TcpServer Server;
        private Process process;
        public Form1()
        {
            InitializeComponent();

            int port = 1203;
            bool bUseIPv6 = false;
            Server = new TcpServer(port, bUseIPv6);
            Server.Start(TransparentProxy.CreateProxy);

            Server.InitListenFinished.WaitOne();
            if (Server.InitListenException != null)
                throw Server.InitListenException;

            var frp = new Frp();

            process = new Process();
            process.StartInfo.FileName = @"C:\Users\Lenovo\source\repos\FuckSign\frp\frpc.exe";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Exited += new EventHandler(p_Exited);
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.Start();

            process.BeginOutputReadLine();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Server.Stop();
            process.Kill();
        }

        void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            Console.WriteLine(e.Data);

        }

        void p_Exited(Object sender, EventArgs e)
        {
            Console.WriteLine("finish");
        }
    }
}
