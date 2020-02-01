using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleController : MonoBehaviour {

    private ObjectMotions objectMotions;
    private MicrophoneHandler microphoneHandler;
    
    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        objectMotions = GetComponent<ObjectMotions>();
        microphoneHandler = GetComponent<MicrophoneHandler>();
    }


    void OnMessage(int from, JToken data)
    {
        Debug.Log("message from device " + from + ", data: " + data);
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
        switch (data["action"].ToString())
        {
            case "motion":
                objectMotions.OnMessage(playerNumber, data);
                break;
            case "government":
                microphoneHandler.OnMessage(playerNumber, data);
                break;
            default:
                Debug.Log(data);
                break;
        }
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null) {
            AirConsole.instance.SetActivePlayers(4);
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}
