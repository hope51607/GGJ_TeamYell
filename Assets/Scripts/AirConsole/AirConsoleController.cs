using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleController : MonoBehaviour
{
    private ObjectMotions objectMotions;
    private MicrophoneHandler microphoneHandler;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        objectMotions = GetComponent<ObjectMotions>();
        microphoneHandler = GetComponent<MicrophoneHandler>();
    }

    void OnMessage(int from, JToken data) {
        //Debug.Log("message from device " + from + ", data: " + data);
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
        if (data["motion"] != null) {
            objectMotions.OnMessage(playerNumber, data);
        }

        if (data["government"] != null) {
            microphoneHandler.OnMessage(playerNumber, data);
        }

        if (data["blow"] != null) {
            GameplayController.Instance.BlowManagers[playerNumber].SetBlowForce();
        }

        if (data["government_threshold"] != null) {
            print("government_threshold: " + data["government_threshold"]);
        }


        //switch (data["action"].ToString())
        //{
        //    case "motion":
        //        objectMotions.OnMessage(playerNumber, data);
        //        break;
        //    case "government":
        //        microphoneHandler.OnMessage(playerNumber, data);
        //        break;
        //    case "blow":
        //        //吹氣
        //        Debug.Log("吹");
        //        break;
        //    default:
        //        Debug.Log(data);
        //        break;
        //}
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}