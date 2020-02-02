using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using UnityEngine;

public class CheckConection : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AirConsole.instance.onConnect += (int deviceId) => {
            AirConsole.instance.SetActivePlayers(2);
            AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);
        };

        AirConsole.instance.onDisconnect += (int deviceId)=>
        {
            AirConsole.instance.SetActivePlayers(0);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
