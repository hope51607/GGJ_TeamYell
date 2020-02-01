using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ObjectMotions : MonoBehaviour
{
    public List<GameObject> movableObject = new List<GameObject>();
    public Vector3 xyzMaxRotateRange;
    public Vector3 xyzMinRotateRange;
    [Header("DisplacmentRange: -x ~ x")]
    public float xDisplacementRange = 3;
    [Header("Controls xDisplacement Sensitivity")]
    public float scaling = 10;
    private List<Vector3> initPositions = new List<Vector3>();

    private void Awake()
    {
        for (int i = 0; i < movableObject.Count; i++)
        {
            initPositions.Add(movableObject[i].transform.position);
        }
    }

    public void OnMessage(int playerNumber, JToken data)
    {
        Debug.Log(playerNumber);
        switch (data["action"].ToString())
        {
            case "motion":
                if (data["motion_data"] != null)
                {
                    if (data["motion_data"]["x"].ToString() != "")
                    {
                        /*float xRotate = (float)data["motion_data"]["gamma"];
                        float yRotate = -(float)data["motion_data"]["alpha"];
                        float zRotate = -(float)data["motion_data"]["beta"];
                        xRotate = Mathf.Clamp(xRotate, xyzMinRotateRange.x, xyzMaxRotateRange.x); // -45~45
                        //yRotate = Mathf.Clamp(yRotate, xyzMinRange.y, xyzMaxRange.y);
                        zRotate = Mathf.Clamp(zRotate, xyzMinRotateRange.z, xyzMaxRotateRange.z); // -30~30
                        Vector3 abgAngles = new Vector3(xRotate, 0, zRotate);*/
                        Vector3 abgAngles = new Vector3(-(float)data["motion_data"]["beta"],
                        -(float)data["motion_data"]["alpha"], -(float)data["motion_data"]["gamma"]);
                        movableObject[playerNumber].transform.eulerAngles = abgAngles;

                        Vector3 abgAccelerometer = new Vector3((float)data["motion_data"]["y"] / scaling, 0, 0);
                        movableObject[playerNumber].transform.position += abgAccelerometer;
                        if (movableObject[playerNumber].transform.position.x >= initPositions[playerNumber].x + xDisplacementRange)
                        {
                            movableObject[playerNumber].transform.position = new Vector3(initPositions[playerNumber].x + xDisplacementRange, movableObject[playerNumber].transform.position.y, movableObject[playerNumber].transform.position.z);
                        }
                        else if (movableObject[playerNumber].transform.position.x <= initPositions[playerNumber].x - xDisplacementRange)
                        {
                            movableObject[playerNumber].transform.position = new Vector3(initPositions[playerNumber].x - xDisplacementRange, movableObject[playerNumber].transform.position.y, movableObject[playerNumber].transform.position.z);
                        }
                    }
                }
                break;
            default:
                Debug.Log(data);
                break;
        }
    }

}