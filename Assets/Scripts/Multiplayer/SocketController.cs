//using System.Collections;
//using System.Collections.Generic;
//using System.Net.Sockets;
//using UnityEngine;

//[RequireComponent(typeof(OnlineManager))]
//public class SocketController : MonoBehaviour
//{
//    private Socket socket;

//    public string serverIP = "000.000.0.000";
//    public int serverSocket = 16384;

//    private byte[] obuffer = new byte[2048];
//    private byte[] ibuffer = new byte[2048];

//    void Start()
//    {
//        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 16384);
//        socket.Connect(remoteEP);

//        StarCoroutine(OnReceived());
//    }

//    public void Send(string str)
//    {
//        byte[] bytes = Encoding.ASCII.GetBytes(str + "$");
//        bytes.CopyTo(obuffer, 0);

//        socket.Send();
//    }

//    private IEnumerator OnReceived()
//    {
//        while (true)
//        {
//            if (socket.Available > 0)
//            {
//                int bytesReceived = socket.Available;

//                byte[] strBuffer = new byte
//            }
//        }
//    }
//}