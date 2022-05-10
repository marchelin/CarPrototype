//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using UnityEngine;

//public class MySocket : MonoBehaviour
//{
//    private Socket socket;

//    private byte[] obuffer = new byte[2048];

//    void Start()
//    {
//        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 16384);
//        socket.Connect(remoteEP);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            Debug.Log("sending " + obuffer);
//            Send(obuffer, 2048);
//        }
//    }

//    private void Send(byte[] buffer, int length)
//    {
//        socket.Send(buffer, length, SocketFlags.None);
//    }
//}