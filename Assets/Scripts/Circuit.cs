using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    public GameObject[] waypoints;
    public List<GameObject> gateWays;

    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }

    private void OndDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    private void DrawGizmos(bool selected)
    {
        if (selected && waypoints.Length > 1)
        {
            Vector3 prevPos = waypoints[0].transform.position;

            for (int i = 1; i < waypoints.Length; i++)
            {
                Vector3 nextPos = waypoints[i].transform.position;
                Gizmos.DrawLine(prevPos, nextPos);
                prevPos = nextPos;
            }

            Gizmos.DrawLine(prevPos, waypoints[0].transform.position);
        }
    }

    public int CarpassThroughGateway(GameObject gateway, int lastGatewayPassedIndex)
    {
        int currentGatewayIndex = gateWays.IndexOf(gateway);
        if (currentGatewayIndex == (lastGatewayPassedIndex + 1) % gateWays.Count)
        {
            return (lastGatewayPassedIndex + 1) % gateWays.Count;
        }
        else if (currentGatewayIndex == lastGatewayPassedIndex)
        {
            return currentGatewayIndex;
        }
        else
        {
            return -1;
        }
    }
}