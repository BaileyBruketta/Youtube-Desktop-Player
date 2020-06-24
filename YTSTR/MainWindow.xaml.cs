using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CefSharp;

namespace YTSTR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //This initiates a webserver by calling a python script to run simpleHTTPServer through a batch script
            void Main()
            {
                Process proc = null;
                try
                {
                    string batDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    proc = new Process();
                    proc.StartInfo.WorkingDirectory = batDir;
                    proc.StartInfo.FileName = "startServer.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();
                    //proc.WaitForExit();
                    MessageBox.Show("server has started");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace.ToString());
                }
            }
            //call main function from start
            Main();
        }

        //This takes in a youtube link from a user, and singles out the video ID to replace in the index.html file, so as to change the embedded player's contents
        void WriteNewIndex(string newYoutubeLink)
        {
            string VideoIdentifier = newYoutubeLink.Remove(0, 32); VideoIdentifier.Replace(" ", "");
            MessageBox.Show(VideoIdentifier);
            string IndexPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index.html").ToString();
            MessageBox.Show(IndexPath);
            string[] originalIndex = System.IO.File.ReadAllLines(IndexPath);
            MessageBox.Show(originalIndex[4]);
            string newIndex = originalIndex[4].Remove(69, 12) + VideoIdentifier + '"';
            originalIndex[4] = newIndex;
            System.IO.File.WriteAllLines(IndexPath, originalIndex);
        }

        //Called when user presses the submit button after providing a link to a youtube video
        private void button_Click(object sender, RoutedEventArgs e)
        {
            { string LinkToSendToWriter = textBox.Text; WriteNewIndex(LinkToSendToWriter);
                Browser.Reload();
            }
        }
    }  
}


//run a local server using python, by calling the python script using the ironpython library
//var engine = Python.CreateEngine();
//var source = engine.CreateScriptSourceFromFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pyServer.py"));
//var scope = engine.CreateScope();
// source.Execute(scope);