# WPF-TCPDemo
 
WPF program which demonstrates the capabilities of TcpClient and TcpListener. (both in namespace System.Net.Sockets). 
The program is expected to work as followings:
1.	When the program starts, it creates a new instance of TcpListener (TCP Server), which waits for a TcpClient to be connected 
(performs a blocking call to accept requests).

2.	The program then creates a new instance of TcpClient, which connects to the IPEndPoint of the TcpListener.

