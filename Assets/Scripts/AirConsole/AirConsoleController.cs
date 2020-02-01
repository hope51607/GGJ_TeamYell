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
        switch (data["action"].ToString())
        {
            case "motion":
                objectMotions.OnMessage(from, data);
                break;
            case "government":
                microphoneHandler.OnMessage(from, data);
                break;
            default:
                Debug.Log(data);
                break;
        }
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}
