using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MicrophoneHandler : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1000;

    public void OnMessage(int from, JToken data)
    {
        if (data["government_value"] != null) {
            //print(data["government_value"]);
            GameplayController.Instance.BlowManagers[from].SetBlowForce((float)data["government_value"]);
        }
    }
}
