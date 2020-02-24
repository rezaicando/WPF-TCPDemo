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
    public static class ClientClass
    {
        public static event Action<string> ClientReadBackFromServerDataCompleted;
        public static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient
                //TcpClient client = new TcpClient(server, Values.port); ==> this is very slow (1 sec delay)
                TcpClient client = new TcpClient();
                client.Connect(server, Values.port); //==> this method of connection will increase the speed
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                // Receive the TcpServer.response.
                // Buffer to store the response bytes.
                data = new Byte[Values.BufferSize];
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                // MessageBox.Show( responseData.ToString());                
                ClientReadBackFromServerDataCompleted(responseData.ToString());
                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("ArgumentNullException: {0}", e.Message);
            }
            catch (SocketException e)
            {
                MessageBox.Show("SocketException: {0}", e.Message);
            }
        }
    }
}
