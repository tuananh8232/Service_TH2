using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Service_TH2
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;) 
        public Service1()
        {
            InitializeComponent();
            
        }
        
        bool flag = true;
        
        protected override void OnStart(string[] args)
        {
            //WriteToFile("Service is started at " + DateTime.Now);
            //timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 5000; //number in milisecinds
            //timer.Enabled = true;
            //Process.Start("cmd.exe", "/c start /IM notepad.exe");
            //Process.Start(@"C:\Program Files\VideoLAN\VLC\vlc.exe");
            //while (flag)
            //{
            //Process[] pname = Process.GetProcessesByName("notepad");
            //if (pname.Length == 0)
            //{
            //    Process.Start("cmd.exe", "/c start /IM notepad.exe");
            //    //WriteToFile("Service is starting Notepad at " + DateTime.Now);
            //    //Thread.Sleep(10000);
            //    flag = false;

            //}

            //Process.Start("cmd.exe", "/c taskkill /IM notepad.exe");
            ////WriteToFile("Service is stopping Notepad at " + DateTime.Now);
            //flag = true;

            //}

            //Process p = new Process();
            //p.StartInfo.FileName = "notepad.exe";
            //p.StartInfo.Arguments = "textFileToDisplay.txt";
            //p.Start();

            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //number in milisecinds
            timer.Enabled = true;


        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("notepad");
            if (pname.Length == 0)
            {
                Process.Start("notepad");
                WriteToFile("Start notepad " + DateTime.Now);
            }    
            else
            {
                foreach (var process in pname)
                {
                    process.Kill();
                    WriteToFile("Stop notepad " + DateTime.Now);
                }
            }    
            WriteToFile("Service is recall at " + DateTime.Now);
        }


        //public bool ProcessIsRunning()
        //{
        //    Process[] pname = Process.GetProcessesByName("notepad");
        //    if (pname.Length == 0)
        //    {
        //        return false;
        //    }    
        //    return true;

        //}
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory +
           "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') +
           ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
