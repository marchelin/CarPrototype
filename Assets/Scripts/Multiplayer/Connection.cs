//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;

//namespace EsneServerApp
//{
//    class Connection
//    {
//        static byte[] ibuffer = new byte[2048];
//        static byte[] obuffer = new byte[2048];

//        private TcpClient client;
//        public Socket socket { get; private set; }

//        public Action<string> SendToAllDelegate;
//        public Action<string, Connection> SendToAllButThisDelegate;

//        public Connection(Socket socket)
//        {
//            this.client = client;
//            this.socket = client.Client;
//        }

//        private override string ToString()
//        {
//            return ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString() + ":" + ((IPEndPoint)
//                client.Client.LocalEndPoint).Port.ToString();
//        }

//        public bool Process()
//        {
//            if (socket.Available > 2)
//            {
//                socket.Receive(ibuffer, socket.Available, SocketFlags.Peek);
//                ushort len = BitConverter.ToUInt16(ibuffer, 0);

//                if (socket.Available >= len)
//                {
//                    socket.Receive(ibuffer, len, SocketFlags.None);
//                    // crear el objeto Message
//                    // procesar el mensaje

//                    Console.WriteLine(ibuffer);
//                }

//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public void Send(MessageBase message)
//        {
//            uint messageId = message.id;
//            // System.array.Copy();
//            // transformar el Message a array de bytes
//            // Message -> obuffer
//            int idx = 2;

//            // ...

//            Send(obuffer, idx);
//        }

//        private void Send(byte[] buffer, int length)
//        {
//            socket.Send(buffer, length, SocketFlags.None);
//        }
//    }
//}