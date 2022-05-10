//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OnlineManager : MonoBehaviour
//{
//    public string playerName = "Federico";

//    public GameObject playerGO;

//    public Dictionary<string, GameObject> onlineCars = new Dictionary<string, GameObject>();

//    public GameObject onlineCarPrefab;

//    public SocketController socket;

//    public float timeToSendData = 0.33f;
//    private float timeToSendDataAux = 0f;

//    void Awake()
//    {
//        socket = GetComponent<SocketController>();
//    }

//    private void LateUpdate()
//    {
//        timeToSendDataAux -= Time.deltaTime;

//        if (timeToSendDataAux <= 0f)
//        {
//            timeToSendDataAux = timeToSendData;
//        }
//    }

//    private void sendPosition()
//    {
//        string currentPosition = "updatePosition| " + playerName + "|" + playerGO.transform.position.ToString();
//        socket.Send(currentPosition);
//    }

//    private void sendRotation()
//    {
//        string currentPosition = "updatePosition| " + playerName + "|" + playerGO.transform.position.ToString();
//        socket.Send(currentPosition);
//    }

//    public void ParseMessages(string str)
//    {
//        string[] strSplit = str.Split('$');
//        for (int i = 0; i < strSplit.Length - 1; i++)
//        {
//            ParseMessage(strSplit[i]);
//        }
//    }

//    private void ParseMessage(string str)
//    {
//        string[] strSplit = str.Split('|');

//        switch (strSplit[0])
//        {
//            case "join":
//                break;
//            case "updatePosition":
//                break;
//            case "updateRotation":
//                break;
//        }
//    }

//    private void NewPlayerJoins(string name)
//    {
//        if (!onlineCars.ContainsKey(name))
//        {
//            GameObject newCar = GameObject.Instantiate(onlineCarPrefab, Vector3.zero, Quaternion.identity);
//            newCar.name = name;
//            onlineCars.Add(name, newCar.GetComponent<OnlineCarController>());

//            socket.Send("join| " + playerName);
//        }
//    }

//    private void PositionReceived(string name, string strVector)
//    {
//        Vector3 targetPosition = StringToVector3(strVector);
//        onlineCars[name].targetPosition = targetPosition;
//    }

//    private void RotationReceived(string name, string sQuaternion)
//    {
//        Vector3 targetRotation = StringToVector3(sQuaternion);
//        onlineCars[name].targetPosition = targetPosition;
//    }

//    private static Vector3 StringToVector3(string sVector)
//    {
//        // Remove the parentheses
//        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
//            sVector = sVector.Substring(1, sVector.Length - 2);

//        // split the items
//        string[] sArray = sVector.Split(',');

//        float wat = float.Parse("1.0", CultureInfo.InvariantCulture.NumberFormat);

//        return new Vector3(
//            float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
//            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat),
//            float.Parse(sArray[2], CultureInfo.InvariantCulture.NumberFormat)
//        );
//    }

//    private static Vector3 StringToQuaternion(string sQuaternion)
//    {
//        // Remove the parentheses
//        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
//            sVector = sVector.Substring(1, sVector.Length - 2);

//        // split the items
//        string[] sArray = sVector.Split(',');

//        float wat = float.Parse("1.0", CultureInfo.InvariantCulture.NumberFormat);

//        return new Vector3(
//            float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
//            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat),
//            float.Parse(sArray[2], CultureInfo.InvariantCulture.NumberFormat)
//        );
//    }
//}