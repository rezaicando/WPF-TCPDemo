using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace WPF_TCPDemo.Classes
{

    public class ServerClass
    {

        private int m_port;
        public ServerClass()
        {
            m_port = Values.port;
        }
        public ServerClass(int port)
        {
            m_port = port;
        }

        public event Action<string> ServerReadClientDataCompleted;


        public void StartServer()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Any, m_port);

                // Start listening for client requests
                server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[Values.BufferSize];
                NetworkStream stream = null;
                StringBuilder data = new StringBuilder();

                //Enter the listening loop
                while (true)
                {
                    // "Waiting for a connection... "
                    // Perform a blocking call to accept requests.                    


                    TcpClient client = server.AcceptTcpClient();
                    
                    // "Connected!"

                    // Get a stream object for reading and writing
                    stream = client.GetStream();

                    int i;
                    // Loop to receive all the data sent by the client.
                    i = stream.Read(bytes, 0, bytes.Length);
                    while (i != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data.Append(System.Text.Encoding.ASCII.GetString(bytes, 0, i));
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data.ToString().Reverse());
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        i = stream.Read(bytes, 0, bytes.Length);
                    }
                    ServerReadClientDataCompleted(data.ToString());
                    data.Clear();
                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message + " " + e.SocketErrorCode);
            }

        }


    }


}
