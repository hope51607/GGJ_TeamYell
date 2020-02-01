using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MicrophoneHandler : MonoBehaviour
{
    public void Message(int from, JToken data)
    {
        if (data["value"] != null)
            print(data["value"]);
    }
}
