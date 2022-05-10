//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;

//namespace EsneServerApp
//{
//    class Listener
//    {
//        TcpListener listener;

//        public Listener(Pool pool, int port)
//        {
//            IPAddress localAddress = IPAddress.Parse("0.0.0.0");
//            listener = new TcpListener(localAddress, port);

//            listener.Start();

//            while (true)
//            {
//                TcpClient client = listener.AcceptTcpClient();
//                client.Client.Blocking = false;

//                if (!pool.AddConnection(client))
//                {
//                    client.Close();
//                }
//            }
//        }
//    }
//}