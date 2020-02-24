using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF_TCPDemo.Classes;

namespace WPF_TCPDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow m_wnd;
        private string m_string = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServerClass TCPServer = new ServerClass(13000);
            TCPServer.ServerReadClientDataCompleted += TCPServer_ServerReadClientDataCompleted;
            Task tcpserver = Task.Run(() => TCPServer.StartServer());
            m_wnd = new MainWindow();
            m_wnd.Show();
        }
        private void TCPServer_ServerReadClientDataCompleted(string obj)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                m_wnd.lst_Server.Items.Add(obj);
            }), DispatcherPriority.SystemIdle);
        }
    }
}
