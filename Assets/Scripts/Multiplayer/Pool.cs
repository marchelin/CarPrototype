//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;

//namespace EsneServerApp
//{
//    class Pool
//    {
//        private int maxConnections;
//        private List<Connection> connections = new List<Connection>();

//        public Pool(int maxConnections)
//        {
//            this.maxConnections = maxConnections;
//        }

//        public bool AddConnection(TcpClient client)
//        {
//            Connection connection = new Connection(client);
//            connection.SendToAllDelegate = SendToAll;
//            connection.SendToAllButThisDelegate = SendToAllButThis;

//            lock (connections)
//            {
//                if (connections.Count < maxConnections)
//                {
//                    Console.WriteLine("New connection: " + connection);
//                    connections.Add(connection);

//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }

//        public void RemoveConnection(Connection connection)
//        {
//            lock (connections)
//            {
//                Console.WriteLine("Connection to cloase: " + connection);
//                connection.socket.Close();
//                connections.Remove(connection);
//            }
//        }

//        public void Process()
//        {
//            List<Connection> toClose = new List<Connection>();

//            lock (connections)
//            {
//                foreach (Connection connection in connections)
//                {
//                    try
//                    {
//                        if (!connection.Process())
//                        {
//                            toClose.Add(connection);
//                        }
//                    }
//                    catch (SocketException e)
//                    {
//                        Console.WriteLine("Exception: " + e.Message);
//                    }
//                }
//            }

//            foreach (Connection connection in toClose)
//            {
//                RemoveConnection(connection);
//            }
//        }

//        public void SendAll(string str)
//        {
//            lock (connections)
//            {
//                foreach (Connection connection in connections)
//                {
//                    connections.Send(str);
//                }
//            }
//        }

//        public void SendToAllButThis(string str, Connection sender)
//        {
//            lock (connections)
//            {
//                foreach (Connection connection in connections)
//                {
//                    if (connection != sender)
//                    {
//                        connection.Send(str);
//                    }
//                }
//            }
//        }
//    }
//}