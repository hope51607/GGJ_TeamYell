using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleController : MonoBehaviour {
    private MicrophoneHandler microphoneHandler;
    
    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        microphoneHandler = GetComponent<MicrophoneHandler>();
    }


    void OnMessage(int from, JToken data)
    {
        Debug.Log("message from device " + from + ", data: " + data);
        switch (data["action"].ToString())
        {
            case "motion":
                break;
            case "government":
                microphoneHandler.Message(from, data);
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
